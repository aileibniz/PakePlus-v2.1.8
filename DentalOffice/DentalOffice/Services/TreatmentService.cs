using Dapper;
using DentalOffice.Data;
using DentalOffice.Models;

namespace DentalOffice.Services;

/// <summary>
/// Service for managing treatment records.
/// </summary>
public class TreatmentService
{
    public List<Treatment> GetAll(int? patientId = null)
    {
        using var db = DatabaseContext.CreateConnection();
        var sql = @"
            SELECT t.*, p.LastName || ' ' || p.FirstName AS PatientName
            FROM Treatments t
            JOIN Patients p ON t.PatientId = p.Id";

        if (patientId.HasValue)
        {
            sql += " WHERE t.PatientId = @PatientId";
            sql += " ORDER BY t.TreatmentDate DESC";
            return db.Query<Treatment>(sql, new { PatientId = patientId.Value }).ToList();
        }

        sql += " ORDER BY t.TreatmentDate DESC";
        return db.Query<Treatment>(sql).ToList();
    }

    public Treatment? GetById(int id)
    {
        using var db = DatabaseContext.CreateConnection();
        return db.QueryFirstOrDefault<Treatment>(@"
            SELECT t.*, p.LastName || ' ' || p.FirstName AS PatientName
            FROM Treatments t
            JOIN Patients p ON t.PatientId = p.Id
            WHERE t.Id = @Id", new { Id = id });
    }

    public int Create(Treatment treatment)
    {
        using var db = DatabaseContext.CreateConnection();
        var sql = @"
            INSERT INTO Treatments (PatientId, AppointmentId, TreatmentDate, TreatmentType, ToothNumber, Description, DoctorName, Cost, Notes)
            VALUES (@PatientId, @AppointmentId, @TreatmentDate, @TreatmentType, @ToothNumber, @Description, @DoctorName, @Cost, @Notes);
            SELECT last_insert_rowid();";
        return db.ExecuteScalar<int>(sql, treatment);
    }

    public void Update(Treatment treatment)
    {
        using var db = DatabaseContext.CreateConnection();
        var sql = @"
            UPDATE Treatments SET
                PatientId = @PatientId,
                AppointmentId = @AppointmentId,
                TreatmentDate = @TreatmentDate,
                TreatmentType = @TreatmentType,
                ToothNumber = @ToothNumber,
                Description = @Description,
                DoctorName = @DoctorName,
                Cost = @Cost,
                Notes = @Notes
            WHERE Id = @Id";
        db.Execute(sql, treatment);
    }

    public void Delete(int id)
    {
        using var db = DatabaseContext.CreateConnection();
        db.Execute("DELETE FROM Treatments WHERE Id = @Id", new { Id = id });
    }

    public decimal GetTotalRevenue(DateTime? from = null, DateTime? to = null)
    {
        using var db = DatabaseContext.CreateConnection();
        var sql = "SELECT COALESCE(SUM(Cost), 0) FROM Treatments WHERE 1=1";
        var parameters = new DynamicParameters();

        if (from.HasValue)
        {
            sql += " AND TreatmentDate >= @From";
            parameters.Add("From", from.Value.ToString("yyyy-MM-dd"));
        }
        if (to.HasValue)
        {
            sql += " AND TreatmentDate <= @To";
            parameters.Add("To", to.Value.ToString("yyyy-MM-dd"));
        }

        return db.ExecuteScalar<decimal>(sql, parameters);
    }
}
