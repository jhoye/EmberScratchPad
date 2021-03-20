using System.Collections.Generic;
using System.Linq;
using EntryEntity = ScratchPad.Data.Entities.Entry;
using Newtonsoft.Json;

namespace ScratchPad.Models
{
    public class Entry
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("comments")]
        public List<Comment> Comments { get; set; }

        [JsonProperty("categories")]
        public List<Category> Categories { get; set; }

        public Entry()
        {}

        public Entry(EntryEntity entryEntity, bool mapRelatedModels = false)
        {
            Id = entryEntity.Id;
            Name = entryEntity.Name;

            if (mapRelatedModels && entryEntity.Comments != null)
            {
                Comments = entryEntity.Comments.Select(a => new Comment(a)).ToList();
            }

            if (mapRelatedModels && entryEntity.Categories != null)
            {
                Categories = entryEntity.Categories.Select(a => new Category(a)).ToList();
            }
        }
    }
}
