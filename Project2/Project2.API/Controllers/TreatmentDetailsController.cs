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
    /// <summary>
    /// Controller Class responsible for Treatment Details resource
    /// </summary>
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
            var result = await tdetailsRepo.GetByIdAsync(id);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        /// <summary>
        /// Gets treatment details based on patient
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns>
        /// An array of treatment details related to a specific patient
        /// </returns>

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
        /// <summary>
        /// Gets treatment details based on patient
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns>
        /// A singular treatment details object related to a specific patient
        /// </returns>

        [HttpGet("GetSinglePatientsTreatment/{patientId}")]
        public async Task<IActionResult> GetSinglePatientsTreatment(int patientId)
        {
            var result = await tdetailsRepo.GetSinglePatientTreatment(patientId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        /// <summary>
        /// Gets treatment details based on doctor
        /// </summary>
        /// <param name="doctorId"></param>
        /// <returns>
        /// A single treatment details object related to a specific Doctor
        /// </returns>

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
            if(tdetailsRepo.GetAllAsync().Result.Any(td => td.TreatmentDetailsId == treatmentDetails.TreatmentDetailsId))
            {
                return Conflict();
            }

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

            var existingDetail = await tdetailsRepo.GetByIdAsync(treatmentDetails.TreatmentDetailsId);
            if(existingDetail != null)
            {
                await tdetailsRepo.UpdateAsync(treatmentDetails);
            }
            else
            {
                return NotFound();
            }
            

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
