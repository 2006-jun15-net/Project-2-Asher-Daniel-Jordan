using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project2.Data.Repository;
using Project2.Domain.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NursesController : ControllerBase
    {
        private readonly INurseRepository nRepo;

        public NursesController(INurseRepository nurseRepository)
        {
            nRepo = nurseRepository;

        }
        // GET: api/Nurses
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var nurses = await nRepo.GetNursesAsync();
            return Ok(nurses);
        }

        // GET api/Nurses/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/Nurses
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/Nurses/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/Nurses/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
