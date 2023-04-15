using Microsoft.AspNetCore.Mvc;
using ProgramListWebAPI.Models;
using ProgramListWebAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProgramListWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SEC_UserController : ControllerBase
    {
        private readonly IUserService userSerice;

        public SEC_UserController(IUserService userService)
        {
            this.userSerice = userService;
        }
        // GET: api/<SEC_UserController>
        [HttpGet]
        public ActionResult<List<SEC_User>> Get()
        {
            return userSerice.GET();
        }

        // GET api/<SEC_UserController>/5
        [HttpGet("{id}")]
        public ActionResult<SEC_User> Get(string id)
        {
            return userSerice.GET(id);
        }

        // POST api/<SEC_UserController>
        [HttpPost]
        public ActionResult<SEC_User> Post([FromBody] SEC_User user)
        {
            userSerice.POST(user);
            return CreatedAtAction(nameof(Get), new { id = user.ID }, user);
        }

        // PUT api/<SEC_UserController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] SEC_User user)
        {
            var existingUser = userSerice.GET(id);

            if (existingUser == null)
            {
                return NotFound($"Program with id = {id} not found");
            }

            userSerice.PUT(id, user);

            return NoContent();
        }

        // DELETE api/<SEC_UserController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var user = userSerice.GET(id);

            if (user == null)
            {
                return NotFound($"Program with id = {id} not found");
            }

            userSerice.DELETE(id);

            return Ok($"Student with id = {id} deleted");
        }
    }
}
