import { helper } from '@ember/component/helper';

function modelsIncludeId(args) {

    let [models, id] = args;

    return models.map(a => a.id).includes(id);
}

export default helper(modelsIncludeId);