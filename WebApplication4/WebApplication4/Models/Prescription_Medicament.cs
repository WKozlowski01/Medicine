using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tutorial5.Models;

[Table("Prescription_Medicament")]
[PrimaryKey(nameof(IdMedicament), nameof(IdPrescription))]
public class Prescription_Medicament
{
    [ForeignKey(nameof(IdMedicament))]
    public int IdMedicament { get; set; }
    
    [ForeignKey(nameof(IdPrescription))]
    public int IdPrescription { get; set; }
    
    
    public int Dose { get; set; }
    
    [MaxLength(100)]
    public string? Details { get; set; }
    
    public Medicament medicament { get; set; }
    
    public Prescription prescription { get; set; }
    
}