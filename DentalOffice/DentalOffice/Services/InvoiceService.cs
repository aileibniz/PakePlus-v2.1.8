using Dapper;
using DentalOffice.Data;
using DentalOffice.Models;

namespace DentalOffice.Services;

/// <summary>
/// Service for managing invoices and billing.
/// </summary>
public class InvoiceService
{
    public List<Invoice> GetAll(int? patientId = null, string? status = null)
    {
        using var db = DatabaseContext.CreateConnection();
        var sql = @"
            SELECT i.*, p.LastName || ' ' || p.FirstName AS PatientName
            FROM Invoices i
            JOIN Patients p ON i.PatientId = p.Id
            WHERE 1=1";

        var parameters = new DynamicParameters();

        if (patientId.HasValue)
        {
            sql += " AND i.PatientId = @PatientId";
            parameters.Add("PatientId", patientId.Value);
        }
        if (!string.IsNullOrWhiteSpace(status))
        {
            sql += " AND i.Status = @Status";
            parameters.Add("Status", status);
        }

        sql += " ORDER BY i.InvoiceDate DESC";
        return db.Query<Invoice>(sql, parameters).ToList();
    }

    public Invoice? GetById(int id)
    {
        using var db = DatabaseContext.CreateConnection();
        return db.QueryFirstOrDefault<Invoice>(@"
            SELECT i.*, p.LastName || ' ' || p.FirstName AS PatientName
            FROM Invoices i
            JOIN Patients p ON i.PatientId = p.Id
            WHERE i.Id = @Id", new { Id = id });
    }

    public List<InvoiceItem> GetItems(int invoiceId)
    {
        using var db = DatabaseContext.CreateConnection();
        return db.Query<InvoiceItem>(
            "SELECT * FROM InvoiceItems WHERE InvoiceId = @InvoiceId",
            new { InvoiceId = invoiceId }).ToList();
    }

    public int Create(Invoice invoice, List<InvoiceItem>? items = null)
    {
        using var db = DatabaseContext.CreateConnection();
        using var transaction = db.BeginTransaction();

        var invoiceId = db.ExecuteScalar<int>(@"
            INSERT INTO Invoices (PatientId, InvoiceDate, TotalAmount, PaidAmount, Status, PaymentMethod, Notes)
            VALUES (@PatientId, @InvoiceDate, @TotalAmount, @PaidAmount, @Status, @PaymentMethod, @Notes);
            SELECT last_insert_rowid();", invoice, transaction);

        if (items != null)
        {
            foreach (var item in items)
            {
                item.InvoiceId = invoiceId;
                db.Execute(@"
                    INSERT INTO InvoiceItems (InvoiceId, TreatmentId, Description, Quantity, UnitPrice)
                    VALUES (@InvoiceId, @TreatmentId, @Description, @Quantity, @UnitPrice)",
                    item, transaction);
            }
        }

        transaction.Commit();
        return invoiceId;
    }

    public void Update(Invoice invoice)
    {
        using var db = DatabaseContext.CreateConnection();
        db.Execute(@"
            UPDATE Invoices SET
                PatientId = @PatientId,
                InvoiceDate = @InvoiceDate,
                TotalAmount = @TotalAmount,
                PaidAmount = @PaidAmount,
                Status = @Status,
                PaymentMethod = @PaymentMethod,
                Notes = @Notes
            WHERE Id = @Id", invoice);
    }

    public void AddPayment(int invoiceId, decimal amount, string paymentMethod)
    {
        using var db = DatabaseContext.CreateConnection();
        var invoice = GetById(invoiceId);
        if (invoice == null) return;

        invoice.PaidAmount += amount;
        invoice.PaymentMethod = paymentMethod;

        if (invoice.PaidAmount >= invoice.TotalAmount)
            invoice.Status = InvoiceStatus.Paid;
        else if (invoice.PaidAmount > 0)
            invoice.Status = InvoiceStatus.PartiallyPaid;

        Update(invoice);
    }

    public void Delete(int id)
    {
        using var db = DatabaseContext.CreateConnection();
        db.Execute("DELETE FROM Invoices WHERE Id = @Id", new { Id = id });
    }

    public decimal GetTotalUnpaid()
    {
        using var db = DatabaseContext.CreateConnection();
        return db.ExecuteScalar<decimal>(
            "SELECT COALESCE(SUM(TotalAmount - PaidAmount), 0) FROM Invoices WHERE Status != @Paid AND Status != @Cancelled",
            new { Paid = InvoiceStatus.Paid, Cancelled = InvoiceStatus.Cancelled });
    }
}
