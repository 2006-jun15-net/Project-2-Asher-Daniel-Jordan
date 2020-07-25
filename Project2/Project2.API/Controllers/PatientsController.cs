using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project2.Domain.Interface;
using Project2.Domain.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
//change made

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
        public async Task<IActionResult> Get()
        {
            var patients = await pRepo.GetPatientsAsync();
            return Ok(patients);
        }

        // GET api/Patients/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet("Nurses/{id}")]

        public async Task<IActionResult> GetByNurse(int nurseId)
        {

            return Ok( await pRepo.GetByNurseAsync(nurseId));

        }

        [HttpGet("Doctors/{id}")]

        public async Task<IActionResult> GetByDoctor(int doctorId)
        {

            return Ok(await pRepo.GetByDoctorAsync(doctorId));

        }

        // POST api/Patients
        [HttpPost("Patients")]
        public async Task<IActionResult> Post([FromBody] Patient patient)
        {
            var person = await pRepo.CreateAsync(patient);

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
