import Route from '@ember/routing/route';
import { inject as service } from '@ember/service';

export default class EntriesRoute extends Route {
    @service store;

    model() {
        return this.store.findAll('entry', { backgroundReload: false });
    }
}
