using _4TEForum.Domain.Entities;
using _4TEForum.Services.Interfaces;
using _4TEForum.Web.Models.Forum;
using _4TEForum.Web.Models.Post;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace _4TEForum.Web.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForumServices _forumService;
        private readonly IPostServices _postService;
        private readonly IHostingEnvironment hostingEnvironment;

        public ForumController(IForumServices forumService, IPostServices postServices, IHostingEnvironment environment)
        {
            _forumService = forumService;
            _postService = postServices;
            hostingEnvironment = environment;
        }
        public IActionResult Index()
        {
            var forums = _forumService.GetAllForum()
                .Select(forum => new ForumListingModel
                {
                    Id = forum.Id,
                    Name = forum.Title,
                    Description = forum.Description,
                    NumberOfPosts = forum.Posts?.Count() ?? 0,
                    NumberOfUsers = _forumService.GetAllActiveUsers(forum.Id).Count()

                });

            var model = new ForumIndexModel
            {
                ForumList = forums
            };
            return View(model);
        }

        public IActionResult Topic(int id, string searchQuery)
        {
            var forum = _forumService.GetForumById(id);
            var posts = _forumService.GetFilteredPosts(id, searchQuery).ToList();
            var noResults = (!string.IsNullOrEmpty(searchQuery) && !posts.Any());

            var postListings = posts.Select(post => new PostListingModel
            {
                Id = post.Id,
                Forum = BuildForumListing(post),
                Author = post.User.UserName,
                AuthorId = post.User.Id,
                AuthorRating = post.User.Rating,
                Title = post.Title,
                DatePosted = post.Created.ToString(CultureInfo.InvariantCulture),
                RepliesCount = post.Replies.Count()
            }).OrderByDescending(post => post.DatePosted);

            var model = new ForumTopicModel
            {
                EmptySearchResults = noResults,
                Posts = postListings,
                SearchQuery = searchQuery,
                Forum = BuildForumListing(forum)
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Search(int id, string searchQuery)
        {
            return RedirectToAction("Topic", new { id, searchQuery });
        }

        public IActionResult Create()
        {
            var model = new AddForumModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddForum(AddForumModel model)
        {

            var imageUri = "";

            if (model.ImageUpload != null)
            {
                var name = Path.GetFileName(model.ImageUpload.FileName);
                var path = Path.Combine(hostingEnvironment.WebRootPath, "images");
                var uniquePath = Path.Combine(path, name);
                model.ImageUpload.CopyTo(new FileStream(uniquePath, FileMode.Create));
                imageUri = "/images/user/" + model.ImageUpload.FileName;
            }
            else
            {
                imageUri = "/images/user/default.jpg";
            }

            var forum = new Domain.Entities.Forum()
            {
                Title = model.Title,
                Description = model.Description,
                Created = DateTime.Now,
                ImageUrl = imageUri
            };

            await _forumService.CreateForum(forum);
            return RedirectToAction("Index", "Forum");
        }

        private ForumListingModel BuildForumListing(Post post)
        {
            var forum = post.Forum;
            return BuildForumListing(forum);
           
        }

        private ForumListingModel BuildForumListing(Forum forum)
        {
            return new ForumListingModel
            {
                Id = forum.Id,
                Name = forum.Title,
                Description = forum.Description,
                ImageUrl = forum.ImageUrl
            };
        }
    }
}
