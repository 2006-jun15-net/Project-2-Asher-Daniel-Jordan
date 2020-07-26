using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project2.Domain.Interface;
using Project2.Domain.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreatmentsController : ControllerBase
    {
        private readonly ITreatmentRepository tRepo;
        private readonly ILogger<TreatmentsController> _logger;

        public TreatmentsController(ITreatmentRepository treatmentRepository, ILogger<TreatmentsController> logger)
        {
            tRepo = treatmentRepository;
            _logger = logger ?? throw new ArgumentException(nameof(logger));
        }

        // GET: api/Treatments
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var treatments = await tRepo.GetTreatmentsAsync();
            return Ok(treatments);
        }


        // GET: api/Treatments/GetByDoctor/2
        [HttpGet]
        [Route("/GetByDoctor/{id}")]
        public async Task<IActionResult> GetTreatments(int id)
        {
            var treatments = await tRepo.GetDoctorTreatmentsAsync(id);
            return Ok(treatments);
        }

        // GET: api/Treatments/GetByIllness/3
        [HttpGet]
        [Route("/GetByIllness/{id}")]
        public async Task<IActionResult> GetIllnessTreatments(int id)
        {
            _logger.LogInformation("Entered getIllnessTreatments method");
            var treatments = await tRepo.TreatmentsByIlllnessAsync(id);
            return Ok(treatments);
        }

        // GET api/Treatments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Treatment>> Get(int id)
        {
            var treatment = await tRepo.GetTreatmentAsync(id);
           if(treatment is Treatment item)
           {
                return item;
           }

            return NotFound();
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
