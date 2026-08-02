using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using InvoicingSystem.Api.Data;
using InvoicingSystem.Api.Models;
using InvoicingSystem.Api.Enums;

namespace InvoicingSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly AppDbContext _context;

    public PaymentController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Payment>>> GetPayments()
    {
        return await _context.Payments.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Payment>> GetPayment(int id)
    {
        var payment = await _context.Payments.FindAsync(id);

        if(payment == null)
        {
            return NotFound();
        }

        return payment;
    }

    [HttpPost]
    public async Task<ActionResult<Payment>> CreatePayment(Payment payment)
    {
        var invoice = await _context.Invoices
            .Include(i => i.Payments)
            .FirstOrDefaultAsync(i => i.Id == payment.InvoiceId);

        if(invoice == null)
        {
            return BadRequest("Invoice not found");
        }

        var alreadyPaid = invoice.Payments.Sum(p => p.Amount);
        var remaining = invoice.Amount - alreadyPaid;

        if(payment.Amount > remaining)
        {
            return BadRequest($"Payment exceeds remaining balance of {remaining}.");
        }

        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();

        await UpdateInvoiceStatus(payment.InvoiceId);

        return CreatedAtAction(nameof(GetPayment), new {id = payment.Id}, payment);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePayment(int id, Payment payment)
    {
        if(id != payment.Id)
        {
            return BadRequest();
        }

        _context.Entry(payment).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePayment(int id)
    {
        var payment = await _context.Payments.FindAsync(id);

        if(payment == null)
        {
            return NotFound();
        }

        _context.Payments.Remove(payment);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private async Task UpdateInvoiceStatus(int invoiceId)
    {
        var invoice = await _context.Invoices  
            .Include(i => i.Payments)
            .FirstOrDefaultAsync(i => i.Id == invoiceId);

        if (invoice == null)
        {
            return;
        }

        var totalPaid = invoice.Payments.Sum(p => p.Amount);

        if(totalPaid >= invoice.Amount)
        {
            invoice.Status = InvoiceStatus.Paid;
        }
        else if(invoice.DueDate < DateTime.Now)
        {
            invoice.Status = InvoiceStatus.Overdue;
        }
        else
        {
            invoice.Status = InvoiceStatus.Unpaid;
        }

        await _context.SaveChangesAsync();
    }
}