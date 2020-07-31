using System;
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
    /// <summary>
    /// The controller responsible for managine the Doctor Resource
    /// </summary>
    
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorRepository drepo;

        public DoctorsController(IDoctorRepository doctorRepository)
        {
            drepo = doctorRepository;
        }

        /// <summary>
        /// Gets All Doctors
        /// </summary>
        /// <returns>
        /// Doctors
        /// </returns>

        // GET: api/Doctors
        [HttpGet]
        public async Task<IActionResult> GetDoctors()
        {
            var Doctors =  await drepo.GetDoctorsAsync();
            return Ok(Doctors);
        }
        /// <summary>
        /// Gets a single doctor
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// A single doctor depending on the id
        /// </returns>
        // GET api/Doctors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctorById(int id)
        {
            var doctor = await drepo.GetDoctorAsync(id);
            if(doctor == null)
            {
                return NotFound();
            }

            return Ok(doctor);
        }
        /// <summary>
        /// Creates a new doctor resource
        /// </summary>
        /// <param name="doctor"></param>
        /// <returns>
        /// Status code: 201 Created
        /// </returns>

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
        /// <summary>
        /// Updates a specific doctor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="doctor"></param>
        /// <returns>
        /// Status code 200 Success
        /// </returns>

        // PUT api/Doctors/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<IActionResult> Put(int id, [FromBody] Doctor doctor)
        {
            if(id != doctor.DoctorId)
            {
                return BadRequest();
            }

            var existingDoctor = await drepo.GetDoctorAsync(id);

            if(existingDoctor != null)
            {
                await drepo.UpdateDoctorAsync(doctor);
            }
            else
            {
                return NotFound();
            }

            return Ok();

        }
        /// <summary>
        /// Deletes a doctor
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// Status code: 200 Success
        /// </returns>

        // DELETE api/Doctors/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id)
        {
            var existingDoctor = await drepo.GetDoctorAsync(id);

            if (existingDoctor != null)
            {
                await drepo.DeleteDoctorAsync(existingDoctor);
            }
            else
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
