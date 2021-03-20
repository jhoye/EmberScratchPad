import Route from '@ember/routing/route';
import { action } from '@ember/object';
import { inject as service } from '@ember/service';

export default class CategoriesEditRoute extends Route {
    @service store;

    model(params) {
        return this.store.findRecord('category', params.category_id, { include: 'entries' });
    }

    @action
    didTransition() {

        let category = this.modelFor(this.routeName);

        // one-way bind data to input fields
        this.controller.name = category.name;
    }

    @action
    save() {

        let self = this,
            model = this.modelFor(this.routeName),
            id = model.id,
            name = this.controller.name,
            entry = this.store.peekRecord('category', id);

        entry.name = name;

        entry.save()
            .then(function () {
                self.transitionTo('categories.details', id);
            });
    }
}
