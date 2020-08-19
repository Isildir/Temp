using EngineerProject.API.Entities;
using EngineerProject.API.Entities.Models;
using EngineerProject.API.Utility;
using EngineerProject.Commons.Dtos.Groups;
using EngineerProject.Commons.Dtos.Querying;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EngineerProject.API.Controllers
{
    [Authorize, ApiController, Route("api/[controller]/[action]")]
    public class MessagesController : Controller
    {
        private readonly EngineerContext context;

        public MessagesController(EngineerContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] GroupQueryDto query)
        {
            var userId = ClaimsReader.GetUserId(Request);

            if (!context.UserGroups.Any(a => a.GroupId == query.GroupId && a.UserId == userId && (a.Relation == GroupRelation.Owner || a.Relation == GroupRelation.User)))
                return BadRequest();

            var result = context.Messages
                .Where(a => a.GroupId == query.GroupId)
                .OrderByDescending(a => a.DateAdded)
                .Skip(query.PageSize * (query.Page - 1))
                .Take(query.PageSize)
                .Select(a => new MessageDto
                {
                    DateAdded = a.DateAdded,
                    Content = a.Content,
                    Owner = a.Sender.Login
                }).ToList();

            return Ok(result);
        }
    }
}