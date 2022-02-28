using _4TEForum.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _4TEForum.Domain.Interfaces
{
    public interface IPostReplyRepository
    {
        PostReply GetById(int id);
        Task Edit(int id, string message);
        Task Delete(int id);
    }
}
