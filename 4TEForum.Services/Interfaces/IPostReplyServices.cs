using _4TEForum.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _4TEForum.Services.Interfaces
{
    public interface IPostReplyServices
    {
        PostReply GetReplyById(int id);
        Task EditReply(int id, string message);
        Task DeleteReply(int id);
    }
}
