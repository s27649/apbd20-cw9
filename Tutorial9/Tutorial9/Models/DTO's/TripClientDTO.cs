namespace Tutorial9.Models.DTO_s;

public class TripClientDTO
{
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime Datefrom { get; set; }

    public DateTime Dateto { get; set; }

    public int Maxpeople { get; set; }
    public IEnumerable<Client> Clients { get; set; }
    public IEnumerable<Country> Countries { get; set; }
}