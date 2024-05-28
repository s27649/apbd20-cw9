using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tutorial9.Data;

namespace Tutorial9.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripsController: ControllerBase
{
    private readonly ApbdContext _context;
    public TripsController(ApbdContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetTrips()
    {
        var trips = await _context.Trips.Select(e => new
        {
            Name = e.Name,
            Contries = e.IdCountries.Select(c => new
            {
                Contry = c.Name
            })
        }).ToListAsync();
        return Ok();
    }
}