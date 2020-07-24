﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> GetDoctors()
        {
            var Doctors =  await drepo.GetDoctorsAsync();
            return Ok(Doctors);
        }

        // GET api/Doctors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctorById(int id)
        {
            var doctor = await drepo.GetDoctorAsync(id);
            return Ok(doctor);
        }

        // POST api/Doctors
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PostDoctor([FromBody] Doctor doctor)
        {
            if( drepo.GetDoctorsAsync().Result.Any(d => d.DoctorId == doctor.DoctorId))
            {
                return Conflict();
            }

            await drepo.CreateDoctorAsync(doctor);

            return CreatedAtAction(
                actionName: nameof(GetDoctorById),
                routeValues: new { id = doctor.DoctorId },
                value: doctor);
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
