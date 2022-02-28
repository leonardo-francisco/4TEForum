using System;

namespace _4TEForum.Web.Models.Post
{
    public class EditPostModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public string ForumName { get; set; }
        public int ForumId { get; set; }
    }
}
