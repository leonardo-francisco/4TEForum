using _4TEForum.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _4TEForum.Services.Interfaces
{
    public interface IForumServices
    {
        Forum GetForumById(int id);
        IEnumerable<Forum> GetAllForum();
        IEnumerable<ApplicationUser> GetAllActiveUsers(int forumId);
        IEnumerable<Post> GetFilteredPosts(int forumId, string modelSearchQuery);

        Task CreateForum(Forum forum);
        Task DeleteForum(int forumId);
        Task UpdateForumByTitle(int forumId, string newTitle);
        Task UpdateForumByDescription(int forumId, string newDescription);
    }
}
