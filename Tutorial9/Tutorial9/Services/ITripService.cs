using Tutorial9.Models;
using Tutorial9.Models.DTO_s;

namespace Tutorial9.Services;

public interface ITripService
{
    public Task<PageDTO> GetTrips(int pageNum, int pageSize);

    public Task<int> DeleteClient(int clientId);
    public Task<bool> IsClientWithTrip(int clientId);
    public Task<int> AddClient(Client clientId);
}