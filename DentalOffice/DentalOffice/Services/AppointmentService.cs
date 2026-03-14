using Dapper;
using DentalOffice.Data;
using DentalOffice.Models;

namespace DentalOffice.Services;

/// <summary>
/// Service for managing appointment data operations.
/// </summary>
public class AppointmentService
{
    public List<Appointment> GetAll(DateTime? date = null, string? status = null)
    {
        using var db = DatabaseContext.CreateConnection();
        var sql = @"
            SELECT a.*, p.LastName || ' ' || p.FirstName || ' ' || COALESCE(p.MiddleName, '') AS PatientName
            FROM Appointments a
            JOIN Patients p ON a.PatientId = p.Id
            WHERE 1=1";

        var parameters = new DynamicParameters();

        if (date.HasValue)
        {
            sql += " AND a.AppointmentDate = @Date";
            parameters.Add("Date", date.Value.ToString("yyyy-MM-dd"));
        }

        if (!string.IsNullOrWhiteSpace(status))
        {
            sql += " AND a.Status = @Status";
            parameters.Add("Status", status);
        }

        sql += " ORDER BY a.AppointmentDate, a.StartTime";
        return db.Query<Appointment>(sql, parameters).ToList();
    }

    public List<Appointment> GetByPatient(int patientId)
    {
        using var db = DatabaseContext.CreateConnection();
        return db.Query<Appointment>(@"
            SELECT a.*, p.LastName || ' ' || p.FirstName AS PatientName
            FROM Appointments a
            JOIN Patients p ON a.PatientId = p.Id
            WHERE a.PatientId = @PatientId
            ORDER BY a.AppointmentDate DESC, a.StartTime",
            new { PatientId = patientId }).ToList();
    }

    public Appointment? GetById(int id)
    {
        using var db = DatabaseContext.CreateConnection();
        return db.QueryFirstOrDefault<Appointment>(@"
            SELECT a.*, p.LastName || ' ' || p.FirstName AS PatientName
            FROM Appointments a
            JOIN Patients p ON a.PatientId = p.Id
            WHERE a.Id = @Id", new { Id = id });
    }

    public int Create(Appointment appointment)
    {
        using var db = DatabaseContext.CreateConnection();
        var sql = @"
            INSERT INTO Appointments (PatientId, AppointmentDate, StartTime, EndTime, DoctorName, Status, Notes, TreatmentType)
            VALUES (@PatientId, @AppointmentDate, @StartTime, @EndTime, @DoctorName, @Status, @Notes, @TreatmentType);
            SELECT last_insert_rowid();";
        return db.ExecuteScalar<int>(sql, new
        {
            appointment.PatientId,
            AppointmentDate = appointment.AppointmentDate.ToString("yyyy-MM-dd"),
            StartTime = appointment.StartTime.ToString(@"hh\:mm"),
            EndTime = appointment.EndTime.ToString(@"hh\:mm"),
            appointment.DoctorName,
            appointment.Status,
            appointment.Notes,
            appointment.TreatmentType
        });
    }

    public void Update(Appointment appointment)
    {
        using var db = DatabaseContext.CreateConnection();
        var sql = @"
            UPDATE Appointments SET
                PatientId = @PatientId,
                AppointmentDate = @AppointmentDate,
                StartTime = @StartTime,
                EndTime = @EndTime,
                DoctorName = @DoctorName,
                Status = @Status,
                Notes = @Notes,
                TreatmentType = @TreatmentType
            WHERE Id = @Id";
        db.Execute(sql, new
        {
            appointment.Id,
            appointment.PatientId,
            AppointmentDate = appointment.AppointmentDate.ToString("yyyy-MM-dd"),
            StartTime = appointment.StartTime.ToString(@"hh\:mm"),
            EndTime = appointment.EndTime.ToString(@"hh\:mm"),
            appointment.DoctorName,
            appointment.Status,
            appointment.Notes,
            appointment.TreatmentType
        });
    }

    public void Delete(int id)
    {
        using var db = DatabaseContext.CreateConnection();
        db.Execute("DELETE FROM Appointments WHERE Id = @Id", new { Id = id });
    }

    public List<Appointment> GetTodayAppointments()
    {
        return GetAll(date: DateTime.Today);
    }

    public int GetTodayCount()
    {
        using var db = DatabaseContext.CreateConnection();
        return db.ExecuteScalar<int>(
            "SELECT COUNT(*) FROM Appointments WHERE AppointmentDate = @Date",
            new { Date = DateTime.Today.ToString("yyyy-MM-dd") });
    }
}
