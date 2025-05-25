using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Tutorial5.Data;
using Tutorial5.Models;
using WebApplication4.Controller;
using WebApplication4.DTOs;
using WebApplication4.Exceptions;


namespace Tutorial5.Services;

public class DbService: IDbService
{
    private readonly DatabaseContext _context;
    public DbService(DatabaseContext context)
    {
        _context = context;
    }
    
   public async Task AddNewPrescriptionAsync(int doctorId, PrescriptionDTO prescriptionDto)
{
  
    if (prescriptionDto.DueDate < prescriptionDto.Date)
    {
        throw new DateException("Due date cannot be earlier than issue date.");
    }

  
    var existingPatient = await _context.Patients
        .FirstOrDefaultAsync(p => p.IdPatient == prescriptionDto.Patient.IdPatient);

    if (existingPatient == null)
    {
        var newPatient = new Patient
        {
            IdPatient = prescriptionDto.Patient.IdPatient,
            FirstName = prescriptionDto.Patient.FirstName,
            LastName = prescriptionDto.Patient.LastName,
            BirthDate = prescriptionDto.Patient.BirthDate
        };
        _context.Patients.Add(newPatient);
        await _context.SaveChangesAsync(); 
    }

 
    var medicamentIds = prescriptionDto.medicaments
        .Select(m => m.IdMedicament)
        .ToList();

    if (medicamentIds.Count > 10)
    {
        throw new ToManyMedicamentsExists("Too many medicaments in prescription.");
    }

    bool allMedicamentsExist = await _context.Medicaments
        .Where(m => medicamentIds.Contains(m.IdMedicament))
        .CountAsync() == medicamentIds.Count;

    if (!allMedicamentsExist)
    {
        throw new MedicamentNotFoundException("Some medicaments do not exist.");
    }

    var doctorExists = await _context.Doctors.AnyAsync(d => d.IdDoctor == doctorId);
    if (!doctorExists)
    {
        throw new Exception("Lekarz nie istnieje.");
    }

 
    var prescription = new Prescription
    {
        Date = prescriptionDto.Date,
        DueDate = prescriptionDto.DueDate,
        IdPatient = prescriptionDto.Patient.IdPatient,
        IdDoctor = doctorId
    };

    await _context.Prescriptions.AddAsync(prescription);
    await _context.SaveChangesAsync(); 


    foreach (var med in prescriptionDto.medicaments)
    {
        var pm = new Prescription_Medicament
        {
            IdPrescription = prescription.IdPrescription,
            IdMedicament = med.IdMedicament,
            Dose = med.Dose,
            Details = med.Description
        };
        _context.PrescriptionMedicaments.Add(pm);
    }

    await _context.SaveChangesAsync();
    
    
}

public async Task<GetDTO.PatientWithPrescriptionsDTO> GetAllPatientAsync(int patientId)
{
    var patient = await _context.Patients
        .Include(p => p.Prescriptions)
        .ThenInclude(pr => pr.Doctor)
        .Include(p => p.Prescriptions)
        .ThenInclude(pr => pr.medicaments)
        .ThenInclude(pm => pm.medicament)
        .FirstOrDefaultAsync(p => p.IdPatient == patientId);
    
    
    if (patient == null)
    {
        return null;
    }
    
    return new GetDTO.PatientWithPrescriptionsDTO
    {
        IdPatient = patient.IdPatient,
        FirstName = patient.FirstName,
        LastName = patient.LastName,
        BirthDate = patient.BirthDate,
        Prescriptions = patient.Prescriptions
            .OrderBy(p => p.DueDate)
            .Select(p => new GetDTO.PrescriptionDetailDTO
            {
                IdPrescription = p.IdPrescription,
                Date = p.Date,
                DueDate = p.DueDate,
                Doctor = new DoctorDTO
                {
                    IdDoctor = p.Doctor.IdDoctor,
                    FirstName = p.Doctor.FirstName,
                    LastName = p.Doctor.LastName,
                    Email = p.Doctor.Email
                },
                Medicaments = p.medicaments.Select(pm => new GetDTO.MedicamentWithDetailsDTO
                {
                    IdMedicament = pm.IdMedicament,
                    Name = pm.medicament.Name,
                    Description = pm.medicament.Description,
                    Dose = pm.Dose,
                   
                }).ToList()
            }).ToList()
    };
    
}
}