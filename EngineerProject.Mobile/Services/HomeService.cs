using EngineerProject.Commons.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EngineerProject.Mobile.Services
{
    public class HomeService : BaseService
    {
        public async Task<RequestResponse> AskForInvite(int id)
        {
            var response = await client.PostAsync($"Groups/AskForInvite", new { id });

            return MapResponse(response);
        }

        public async Task<RequestResponse> Create(string name, string description, bool isPrivate)
        {
            var response = await client.PostAsync($"Groups/Create", new { name, description, isPrivate });

            return MapResponse(response);
        }

        public async Task<DataRequestResponse<List<GroupTileDto>>> GetGroups(int page, int pageSize, string filter)
        {
            var response = await client.GetAsync<List<GroupTileDto>>($"Groups/Get?page={page}&pageSize={pageSize}&filter={filter}");

            return MapResponse(response);
        }

        public async Task<DataRequestResponse<UserGroupsWrapperDto>> GetUserGroups()
        {
            var response = await client.GetAsync<UserGroupsWrapperDto>($"Groups/GetUserGroups");

            return MapResponse(response);
        }

        public async Task<RequestResponse> ResolveGroupInvite(int id, bool value)
        {
            var response = await client.PostAsync($"Groups/ResolveGroupInvite", new { id, value });

            return MapResponse(response);
        }
    }
}