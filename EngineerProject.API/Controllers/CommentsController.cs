using EngineerProject.API.Entities;
using EngineerProject.API.Entities.Models;
using EngineerProject.API.Utility;
using EngineerProject.Commons.Dtos.Groups;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace EngineerProject.API.Controllers
{
    [Authorize, ApiController, Route("api/[controller]/[action]")]
    public class CommentsController : Controller
    {
        private readonly EngineerContext context;

        public CommentsController(EngineerContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CommentCreateDto data)
        {
            if (string.IsNullOrEmpty(data.Content))
                return BadRequest();

            var userId = ClaimsReader.GetUserId(Request);
            var post = context.Posts.FirstOrDefault(a => a.Id == data.PostId);

            if (post == null)
                return NotFound();

            if (!context.UserGroups.Any(a => a.GroupId == post.GroupId && a.UserId == userId && (a.Relation == GroupRelation.Owner || a.Relation == GroupRelation.User)))
                return BadRequest();

            var comment = new Comment
            {
                DateAdded = DateTime.UtcNow,
                Content = data.Content,
                PostId = data.PostId,
                UserId = userId
            };

            context.Comments.Add(comment);

            try
            {
                context.SaveChanges();

                return Ok(new { comment.Id, comment.Content, comment.DateAdded, IsOwner = true });
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
            var comment = context.Comments.FirstOrDefault(a => a.Id == id && a.UserId == userId);

            if (comment == null)
                return NotFound();

            context.Comments.Remove(comment);

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

        [HttpPost]
        public IActionResult Modify([FromBody] CommentModifyDto data)
        {
            if (string.IsNullOrEmpty(data.Title) || string.IsNullOrEmpty(data.Content))
                return BadRequest();

            var userId = ClaimsReader.GetUserId(Request);
            var comment = context.Comments.FirstOrDefault(a => a.Id == data.Id && a.UserId == userId);

            if (comment == null)
                return NotFound();

            comment.Edited = true;
            comment.EditDate = DateTime.UtcNow;
            comment.Content = data.Content;

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