using Factory.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory.Repository
{
	public class OrderRepository : IRepository<Orders>
	{
		private readonly FactoryContext _context;

		public OrderRepository(FactoryContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Orders>> GetAllAsync()
		{
			return await _context.Orders.Include(o => o.Model).ToListAsync();  
		}

		public async Task<Orders> GetByIdAsync(int id)
		{
			return await _context.Orders.Include(o => o.Model).FirstOrDefaultAsync(o => o.Id == id);
		}

		public async Task AddAsync(Orders entity)
		{
			await _context.Orders.AddAsync(entity);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Orders entity)
		{
			_context.Orders.Update(entity);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var order = await GetByIdAsync(id);
			if (order != null)
			{
				_context.Orders.Remove(order);
				await _context.SaveChangesAsync();
			}
		}
	}
}
