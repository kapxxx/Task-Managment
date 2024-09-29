using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
	public class Comments
	{
		public int Id { get; set; }

		[Required]
		public string CommentText { get; set; } = string.Empty;

		[Required]
		public DateTime CreatedAt { get; set; }

	
		public Guid UserId { get; set; }
		public ApplicationUser? User { get; set; }

		public Guid? TaskId { get; set; }
		public Task? Task { get; set; }
	}
}
