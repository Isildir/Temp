using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using EngineerProject.Mobile.Views;
using EngineerProject.Mobile.Utility;

namespace EngineerProject.Mobile.Services
{
    public static class CharacterService
    {
        private static readonly Uri url = new Uri($"{ConfigData.Url}characters/");
        /*
        public static async Task<List<Character>> GetCharactersList()
        {
            HttpClientHandler httpClientHandler = new HttpClientHandler()
            {
                Proxy = new WebProxy("http://localhost:3000/")
            };

            var client = new HttpClient()
            {
                BaseAddress = url
            };

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.Token);

            try
            {
                client.Timeout = TimeSpan.FromSeconds(10);
                var response = await client.GetAsync($"GetUserCharacters/");
                var responseString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var data = await Task.Run(() => JsonConvert.DeserializeObject<List<Character>>(responseString));

                    return data;
                }
            }
            catch (Exception e)
            {
            }

            return null;
        }

        public static async Task<List<CharacterCreationDto>> GetRaces()
        {
            HttpClientHandler httpClientHandler = new HttpClientHandler()
            {
                Proxy = new WebProxy("http://localhost:3000/")
            };

            var client = new HttpClient()
            {
                BaseAddress = url
            };

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.Token);

            try
            {
                client.Timeout = TimeSpan.FromSeconds(10);
                var response = await client.GetAsync($"characterCreator/GetNewCharacterData/");
                var responseString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var data = await Task.Run(() => JsonConvert.DeserializeObject<List<CharacterCreationDto>>(responseString));

                    return data;
                }
            }
            catch (Exception e)
            {
            }

            return null;
        }



        public static async Task<CharacterCreationProfessionDto> GetProfessionData(int id)
        {
            HttpClientHandler httpClientHandler = new HttpClientHandler()
            {
                Proxy = new WebProxy("http://localhost:3000/")
            };

            var client = new HttpClient()
            {
                BaseAddress = url
            };

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.Token);

            try
            {
                client.Timeout = TimeSpan.FromSeconds(10);
                var response = await client.GetAsync($"characterCreator/GetProfessionData?id={id}");
                var responseString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var data = await Task.Run(() => JsonConvert.DeserializeObject<CharacterCreationProfessionDto>(responseString));

                    return data;
                }
            }
            catch (Exception e)
            {
            }

            return null;
        }


        public static async Task<List<ShortProfessionDto>> GetAvailableProfessions(Race race)
        {
            HttpClientHandler httpClientHandler = new HttpClientHandler()
            {
                Proxy = new WebProxy("http://localhost:3000/")
            };

            var client = new HttpClient()
            {
                BaseAddress = url
            };

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.Token);

            try
            {
                client.Timeout = TimeSpan.FromSeconds(10);
                var response = await client.GetAsync($"characterCreator/GetAvailableProfessions?race={(byte)race}");
                var responseString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var data = await Task.Run(() => JsonConvert.DeserializeObject<List<ShortProfessionDto>>(responseString));

                    return data;
                }
            }
            catch (Exception e)
            {
            }

            return null;
        }*/
    }
}