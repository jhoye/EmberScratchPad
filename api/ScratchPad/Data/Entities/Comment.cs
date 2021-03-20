namespace ScratchPad.Data.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public Entry Entry { get; set; }
    }
}
