using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.ResponseDTO
{
    public class GetUserByTaskStatus
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string TaskName { get; set; }
        public int TaskStatus { get; set; }
        public int Priority { get; set; }
    }
}
