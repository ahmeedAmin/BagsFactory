using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory.Model
{
	public class Brands
	{
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100,ErrorMessage = "Name of brand should be less than 100 character")]
        public string Name { get; set; }
		public string? LogoPath { get; set; }

		[EmailAddress(ErrorMessage = "Invalid Email")]
        public string? Email { get; set; }
		[RegularExpression(@"^(011|012|010|015|02)\d{7,}$", ErrorMessage = "Invalid Phone Number")]

		public string? PhoneNumber { get; set; }
        public string? Info { get; set; }
		public DateTime BrandDate { get; set; } = DateTime.Now;

		public virtual ICollection<Models> Models { get; set; } = new List<Models>();

		public bool HasActiveModels => Models != null && Models.Any(m => m.HasActiveOrders);
		public int AllQuantityOfBrand => Models?.Sum(model => model.AllQuantityOfModel) ?? 0;
		public decimal AllGainOfBrand => Models?.Sum(model => model.AllGainOfMadel) ?? 0;
	}
}
