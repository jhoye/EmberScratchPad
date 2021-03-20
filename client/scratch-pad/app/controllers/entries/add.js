import Controller from '@ember/controller';
import { action } from '@ember/object';
import { inject as service } from '@ember/service';

export default class EntriesAddController extends Controller {
    @service store;

    name = ''

    @action
    createEntry() {

        let self = this,
            entry = this.store
                .createRecord('entry', {
                    name: this.get('name')
                });

        entry.save()
            .then(function () {
                self.transitionToRoute('entries');
            });
    }
}
