using Factory.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory.Repository
{
	public class ModelRepository : IRepository<Models>
	{
		private readonly FactoryContext _context;

		public ModelRepository(FactoryContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Models>> GetAllAsync()
		{
			return await _context.Models.Include(m => m.Brand).ToListAsync();
		}

		public async Task<Models> GetByIdAsync(int id)
		{
			return await _context.Models.Include(m => m.Brand).FirstOrDefaultAsync(m => m.Id == id);
		}

		public async Task AddAsync(Models entity)
		{
			await _context.Models.AddAsync(entity);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Models entity)
		{
			_context.Models.Update(entity);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var model = await GetByIdAsync(id);
			if (model != null)
			{
				_context.Models.Remove(model);
				await _context.SaveChangesAsync();
			}
		}
	}
}
