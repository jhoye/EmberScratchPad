import Component from '@glimmer/component';

export default class MarkupEditorComponent extends Component {
    didInsertHandler(element) {
        console.info('didInsertHandler', element.id);
        if (typeof tinymce == 'undefined') {
            console.info('didInsertHandler - tinymce not defined!');
        } else {
            console.info('txt', element.id);
        }
    }
}
