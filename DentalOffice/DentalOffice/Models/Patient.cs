namespace DentalOffice.Models;

/// <summary>
/// Represents a patient in the dental office system.
/// </summary>
public class Patient
{
    public int Id { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? InsurancePolicy { get; set; }
    public string? Allergies { get; set; }
    public string? MedicalNotes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public string FullName => $"{LastName} {FirstName} {MiddleName}".Trim();

    public override string ToString() => FullName;
}
