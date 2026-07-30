namespace InvoicingSystem.Api.Models;

public class Payment
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }

    public int InvoiceId { get; set; } //FK
    public Invoice? Invoice { get; set; }
}