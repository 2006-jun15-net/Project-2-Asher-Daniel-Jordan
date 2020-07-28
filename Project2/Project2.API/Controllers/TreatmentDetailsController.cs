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

        // GET api/TreatmentDetails/5/2
        [HttpGet("{patientId}/{treatmentId}")]
        public async Task<IActionResult> Get(int patientId, int treatmentId)
        {
            return Ok(await tdetailsRepo.GetByIdAsync(patientId, treatmentId));
        }

        // POST api/<TreatmentDetailsController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] TreatmentDetails value)
        {
            if(tdetailsRepo.GetAllAsync().Result.Any(td => 
            td.TreatmentId == value.TreatmentId && td.PatientId == value.PatientId))
            {
                return Conflict();
            }

            await tdetailsRepo.CreateAsync(value);

            return CreatedAtAction(
                actionName: nameof(Get),
                value: value);
        }

        // PUT api/<TreatmentDetailsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TreatmentDetailsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
