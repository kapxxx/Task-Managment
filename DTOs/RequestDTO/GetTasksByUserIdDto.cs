using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.RequestDTO
{
    public class GetTasksByUserIdDto:PaginationDTO
    {
        public string TaskName { get; set; } = null; // Optional
        public string SortColumn { get; set; } = "Username"; // Default sort column
        public string SortOrder { get; set; } = "ASC"; // Default sort order
        public string Description { get; set; } = null; // Optional
        public int? TaskStatus { get; set; } = null; // Optional, nullable
        public int? TaskPriority { get; set; } = null; // Optional, nullable
        public DateTime? StartDate { get; set; } = null; // Optional, nullable
        public DateTime? EndDate { get; set; } = null; // Optional, nullable
    }
}
