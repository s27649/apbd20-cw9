using Tutorial9.Models;

namespace Tutorial9.Services;

public interface ITripService
{
    public Task<Trip> GetTrips(int pageNum, int pageSize);

    public Task<int> DeleteClient(int clientId);
    public Task<bool> IsClientWithTrip(int clientId);
}