import { JSONAPISerializer } from 'ember-cli-mirage';
import { mapEntryCategories } from './entry-categories-helper'

export default JSONAPISerializer.extend({
    serialize(/* primaryResource, request */) {

        let json = JSONAPISerializer.prototype.serialize.apply(this, arguments);

        mapEntryCategories(this, 'categories', 'categoryId', 'category', json);

        return json;
    }
});
