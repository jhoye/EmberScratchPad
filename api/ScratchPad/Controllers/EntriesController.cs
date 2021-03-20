using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ScratchPad.Data;
using ScratchPad.Models;
using EntryData = ScratchPad.Data.Entities.Entry;

namespace ScratchPad.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EntriesController : ScratchPadControllerBase
    {
        private readonly ILogger<EntriesController> _logger;

        public EntriesController(
            ILogger<EntriesController> logger,
            ScratchPadContext scratchPadContext
        ) : base(scratchPadContext)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<List<Entry>> Get(string include = "")
        {
            return await ScratchPadContext.GetEntries(include)
                .Select(entryData => new Entry(entryData, true))
                .ToListAsync();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Entry> Get(int id, string include = "")
        {
            var entryData = await ScratchPadContext.GetEntries(include)
                .SingleOrDefaultAsync(a => a.Id == id);

            if (entryData == null)
            {
                ThrowNotFoundException(EntryNotFoundMessage(id));
            }

            return new Entry(entryData, true);
        }

        [HttpPost]
        public async Task<Entry> Post([FromBody] Entry entry)
        {
            var entryData = new EntryData
            {
                Name = string.IsNullOrWhiteSpace(entry.Name)
                    ? $"new entry..."
                    : entry.Name
            };

            ScratchPadContext.Entries.Add(entryData);

            await ScratchPadContext.SaveChangesAsync();

            return new Entry(entryData);
        }

        [HttpPut]
        [Route("{id}")]
        public async void Put([FromBody] Entry entry)
        {
            var entryData = await ScratchPadContext.Entries
                .Include(a => a.Categories)
                .SingleOrDefaultAsync(a => a.Id == entry.Id);

            if (entryData == null)
            {
                ThrowNotFoundException(EntryNotFoundMessage(entry.Id));
            }

            entryData.Name = string.IsNullOrWhiteSpace(entry.Name)
                ? $"entry {entry.Id}..."
                : entry.Name;

            var categoryIds = entry.Categories.Select(a => a.Id).ToList();

            foreach(var category in entryData.Categories.Where(a => !categoryIds.Contains(a.Id)).ToList())
            {
                entryData.Categories.Remove(category);
            }

            foreach(var categoryId in categoryIds.Where(a => !entryData.Categories.Select(b => b.Id).Contains(a)))
            {
                var category = await ScratchPadContext.Categories.SingleAsync(a => a.Id == categoryId);

                entryData.Categories.Add(category);
            }

            await ScratchPadContext.SaveChangesAsync();
        }

        [HttpDelete]
        [Route("{id}")]
        public async void Delete(int id)
        {
            var entryData = await ScratchPadContext.Entries
                .Include(a => a.Categories)
                .Include(a => a.Comments)
                .SingleOrDefaultAsync(a => a.Id == id);

            if (entryData == null)
            {
                ThrowNotFoundException(EntryNotFoundMessage(id));
            }

            foreach(var category in entryData.Categories.ToList())
            {
                entryData.Categories.Remove(category);
            }

            ScratchPadContext.Comments.RemoveRange(entryData.Comments);

            ScratchPadContext.Entries.Remove(entryData);

            await ScratchPadContext.SaveChangesAsync();
        }
    }
}
