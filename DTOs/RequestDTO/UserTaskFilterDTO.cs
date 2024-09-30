using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.RequestDTO
{
    public class UserTaskFilterDTO: PaginationDTO
    {
        public string FilterText { get; set; } // Combined search for username, email, and phone number
        public string SortColumn { get; set; } = "Username"; // Default sort column
        public string SortOrder { get; set; } = "ASC"; // Default sort order
        public int? TaskStatus { get; set; } // Filter for task status
        public int? TaskPriority { get; set; } // Filter for task priority
        public DateTime? StartDate { get; set; } // Start date for tasks
        public DateTime? EndDate { get; set; } // End date for tasks
    }

}
