import Route from '@ember/routing/route';
import { A } from '@ember/array';
import { action } from '@ember/object';
import { inject as service } from '@ember/service';
import RSVP from 'rsvp';
import _ from 'lodash';

export default class EntriesEditRoute extends Route {
    @service store;

    getCategoryIds(entry) {
        return A(
            entry.categories.map(
                (a) => {
                    return a.id;
                }
            )
        );
    }

    model(params) {
        return RSVP.hash({
            entry: this.store.findRecord('entry', params.entry_id, { include: 'categories,comments', reload: true }),
            categories: this.store.findAll('category', { backgroundReload: false })
        });
    }

    setupController(controller, model) {
        super.setupController(controller, model);

        // one-way bind data to input fields
        controller.name = model.entry.name;
        controller.selectedCategoryIds = this.getCategoryIds(model.entry);
    }

    @action
    save() {

        let self = this,
            id = this.modelFor(this.routeName).entry.id,
            selectedCategoryIds = this.controller.selectedCategoryIds,
            entry = this.store.peekRecord('entry', id),
            category;

        // update name from input
        entry.name = this.controller.name;

        // remove previously set categories
        _.each(this.getCategoryIds(entry), function (categoryId) {
            category = self.store.peekRecord('category', categoryId);
            entry.categories.removeObject(category);
        });

        // add selected categories
        _.each(selectedCategoryIds, function (categoryId) {
            category = self.store.peekRecord('category', categoryId);
            entry.categories.addObject(category);
        });

        // API call and redirect back to details
        entry.save()
            .then(function (a, b, c) {
                console.info('callback...', id, a, b, c);
                self.transitionTo('entries.details', id);
            });
    }
}
