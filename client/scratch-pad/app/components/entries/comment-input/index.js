import Component from '@glimmer/component';
import { action } from '@ember/object';

export default class CommentInputComponent extends Component {
    text = ''
    buttonText = ''
    isReadOnly = false

    @action
    buttonClick() {
        if (this.args.buttonClickHandler) {
            this.args.buttonClickHandler();
        }
    }
}
