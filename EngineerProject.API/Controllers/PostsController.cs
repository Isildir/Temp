using EngineerProject.API.Entities;
using EngineerProject.API.Entities.Models;
using EngineerProject.API.Utility;
using EngineerProject.Commons.Dtos.Groups;
using EngineerProject.Commons.Dtos.Querying;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace EngineerProject.API.Controllers
{
    [Authorize, ApiController, Route("api/[controller]/[action]")]
    public class PostsController : Controller
    {
        private readonly EngineerContext context;

        public PostsController(EngineerContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public IActionResult Create([FromBody] PostCreateDto data)
        {
            if (string.IsNullOrEmpty(data.Title) || string.IsNullOrEmpty(data.Content))
                return BadRequest();

            var userId = ClaimsReader.GetUserId(Request);

            if (!context.UserGroups.Any(a => a.GroupId == data.GroupId && a.UserId == userId && (a.Relation == GroupRelation.Owner || a.Relation == GroupRelation.User)))
                return BadRequest();

            var post = new Post
            {
                DateAdded = DateTime.UtcNow,
                Content = data.Content,
                GroupId = data.GroupId,
                Title = data.Title,
                UserId = userId
            };

            context.Posts.Add(post);

            try
            {
                context.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var userId = ClaimsReader.GetUserId(Request);

            var post = context.Posts.FirstOrDefault(a => a.Id == id && a.UserId == userId);

            if (post == null)
                return NotFound();

            context.Posts.Remove(post);

            try
            {
                context.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult Get([FromQuery] GroupQueryDto query)
        {
            var userId = ClaimsReader.GetUserId(Request);

            if (!context.UserGroups.Any(a => a.GroupId == query.GroupId && a.UserId == userId && (a.Relation == GroupRelation.Owner || a.Relation == GroupRelation.User)))
                return BadRequest();

            var result = context.Posts
                .Where(a => a.GroupId == query.GroupId)
                .OrderBy(a => a.Pinned)
                .ThenBy(a => a.DateAdded)
                .Skip(query.PageSize * (query.Page - 1))
                .Take(query.PageSize)
                .Select(a => new PostDto
                {
                    Id = a.Id,
                    Title = a.Title,
                    DateAdded = a.DateAdded,
                    Content = a.Content,
                    Edited = a.Edited,
                    Pinned = a.Pinned,
                    EditDate = a.EditDate,
                    IsOwner = a.UserId == userId,
                    Comments = a.Comments.Select(b => new CommentDto
                    {
                        Id = a.Id,
                        DateAdded = a.DateAdded,
                        Content = a.Content,
                        Edited = a.Edited,
                        EditDate = a.EditDate,
                        IsOwner = a.UserId == userId,
                    }).ToList()
                }).ToList();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Modify([FromBody] PostModifyDto data)
        {
            if (string.IsNullOrEmpty(data.Title) || string.IsNullOrEmpty(data.Content))
                return BadRequest();

            var userId = ClaimsReader.GetUserId(Request);
            var post = context.Posts.FirstOrDefault(a => a.Id == data.Id && a.UserId == userId);

            if (post == null)
                return NotFound();

            post.Edited = true;
            post.EditDate = DateTime.UtcNow;
            post.Content = data.Content;
            post.Title = data.Title;

            try
            {
                context.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}