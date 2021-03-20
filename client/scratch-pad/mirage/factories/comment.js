import { Factory } from 'ember-cli-mirage';

export default Factory.extend({
    text(i) {
        return `comment text ${(i + 1)}...`;
    }
});
