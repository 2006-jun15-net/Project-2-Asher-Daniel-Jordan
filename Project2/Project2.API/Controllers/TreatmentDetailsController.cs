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

        // GET api/<TreatmentDetailsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TreatmentDetailsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
