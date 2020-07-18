using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project2.Domain.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IllnessesController : ControllerBase
    {
        private readonly IIllnessRepository iRepo;

        public IllnessesController(IIllnessRepository illnessRepository)
        {
            iRepo = illnessRepository;
        }
        // GET: api/Illnesses
        [HttpGet]
        public IActionResult Get()
        {
            var illnesses = iRepo.GetAll();
            return Ok(illnesses);
        }

        // GET api/Ilnesses/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/Illnesses
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/Illnesses/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/Illnesses/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
