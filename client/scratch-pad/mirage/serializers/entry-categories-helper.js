import _ from 'lodash';

const serialize = (serializer, relationshipsKey, idKey, type, data) => {
    return data.relationships[relationshipsKey].data.map(entryCategory => ({
        id: serializer.registry.schema.entryCategories.find(entryCategory.id)[idKey],
        type: type,
    }));
};

export function mapEntryCategories(serializer, relationshipsKey, idKey, type, json) {
    if (Array.isArray(json.data)) {
        json.data.forEach((data, i) => {
            if (!_.isUndefined(json.data[i].relationships) &&
                !_.isUndefined(json.data[i].relationships[relationshipsKey])
            ) {
                json.data[i].relationships[relationshipsKey].data = serialize(
                    serializer, relationshipsKey, idKey, type, data
                );
            }
        });
    } else {
        if (!_.isUndefined(json.data.relationships) &&
            !_.isUndefined(json.data.relationships[relationshipsKey])
        ) {
            json.data.relationships[relationshipsKey].data = serialize(
                serializer, relationshipsKey, idKey, type, json.data
            );
        }
    }
};