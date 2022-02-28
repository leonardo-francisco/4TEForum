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
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Post post)
        {
            _context.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task AddReply(PostReply reply)
        {
            _context.PostReplies.Add(reply);
            await _context.SaveChangesAsync();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task EditPostContent(int id, string newContent)
        {
            var post = GetById(id);
            post.Content = newContent;
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Post> GetAll()
        {
            return _context.Posts
               .Include(post => post.User)
               .Include(post => post.Replies).ThenInclude(reply => reply.User)
               .Include(post => post.Forum);
               
        }

        public IEnumerable<ApplicationUser> GetAllUsers(IEnumerable<Post> posts)
        {
            var users = new List<ApplicationUser>();

            foreach (var post in posts)
            {
                users.Add(post.User);

                if (!post.Replies.Any()) continue;

                users.AddRange(post.Replies.Select(reply => reply.User));
            }

            return users.Distinct();
        }

        public Post GetById(int id)
        {
            return _context.Posts.Where(post => post.Id == id)
                .Include(post => post.User)
                .Include(post => post.Replies).ThenInclude(reply => reply.User)
                .Include(post => post.Forum)
                .First();
        }

        public IEnumerable<Post> GetFilteredPosts(string searchQuery)
        {
            var query = searchQuery.ToLower();

            return _context.Posts
                .Include(post => post.Forum)
                .Include(post => post.User)
                .Include(post => post.Replies)
                .Where(post =>
                    post.Title.ToLower().Contains(query)
                 || post.Content.ToLower().Contains(query));
        }

        public IEnumerable<Post> GetLastestPosts(int nPosts)
        {
            return GetAll().OrderByDescending(post => post.Created).Take(nPosts);
        }

        public IEnumerable<Post> GetPostsByForum(int id)
        {
            return _context.Forums
                .Where(forum => forum.Id == id).First()
                .Posts;
        }
    }
}
