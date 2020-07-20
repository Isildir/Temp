using EngineerProject.Mobile.Utility;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EngineerProject.Mobile.Services
{
    public static class UserService
    {
        private static readonly Uri url = new Uri($"{ConfigData.Url}users/");

        public static async Task<LoginResponse> Login(string userName, string password)
        {
            var values = new Credentials
            {
                Login = userName,
                Password = password
            };

            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(values));

            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            HttpClientHandler httpClientHandler = new HttpClientHandler()
            {
                Proxy = new WebProxy("http://localhost:3000/")
            };

            var client = new HttpClient()
            {
                BaseAddress = url
            };

            var result = new LoginResponse();

            try
            {
                var response = await client.PostAsync($"Authenticate/", content);
                var responseString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var data = await Task.Run(() => JsonConvert.DeserializeObject<SuccessfulLogin>(responseString));

                    result.IsSuccessful = true;
                    result.Token = data.Token;
                }
                else
                {
                    var data = await Task.Run(() => JsonConvert.DeserializeObject<RequestError>(responseString));

                    result.ErrorMessage = data.Message;
                }
            }
            catch (Exception e)
            {
                result.ErrorMessage = "Exception";
            }

            return result;
        }

        public static async Task<RegisterResponse> Register(string userName, string password)
        {
            var values = new Credentials
            {
                Login = userName,
                Password = password
            };

            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(values));

            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            HttpClientHandler httpClientHandler = new HttpClientHandler()
            {
                Proxy = new WebProxy("http://localhost:3000/")
            };

            var client = new HttpClient()
            {
                BaseAddress = url
            };

            var result = new RegisterResponse();

            try
            {
                var response = await client.PostAsync($"Register/", content);
                var responseString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var data = await Task.Run(() => JsonConvert.DeserializeObject<RequestError>(responseString));

                    result.ErrorMessage = data.Message;
                }
                else
                    result.IsSuccessful = true;
            }
            catch (Exception e)
            {
                result.ErrorMessage = "Exception";
            }

            return result;
        }

        private class Credentials
        {
            [JsonProperty("login")]
            public string Login { get; set; }

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

    public class LoginResponse : RegisterResponse
    {
        public string Token { get; set; }
    }

    public class RegisterResponse
    {
        public string ErrorMessage { get; set; }

        public bool IsSuccessful { get; set; }
    }

    public class RequestError
    {
        public string Message { get; set; }
    }
}