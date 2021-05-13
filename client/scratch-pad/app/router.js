import EmberRouter from '@ember/routing/router';
import config from 'scratch-pad/config/environment';

export default class Router extends EmberRouter {
  location = config.locationType;
  rootURL = config.rootURL;
}

Router.map(function () {
  this.route('entries', function() {
    this.route('add');
    this.route('details', {
        path: 'details/:entry_id'
    });
    this.route('edit', {
        path: 'edit/:entry_id'
    });
  });
  this.route('categories', function() {
    this.route('add');
    this.route('details', {
        path: 'details/:category_id'
    });
    this.route('edit', {
        path: 'edit/:category_id'
    });
  });
  this.route('comments', function() {
    this.route('edit', {
        path: 'edit/:comment_id'
    });
  });
  this.route('tinymce-demo');
});
