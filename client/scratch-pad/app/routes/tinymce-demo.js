//import { fetch } from 'fetch'
import Route from '@ember/routing/route';

export default class TinymceDemoRoute extends Route {
    beforeModel(){
        this._super(...arguments);

        const url = 'https://localhost:5001/assets/tinymce/js/tinymce.min.js';

        if (typeof tinymce == 'undefined'){
            console.info('No tinymce defined...');

            // This fails in browser with...
            // Referrer Policy: strict-origin-when-cross-origin
            //return fetch(url)
        }
    }
}
