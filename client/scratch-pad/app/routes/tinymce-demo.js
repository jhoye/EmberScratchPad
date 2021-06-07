import { fetch } from 'fetch'
import Route from '@ember/routing/route';

export default class TinymceDemoRoute extends Route {
    beforeModel(){
        this._super(...arguments);

        const url = 'https://localhost:5001/assets/tinymce/tinymce.min.js';

        if (typeof tinymce == 'undefined'){
            console.info('No tinymce defined...');

            return fetch(url, {
                // headers: {
                //     'Access-Control-Allow-Origin': '*'
                // },
                //credentials: 'omit',
                cache: 'no-store',
                //referrerPolicy: 'unsafe-url',
                //mode: 'cors'
            }).then((d) => {
                console.info('fetched...', d)
            });
        }
    }
}
