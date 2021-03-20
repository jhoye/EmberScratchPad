using System.Collections.Generic;

namespace ScratchPad.Data.Entities
{
    public class Entry
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Comment> Comments { get; set; }

        public List<Category> Categories { get; set; }
    }
}
