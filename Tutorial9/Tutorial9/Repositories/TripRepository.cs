using Microsoft.EntityFrameworkCore;
using Tutorial9.Data;
using Tutorial9.Models;

namespace Tutorial9.Repositories;

public class TripRepository : ITripRepository
{
    private readonly ApbdContext _context;

    public TripRepository(ApbdContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Trip>> GetTrips(int pageNum, int pageSize)
    {
        var trips = await _context.Trips
            .Include(t => t.IdCountries)
            .Include(t => t.ClientTrips)
            .ThenInclude(ct => ct.IdClientNavigation)
            .OrderByDescending(t => t.DateFrom)
            .Skip((pageNum - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return trips;
    }

    public async Task<int> DeleteClient(int clientId)
    {
        var client= new Client()
        {
            IdClient = clientId
        }; 
        _context.Clients.Attach(client);
        var entry = _context.Entry(client);
        entry.State = EntityState.Deleted;
        await _context.SaveChangesAsync();
        return 1;
    }
    public async Task<bool> IsClientWithTrip(int clientId)
    {
        return await _context.ClientTrips.AnyAsync(trip => trip.IdClient == clientId);
    }
}