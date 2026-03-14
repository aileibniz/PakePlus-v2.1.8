namespace DentalOffice.Models;

/// <summary>
/// Represents the condition/status of a specific tooth for a patient.
/// </summary>
public class ToothRecord
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public string ToothNumber { get; set; } = string.Empty;
    public string Status { get; set; } = ToothStatus.Healthy;
    public string? Notes { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}

public static class ToothStatus
{
    public const string Healthy = "Здоров";
    public const string Caries = "Кариес";
    public const string Filled = "Пломба";
    public const string Crown = "Коронка";
    public const string Missing = "Отсутствует";
    public const string Implant = "Имплант";
    public const string RootCanal = "Депульпирован";
    public const string NeedsTreatment = "Нуждается в лечении";

    public static string[] All => new[]
    {
        Healthy, Caries, Filled, Crown, Missing, Implant, RootCanal, NeedsTreatment
    };
}

/// <summary>
/// Standard dental numbering (FDI notation) for adult teeth.
/// </summary>
public static class ToothChart
{
    // Upper right (1st quadrant): 18-11
    // Upper left (2nd quadrant): 21-28
    // Lower left (3rd quadrant): 38-31
    // Lower right (4th quadrant): 41-48
    public static string[] UpperRight => new[] { "18", "17", "16", "15", "14", "13", "12", "11" };
    public static string[] UpperLeft => new[] { "21", "22", "23", "24", "25", "26", "27", "28" };
    public static string[] LowerLeft => new[] { "38", "37", "36", "35", "34", "33", "32", "31" };
    public static string[] LowerRight => new[] { "41", "42", "43", "44", "45", "46", "47", "48" };

    public static string[] AllTeeth
    {
        get
        {
            var all = new List<string>();
            all.AddRange(UpperRight);
            all.AddRange(UpperLeft);
            all.AddRange(LowerLeft);
            all.AddRange(LowerRight);
            return all.ToArray();
        }
    }
}
