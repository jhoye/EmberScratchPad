export default function() {
    // timing = 5000;

    // entries
    this.get('/entries');
    this.get('/entries/:id');
    this.post('/entries');
    this.put('/entries/:id');
    this.patch('/entries/:id');
    this.del('/entries/:id');

    // categories
    this.get('/categories');
    this.get('/categories/:id');
    this.post('/categories');
    this.put('/categories/:id');
    this.patch('/categories/:id');
    this.del('/categories/:id');

    // comments
    this.get('/comments');
    this.get('/comments/:id');
    // this.post('/comments', { timing: 5000 });
    this.post('/comments');
    this.put('/comments/:id');
    this.patch('/comments/:id');
    this.del('/comments/:id');
}
