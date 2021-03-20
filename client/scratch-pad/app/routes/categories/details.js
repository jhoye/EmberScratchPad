import Route from '@ember/routing/route';
import { action } from '@ember/object';
import { inject as service } from '@ember/service';

export default class CategoriesDetailsRoute extends Route {
    @service store;

    model(params) {
        return this.store.findRecord('category', params.category_id, { include: 'entries' });
    }

    @action
    delete() {

        let self = this,
            model = this.modelFor(this.routeName);

        if (window.confirm('Category will be permanently deleted.')) {
            this.store
                .peekRecord('category', model.id)
                .destroyRecord()
                .then(function () {
                    self.transitionTo('categories');
                });
        }
    }
}
