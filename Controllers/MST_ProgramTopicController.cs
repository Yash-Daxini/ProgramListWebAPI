
using Microsoft.AspNetCore.Mvc;
using ProgramListWebAPI.Models;
using ProgramListWebAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProgramListWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MST_ProgramTopicController : ControllerBase
    {
        private readonly ITopicListService topicListService;

        public MST_ProgramTopicController(ITopicListService topicListService)
        {
            this.topicListService = topicListService;
        }
        // GET: api/<MST_ProgramTopicController>
        [HttpGet]
        public ActionResult<List<MST_ProgramTopic>> Get()
        {
            return topicListService.GET();
        }

        // GET api/<MST_ProgramTopicController>/5
        [HttpGet("{id}")]
        public ActionResult<MST_ProgramTopic> Get(string id)
        {
            var topic = topicListService.GET(id);

            if (topic == null)
            {
                return NotFound($"topic with id = {id} not found");
            }

            return topic;
        }

        // POST api/<MST_ProgramTopicController>
        [HttpPost]
        public ActionResult<MST_ProgramTopic> Post([FromBody] MST_ProgramTopic topic)
        {
            topicListService.POST(topic);

            return CreatedAtAction(nameof(Get), new { id = topic.ID }, topic);
        }

        // PUT api/<MST_ProgramTopicController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] MST_ProgramTopic topic)
        {
            var existingProgram = topicListService.GET(id);

            if (existingProgram == null)
            {
                return NotFound($"Program with id = {id} not found");
            }

            topicListService.PUT(id, topic);

            return NoContent();
        }

        // DELETE api/<MST_ProgramTopicController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var program = topicListService.GET(id);

            if (program == null)
            {
                return NotFound($"Program with id = {id} not found");
            }

            topicListService.DELETE(id);

            return Ok($"Student with id = {id} deleted");
        }
    }
}
