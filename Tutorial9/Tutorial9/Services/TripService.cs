using Microsoft.EntityFrameworkCore;
using Tutorial9.Data;
using Tutorial9.Models;
using Tutorial9.Repositories;

namespace Tutorial9.Services;

public class TripService : ITripService
{
    public ITripRepository _repository;
    public ApbdContext _context;

    public TripService(ITripRepository repository,ApbdContext context)
    {
        _repository = repository;
        _context = context;
    }

    public  async Task<Trip> GetTrips(int pageNum, int pageSize)
    {
        var totalTrips = await _context.Trips.CountAsync();
        var totalPages = (int)Math.Ceiling(totalTrips / (double)pageSize);
        var trips = await _repository.GetTrips(pageNum, pageSize);
        var res = new
        {
            pageNum = pageNum,
            pageSize = pageSize,
            allPages = totalPages,
            trips = trips.Select(t => new
            {
                Name = t.Name,
                Description = t.Description,
                DateFrom = t.DateFrom.ToString("yyyy-MM-dd"),
                DateTo = t.DateTo.ToString("yyyy-MM-dd"),
                MaxPeople = t.MaxPeople,
                Countries = t.IdCountries.Select(c => new
                {
                    Name= c.Name 
                    
                }),
                Clients = t.ClientTrips.Select(ct => new
                    {   FirstName= ct.IdClientNavigation.FirstName, 
                        LastName= ct.IdClientNavigation.LastName 
                    }
                )
            })
        };
        return res;
    }

    public async Task<int> DeleteClient(int clientId)
    {
        if (await IsClientWithTrip(clientId))
        {
            return -1;
        }
        var result = await _repository.DeleteClient(clientId);
        return result;
    }

    public async Task<bool> IsClientWithTrip(int clientId)
    {
        return await _repository.IsClientWithTrip(clientId);
    }
}