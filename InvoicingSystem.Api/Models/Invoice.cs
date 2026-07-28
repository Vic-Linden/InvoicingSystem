using InvoicingSystem.Api.Enums;

namespace InvoicingSystem.Api.Models;

public class Invoice
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime DueDate { get; set; }
    public InvoiceStatus Status { get; set; } = InvoiceStatus.Unpaid;
}