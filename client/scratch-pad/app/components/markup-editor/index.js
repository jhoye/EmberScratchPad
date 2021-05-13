import Component from '@glimmer/component';

export default class MarkupEditorComponent extends Component {
    didInsertHandler(element) {
        console.info('didInsertHandler', element.id);
    }
}
