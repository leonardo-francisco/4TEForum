using _4TEForum.Domain.Entities;
using _4TEForum.Domain.Interfaces;
using _4TEForum.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _4TEForum.Services
{
    public class ForumServices : IForumServices
    {
        private readonly IForumRepository _forumRepository;

        public ForumServices(IForumRepository forumRepository)
        {
            _forumRepository = forumRepository;
        }
        public Task CreateForum(Forum forum)
        {
            return _forumRepository.Create(forum);
        }

        public Task DeleteForum(int forumId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApplicationUser> GetAllActiveUsers(int forumId)
        {
            return _forumRepository.GetAllActiveUsers(forumId);
        }

        public IEnumerable<Forum> GetAllForum()
        {
            return _forumRepository.GetAll();
        }

        public IEnumerable<Post> GetFilteredPosts(int forumId, string modelSearchQuery)
        {
            return _forumRepository.GetFilteredPosts(forumId, modelSearchQuery);
        }

        public Forum GetForumById(int id)
        {
            return _forumRepository.GetById(id);
        }

        public Task UpdateForumByDescription(int forumId, string newDescription)
        {
            throw new NotImplementedException();
        }

        public Task UpdateForumByTitle(int forumId, string newTitle)
        {
            throw new NotImplementedException();
        }
    }
}
