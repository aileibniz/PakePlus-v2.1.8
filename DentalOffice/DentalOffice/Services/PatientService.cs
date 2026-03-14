using Dapper;
using DentalOffice.Data;
using DentalOffice.Models;

namespace DentalOffice.Services;

/// <summary>
/// Service for managing patient data operations.
/// </summary>
public class PatientService
{
    public List<Patient> GetAll(string? searchTerm = null)
    {
        using var db = DatabaseContext.CreateConnection();
        var sql = "SELECT * FROM Patients";
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            sql += " WHERE LastName LIKE @Search OR FirstName LIKE @Search OR Phone LIKE @Search";
            return db.Query<Patient>(sql, new { Search = $"%{searchTerm}%" }).ToList();
        }
        return db.Query<Patient>(sql + " ORDER BY LastName, FirstName").ToList();
    }

    public Patient? GetById(int id)
    {
        using var db = DatabaseContext.CreateConnection();
        return db.QueryFirstOrDefault<Patient>(
            "SELECT * FROM Patients WHERE Id = @Id", new { Id = id });
    }

    public int Create(Patient patient)
    {
        using var db = DatabaseContext.CreateConnection();
        var sql = @"
            INSERT INTO Patients (LastName, FirstName, MiddleName, DateOfBirth, Gender, Phone, Email, Address, InsurancePolicy, Allergies, MedicalNotes)
            VALUES (@LastName, @FirstName, @MiddleName, @DateOfBirth, @Gender, @Phone, @Email, @Address, @InsurancePolicy, @Allergies, @MedicalNotes);
            SELECT last_insert_rowid();";
        return db.ExecuteScalar<int>(sql, patient);
    }

    public void Update(Patient patient)
    {
        using var db = DatabaseContext.CreateConnection();
        var sql = @"
            UPDATE Patients SET
                LastName = @LastName,
                FirstName = @FirstName,
                MiddleName = @MiddleName,
                DateOfBirth = @DateOfBirth,
                Gender = @Gender,
                Phone = @Phone,
                Email = @Email,
                Address = @Address,
                InsurancePolicy = @InsurancePolicy,
                Allergies = @Allergies,
                MedicalNotes = @MedicalNotes,
                UpdatedAt = datetime('now','localtime')
            WHERE Id = @Id";
        db.Execute(sql, patient);
    }

    public void Delete(int id)
    {
        using var db = DatabaseContext.CreateConnection();
        db.Execute("DELETE FROM Patients WHERE Id = @Id", new { Id = id });
    }

    public int GetTotalCount()
    {
        using var db = DatabaseContext.CreateConnection();
        return db.ExecuteScalar<int>("SELECT COUNT(*) FROM Patients");
    }
}
