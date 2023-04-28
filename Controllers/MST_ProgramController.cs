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
        [HttpGet("programsByTopicName/{topic_name}")]
        public ActionResult<List<MST_Program>> GETProgramByTopicName(string topic_name)
        {
            return programListService.GETProgramByTopicName(topic_name);
        }

        // GET api/<MST_ProgramController>/
        [HttpGet("problemcount/")]
        public ActionResult<List<POG_ProgramCount>> GETCountOfProgramByTopicName()
        {
            return programListService.GETCountOfProgramByTopicName();
        }

        // GET api/<MST_ProgramController>/array
        [HttpGet("problemcount/{topic_name}")]
        public int GETCountByTopicName(string topic_name)
        {
            return programListService.GETCountByTopicName(topic_name);
        }
        
        // GET api/<MST_Program>/array/Easy
        [HttpGet("getByFilter/{program_Topic=}/{program_Difficulty=}")]
        public List<MST_Program> GETByFilter(string program_Topic = "all", string program_Difficulty = "all")
        {
            return programListService.GETByFilter(program_Topic, program_Difficulty);
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
