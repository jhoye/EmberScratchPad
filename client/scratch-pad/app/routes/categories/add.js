import Route from '@ember/routing/route';
import { action } from '@ember/object'

export default class CategoriesAddRoute extends Route {
    @action
    deactivate(){
        this.controller.name = '';
    }
}
