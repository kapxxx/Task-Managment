using DTOs.Enums;

using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

using System.Xml.Linq;

namespace Repository.Models
{
	public class Tasks : Auditing
	{
		[Key]
		public Guid Id { get; set; }

		[Required]
		[StringLength(200)]
		public string Name { get; set; }

		[StringLength(1000)]
		public string Description { get; set; }

		[Required]
		public DateTime DueDate { get; set; }

		[Required]
		public TaskStatuses Status { get; set; } // Enum for status: To Do, In Progress, Completed

		[Required]
		public TaskPriority Priority { get; set; } // Enum for priority: Low, Medium, High

		
		public string AssignedToUserId { get; set; }
		public ApplicationUser? AssignedTo { get; set; }

		
		public string CreatedByUserId { get; set; }
		public ApplicationUser CreatedBy { get; set; }

		//public ICollection<Comments> Comments { get; set; } = new List<Comments>();




	}
}
