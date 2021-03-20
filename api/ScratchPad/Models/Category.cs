using System.Collections.Generic;
using System.Linq;
using CategoryEntity = ScratchPad.Data.Entities.Category;
using Newtonsoft.Json;

namespace ScratchPad.Models
{
    public class Category
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("entries")]
        public List<Entry> Entries { get; set; }

        public Category()
        {}

        public Category(CategoryEntity categoryEntity, bool mapRelatedModels = false)
        {
            Id = categoryEntity.Id;
            Name = categoryEntity.Name;

            if (mapRelatedModels && categoryEntity.Entries != null)
            {
                Entries = categoryEntity.Entries.Select(a => new Entry(a)).ToList();
            }
        }
    }
}
