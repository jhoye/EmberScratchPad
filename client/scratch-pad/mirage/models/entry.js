import { Model, hasMany } from 'ember-cli-mirage';

export default Model.extend({
    categories: hasMany('entry-category'),
    comments: hasMany('comment')
});
