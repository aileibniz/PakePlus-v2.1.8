using Dapper;
using DentalOffice.Data;
using DentalOffice.Models;

namespace DentalOffice.Services;

/// <summary>
/// Service for managing tooth records per patient.
/// </summary>
public class ToothRecordService
{
    public List<ToothRecord> GetByPatient(int patientId)
    {
        using var db = DatabaseContext.CreateConnection();
        return db.Query<ToothRecord>(
            "SELECT * FROM ToothRecords WHERE PatientId = @PatientId ORDER BY ToothNumber",
            new { PatientId = patientId }).ToList();
    }

    public ToothRecord? GetByPatientAndTooth(int patientId, string toothNumber)
    {
        using var db = DatabaseContext.CreateConnection();
        return db.QueryFirstOrDefault<ToothRecord>(
            "SELECT * FROM ToothRecords WHERE PatientId = @PatientId AND ToothNumber = @ToothNumber",
            new { PatientId = patientId, ToothNumber = toothNumber });
    }

    public void SaveOrUpdate(ToothRecord record)
    {
        using var db = DatabaseContext.CreateConnection();
        var sql = @"
            INSERT INTO ToothRecords (PatientId, ToothNumber, Status, Notes, UpdatedAt)
            VALUES (@PatientId, @ToothNumber, @Status, @Notes, datetime('now','localtime'))
            ON CONFLICT(PatientId, ToothNumber)
            DO UPDATE SET Status = @Status, Notes = @Notes, UpdatedAt = datetime('now','localtime')";
        db.Execute(sql, record);
    }

    /// <summary>
    /// Initialize all 32 teeth as healthy for a patient if no records exist.
    /// </summary>
    public void InitializeForPatient(int patientId)
    {
        var existing = GetByPatient(patientId);
        if (existing.Count > 0) return;

        using var db = DatabaseContext.CreateConnection();
        using var transaction = db.BeginTransaction();

        foreach (var tooth in ToothChart.AllTeeth)
        {
            db.Execute(@"
                INSERT OR IGNORE INTO ToothRecords (PatientId, ToothNumber, Status)
                VALUES (@PatientId, @ToothNumber, @Status)",
                new { PatientId = patientId, ToothNumber = tooth, Status = ToothStatus.Healthy },
                transaction);
        }

        transaction.Commit();
    }
}
