using System;
using System.Collections.Generic;
using System.Text;

namespace DomainFramework
{
    public class PaginationResult<T>
    {
        public IReadOnlyList<T> Items { get; }

        public int Page { get; }

        public int PageSize { get; }

        public int TotalCount { get; }

        public PaginationResult(IReadOnlyList<T> items, int page, int pageSize, int totalCount)
        {
            Items = items;
            Page = page;
            PageSize = pageSize;
            TotalCount = totalCount;
        }

        public static PaginationResult<T> Create(IReadOnlyList<T> items, int page, int pageSize, int totalCount)
        {
            return new PaginationResult<T>(items, page, pageSize, totalCount);
        }
    }
}
