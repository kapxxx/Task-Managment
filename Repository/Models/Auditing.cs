using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
	public class Auditing
	{
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public Guid? CreatedBy { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public Guid? UpdatedBy { get; set; }
	}
}
