using ENote.Shared;
using ENotes.Repository.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace ENotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TestController : ControllerBase
    {
        private readonly ITestRepository testRepository;

        public TestController(ITestRepository testRepository)
        {
            this.testRepository = testRepository;
        }
        [HttpPost]
        public async Task<ActionResult> Add(TestDTO dto)
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            if (claim != null)
            {
                string userid = claim.Value;
                string testid = await testRepository.AddTest(dto, userid);
                return CreatedAtAction(nameof(GetTestByIdAsync), new { id = testid }, dto);
            }
            else
            {
                return Unauthorized();
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(string id)
        {
            bool result = await testRepository.DeleteTest(id);
            if (result)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        public async Task<ActionResult<List<TestDTO>>> Get()
        {

            List<TestDTO> results = await testRepository.GetTest();
            return Ok(results);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TestDTO>> GetTestByIdAsync(string id)
        {
            var result = await testRepository.GetTestById(id);
            if (result is not null)
                return Ok(result);
            else
                return NotFound();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(string id, TestDTO testdto)
        {
            if (id != testdto.Id)
            {
                return BadRequest();
            }
            bool result = await testRepository.UpdateTest(testdto);
            if (result)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }

        }
    }
}
