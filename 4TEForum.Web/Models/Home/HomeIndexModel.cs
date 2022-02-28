using _4TEForum.Web.Models.Post;
using System.Collections.Generic;

namespace _4TEForum.Web.Models.Home
{
    public class HomeIndexModel
    {
        public string SearchQuery { get; set; }
        public IEnumerable<PostListingModel> LatestPosts { get; set; }
    }
}
