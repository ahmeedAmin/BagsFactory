using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory.Model
{
	public class Models
	{
		[Key]
        public int Id { get; set; }
		[Required]
		[StringLength(100, ErrorMessage = "Name of brand should be less than 100 character")]
		public string Name { get; set; }
		public string? Description { get; set; }
		public string? ImagePath { get; set; }
		public DateTime ModelDate { get; set; } = DateTime.Now;

		[Required]
		public int BrandId { get; set; }
        public Brands? Brand { get; set; }
		public virtual ICollection<Orders>? Orders { get; set; } = new List<Orders>();

		public bool HasActiveOrders => Orders != null && Orders.Any(o => o.Status == OrderStatus.Active || o.Status == OrderStatus.Pending);
		public int AllQuantityOfModel => Orders?.Sum(order => order.Quantity) ?? 0;
		public decimal AllGainOfMadel => Orders?.Sum(order=>order.Gain) ?? 0;

	}
}
