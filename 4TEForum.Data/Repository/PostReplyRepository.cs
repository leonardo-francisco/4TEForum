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
    public class PostReplyRepository : IPostReplyRepository
    {
        private readonly ApplicationDbContext _context;

        public PostReplyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Delete(int id)
        {
            var reply = GetById(id);
            _context.Remove(reply);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(int id, string message)
        {
            var reply = GetById(id);
            reply.Content = message;
            //await _context.SaveChangesAsync();
            //_context.Update(reply);
            //await _context.SaveChangesAsync();

            _context.PostReplies.Attach(reply);
            _context.Entry(reply).Property(X => X.Content).IsModified = true;
            _context.SaveChanges();
        }

        public PostReply GetById(int id)
        {
            return _context.PostReplies
                .Include(r => r.Post)
                .ThenInclude(post => post.Forum)
                .Include(r => r.Post)
                .ThenInclude(post => post.User).First(r => r.Id == id);
        }
    }
}
