using Tutorial9.Models;

namespace Tutorial9.Repositories;

public interface ITripRepository
{
    public Task<IEnumerable<Trip>> GetTrips(int pageNum, int pageSize);
    public Task<int> DeleteClient(int clientId);
    public Task<bool> IsClientWithTrip(int clientId);
    public Task<int> AddClient(Client client);
}