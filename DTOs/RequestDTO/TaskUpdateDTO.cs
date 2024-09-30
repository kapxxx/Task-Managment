using DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.RequestDTO
{
    public class TaskUpdateDTO: AuditingDTO
    {
        
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public TaskStatuses Status { get; set; } // Enum for status: To Do, In Progress, Completed
        public TaskPriority Priority { get; set; } // Enum for priority: Low, Medium, High
        public string AssignedToUserId { get; set; }
        public string CreatedByUserId { get; set; }


    }
}
