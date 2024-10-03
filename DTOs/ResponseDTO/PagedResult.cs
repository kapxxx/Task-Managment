using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.ResponseDTO
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; } // List of paginated items
        public int TotalRecords { get; set; } // Total count of records
        public int PageNumber { get; set; } // Current page number
        public int PageSize { get; set; } // Page size
        public bool HasNextPage => (PageNumber * PageSize) < TotalRecords; // Check if there is a next page
        public bool HasPreviousPage => PageNumber > 1; // Check if there is a previous page
    }
}
