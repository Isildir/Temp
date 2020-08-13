using EngineerProject.Mobile.Utility;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EngineerProject.Mobile.Services
{
    public class HttpService
    {
        internal RequestError error;

        private readonly HttpClient httpClient;

        public HttpService()
        {
            httpClient = new HttpClient() { BaseAddress = new Uri(ApplicationConfigurationData.Url) };

            if (!string.IsNullOrEmpty(App.Token))
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.Token);
        }

        public async Task<ResponseType> DeleteAsync<ResponseType>(string url, CancellationToken cancellationToken) where ResponseType : class
        {
            try
            {
                var response = await httpClient.DeleteAsync(url, cancellationToken);

                var responseData = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<ResponseType>(responseData);
                else
                    error = JsonConvert.DeserializeObject<RequestError>(responseData);
            }
            catch (Exception e)
            {
                error = new RequestError { Message = e.Message };
            }

            return null;
        }

        public async Task<ResponseType> GetAsync<ResponseType>(string url, CancellationToken cancellationToken) where ResponseType : class
        {
            try
            {
                var response = await httpClient.GetAsync(url, cancellationToken);

                var responseData = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<ResponseType>(responseData);
                else
                    error = JsonConvert.DeserializeObject<RequestError>(responseData);
            }
            catch (Exception e)
            {
                error = new RequestError { Message = e.Message };
            }

            return null;
        }

        public async Task<ResponseType> PutAsync<ResponseType>(string url, object data, CancellationToken cancellationToken) where ResponseType : class
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
                    error = JsonConvert.DeserializeObject<RequestError>(responseData);
            }
            catch (Exception e)
            {
                error = new RequestError { Message = e.Message };
            }

            return null;
        }

        public async Task<bool> PostAsync(string url, object data, CancellationToken cancellationToken)
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
                    error = JsonConvert.DeserializeObject<RequestError>(responseData);
            }
            catch (Exception e)
            {
                error = new RequestError { Message = e.Message };
            }

            return false;
        }

        public async Task<ResponseType> PostAsync<ResponseType>(string url, object data, CancellationToken cancellationToken) where ResponseType : class
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
                    error = JsonConvert.DeserializeObject<RequestError>(responseData);
            }
            catch (Exception e)
            {
                error = new RequestError { Message = e.Message };
            }

            return null;
        }
    }
}