using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Common.Collection
{
    public interface IPagedList<T>
    {
        IReadOnlyList<T> Items { get; }
        int PageNumber { get; }
        int TotalPages { get; }
        int TotalCount { get; }
        int PageSize { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
    }
}
