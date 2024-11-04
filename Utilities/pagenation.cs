// public class QueryParameters
// {
//     public int PageNumber { get; set; } = 1;
//     public int PageSize { get; set; } = 10;
//     public string SearchTerm { get; set; } = string.Empty;
//     public string SortBy { get; set; } = "Name"; // Default sorting by name
//     public string SortOrder { get; set; } = "asc"; // asc or desc
// }
//     public class PaginatedResult<T>
// {
//     public List<T>? Items { get; set; }
//     public int TotalCount { get; set; }
//     public int PageNumber { get; set; }
//     public int PageSize { get; set; }
//      public string? SearchBy { get; set; }
     
// }
public class QueryParameters
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string SearchTerm { get; set; } = string.Empty;
    public string SortBy { get; set; } = "Name"; // Default sorting by name
    public string SortOrder { get; set; } = "asc"; // asc or desc
}

public class PaginatedResult<T>
{
    public List<T>? Items { get; set; }
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? SearchBy { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}
