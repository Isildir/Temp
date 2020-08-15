﻿using EngineerProject.Commons.Dtos.Groups;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EngineerProject.Mobile.Services
{
    public class GroupService : BaseService
    {
        public async Task<DataRequestResponse<List<PostDto>>> GetPosts(int groupId, int page, int pageSize)
        {
            var response = await client.GetAsync<List<PostDto>>($"Posts/Get?groupId={groupId}&page={page}&pageSize={pageSize}");

            return MapResponse(response);
        }

        public async Task<DataRequestResponse<GroupDetailsDto>> GetDetails(int groupId)
        {
            var response = await client.GetAsync<GroupDetailsDto>($"Groups/Details?id={groupId}");

            return MapResponse(response);
        }

        public async Task<DataRequestResponse<GroupAdminDetailsDto>> GetAdminGroupDetails(int groupId)
        {
            var response = await client.GetAsync<GroupAdminDetailsDto>($"Groups/GetAdminGroupDetails?id={groupId}");

            return MapResponse(response);
        }

        public async Task<DataRequestResponse<List<MessageDto>>> LoadComments(int groupId, int page, int pageSize)
        {
            var response = await client.GetAsync<List<MessageDto>>($"Messages/Get?groupId={groupId}&page={page}&pageSize={pageSize}");

            return MapResponse(response);
        }

        public async Task<RequestResponse> AddPost(int groupId, string title, string content)
        {
            var response = await client.PostAsync($"Posts/Create", new { groupId, title, content });

            return MapResponse(response);
        }

        public async Task<RequestResponse> AddComment(int postId, string content)
        {
            var response = await client.PostAsync($"Comments/Create", new { postId, content });

            return MapResponse(response);
        }

        public async Task<RequestResponse> ResolveApplication(int groupId, int userId, bool accepted)
        {
            var response = await client.PostAsync($"Groups/ResolveApplication", new { groupId, userId, accepted });

            return MapResponse(response);
        }

        public async Task<RequestResponse> InviteUser(int groupId, string userIdentifier)
        {
            var response = await client.PostAsync($"Groups/InviteUser", new { groupId, userIdentifier });

            return MapResponse(response);
        }

        public async Task<RequestResponse> ModifyPost(int postId, string title, string content)
        {
            var response = await client.PutAsync($"Posts/Modify?id={postId}", new { title, content });

            return MapResponse(response);
        }

        public async Task<RequestResponse> DeletePost(int postId)
        {
            var response = await client.DeleteAsync($"Posts/Delete?id={postId}");

            return MapResponse(response);
        }

        public async Task<RequestResponse> DeleteComment(int commentId)
        {
            var response = await client.DeleteAsync($"Comments/Delete?id={commentId}");

            return MapResponse(response);
        }
    }
}