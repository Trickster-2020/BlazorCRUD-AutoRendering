using ENote.Shared;
using ENotes.Client.Services.Contracts;
using System.Net.Http.Json;

namespace ENotes.Client.Services
{
    public class TestService : ITestService
    {
        private readonly HttpClient httpClient;

        public TestService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<string> AddTest(TestDTO test)
        {
            var result = await httpClient.PostAsJsonAsync("api/test", test);
            if(result.StatusCode == System.Net.HttpStatusCode.Created) 
            {
                Uri u = result.Headers.Location;
                string id = u.Segments.Last();
                return id;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeleteTest(string id)
        {
            var result = await httpClient.DeleteAsync("api/test/" + id);
            if(result.IsSuccessStatusCode)
            {
                return true;
            }
            else { return false; }
        }

        public async Task<List<TestDTO>> GetTests()
        {
            return await httpClient.GetFromJsonAsync<List<TestDTO>>("api/test");
        }

        public bool OnClient()
        {
            return true;
        }

        public async Task<bool> UpdateTest(TestDTO test)
        {
            var result = await httpClient.PutAsJsonAsync("api/test" + test.Id,test);
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
