namespace CareSync.Common
{
    public class PaginatedResult<T>
    {
        public int Page { get; set; }

        public int TotalCount { get; set; }

        public int TotalRecords { get; set; }

        public string? SearchKeyword { get; set; }

        public string? SecondarySearchKeyword { get; set; }

        public IEnumerable<T> Result { get; set; }
    }
}
