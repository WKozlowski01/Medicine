using Microsoft.AspNetCore.Mvc;
using Tutorial5.Services;
using WebApplication4.DTOs;
using WebApplication4.Exceptions;

namespace WebApplication4.Controller;

[Route("/api")]
[ApiController]
public class PrescriptionController:ControllerBase
{
   
   
        private readonly IDbService _dbService;

        public PrescriptionController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpPost("/recepta/{doctorId}")]
        public async Task<IActionResult> AddNewPrescription(int doctorId,PrescriptionDTO prescriptionDto)
        {

            try
            {
                await this._dbService.AddNewPrescriptionAsync(doctorId, prescriptionDto);
                return NoContent();
            }
            catch (DateException e)
            {
                return BadRequest(e.Message);
            }
            catch (MedicamentNotFoundException)
            {
                return NotFound();
            }
            catch (ToManyMedicamentsExists)
            {
                return Conflict();
            }



        }


        [HttpGet("patient/{id}")]
        public async Task<IActionResult> GetPrescription(int id)
        {
            var patient = await _dbService.GetAllPatientAsync(id);
    
            if (patient == null)
            {
                return NotFound("Pacjent o id: "+id+" nie istnieje");
            }

            return Ok(patient);
        }
}

    
    


