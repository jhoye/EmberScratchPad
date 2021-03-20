using System.Linq;
using Microsoft.EntityFrameworkCore;
using ScratchPad.Data.Entities;

namespace ScratchPad.Data
{
    public class ScratchPadContext : DbContext
    {
        public ScratchPadContext (DbContextOptions<ScratchPadContext> options)
            : base(options)
        {
        }

        public DbSet<Entry> Entries { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Category> Categories { get; set; }

        public IQueryable<Data.Entities.Entry> GetEntries(string include)
        {
            var included = SplitToLower(include);

            IQueryable<Data.Entities.Entry> entries = Entries;

            if (included.Contains("categories"))
            {
                entries = entries.Include(a => a.Categories);
            }

            if (included.Contains("comments"))
            {
                entries = entries.Include(a => a.Comments);
            }

            return entries;
        }

        public IQueryable<Data.Entities.Comment> GetComments(string include)
        {
            var included = SplitToLower(include);

            IQueryable<Data.Entities.Comment> comments = Comments;

            if (included.Contains("entries"))
            {
                comments = comments.Include(a => a.Entry);
            }

            return comments;
        }

        public IQueryable<Data.Entities.Category> GetCategories(string include)
        {
            var included = SplitToLower(include);

            IQueryable<Data.Entities.Category> categories = Categories;

            if (included.Contains("entries"))
            {
                categories = categories.Include(a => a.Entries);
            }

            return categories;
        }

        private string[] SplitToLower(string input)
        {
            return string.IsNullOrWhiteSpace(input)
                ? new string [0]
                : input.ToLower().Replace(" ", "").Split(",");
        }
    }
}