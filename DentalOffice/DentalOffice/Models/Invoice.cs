namespace DentalOffice.Models;

/// <summary>
/// Represents an invoice/bill for dental services.
/// </summary>
public class Invoice
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public DateTime InvoiceDate { get; set; } = DateTime.Now;
    public decimal TotalAmount { get; set; }
    public decimal PaidAmount { get; set; }
    public string Status { get; set; } = InvoiceStatus.Unpaid;
    public string? PaymentMethod { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public decimal RemainingAmount => TotalAmount - PaidAmount;
}

public class InvoiceItem
{
    public int Id { get; set; }
    public int InvoiceId { get; set; }
    public int? TreatmentId { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Quantity { get; set; } = 1;
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice => Quantity * UnitPrice;
}

public static class InvoiceStatus
{
    public const string Unpaid = "Не оплачен";
    public const string PartiallyPaid = "Частично оплачен";
    public const string Paid = "Оплачен";
    public const string Cancelled = "Отменен";

    public static string[] All => new[]
    {
        Unpaid, PartiallyPaid, Paid, Cancelled
    };
}

public static class PaymentMethods
{
    public const string Cash = "Наличные";
    public const string Card = "Банковская карта";
    public const string Transfer = "Перевод";
    public const string Insurance = "Страховка";

    public static string[] All => new[]
    {
        Cash, Card, Transfer, Insurance
    };
}
