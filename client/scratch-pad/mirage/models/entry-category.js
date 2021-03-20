import { Model, belongsTo } from 'ember-cli-mirage';

export default Model.extend({
    entry: belongsTo(),
    category: belongsTo()
});
