using ENote.Shared;

namespace ENotes.Repository.Contracts
{
    public interface ITestRepository
    {
        public Task<List<TestDTO>> GetTest();
        public Task<TestDTO> GetTestById(string id);
        public Task<String> AddTest(TestDTO testDTO,string userid);
        public Task<bool> UpdateTest(TestDTO testDTO);
        public Task<bool> DeleteTest(string id);
    }
}
