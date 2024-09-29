using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
	public class ApplicationUser : IdentityUser 
	{
		public ICollection<Tasks> Tasks { get; set; } = new List<Tasks>();
	}
}
 