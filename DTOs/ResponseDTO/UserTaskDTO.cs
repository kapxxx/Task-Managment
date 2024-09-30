using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.ResponseDTO
{
   
        public class UserTaskDTO
        {
            public string? UserName { get; set; }
            public string? Email { get; set; }
            public string? PhoneNumber { get; set; }
            // Add other properties from AspNetUsers


            public string? TaskName { get; set; }
            public string? Description { get; set; }
            public DateTime? DueDate { get; set; }
            public int? TaskStatus { get; set; }
            public int? TaskPriority { get; set; }
            // Add other properties from Tasks
        

    }
}
