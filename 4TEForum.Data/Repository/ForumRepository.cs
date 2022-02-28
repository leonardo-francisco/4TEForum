using _4TEForum.Domain.Entities;
using _4TEForum.Domain.Interfaces;
using _4TEForum.Web.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4TEForum.Data.Repository
{
    public class ForumRepository : IForumRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IPostRepository _postRepository;

        public ForumRepository(ApplicationDbContext context, IPostRepository postRepository)
        {
            _context = context;
            _postRepository = postRepository;
        }
        public async Task Create(Forum forum)
        {
            _context.Add(forum);
            await _context.SaveChangesAsync();
        }

        public Task Delete(int forumId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Forum> GetAll()
        {
            return _context.Forums.Include(forum => forum.Posts);
        }

        public IEnumerable<ApplicationUser> GetAllActiveUsers(int forumId)
        {
            var posts = GetById(forumId).Posts;

            if (posts == null || !posts.Any())
            {
                return new List<ApplicationUser>();
            }

            return _postRepository.GetAllUsers(posts);
        }

        public Forum GetById(int id)
        {
            var forum = _context.Forums.Where(f => f.Id == id)
                .Include(f => f.Posts)
                    .ThenInclude(p => p.User)
                .Include(f => f.Posts)
                    .ThenInclude(p => p.Replies)
                         .ThenInclude(r => r.User)
                .FirstOrDefault();

            return forum;
        }

        public IEnumerable<Post> GetFilteredPosts(int forumId, string modelSearchQuery)
        {
            if (forumId == 0) return _postRepository.GetFilteredPosts(modelSearchQuery);

            var forum = GetById(forumId);

            return string.IsNullOrEmpty(modelSearchQuery)
                ? forum.Posts
                : forum.Posts.Where(post
                    => post.Title.Contains(modelSearchQuery) || post.Content.Contains(modelSearchQuery));
        }

        public Task UpdateForumDescription(int forumId, string newDescription)
        {
            throw new NotImplementedException();
        }

        public Task UpdateForumTitle(int forumId, string newTitle)
        {
            throw new NotImplementedException();
        }
    }
}
