using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project2.Domain.Interface;
using Project2.Domain.Model;
using Project2.Domain.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
//change made

namespace Project2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientRepository pRepo;

        private readonly PatientService patientService;

        public PatientsController(IPatientRepository patientRepository, PatientService pService)
        {
            pRepo = patientRepository;

            patientService = pService;
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
        public async Task <IActionResult> Get(int id)
        {
            var result = await pRepo.GetByIdAsync(id);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("Nurses/{nurseId}")]

        public async Task<IActionResult> GetByNurse(int nurseId)
        {
            var results = await pRepo.GetByNurseAsync(nurseId);
            if(results == null)
            {
                return NotFound();
            }

            return Ok(results);
        }

        [HttpGet("Doctors/{id}")]

        public async Task<IActionResult> GetByDoctor(int doctorId)
        {
            var results = await pRepo.GetByDoctorAsync(doctorId);
            if(results == null)
            {
                return NotFound();
            }
            return Ok(results);
        }

        [HttpGet("PatientRoom/{id}")]
        public async Task<IActionResult> GetByPatientRoom(int id)
        {
            var patient = await pRepo.GetByPatientRoom(id);
            if(patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }

        [HttpPut("AssignPatient/{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<IActionResult> AssignPatientRoom(int id)
        {

            var existingPatient = await pRepo.GetByIdAsync(id);
            if (existingPatient != null)
            {
                await patientService.AssignPatientToRoomAsync(existingPatient);
            }
            else
            {
                return NotFound();
            }

            return Ok();
        }

        // POST api/Patients
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] Patient patient)
        {
            if(pRepo.GetPatientsAsync().Result.Any(p => p.PatientId == patient.PatientId))
            {
                return Conflict();
            }

            await pRepo.CreateAsync(patient);

            return CreatedAtAction(
                actionName: nameof(Get),
                routeValues: new { id = patient.PatientId },
                value: patient);
        }

        // PUT api/Patients/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<IActionResult> Put(int id, [FromBody] Patient patient)
        {
            if(id != patient.PatientId)
            {
                return BadRequest();
            }

            var existingPatient = await pRepo.GetByIdAsync(id);
            if (existingPatient != null)
            {
                await pRepo.UpdateAsync(patient);
            }
            else
            {
                return NotFound();
            }

            return Ok();

        }

        // DELETE api/Patients/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id)
        {
            var existingPatient = await pRepo.GetByIdAsync(id);

            if (existingPatient != null)
            {
                await pRepo.DeletePatientAsync(existingPatient);
            }
            else
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
