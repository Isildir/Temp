using EngineerProject.Commons.Dtos;
using System.Threading.Tasks;

namespace EngineerProject.Mobile.Services
{
    public class UserService : BaseService
    {
        public async Task<RequestResponse> ChangeNotificationSettings()
        {
            var response = await client.PostAsync($"Users/ChangeNotificationSettings", null);

            return MapResponse(response);
        }

        public async Task<RequestResponse> ChangePassword(string oldPassword, string password)
        {
            var response = await client.PostAsync($"Users/ChangePassword", new { oldPassword, password });

            return MapResponse(response);
        }

        public async Task<DataRequestResponse<UserProfileDto>> GetProfile()
        {
            var response = await client.GetAsync<UserProfileDto>($"Users/GetProfile");

            return MapResponse(response);
        }

        public async Task<DataRequestResponse<string>> Login(string userName, string password)
        {
            var response = await client.PostAsync<LoginRequestResponse>($"Users/Authenticate", new { Identifier = userName, Password = password });

            return MapResponse(response.Token);
        }

        public async Task<RequestResponse> Register(string login, string email, string password)
        {
            var response = await client.PostAsync($"Users/Register", new { login, email, password });

            return MapResponse(response);
        }

        public async Task<RequestResponse> SendPasswordRecovery(string identifier)
        {
            var response = await client.PostAsync($"Users/SendPasswordRecovery", identifier);

            return MapResponse(response);
        }

        private class LoginRequestResponse
        {
            public string Token { get; set; }
        }
    }
}