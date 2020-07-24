using EngineerProject.API.Entities;
using EngineerProject.API.Entities.Models;
using EngineerProject.API.Utility;
using EngineerProject.Commons.Dtos.Groups;
using EngineerProject.Commons.Dtos.Querying;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EngineerProject.API.Controllers
{
    [Authorize, ApiController, Route("api/[controller]/[action]")]
    public class GroupsController : Controller
    {
        private readonly EngineerContext context;

        public GroupsController(EngineerContext context)
        {
            this.context = context;
        }

        #region Interactions

        [HttpPost]
        public IActionResult AskForInvite(int groupId)
        {
            var userId = ClaimsReader.GetUserId(Request);
            var group = context.Groups.SingleOrDefault(a => a.Id == groupId);

            if (group == null)
                return NotFound();

            var connection = new UserGroup
            {
                UserId = userId,
                GroupId = group.Id,
                Relation = group.IsPrivate ? GroupRelation.Requesting : GroupRelation.User
            };

            context.UserGroups.Add(connection);

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
        public IActionResult GetUserGroups()
        {
            var userId = ClaimsReader.GetUserId(Request);

            var result = context.Users
                .Include(a => a.Groups).ThenInclude(a => a.Group)
                .FirstOrDefault(a => a.Id == userId)
                .Groups
                .Select(a => new GroupGridDto
                {
                    Id = a.Id,
                    Name = a.Group.Name
                }).ToList();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult InviteUser([FromBody] InviteUserDto data)
        {
            var userId = ClaimsReader.GetUserId(Request);

            if (!CheckAdminPriviliges(userId, data.GroupId))
                return BadRequest();

            var targetId = context.Users.SingleOrDefault(a => a.Login.Equals(data.UserIdentifier) || a.Email.Equals(data.UserIdentifier)).Id;
            var group = context.Groups.SingleOrDefault(a => a.Id == data.GroupId);

            var connection = new UserGroup
            {
                UserId = targetId,
                GroupId = group.Id,
                Relation = GroupRelation.Invited
            };

            context.UserGroups.Add(connection);

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
        public IActionResult RemoveUser([FromBody] RemoveGroupUserDto data)
        {
            var userId = ClaimsReader.GetUserId(Request);

            if (!CheckAdminPriviliges(userId, data.GroupId))
                return BadRequest();

            var targetId = context.Users.SingleOrDefault(a => a.Id == data.UserId).Id;
            var group = context.Groups.SingleOrDefault(a => a.Id == data.GroupId);
            var connection = context.UserGroups.SingleOrDefault(a => a.UserId == targetId && a.GroupId == data.GroupId);

            connection.Relation = GroupRelation.Rejected;

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
        public IActionResult ResolveApplication([FromBody] GroupAccessAcceptanceDto data)
        {
            var userId = ClaimsReader.GetUserId(Request);

            if (!CheckAdminPriviliges(userId, data.GroupId))
                return BadRequest();

            var targetId = context.Users.SingleOrDefault(a => a.Id == data.UserId).Id;
            var group = context.Groups.SingleOrDefault(a => a.Id == data.GroupId);
            var connection = context.UserGroups.SingleOrDefault(a => a.UserId == targetId && a.GroupId == data.GroupId);

            connection.Relation = data.Accepted ? GroupRelation.User : GroupRelation.Rejected;

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

        #endregion Interactions

        #region CRUD

        [HttpPost]
        public IActionResult Create([FromBody] GroupCreateDto data)
        {
            if (string.IsNullOrEmpty(data.Name))
                return BadRequest();

            var group = new Group
            {
                IsPrivate = data.IsPrivate,
                Name = data.Name,
                Description = data.Description
            };

            var userId = ClaimsReader.GetUserId(Request);

            var connection = new UserGroup
            {
                UserId = userId,
                Group = group,
                Relation = GroupRelation.Owner
            };

            context.Groups.Add(group);
            context.UserGroups.Add(connection);

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
        public IActionResult Delete(int id)
        {
            var userId = ClaimsReader.GetUserId(Request);

            if (CheckAdminPriviliges(userId, id))
                return BadRequest();

            var group = context.Groups.SingleOrDefault(a => a.Id == id);

            context.Groups.Remove(group);

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
        public IActionResult Details(int id)
        {
            var userId = ClaimsReader.GetUserId(Request);

            if (!CheckAdminPriviliges(userId, id))
                return BadRequest();

            var group = context.Groups.SingleOrDefault(a => a.Id == id);

            var result = new GroupDetailsDto
            {
                Id = group.Id,
                Name = group.Name,
                Description = group.Description,
                IsPrivate = group.IsPrivate
            };

            return Ok(result);
        }

        [HttpGet]
        public IActionResult Get([FromQuery] QueryDto query)
        {
            var formattedFilter = string.IsNullOrEmpty(query.Filter) ? string.Empty : query.Filter.ToLower();

            var result = context.Groups
                .OrderBy(a => a.Id)
                .Where(a => a.Name.ToLower().Contains(formattedFilter))
                .Skip(query.PageSize * (query.Page - 1))
                .Take(query.PageSize)
                .Select(a => new GroupGridDto
                {
                    Id = a.Id,
                    Name = a.Name
                }).ToList();

            return Ok(result);
        }

        private bool CheckAdminPriviliges(int userId, int groupId) => context.UserGroups.Any(a => a.GroupId == groupId && a.UserId == userId && a.Relation == GroupRelation.Owner);

        #endregion CRUD
    }
}