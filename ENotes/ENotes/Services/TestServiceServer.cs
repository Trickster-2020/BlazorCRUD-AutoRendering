using ENote.Shared;
using ENotes.Client.Services.Contracts;

namespace ENotes.Services
{
    public class TestServiceServer : ITestService
    {
        public Task<string> AddTest(TestDTO test)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteTest(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TestDTO>> GetTests()
        {
            throw new NotImplementedException();
        }

        public bool OnClient()
        {
            return false;
        }

        public Task<bool> UpdateTest(TestDTO test)
        {
            throw new NotImplementedException();
        }
    }
}
