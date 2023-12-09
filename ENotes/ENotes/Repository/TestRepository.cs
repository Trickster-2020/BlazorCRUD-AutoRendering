using ENote.Shared;
using ENotes.Data;
using ENotes.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ENotes.Repository
{
    public class TestRepository : ITestRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public TestRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public async Task<string> AddTest(TestDTO test, string userid)
        {
            Test t = new Test();
            t.Id = Guid.NewGuid().ToString();
            t.CreatedDate = DateTime.Now;
            t.Title = test.Title;
            t.Description = test.Description;
            t.UserId = userid;
            applicationDbContext.Add(t);
            await applicationDbContext.SaveChangesAsync();
            return t.Id;
        }

        public async Task<bool> DeleteTest(string id)
        {
            var test = await applicationDbContext.Tests.FindAsync(id);
            if(test != null)
            {
                applicationDbContext.Tests.Remove(test);
                await applicationDbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<TestDTO>> GetTest()
        {
           List<TestDTO> results = new List<TestDTO>();

            List<Test> tests = await applicationDbContext.Tests.Include("User").ToListAsync();
            foreach(var test in tests)
            {
                TestDTO testDTO = new TestDTO();
                testDTO.Id = test.Id;
                testDTO.Title = test.Title;
                testDTO.Description = test.Description;
                testDTO.Author = test.User.Email!;
                testDTO.CreatedDate = test.CreatedDate;
                results.Add(testDTO);
            }
            return results;
        }

        public async Task<TestDTO> GetTestById(string id)
        {
            Test? test = await applicationDbContext.Tests.SingleOrDefaultAsync(p => p.Id == id); 
            if(test is not null)
            {
                TestDTO testDTO = new TestDTO();
                testDTO.Id = test.Id;
                testDTO.Title = test.Title;
                testDTO.Description = test.Description;
                testDTO.Author = test.User.Email!;
                testDTO.CreatedDate = test.CreatedDate;
                return testDTO;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> UpdateTest(TestDTO test)
        {
           var t = await applicationDbContext.Tests.Where(t => t.Id == test.Id).FirstOrDefaultAsync();
            if(t is not null)
            {
                t.Title = test.Title;
                t.Description = test.Description;
                await applicationDbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
