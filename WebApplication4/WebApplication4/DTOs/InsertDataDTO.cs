using System.Text.Json.Serialization;
using Tutorial5.Models;

namespace WebApplication4.DTOs;

public class PrescriptionDTO
{
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }


    public List<Prescription_MedicamentDTO> medicaments { get; set; }
        
    public PatientDTO Patient { get; set; }
    
}

public class DoctorDTO
{
    public int IdDoctor { get; set; }
    public string FirstName { get; set; }
    [JsonIgnore]
    public string LastName { get; set; }
    [JsonIgnore]
    public string Email { get; set; }
}

public class PatientDTO
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    
    
}

public class MedicamentDTO
{
    public int IdMedicament { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
}

public class Prescription_MedicamentDTO
{
    public int IdMedicament { get; set; }
    public int Dose { get; set; }
    public string Description { get; set; }
}
public class PatientOutputDTO
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    
    
}