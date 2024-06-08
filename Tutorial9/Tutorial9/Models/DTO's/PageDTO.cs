namespace Tutorial9.Models.DTO_s;

public class PageDTO
{
    public int PageNum { get; set; }
    public int PageSize { get; set; }
    public int AllPages { get; set; }
    public IEnumerable<TripClientDTO>? Trips { get; set; }
}