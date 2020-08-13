using EngineerProject.Commons.Dtos.Groups;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EngineerProject.Mobile.Services
{
    public class HomeService : BaseService
    {
        public async Task<DataRequestResponse<UserGroupsWrapperDto>> GetUserGroups()
        {
            var result = new DataRequestResponse<UserGroupsWrapperDto>();

            var response = await client.GetAsync<UserGroupsWrapperDto>($"Groups/GetUserGroups", new CancellationToken());

            if (response != null)
            {
                result.IsSuccessful = true;
                result.Data = response;
            }
            else
                result.ErrorMessage = client.error.Message;

            return result;
        }

        public async Task<DataRequestResponse<List<GroupTileDto>>> GetGroups(int page, int pageSize, string filter)
        {
            var result = new DataRequestResponse<List<GroupTileDto>>();

            var response = await client.GetAsync<List<GroupTileDto>>($"Groups/Get?page={page}&pageSize={pageSize}&filter={filter}", new CancellationToken());

            if (response != null)
            {
                result.IsSuccessful = true;
                result.Data = response;
            }
            else
                result.ErrorMessage = client.error.Message;

            return result;
        }

        public async Task<RequestResponse> Create(string name, string description, bool isPrivate)
        {
            var result = new RequestResponse();

            var response = await client.PostAsync($"Groups/Create", new { name, description, isPrivate }, new CancellationToken());

            if (response)
                result.IsSuccessful = true;
            else
                result.ErrorMessage = client.error.Message;

            return result;
        }

        public async Task<RequestResponse> AskForInvite(int id)
        {
            var result = new RequestResponse();

            var response = await client.PostAsync($"Groups/AskForInvite", new { id }, new CancellationToken());

            if (response)
                result.IsSuccessful = true;
            else
                result.ErrorMessage = client.error.Message;

            return result;
        }


        public async Task<RequestResponse> ResolveGroupInvite(int id, bool value)
        {
            var result = new RequestResponse();

            var response = await client.PostAsync($"Groups/ResolveGroupInvite", new { id, value }, new CancellationToken());

            if (response)
                result.IsSuccessful = true;
            else
                result.ErrorMessage = client.error.Message;

            return result;
        }
    }
}