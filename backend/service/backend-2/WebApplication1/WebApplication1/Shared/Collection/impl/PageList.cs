namespace WebApplication1.Shared.Collection.Impl;
 public class PagedList<T> : IPagedList<T>
 {
     public IReadOnlyList<T> Items { get; }
     public int PageNumber { get; }
     public int TotalPages { get; }
     public int TotalCount { get; }
     public int PageSize { get; }

     public bool HasPreviousPage => PageNumber > 1;
     public bool HasNextPage => PageNumber < TotalPages;

     public PagedList(List<T> items, int count, int pageNumber, int pageSize)
     {
         Items = items.AsReadOnly();
         TotalCount = count;
         PageNumber = pageNumber;
         PageSize = pageSize;
         TotalPages = (int)Math.Ceiling(count / (double)pageSize);
     }
 }