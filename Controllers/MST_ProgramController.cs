using Microsoft.AspNetCore.Mvc;
using ProgramListWebAPI.Models;
using ProgramListWebAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProgramListWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MST_ProgramController : ControllerBase
    {
        private readonly IProgramListService programListService;

        // GET: api/<MST_ProgramController>

        public MST_ProgramController(IProgramListService programListService)    
        {
            this.programListService = programListService;
        }

        [HttpGet]
        public ActionResult<List<MST_Program>> Get()
        {
            return programListService.GET();
        }

        // GET api/<MST_ProgramController>/5
        [HttpGet("{id}")]
        public ActionResult<MST_Program> Get(string id)
        {
            var program = programListService.GET(id);

            if( program == null)
            {
                return NotFound($"Program with id = {id} not found");
            }

            return program;
        }
        // GET api/<MST_ProgramTopicController>/array
        [HttpGet("{id}/{topic_name}")]
        public ActionResult<List<MST_Program>> GetByName(string id, string topic_name)
        {
            return programListService.GETByName(id, topic_name);
        }

        // GET api/<MST_ProgramTopicController>/array
        [HttpGet("problemcount/{topic_nameForCount}")]
        public int GetCount(string topic_nameForCount)
        {
            return programListService.GETCOUNT(topic_nameForCount);
        }

        // POST api/<MST_ProgramController>
        [HttpPost]
        public ActionResult<MST_Program> Post([FromBody] MST_Program program)
        {
            programListService.POST(program);

            return CreatedAtAction( nameof(Get), new { id = program.ID } , program );
            
        }

        // PUT api/<MST_ProgramController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] MST_Program program)
        {
            var existingProgram = programListService.GET(id);

            if( existingProgram == null)
            {
                return NotFound($"Program with id = {id} not found");
            }
            
            programListService.PUT(id , program);

            return NoContent();

        }

        // DELETE api/<MST_ProgramController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var program = programListService.GET(id);

            if( program == null)
            {
                return NotFound($"Program with id = {id} not found");
            }

            programListService.DELETE(id);

            return Ok($"Student with id = {id} deleted");
        }
    }
}
