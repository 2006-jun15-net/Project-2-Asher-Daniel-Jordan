using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project2.Data.Repository;
using Project2.Domain.Interface;
using Project2.Domain.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorRepository drepo;

        public DoctorsController(IDoctorRepository doctorRepository)
        {
            drepo = doctorRepository;
        }

        // GET: api/Doctors
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var Doctors = await drepo.GetDoctorsAsync();
            return Ok(Doctors);
        }

        // GET api/Doctors/5
        [HttpGet("{id}")]
        public async Task<Doctor> Get(int id)
        {
            var doctor = await drepo.GetDoctorAsync(id);
            return doctor;
        }

        // POST api/Doctors
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/Doctors/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/Doctors/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
