using System.Collections.Generic;

namespace ScratchPad.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Entry> Entries { get; set; }
    }
}
