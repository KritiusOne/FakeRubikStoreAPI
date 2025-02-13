using Aplication.CustomEntities.ExternalsClass;
using Aplication.Exceptions;
using Aplication.Interfaces;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Aplication.Services
{
    public class FactusServices : IFactusServices
    {
        private HttpClient Client;
        public string URL { get; set; } = null!;
        public FactusServices()
        {
            Client = new HttpClient();
        }
        public async Task<string> BillCreate(string token, string jsonBody, string typeToken)
        {
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(typeToken, token);
            Console.WriteLine(jsonBody); 
            try
            {
                var res = await Client.PostAsync(URL, content);
                if (res.IsSuccessStatusCode)
                {
                    return await res.Content.ReadAsStringAsync();
                }
                var contentRes = await res.Content.ReadAsStringAsync();
                Console.WriteLine(res.StatusCode.ToString());
                Console.WriteLine("%--$&$--%");
                Console.WriteLine("%--$&$--%");
                Console.WriteLine("%--$&$--%");
                Console.WriteLine("%--$&$--%");
                Console.WriteLine("%--$&$--%");
                Console.WriteLine("%--$&$--%");
                Console.WriteLine("%--$&$--%");
                Console.WriteLine(contentRes);
                throw new Exception($"Error on data fetching");
            }
            catch(Exception e)
            {
                throw new Exception("Error when We did try to create the bill", e);
            }
        }

        public async Task<string> OAuth(string username, string password, string clientId, string ClientSecret)
        {
            string[] arrNameContents = new string[]
            {
                "grant_type", "client_id", "client_secret", "username", "password"
            };
            string[] arrContents = new string[]
            {
                "password", clientId, ClientSecret, username, password
            };
            var collection = new MultipartFormDataContent();
            for (int i = 0; i < arrNameContents.Length; i++)
            {
                HttpContent content = new StringContent(arrContents[i]);
                collection.Add(content, arrNameContents[i]);
            }
            var request = new HttpRequestMessage(HttpMethod.Post, URL)
            {
                Content = collection
            };
            try
            {
                var response = await Client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var finalRes = JsonSerializer.Deserialize<FactusResponseOAuth>(jsonResponse);
                    return finalRes.access_token;
                }
                else
                {
                    throw new BaseException($"Error en la solcitud: {response.StatusCode}");
                }
            }
            catch(Exception e)
            {
                throw new BaseException($"Error: {e.Message}");
            }
        }

        public Task<string> Refresh(string username, string password, string clientId, string ClientSecret)
        {
            throw new NotImplementedException();
        }

        public void SetURL(string URL)
        {
            this.URL = URL;
        }
    }
}
