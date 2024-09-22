using Microsoft.AspNetCore.Mvc;
using DrugsAPI_New.Services;
using DrugsAPI_New.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace DrugsAPI_New.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }


        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetAllDoctorsAsync()
        {
            var doctors = await _doctorService.GetAllDoctorsAsync();
            return Ok(doctors);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctor(int id)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(id);
            if (doctor == null)
                return NotFound();
            return Ok(doctor);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDoctor([FromBody] Doctor doctor)
        {
            var createdDoctor = await _doctorService.AddDoctorAsync(doctor);
            return CreatedAtAction(nameof(GetDoctor), new { id = createdDoctor.Id }, createdDoctor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDoctor(int id, [FromBody] Doctor doctor)
        {
            if (id != doctor.Id)
                return BadRequest();

            var updatedDoctor = await _doctorService.UpdateDoctorAsync(doctor.Id, doctor);
            if (updatedDoctor == null)
                return NotFound();

            return Ok(updatedDoctor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var result = await _doctorService.DeleteDoctorAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
