using _4TEForum.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _4TEForum.Services.Interfaces
{
    public interface IPostServices
    {
        Post GetPostById(int id);
        IEnumerable<Post> GetAllPost();
        IEnumerable<Post> GetFilteredPosts(string searchQuery);
        IEnumerable<Post> GetPostsByForum(int id);
        IEnumerable<Post> GetLastestPosts(int nPosts);
        IEnumerable<ApplicationUser> GetAllUsers(IEnumerable<Post> posts);

        Task AddPost(Post post);
        Task AddReply(PostReply reply);
        Task DeletePost(int id);
        Task EditPostContent(int id, string newContent);
    }
}
