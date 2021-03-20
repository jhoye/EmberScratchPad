import Route from '@ember/routing/route';
import { action } from '@ember/object';
import { inject as service } from '@ember/service';

export default class CommentsEditRoute extends Route {
    @service store;

    model(params) {
        return this.store.findRecord('comment', params.comment_id, { include: 'entry' });
    }

    @action
    didTransition() {

        let comment = this.modelFor(this.routeName);

        // one-way bind data to input fields
        this.controller.commentText = comment.text;
    }
}
