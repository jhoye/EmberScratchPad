using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ScratchPad.Data;
using ScratchPad.Models;
using CategoryData = ScratchPad.Data.Entities.Category;

namespace ScratchPad.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ScratchPadControllerBase
    {
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(
            ILogger<CategoriesController> logger,
            ScratchPadContext scratchPadContext
        ) : base(scratchPadContext)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<List<Category>> Get(string include = "")
        {
            return await ScratchPadContext.GetCategories(include)
                .Select(categoryData => new Category(categoryData, true))
                .ToListAsync();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Category> Get(int id, string include = "")
        {
            var categoryData = await ScratchPadContext.GetCategories(include)
                .SingleOrDefaultAsync(a => a.Id == id);

            if (categoryData == null)
            {
                ThrowNotFoundException(CategoryNotFoundMessage(id));
            }

            return new Category(categoryData, true);
        }

        [HttpPost]
        public async Task<Category> Post([FromBody] Category category)
        {
            var categoryData = new CategoryData
            {
                Name = string.IsNullOrWhiteSpace(category.Name)
                    ? $"new category..."
                    : category.Name
            };

            ScratchPadContext.Categories.Add(categoryData);

            if (category.Entries.Any())
            {
                foreach(var entryId in category.Entries.Select(b => b.Id))
                {
                    var entry = await ScratchPadContext.Entries.SingleAsync(a => a.Id == entryId);

                    categoryData.Entries = new List<Data.Entities.Entry> { entry };
                }
            }

            await ScratchPadContext.SaveChangesAsync();

            return new Category(categoryData);
        }

        [HttpPut]
        [Route("{id}")]
        public async void Put([FromBody] Category category)
        {
            var categoryData = await ScratchPadContext.Categories
                .Include(a => a.Entries)
                .SingleOrDefaultAsync(a => a.Id == category.Id);

            if (categoryData == null)
            {
                ThrowNotFoundException(CategoryNotFoundMessage(category.Id));
            }

            categoryData.Name = string.IsNullOrWhiteSpace(category.Name)
                ? $"category {category.Id}..."
                : category.Name;

            var entryIds = category.Entries.Select(a => a.Id).ToList();

            foreach(var entry in categoryData.Entries.Where(a => !entryIds.Contains(a.Id)).ToList())
            {
                categoryData.Entries.Remove(entry);
            }

            foreach(var entryId in entryIds.Where(a => !categoryData.Entries.Select(b => b.Id).Contains(a)))
            {
                var entry = await ScratchPadContext.Entries.SingleAsync(a => a.Id == entryId);

                categoryData.Entries.Add(entry);
            }

            await ScratchPadContext.SaveChangesAsync();
        }

        [HttpDelete]
        [Route("{id}")]
        public async void Delete(int id)
        {
            var categoryData = await ScratchPadContext.Categories
                .Include(a => a.Entries)
                .SingleOrDefaultAsync(a => a.Id == id);

            if (categoryData == null)
            {
                ThrowNotFoundException(CategoryNotFoundMessage(id));
            }

            foreach(var entry in categoryData.Entries.ToList())
            {
                categoryData.Entries.Remove(entry);
            }

            ScratchPadContext.Categories.Remove(categoryData);

            await ScratchPadContext.SaveChangesAsync();
        }
    }
}
