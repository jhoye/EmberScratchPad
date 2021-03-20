import Component from '@glimmer/component';
import { action } from '@ember/object';

export default class CheckboxListComponent extends Component {

    @action
    checkboxClick(event)
    {
        let id = event.target.value;

        if (event.target.checked) {
            this.args.addCategoryHandler(id)
        } else {
            this.args.removeCategoryHandler(id)
        }
    }
}
