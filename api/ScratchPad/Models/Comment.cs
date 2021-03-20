using CommentEntity = ScratchPad.Data.Entities.Comment;
using Newtonsoft.Json;

namespace ScratchPad.Models
{
    public class Comment
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("entry")]
        public Entry Entry { get; set; }

        public Comment()
        {}

        public Comment(CommentEntity commentEntity, bool mapRelatedModels = false)
        {
            Id = commentEntity.Id;
            Text = commentEntity.Text;
            
            if (mapRelatedModels && commentEntity.Entry != null)
            {
                Entry = new Entry(commentEntity.Entry);
            }
        }
    }
}
