namespace DentalOffice.Models;

/// <summary>
/// Represents a dental treatment performed on a patient.
/// </summary>
public class Treatment
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public int? AppointmentId { get; set; }
    public DateTime TreatmentDate { get; set; } = DateTime.Now;
    public string TreatmentType { get; set; } = string.Empty;
    public string? ToothNumber { get; set; }
    public string Description { get; set; } = string.Empty;
    public string DoctorName { get; set; } = string.Empty;
    public decimal Cost { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}

public static class TreatmentTypes
{
    public const string Examination = "Осмотр";
    public const string Cleaning = "Чистка";
    public const string Filling = "Пломбирование";
    public const string Extraction = "Удаление";
    public const string RootCanal = "Лечение каналов";
    public const string Crown = "Коронка";
    public const string Bridge = "Мост";
    public const string Implant = "Имплантация";
    public const string Whitening = "Отбеливание";
    public const string Orthodontics = "Ортодонтия";
    public const string Prosthetics = "Протезирование";
    public const string Xray = "Рентген";
    public const string Other = "Другое";

    public static string[] All => new[]
    {
        Examination, Cleaning, Filling, Extraction, RootCanal,
        Crown, Bridge, Implant, Whitening, Orthodontics,
        Prosthetics, Xray, Other
    };
}
