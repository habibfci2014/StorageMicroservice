namespace Application.Dtos;

public class PagedData<T>
{
    public List<T> Result { get; set; } = new List<T>();
    public int TotalRecords { get; set; }
    public int LastPage { get; set; }
    public int CurrentPage { get; set; }
}