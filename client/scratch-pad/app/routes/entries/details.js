import Route from '@ember/routing/route';
import { action } from '@ember/object';
import { inject as service } from '@ember/service';
import RSVP from 'rsvp';

export default class EntriesDetailsRoute extends Route {
    @service store;

    model(params) {
        return RSVP.hash({
            entry: this.store.findRecord('entry', params.entry_id, { include: 'categories,comments' }),
            categories: this.store.findAll('category', { backgroundReload: false })
        });
    }

    @action
    delete() {

        let self = this,
            entry = this.modelFor(this.routeName).entry;

        if (window.confirm('Entry will be permanently deleted.')) {
            this.store
                .peekRecord('entry', entry.id)
                .destroyRecord()
                .then(function () {
                    self.transitionTo('entries');
                });
        }
    }

    @action
    deleteComment(id) {
        if (window.confirm('Entry will be permanently deleted.')) {
            this.store
                .peekRecord('comment', id)
                .destroyRecord();
        }
    }
}
