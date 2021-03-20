import Model, { attr, hasMany } from '@ember-data/model';

export default class EntryModel extends Model {
    @attr('string') name;
    @hasMany('comment') comments;
    @hasMany('category') categories;
}
