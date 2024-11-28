using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory.Model
{
	public class Admins
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Username { get; set; }

		[Required]
		[StringLength(100)]
		public string Password { get; set; } 

		public string? Email { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.Now;
	}
}
