using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project2.Domain.Interface;
using Project2.Domain.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientRepository pRepo;

        public PatientsController(IPatientRepository patientRepository)
        {
            pRepo = patientRepository;
        }
        // GET: api/Patients
        [HttpGet]
        public IActionResult Get()
        {
            var patients = pRepo.GetAll();
            return Ok(patients);
        }

        // GET api/Patients/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet("Nurses/{id}")]

        public IActionResult GetByNurse(int nurseId)
        {
            return Ok(pRepo.GetByNurse(nurseId));
        }

        [HttpGet("Doctors/{id}")]

        public IActionResult GetByDoctor(int doctorId)
        {
            return Ok(pRepo.GetByDoctor(doctorId));
        }

        // POST api/Patients
        [HttpPost("Patients")]
        public IActionResult Post([FromBody] Patient patient)
        {
            var person = pRepo.Create(patient);

            return Ok(person);
        }

        // PUT api/Patients/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/Patients/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
