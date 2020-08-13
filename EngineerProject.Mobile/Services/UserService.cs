using EngineerProject.Commons.Dtos;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace EngineerProject.Mobile.Services
{
    public class UserService : BaseService
    {
        private class LoginRequestResponse
        {
            public string Token { get; set; }
        }

        public async Task<DataRequestResponse<string>> Login(string userName, string password)
        {
            var result = new DataRequestResponse<string>();

            var response = await client.PostAsync<LoginRequestResponse>($"Users/Authenticate", new { Identifier = userName, Password = password }, new CancellationToken());

            if (response != null)
            {
                result.IsSuccessful = true;
                result.Data = response.Token;
            }
            else
                result.ErrorMessage = client.error.Message;

            return result;
        }

        public async Task<RequestResponse> Register(string login, string email, string password)
        {
            var result = new RequestResponse();

            var response = await client.PostAsync($"Users/Register", new { login, email, password }, new CancellationToken());

            if (response)
                result.IsSuccessful = true;
            else
                result.ErrorMessage = client.error.Message;

            return result;
        }

        public async Task<DataRequestResponse<UserProfileDto>> GetProfile()
        {
            var result = new DataRequestResponse<UserProfileDto>();

            var response = await client.GetAsync<UserProfileDto>($"Users/GetProfile", new CancellationToken());

            if (response != null)
                result.IsSuccessful = true;
            else
                result.ErrorMessage = client.error.Message;

            return result;
        }

        public async Task<RequestResponse> ChangeNotificationSettings()
        {
            var result = new RequestResponse();

            var response = await client.PostAsync($"Users/ChangeNotificationSettings", null, new CancellationToken());

            if (response)
                result.IsSuccessful = true;
            else
                result.ErrorMessage = client.error.Message;

            return result;
        }

        public async Task<RequestResponse> ChangePassword(string oldPassword, string password)
        {
            var result = new RequestResponse();

            var response = await client.PostAsync($"Users/ChangePassword", new { oldPassword, password }, new CancellationToken());

            if (response)
                result.IsSuccessful = true;
            else
                result.ErrorMessage = client.error.Message;

            return result;
        }

        public async Task<RequestResponse> SendPasswordRecovery(string identifier)
        {
            var result = new RequestResponse();

            var response = await client.PostAsync($"Users/SendPasswordRecovery", identifier, new CancellationToken());

            if (response)
                result.IsSuccessful = true;
            else
                result.ErrorMessage = client.error.Message;

            return result;
        }
    }
}