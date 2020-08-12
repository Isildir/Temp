using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace EngineerProject.Mobile.Services
{
    public static class UserService
    {
        public static async Task<LoginResponse> Login(string userName, string password)
        {
            var data = new Credentials
            {
                Identifier = userName,
                Password = password
            };

            var client = new HttpService();
            var result = new LoginResponse();

            var response = await client.PostAsync<SuccessfulLogin>($"Users/Authenticate", data, new CancellationToken());

            if (response != null)
            {
                result.IsSuccessful = true;
                result.Token = response.Token;
            }
            else
                result.ErrorMessage = client.error.Message;

            return result;
        }

        public static async Task<RegisterResponse> Register(string userName, string password)
        {
            var data = new Credentials
            {
                Identifier = userName,
                Password = password
            };

            var client = new HttpService();
            var result = new RegisterResponse();

            var response = await client.PostAsync<SuccessfulLogin>($"Users/Register", data, new CancellationToken());

            if (response != null)
                result.IsSuccessful = true;
            else
                result.ErrorMessage = client.error.Message;

            return result;
        }

        private class Credentials
        {
            [JsonProperty("identifier")]
            public string Identifier { get; set; }

            [JsonProperty("password")]
            public string Password { get; set; }
        }

        private class SuccessfulLogin
        {
            public int Id { get; set; }

            public string Login { get; set; }

            public string Token { get; set; }
        }
    }
}