using System.Text.Json.Serialization;
using WebApplication4.DTOs;

namespace WebApplication4.Controller;

public class GetDTO
{
    public class PatientWithPrescriptionsDTO
    {
        public int IdPatient { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public List<PrescriptionDetailDTO> Prescriptions { get; set; }
    }

    public class PrescriptionDetailDTO
    {
        public int IdPrescription { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public DoctorDTO Doctor { get; set; }
        public List<MedicamentWithDetailsDTO> Medicaments { get; set; }
    }

    public class MedicamentWithDetailsDTO
    {
        public int IdMedicament { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    
        public int Dose { get; set; }
     
    }
}