using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory.Model
{
	public enum OrderStatus
	{
		Active,
		Completed,
		Pending,
		Cancelled
	}
	public class Orders
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int ModelId { get; set; }
		public Models Model { get; set; }
		[Required]
		public string Color { get; set; }

		[Range(1, int.MaxValue, ErrorMessage = "The Number Must Be positive")]
		public int Quantity { get; set; }
		[Range(0, double.MaxValue, ErrorMessage = "The number must be positive")]
		public decimal Price { get; set; }
		[Range(0, double.MaxValue, ErrorMessage = "The number must be positive")]
		public decimal? Cost { get; set; }
		public string? Description { get; set; }

		public DateTime OrderDate { get; set; } = DateTime.Now;
		public DateTime? CompletionDate { get; set; }

		public OrderStatus Status { get; set; } = OrderStatus.Active;
		public decimal? Gain => Status == OrderStatus.Completed ? (Price - Cost) : 0;

		public void CompleteOrder()
		{
			Status = OrderStatus.Completed;
			CompletionDate = DateTime.Now;
		}
		public void CancelOrder()
		{
			Status = OrderStatus.Cancelled;
		}
		public void PendingOrder()
		{
			Status = OrderStatus.Pending;
		}
		public void ActiveOrder()
		{
			Status = OrderStatus.Active;
		}
	}
}
