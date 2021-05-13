import { module, test } from 'qunit';
import { setupTest } from 'ember-qunit';

module('Unit | Route | tinymce-demo', function(hooks) {
  setupTest(hooks);

  test('it exists', function(assert) {
    let route = this.owner.lookup('route:tinymce-demo');
    assert.ok(route);
  });
});
