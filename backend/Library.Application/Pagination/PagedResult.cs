namespace Library.Application.Pagination;

public class PagedResult<T> 
{
    public T[] Data { get;  }

    public int Count { get; }
    public PagedResult(T[] data, int countElements)
    {
        Data = data;
        Count = countElements;
    }
}
