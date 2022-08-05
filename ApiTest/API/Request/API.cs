using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiTest.API.Request
{
    class API
    {
        private HttpClient restClient = new HttpClient();
        private string URI = "https://restful-booker.herokuapp.com";

        public async Task<string> testRequest()
        {
            var Builder = new System.UriBuilder($"{URI}/auth");

            var response = await restClient.GetAsync(Builder.Uri);
            var context = await response.Content.ReadAsStringAsync();
            return context;
        }
    }
}
