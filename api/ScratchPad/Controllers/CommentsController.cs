using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ScratchPad.Data;
using ScratchPad.Models;
using CommentData = ScratchPad.Data.Entities.Comment;

namespace ScratchPad.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ScratchPadControllerBase
    {
        private readonly ILogger<CommentsController> _logger;

        public CommentsController(
            ILogger<CommentsController> logger,
            ScratchPadContext scratchPadContext
        ) : base(scratchPadContext)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<List<Comment>> Get(string include = "")
        {
            return await ScratchPadContext.GetComments(include)
                .Select(commentData => new Comment(commentData, true))
                .ToListAsync();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Comment> Get(int id, string include = "")
        {
            var commentData = await ScratchPadContext.GetComments(include)
                .SingleOrDefaultAsync(a => a.Id == id);

            if (commentData == null)
            {
                ThrowNotFoundException(CommentNotFoundMessage(id));
            }

            return new Comment(commentData, true);
        }

        [HttpPost]
        public async Task<Comment> Post([FromBody] Comment comment)
        {
            var commentData = new CommentData
            {
                Text = string.IsNullOrWhiteSpace(comment.Text)
                    ? $"new comment..."
                    : comment.Text
            };

            var entryData = await ScratchPadContext.Entries
                .SingleAsync(a => a.Id == comment.Entry.Id);

            ScratchPadContext.Comments.Add(commentData);

            if (entryData.Comments == null)
            {
                entryData.Comments = new List<CommentData> { commentData };
            }
            else
            {
                entryData.Comments.Add(commentData);
            }

            await ScratchPadContext.SaveChangesAsync();

            return new Comment(commentData);
        }

        [HttpPut]
        [Route("{id}")]
        public async void Put([FromBody] Comment comment)
        {
            var commentData = await ScratchPadContext.Comments
                .SingleOrDefaultAsync(a => a.Id == comment.Id);

            if (commentData == null)
            {
                ThrowNotFoundException(CommentNotFoundMessage(comment.Id));
            }

            commentData.Text = string.IsNullOrWhiteSpace(comment.Text)
                ? $"comment {comment.Id}..."
                : comment.Text;

            await ScratchPadContext.SaveChangesAsync();
        }

        [HttpDelete]
        [Route("{id}")]
        public async void Delete(int id)
        {
            var commentData = await ScratchPadContext.Comments
                .Include(a => a.Entry)
                .SingleOrDefaultAsync(a => a.Id == id);

            if (commentData == null)
            {
                ThrowNotFoundException(CommentNotFoundMessage(id));
            }

            commentData.Entry.Comments.Remove(commentData);

            ScratchPadContext.Comments.Remove(commentData);

            await ScratchPadContext.SaveChangesAsync();
        }
    }
}
