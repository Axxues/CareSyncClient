using Microsoft.AspNetCore.Mvc;

namespace CareSync.Common
{
    public class PaginatedRequest
    {
        public const int ITEMS_PER_PAGE = 10;

        [FromQuery(Name = "p")]
        public int PageNumber { get; set; } = 1;

        [FromQuery(Name = "s")]
        public string? SearchKeyword { get; set; }

        [FromQuery(Name = "a")]
        public string? SecondarySearchKeyword { get; set; }
    }
}
