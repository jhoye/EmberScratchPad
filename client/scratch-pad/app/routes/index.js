import Route from '@ember/routing/route';
import { inject as service } from '@ember/service';
import RSVP from 'rsvp';

export default class IndexRoute extends Route {
    @service store;

    model() {
        return RSVP.hash({
            entries: this.store.findAll('entry'),
            categories: this.store.findAll('category')
        });
    }
}
