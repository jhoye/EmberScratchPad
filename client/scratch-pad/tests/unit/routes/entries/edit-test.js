import { module, test } from 'qunit';
import { setupTest } from 'ember-qunit';

module('Unit | Route | entries/edit', function(hooks) {
  setupTest(hooks);

  test('it exists', function(assert) {
    let route = this.owner.lookup('route:entries/edit');
    assert.ok(route);
  });
});
