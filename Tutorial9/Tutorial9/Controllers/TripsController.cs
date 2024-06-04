
using Microsoft.AspNetCore.Mvc;
using Tutorial9.Data;
using Tutorial9.Services;

namespace Tutorial9.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripsController: ControllerBase
{
    private readonly ApbdContext _context;
    private readonly ITripService _tripService;
    public TripsController(ApbdContext context,ITripService tripService)
    {
        _context = context;
        _tripService = tripService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTrips([FromQuery]int page=1,[FromQuery] int pageSize=10)
    {
        var trips = await _tripService.GetTrips(page, pageSize);
        return Ok(trips);
    }
    
    [Route("api/clients/{idClient:int}")]
    [HttpDelete]
    public async Task<IActionResult> RemoveClient(int idClient)
    {
        var effectedCount = await _tripService.DeleteClient(idClient);
        if (effectedCount == -1)
        {
            return StatusCode(StatusCodes.Status304NotModified);
        }

        return StatusCode(StatusCodes.Status200OK);
    }
}