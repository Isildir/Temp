using EngineerProject.Mobile.Utility;
using EngineerProject.Mobile.Views.Home;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EngineerProject.Mobile.Services
{
    public class HttpService
    {
        internal CancellationToken cancellationToken;
        internal RequestError error;
        private readonly HttpClient httpClient;

        public HttpService()
        {
            cancellationToken = new CancellationToken();
            httpClient = new HttpClient() { BaseAddress = new Uri(ConfigurationData.Url) };

            if (!string.IsNullOrEmpty(ConfigurationData.Token))
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ConfigurationData.Token);
        }

        public async Task<bool> DeleteAsync(string url)
        {
            try
            {
                var response = await httpClient.DeleteAsync(url, cancellationToken);

                var responseData = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                    return true;
                else
                    await HandleErrorMessage(response, responseData);
            }
            catch (Exception e)
            {
                error = new RequestError { Message = e.Message };
            }

            return false;
        }

        public async Task<ResponseType> GetAsync<ResponseType>(string url) where ResponseType : class
        {
            try
            {
                var response = await httpClient.GetAsync(url, cancellationToken);

                var responseData = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<ResponseType>(responseData);
                else
                    await HandleErrorMessage(response, responseData);
            }
            catch (Exception e)
            {
                error = new RequestError { Message = e.Message };
            }

            return null;
        }

        public async Task<bool> PostAsync(string url, object data)
        {
            var serializedData = JsonConvert.SerializeObject(data);

            var requestBody = new StringContent(serializedData, Encoding.UTF8, "application/json");

            try
            {
                var response = await httpClient.PostAsync(url, requestBody, cancellationToken);

                var responseData = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                    return true;
                else
                    await HandleErrorMessage(response, responseData);
            }
            catch (Exception e)
            {
                error = new RequestError { Message = e.Message };
            }

            return false;
        }

        public async Task<ResponseType> PostAsync<ResponseType>(string url, object data) where ResponseType : class
        {
            var serializedData = JsonConvert.SerializeObject(data);

            var requestBody = new StringContent(serializedData, Encoding.UTF8, "application/json");

            try
            {
                var response = await httpClient.PostAsync(url, requestBody, cancellationToken);

                var responseData = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<ResponseType>(responseData);
                else
                    await HandleErrorMessage(response, responseData);
            }
            catch (Exception e)
            {
                error = new RequestError { Message = e.Message };
            }

            return null;
        }

        public async Task<ResponseType> PutAsync<ResponseType>(string url, object data) where ResponseType : class
        {
            var serializedData = JsonConvert.SerializeObject(data);

            var requestBody = new StringContent(serializedData, Encoding.UTF8, "application/json");

            try
            {
                var response = await httpClient.PutAsync(url, requestBody, cancellationToken);

                var responseData = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<ResponseType>(responseData);
                else
                    await HandleErrorMessage(response, responseData);
            }
            catch (Exception e)
            {
                error = new RequestError { Message = e.Message };
            }

            return null;
        }

        public async Task<bool> PutAsync(string url, object data)
        {
            var serializedData = JsonConvert.SerializeObject(data);

            var requestBody = new StringContent(serializedData, Encoding.UTF8, "application/json");

            try
            {
                var response = await httpClient.PutAsync(url, requestBody, cancellationToken);

                var responseData = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                    return true;
                else
                    await HandleErrorMessage(response, responseData);
            }
            catch (Exception e)
            {
                error = new RequestError { Message = e.Message };
            }

            return false;
        }

        private async Task HandleErrorMessage(HttpResponseMessage responseMessage, string responseData)
        {
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                await NavigationHelpers.LogoutUser();

                return;
            }

            error = JsonConvert.DeserializeObject<RequestError>(responseData);
        }
    }
}