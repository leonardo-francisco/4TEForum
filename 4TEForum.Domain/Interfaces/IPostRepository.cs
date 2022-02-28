using _4TEForum.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _4TEForum.Domain.Interfaces
{
    public interface IPostRepository
    {
        Post GetById(int id);
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetFilteredPosts(string searchQuery);
        IEnumerable<Post> GetPostsByForum(int id);
        IEnumerable<Post> GetLastestPosts(int nPosts);
        IEnumerable<ApplicationUser> GetAllUsers(IEnumerable<Post> posts);

        Task Add(Post post);
        Task AddReply(PostReply reply);
        Task Delete(int id);
        Task EditPostContent(int id, string newContent);

        
    }
}
