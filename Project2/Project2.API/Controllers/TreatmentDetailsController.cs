using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project2.Domain.Interface;
using Project2.Domain.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreatmentDetailsController : ControllerBase
    {
        private readonly ITreatmentDetailsRepository tdetailsRepo;

        public TreatmentDetailsController(ITreatmentDetailsRepository treatmentDetailsRepository)
        {
            tdetailsRepo = treatmentDetailsRepository;
        }
        // GET: api/<TreatmentDetailsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var treatmentDetails = await tdetailsRepo.GetAllAsync();
            return Ok(treatmentDetails);
        }

        // GET api/TreatmentDetails/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTreatmentDetail(int id)
        {
            return Ok(await tdetailsRepo.GetByIdAsync(id));
        }

        // GET
        [HttpGet("GetPatientsTreatment/{patientId}")]
        public async Task<IActionResult> GetPatientsTreatment(int patientId)
        {
            var result = await tdetailsRepo.GetPatientTreatment(patientId);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        //GET api/TreatmentDetails/Doctor/5
        [HttpGet("Doctor/{doctorId}")]

        public async Task<IActionResult> GetTreatmentDetailsByDoctor(int doctorId)
        {
            var result = await tdetailsRepo.GetByDoctorAsync(doctorId);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);

        }

        // POST api/<TreatmentDetailsController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] TreatmentDetails treatmentDetails)
        {
            await tdetailsRepo.CreateAsync(treatmentDetails);

            return CreatedAtAction(
                actionName: nameof(Get),
                routeValues: new {id = treatmentDetails.TreatmentDetailsId},
                value: treatmentDetails);
        }

        // PUT api/<TreatmentDetailsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TreatmentDetails treatmentDetails)
        {
            if(id != treatmentDetails.TreatmentDetailsId)
            {
                return BadRequest();
            }

            await tdetailsRepo.UpdateAsync(treatmentDetails);

            return NoContent();
        }

        // DELETE api/<TreatmentDetailsController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id)
        {
            var existingTreatmentDetails = await tdetailsRepo.GetByIdAsync(id);

            if (existingTreatmentDetails != null)
            {
                await tdetailsRepo.DeleteAsync(existingTreatmentDetails);
            }
            else
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
