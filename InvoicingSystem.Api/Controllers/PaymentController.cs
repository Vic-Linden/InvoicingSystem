using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using InvoicingSystem.Api.Data;
using InvoicingSystem.Api.Models;

namespace InvoicingSystem.Api.Controllers;

[ApiController]
[Route("api/controller")]
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
}