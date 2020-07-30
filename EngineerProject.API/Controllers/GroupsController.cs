using EngineerProject.API.Entities;
using EngineerProject.API.Entities.Models;
using EngineerProject.API.Utility;
using EngineerProject.Commons.Dtos.Groups;
using EngineerProject.Commons.Dtos.Querying;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog.Targets;
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
        public IActionResult AskForInvite([FromBody] GroupInviteDto data)
        {
            var userId = ClaimsReader.GetUserId(Request);
            var group = context.Groups.SingleOrDefault(a => a.Id == data.Id);

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

            var values = context.UserGroups
                .Include(a => a.Group)
                .Where(a => a.UserId == userId)
                .Select(a => new
                {
                    a.Relation,
                    Value = new GroupTileDto
                    {
                        Id = a.GroupId,
                        Name = a.Group.Name
                    }
                }).ToList();

            var result = new UserGroupsWrapperDto();

            result.Participant.AddRange(values.Where(a => a.Relation == GroupRelation.Owner || a.Relation == GroupRelation.User).Select(a => a.Value));
            result.Invited.AddRange(values.Where(a => a.Relation == GroupRelation.Invited).Select(a => a.Value));
            result.Waiting.AddRange(values.Where(a => a.Relation == GroupRelation.Requesting).Select(a => a.Value));

            return Ok(result);
        }

        [HttpPost]
        public IActionResult InviteUser([FromBody] InviteUserDto data)
        {
            var userId = ClaimsReader.GetUserId(Request);

            if (!CheckAdminPriviliges(userId, data.GroupId))
                return BadRequest();

            var targetId = context.Users.SingleOrDefault(a => a.Login.Equals(data.UserIdentifier) || a.Email.Equals(data.UserIdentifier)).Id;
            var group = context.Groups.Include(a => a.Users).SingleOrDefault(a => a.Id == data.GroupId);

            if (group.Users.Any(a => a.UserId == targetId))
                return BadRequest($"Relacja pomiędzy tą grupą oraz użytkownikiem z identyfikatorem {data.UserIdentifier} już istnieje");

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

            var connection = context.UserGroups.SingleOrDefault(a => a.UserId == userId && a.GroupId == data.GroupId);

            context.UserGroups.Remove(connection);

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

            if (data.Accepted)
                connection.Relation = GroupRelation.User;
            else
                context.UserGroups.Remove(connection);

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
        public IActionResult ResolveGroupInvite([FromBody] GroupInviteResolveDto data)
        {
            var userId = ClaimsReader.GetUserId(Request);
            var connection = context.UserGroups.SingleOrDefault(a => a.UserId == userId && a.GroupId == data.Id);

            if (data.Value)
                connection.Relation = GroupRelation.User;
            else
                context.UserGroups.Remove(connection);

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

        [HttpDelete]
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

        [HttpGet]
        public IActionResult Details([FromQuery] int id)
        {
            var userId = ClaimsReader.GetUserId(Request);
            var group = context.Groups.SingleOrDefault(a => a.Id == id);

            var result = new GroupDetailsDto
            {
                Id = group.Id,
                Name = group.Name,
                Description = group.Description,
                IsPrivate = group.IsPrivate,
                IsOwner = CheckAdminPriviliges(userId, id)
            };

            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetAdminGroupDetails([FromQuery] int id)
        {
            var userId = ClaimsReader.GetUserId(Request);

            if (!CheckAdminPriviliges(userId, id))
                return BadRequest();

            var group = context.Groups
                .Include(a => a.Users)
                .ThenInclude(a => a.User)
                .FirstOrDefault(a => a.Id == id);

            var result = new GroupAdminDetailsDto
            {
                Name = group.Name,
                Description = group.Description,
                IsPrivate = group.IsPrivate,
                Candidates = group.Users
                .Where(a => a.Relation == GroupRelation.Requesting)
                .Select(a => new GroupCandidateDto 
                { 
                    UserId = a.UserId, 
                    UserLogin = a.User.Login 
                }).ToList()
            };

            return Ok(result);
        }

        [HttpGet]
        public IActionResult Get([FromQuery] QueryDto query)
        {
            var userId = ClaimsReader.GetUserId(Request);
            var formattedFilter = string.IsNullOrEmpty(query.Filter) ? string.Empty : query.Filter.ToLower();

            var result = context.Groups
                .OrderBy(a => a.Id)
                .Where(a => !a.IsPrivate && a.Name.ToLower().Contains(formattedFilter) && !a.Users.Any(b => b.UserId == userId))
                .Skip(query.PageSize * (query.Page - 1))
                .Take(query.PageSize)
                .Select(a => new GroupTileDto
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