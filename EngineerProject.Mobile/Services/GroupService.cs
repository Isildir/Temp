using EngineerProject.Commons.Dtos.Groups;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EngineerProject.Mobile.Services
{
    public class GroupService : BaseService
    {
        public async Task<DataRequestResponse<List<PostDto>>> GetPosts(int groupId, int page, int pageSize)
        {
            var result = new DataRequestResponse<List<PostDto>>();

            var response = await client.GetAsync<List<PostDto>>($"Posts/Get?groupId={groupId}&page={page}&pageSize={pageSize}", new CancellationToken());

            if (response != null)
            {
                result.IsSuccessful = true;
                result.Data = response;
            }
            else
                result.ErrorMessage = client.error.Message;

            return result;
        }

        public async Task<DataRequestResponse<GroupDetailsDto>> GetDetails(int groupId)
        {
            var result = new DataRequestResponse<GroupDetailsDto>();

            var response = await client.GetAsync<GroupDetailsDto>($"Groups/Details?id={groupId}", new CancellationToken());

            if (response != null)
            {
                result.IsSuccessful = true;
                result.Data = response;
            }
            else
                result.ErrorMessage = client.error.Message;

            return result;
        }

        public async Task<DataRequestResponse<GroupAdminDetailsDto>> GetAdminGroupDetails(int groupId)
        {
            var result = new DataRequestResponse<GroupAdminDetailsDto>();

            var response = await client.GetAsync<GroupAdminDetailsDto>($"Groups/GetAdminGroupDetails?id={groupId}", new CancellationToken());

            if (response != null)
            {
                result.IsSuccessful = true;
                result.Data = response;
            }
            else
                result.ErrorMessage = client.error.Message;

            return result;
        }

        public async Task<DataRequestResponse<List<MessageDto>>> LoadComments(int groupId, int page, int pageSize)
        {
            var result = new DataRequestResponse<List<MessageDto>>();

            var response = await client.GetAsync<List<MessageDto>>($"Messages/Get?groupId={groupId}&page={page}&pageSize={pageSize}", new CancellationToken());

            if (response != null)
            {
                result.IsSuccessful = true;
                result.Data = response;
            }
            else
                result.ErrorMessage = client.error.Message;

            return result;
        }

        public async Task<RequestResponse> AddPost(int groupId, string title, string content)
        {
            var result = new RequestResponse();

            var response = await client.PostAsync($"Posts/Create", new { groupId, title, content }, new CancellationToken());

            if (response)
                result.IsSuccessful = true;
            else
                result.ErrorMessage = client.error.Message;

            return result;
        }

        public async Task<RequestResponse> AddComment(int postId, string content)
        {
            var result = new RequestResponse();

            var response = await client.PostAsync($"Comments/Create", new { postId, content }, new CancellationToken());

            if (response)
                result.IsSuccessful = true;
            else
                result.ErrorMessage = client.error.Message;

            return result;
        }

        public async Task<RequestResponse> ResolveApplication(int groupId, int userId, bool accepted)
        {
            var result = new RequestResponse();

            var response = await client.PostAsync($"Groups/ResolveApplication", new { groupId, userId, accepted }, new CancellationToken());

            if (response)
                result.IsSuccessful = true;
            else
                result.ErrorMessage = client.error.Message;

            return result;
        }

        public async Task<RequestResponse> InviteUser(int groupId, string userIdentifier)
        {
            var result = new RequestResponse();

            var response = await client.PostAsync($"Groups/InviteUser", new { groupId, userIdentifier }, new CancellationToken());

            if (response)
                result.IsSuccessful = true;
            else
                result.ErrorMessage = client.error.Message;

            return result;
        }

        public async Task<RequestResponse> ModifyPost(int postId, string title, string content)
        {
            var result = new RequestResponse();

            var response = await client.PutAsync($"Posts/Modify?id={postId}", new { title, content }, new CancellationToken());

            if (response)
                result.IsSuccessful = true;
            else
                result.ErrorMessage = client.error.Message;

            return result;
        }

        public async Task<RequestResponse> DeletePost(int postId)
        {
            var result = new RequestResponse();

            var response = await client.DeleteAsync($"Posts/Delete?id={postId}", new CancellationToken());

            if (response)
                result.IsSuccessful = true;
            else
                result.ErrorMessage = client.error.Message;

            return result;
        }

        public async Task<RequestResponse> DeleteComment(int commentId)
        {
            var result = new RequestResponse();

            var response = await client.DeleteAsync($"Comments/Delete?id={commentId}", new CancellationToken());

            if (response)
                result.IsSuccessful = true;
            else
                result.ErrorMessage = client.error.Message;

            return result;
        }
    }
}