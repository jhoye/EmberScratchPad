export default function(server) {
    server.createList('entry', 3);
    server.createList('category', 4);
    server.create('entry-category', { entryId: 1, categoryId: 1 });
    server.create('entry-category', { entryId: 1, categoryId: 2 });
    server.create('entry-category', { entryId: 2, categoryId: 1 });
    server.create('comment', { entryId: 1, text: 'comment text 1...' });
    server.create('comment', { entryId: 1, text: 'comment text 2...' });
    server.create('comment', { entryId: 1, text: 'comment text 3...' });
    server.create('comment', { entryId: 2, text: 'comment text 4...' });
    server.create('comment', { entryId: 2, text: 'comment text 5...' });
    server.create('comment', { entryId: 3, text: 'comment text 6...' });
}
