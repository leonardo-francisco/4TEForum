using _4TEForum.Domain.Entities;
using _4TEForum.Domain.Interfaces;
using _4TEForum.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _4TEForum.Services
{
    public class PostServices : IPostServices
    {
        private readonly IPostRepository _postRepository;

        public PostServices(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public Task AddPost(Post post)
        {
            return _postRepository.Add(post);
        }

        public Task AddReply(PostReply reply)
        {
            return _postRepository.AddReply(reply);
        }

        public Task DeletePost(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditPostContent(int id, string newContent)
        {
            return _postRepository.EditPostContent(id, newContent); ;
        }

        public IEnumerable<Post> GetAllPost()
        {
            return _postRepository.GetAll();
        }

        public IEnumerable<ApplicationUser> GetAllUsers(IEnumerable<Post> posts)
        {
            return _postRepository.GetAllUsers(posts);
        }

        public IEnumerable<Post> GetFilteredPosts(string searchQuery)
        {
            return _postRepository.GetFilteredPosts(searchQuery);
        }

        public IEnumerable<Post> GetLastestPosts(int nPosts)
        {
            return _postRepository.GetLastestPosts(nPosts);
        }

        public Post GetPostById(int id)
        {
            return _postRepository.GetById(id);
        }

        public IEnumerable<Post> GetPostsByForum(int id)
        {
            return _postRepository.GetPostsByForum(id);
        }
    }
}
