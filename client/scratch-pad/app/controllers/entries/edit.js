import Controller from '@ember/controller';
import { A } from '@ember/array';
import { action } from '@ember/object';

export default class EntriesEditController extends Controller {
    name = ''
    selectedCategoryIds = A()

    @action
    addCategory(id) {
        this.selectedCategoryIds.addObject(id);
        console.info(`addCategory(${id})`, this.selectedCategoryIds)
    }

    @action
    removeCategory(id) {
        this.selectedCategoryIds.removeObject(id);
        console.info(`removeCategory(${id})`, this.selectedCategoryIds)
    }
}
