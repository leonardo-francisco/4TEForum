using _4TEForum.Domain.Entities;
using _4TEForum.Domain.Interfaces;
using _4TEForum.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _4TEForum.Services
{
    public class PostReplyServices : IPostReplyServices
    {
        private readonly IPostReplyRepository _postReplyRepository;

        public PostReplyServices(IPostReplyRepository postReplyRepository)
        {
            _postReplyRepository = postReplyRepository;
        }

        public Task DeleteReply(int id)
        {
            return _postReplyRepository.Delete(id);
        }

        public Task EditReply(int id, string message)
        {
            return _postReplyRepository.Edit(id, message);
        }

        public PostReply GetReplyById(int id)
        {
            return _postReplyRepository.GetById(id);
        }
    }
}
