using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InvoicingSystem.Api.Data;
using InvoicingSystem.Api.Models;

namespace InvoicingSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoiceController : ControllerBase
{
    private readonly AppDbContext _context;

    public InvoiceController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices()
    {
        return await _context.Invoices.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Invoice>> GetInvoice(int id)
    {
        var invoice = await _context.Invoices.FindAsync(id);

        if (invoice == null)
        {
            return NotFound();
        }

        return invoice;
    }

    [HttpPost]
    public async Task<ActionResult<Invoice>> CreateInvoice (Invoice invoice)
    {
        _context.Invoices.Add(invoice);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(invoice), new {id = invoice.Id}, invoice);
    }
}
