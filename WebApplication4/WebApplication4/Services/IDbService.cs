

using Tutorial5.Models;
using WebApplication4.Controller;
using WebApplication4.DTOs;

namespace Tutorial5.Services;

public interface IDbService
{ 
    Task AddNewPrescriptionAsync(int doctorId, PrescriptionDTO prescriptionDto);
    Task<GetDTO.PatientWithPrescriptionsDTO>  GetAllPatientAsync(int patientId);
    
}