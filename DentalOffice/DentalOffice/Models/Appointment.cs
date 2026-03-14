namespace DentalOffice.Models;

/// <summary>
/// Represents a scheduled appointment.
/// </summary>
public class Appointment
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public DateTime AppointmentDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public string DoctorName { get; set; } = string.Empty;
    public string Status { get; set; } = AppointmentStatus.Scheduled;
    public string? Notes { get; set; }
    public string? TreatmentType { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public string TimeRange => $"{StartTime:hh\\:mm} - {EndTime:hh\\:mm}";
}

public static class AppointmentStatus
{
    public const string Scheduled = "Запланирован";
    public const string InProgress = "В процессе";
    public const string Completed = "Завершен";
    public const string Cancelled = "Отменен";
    public const string NoShow = "Не явился";

    public static string[] All => new[]
    {
        Scheduled, InProgress, Completed, Cancelled, NoShow
    };
}
