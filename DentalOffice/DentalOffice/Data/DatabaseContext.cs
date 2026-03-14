using Microsoft.Data.Sqlite;
using Dapper;

namespace DentalOffice.Data;

/// <summary>
/// SQLite database context for the dental office application.
/// Manages connection and schema initialization.
/// </summary>
public static class DatabaseContext
{
    private static readonly string DbPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "DentalOffice",
        "dental_office.db");

    public static string ConnectionString => $"Data Source={DbPath}";

    public static SqliteConnection CreateConnection()
    {
        var connection = new SqliteConnection(ConnectionString);
        connection.Open();
        return connection;
    }

    public static void Initialize()
    {
        var directory = Path.GetDirectoryName(DbPath)!;
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        using var connection = CreateConnection();

        connection.Execute(@"
            CREATE TABLE IF NOT EXISTS Patients (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                LastName TEXT NOT NULL,
                FirstName TEXT NOT NULL,
                MiddleName TEXT,
                DateOfBirth TEXT NOT NULL,
                Gender TEXT NOT NULL,
                Phone TEXT NOT NULL,
                Email TEXT,
                Address TEXT,
                InsurancePolicy TEXT,
                Allergies TEXT,
                MedicalNotes TEXT,
                CreatedAt TEXT NOT NULL DEFAULT (datetime('now','localtime')),
                UpdatedAt TEXT NOT NULL DEFAULT (datetime('now','localtime'))
            );
        ");

        connection.Execute(@"
            CREATE TABLE IF NOT EXISTS Appointments (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                PatientId INTEGER NOT NULL,
                AppointmentDate TEXT NOT NULL,
                StartTime TEXT NOT NULL,
                EndTime TEXT NOT NULL,
                DoctorName TEXT NOT NULL,
                Status TEXT NOT NULL DEFAULT 'Запланирован',
                Notes TEXT,
                TreatmentType TEXT,
                CreatedAt TEXT NOT NULL DEFAULT (datetime('now','localtime')),
                FOREIGN KEY (PatientId) REFERENCES Patients(Id) ON DELETE CASCADE
            );
        ");

        connection.Execute(@"
            CREATE TABLE IF NOT EXISTS Treatments (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                PatientId INTEGER NOT NULL,
                AppointmentId INTEGER,
                TreatmentDate TEXT NOT NULL,
                TreatmentType TEXT NOT NULL,
                ToothNumber TEXT,
                Description TEXT NOT NULL,
                DoctorName TEXT NOT NULL,
                Cost REAL NOT NULL DEFAULT 0,
                Notes TEXT,
                CreatedAt TEXT NOT NULL DEFAULT (datetime('now','localtime')),
                FOREIGN KEY (PatientId) REFERENCES Patients(Id) ON DELETE CASCADE,
                FOREIGN KEY (AppointmentId) REFERENCES Appointments(Id) ON DELETE SET NULL
            );
        ");

        connection.Execute(@"
            CREATE TABLE IF NOT EXISTS ToothRecords (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                PatientId INTEGER NOT NULL,
                ToothNumber TEXT NOT NULL,
                Status TEXT NOT NULL DEFAULT 'Здоров',
                Notes TEXT,
                UpdatedAt TEXT NOT NULL DEFAULT (datetime('now','localtime')),
                FOREIGN KEY (PatientId) REFERENCES Patients(Id) ON DELETE CASCADE,
                UNIQUE(PatientId, ToothNumber)
            );
        ");

        connection.Execute(@"
            CREATE TABLE IF NOT EXISTS Invoices (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                PatientId INTEGER NOT NULL,
                InvoiceDate TEXT NOT NULL,
                TotalAmount REAL NOT NULL DEFAULT 0,
                PaidAmount REAL NOT NULL DEFAULT 0,
                Status TEXT NOT NULL DEFAULT 'Не оплачен',
                PaymentMethod TEXT,
                Notes TEXT,
                CreatedAt TEXT NOT NULL DEFAULT (datetime('now','localtime')),
                FOREIGN KEY (PatientId) REFERENCES Patients(Id) ON DELETE CASCADE
            );
        ");

        connection.Execute(@"
            CREATE TABLE IF NOT EXISTS InvoiceItems (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                InvoiceId INTEGER NOT NULL,
                TreatmentId INTEGER,
                Description TEXT NOT NULL,
                Quantity INTEGER NOT NULL DEFAULT 1,
                UnitPrice REAL NOT NULL DEFAULT 0,
                FOREIGN KEY (InvoiceId) REFERENCES Invoices(Id) ON DELETE CASCADE,
                FOREIGN KEY (TreatmentId) REFERENCES Treatments(Id) ON DELETE SET NULL
            );
        ");

        // Create indexes for performance
        connection.Execute("CREATE INDEX IF NOT EXISTS idx_appointments_date ON Appointments(AppointmentDate);");
        connection.Execute("CREATE INDEX IF NOT EXISTS idx_appointments_patient ON Appointments(PatientId);");
        connection.Execute("CREATE INDEX IF NOT EXISTS idx_treatments_patient ON Treatments(PatientId);");
        connection.Execute("CREATE INDEX IF NOT EXISTS idx_invoices_patient ON Invoices(PatientId);");
        connection.Execute("CREATE INDEX IF NOT EXISTS idx_tooth_records_patient ON ToothRecords(PatientId);");
    }
}
