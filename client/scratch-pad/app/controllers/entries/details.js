import Controller from '@ember/controller';
import { action } from '@ember/object';
import { tracked } from '@glimmer/tracking';
import { inject as service } from '@ember/service';

export default class EntriesDetailsController extends Controller {
    @service store;

    @tracked
    commentText = ''

    @tracked
    isSaving = false

    @action
    addComment()
    {
        let self = this,
            entry = this.store.peekRecord('entry', this.model.entry.id);

            this.isSaving = true;

            this.store.createRecord('comment', {
                    text: this.commentText,
                    entry: entry
                })
                .save()
                .then(function () {
                    self.commentText = '';
                    self.isSaving = false;
                });
    }
}
