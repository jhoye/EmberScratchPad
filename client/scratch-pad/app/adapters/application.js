import ENV from '../config/environment';
import JSONAPIAdapter from '@ember-data/adapter/json-api';

export default class ApplicationAdapter extends JSONAPIAdapter {
    namespace = ENV.API.namespace;
    host = ENV.API.host;
    updateRecord = (store, type, snapshot) => {

        let data = {},
            serializer = store.serializerFor(type.modelName),
            id,
            url;

        serializer.serializeIntoHash(data, type, snapshot, { includeId: true });

        id = snapshot.id;
        url = this.buildURL(type.modelName, id, snapshot, 'updateRecord');

        return this.ajax(url, 'PUT', { data: data });
    }
}