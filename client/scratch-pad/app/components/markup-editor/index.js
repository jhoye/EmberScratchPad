import Component from '@glimmer/component';

export default class MarkupEditorComponent extends Component {
    initializeEditor(element) {
        console.info('didRender', element);
        tinymce.init({ selector: '#txt' });
    }
}
