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
    public class TreatmentsController : ControllerBase
    {
        private readonly ITreatmentRepository tRepo;

        public TreatmentsController(ITreatmentRepository treatmentRepository)
        {
            tRepo = treatmentRepository;
        }

        // GET: api/Treatments
        [HttpGet]
        public IActionResult Get()
        {
            var treatments = tRepo.GetAll();
            return Ok(treatments);
        }

        
        // GET: api/Treatments/GetByDoctor/2
        [HttpGet]
        [Route("/GetByDoctor/{id}")]
        public IActionResult GetTreatments(int id)
        {
            var treatments = tRepo.GetAllByDoctor(id);
            return Ok(treatments);
        }
        
        // GET api/Treatments/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/Treatments
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
        
        // PUT api/Treatments/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/Treatments/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
