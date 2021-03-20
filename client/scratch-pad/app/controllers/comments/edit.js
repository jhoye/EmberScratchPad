import Controller from '@ember/controller';
import { action } from '@ember/object';
import { tracked } from '@glimmer/tracking';
import { inject as service } from '@ember/service';

export default class CommentsEditController extends Controller {
    @service store;

    @tracked
    commentText = ''

    @tracked
    isSaving = false

    @action
    save() {

        let self = this,
            entryId = this.model.belongsTo("entry").id();

        // update text from input
        this.model.text = this.commentText;

        this.isSaving = true;

        // API call and redirect back to details
        this.model.save()
            .then(function () {
                self.commentText = '';
                self.isSaving = false;
                self.transitionToRoute('entries.details', entryId);
            });
    }
}
