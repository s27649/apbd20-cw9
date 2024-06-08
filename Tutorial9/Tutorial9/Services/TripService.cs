using Microsoft.EntityFrameworkCore;
using Tutorial9.Data;
using Tutorial9.Models;
using Tutorial9.Models.DTO_s;
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

    public  async Task<PageDTO> GetTrips(int pageNum, int pageSize)
    {
        var trips = await _repository.GetTrips(pageNum, pageSize);
        var tripsClientsCountries = trips.Select(trip => new TripClientDTO()
        {
            Name = trip.Name,
            Description = trip.Description,
            Datefrom = trip.DateFrom,
            Dateto = trip.DateTo,
            Maxpeople = trip.MaxPeople,
            Clients = trip.ClientTrips.Select(clientTrip => new Client()
            {
                FirstName = clientTrip.IdClientNavigation.FirstName,
                LastName = clientTrip.IdClientNavigation.LastName
            }).ToList(),
            Countries = trip.IdCountries.Select(country => new Country()
            {
                Name = country.Name
            }).ToList()
        }).ToList();
        var count = await _context.Trips.CountAsync();
        var allPages = count / pageSize;
        if (count % pageSize != 0)
        {
            allPages++;
        }
        var pageTripsModel = new PageDTO()
        {
            PageNum = pageNum,
            AllPages = allPages,
            PageSize = pageSize,
            Trips = tripsClientsCountries
        };
        return pageTripsModel;
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

    public Task<int> AddClient(Client client)
    {
        var newClient = _repository.AddClient(client);
        return newClient;
    }
}