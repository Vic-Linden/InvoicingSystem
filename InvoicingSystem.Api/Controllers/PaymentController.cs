using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using InvoicingSystem.Api.Data;
using InvoicingSystem.Api.Models;

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
        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();

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
}