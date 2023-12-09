using ENote.Shared;

namespace ENotes.Client.Services.Contracts
{
    public interface ITestService
    {
        public bool OnClient();
        public Task<List<TestDTO>> GetTests();
        public Task<string> AddTest(TestDTO test);
        public Task<bool> UpdateTest(TestDTO test);
        public Task<bool> DeleteTest(string id);
    }
}
