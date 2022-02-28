using _4TEForum.Domain.Entities;
using _4TEForum.Services.Interfaces;
using _4TEForum.Web.Models.Reply;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace _4TEForum.Web.Controllers
{
    public class ReplyController : Controller
    {
        private readonly IPostServices _postService;
        private readonly IForumServices _forumService;
        private readonly IPostReplyServices _postReplyService;
        private static UserManager<ApplicationUser> _userManager;
        //private readonly IApplicationUser _userService;
        

        public ReplyController(IForumServices forumService, IPostServices postService, /*IApplicationUser userService,*/ IPostReplyServices postReplyService, UserManager<ApplicationUser> userManager)
        {
            _forumService = forumService;
            _postService = postService;
            _postReplyService = postReplyService;
            //_userService = userService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Create(int id)
        {
            var post = _postService.GetPostById(id);
            var forum = _forumService.GetForumById(post.Forum.Id);
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var model = new PostReplyModel
            {
                PostContent = post.Content,
                PostTitle = post.Title,
                PostId = post.Id,

                ForumName = forum.Title,
                ForumId = forum.Id,
                ForumImageUrl = forum.ImageUrl,

                AuthorName = User.Identity.Name,
                AuthorImageUrl = user.ProfileImageUrl,
                AuthorId = user.Id,
                AuthorRating = user.Rating,
                IsAuthorAdmin = user.IsAdmin,

                Date = DateTime.Now
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddReply(PostReplyModel model)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);

            var reply = BuildReply(model, user);
            await _postService.AddReply(reply);
            //await _userService.BumpRating(userId, typeof(PostReply));

            return RedirectToAction("Index", "Post", new { id = model.PostId });
        }

        public IActionResult Edit(int id)
        {
            var reply = _postReplyService.GetReplyById(id);

            var model = new PostReplyModel
            {
                Id = reply.Id,
                ReplyContent = reply.Content,
                PostTitle = reply.Post.Title,
                PostId = reply.Post.Id
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditReply(int id, PostReplyModel model)
        {
            await _postReplyService.EditReply(id, model.ReplyContent);           

            return RedirectToAction("Index", "Post", new { id = model.PostId });
        }

        private PostReply BuildReply(PostReplyModel reply, ApplicationUser user)
        {
            var now = DateTime.Now;
            var post = _postService.GetPostById(reply.PostId);

            return new PostReply
            {
                Post = post,
                Content = reply.ReplyContent,
                Created = now,
                User = user
            };
        }
    }
}
