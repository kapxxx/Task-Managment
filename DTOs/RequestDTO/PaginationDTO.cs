using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.RequestDTO
{
    public class PaginationDTO
    {
        public int PageNumber { get; set; } = 1; // Default page number
        public int PageSize { get; set; } = 10; // Default page size
    }

}
