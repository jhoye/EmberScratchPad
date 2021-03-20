import Controller from '@ember/controller';
import { action } from '@ember/object';
import { inject as service } from '@ember/service';

export default class CategoriesAddController extends Controller {
    @service store;

    name = ''

    @action
    createCategory() {

        let self = this,
            entry = this.store
                .createRecord('category', {
                    name: this.get('name')
                });

        entry.save()
            .then(function () {
                self.transitionToRoute('categories');
            });
    }
}
