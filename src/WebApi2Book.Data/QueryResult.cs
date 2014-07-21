using System.Collections.Generic;

namespace WebApi2Book.Data
{
    public class QueryResult<T>
    {
        public QueryResult(IEnumerable<T> queriedItems, int totalItemCount, int pageSize)
        {
            PageSize = pageSize;
            TotalItemCount = totalItemCount;
            QueriedItems = queriedItems ?? new List<T>();
        }

        public int TotalItemCount { get; private set; }

        public int TotalPageCount
        {
            get { return ResultsPagingUtility.CalculatePageCount(TotalItemCount, PageSize); }
        }

        public IEnumerable<T> QueriedItems { get; private set; }
        public int PageSize { get; private set; }
    }
}