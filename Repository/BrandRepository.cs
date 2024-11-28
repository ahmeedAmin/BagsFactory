using Factory.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory.Repository
{
	public class BrandRepository : IRepository<Brands>
	{
		private readonly FactoryContext _context;

		public BrandRepository(FactoryContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Brands>> GetAllAsync()
		{
			return await _context.Brands.ToListAsync();
		}

		public async Task<Brands> GetByIdAsync(int id)
		{
			return await _context.Brands
								 .Include(b => b.Models) 
								 .FirstOrDefaultAsync(b => b.Id == id);
		}

		public async Task AddAsync(Brands entity)
		{
			await _context.Brands.AddAsync(entity);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Brands entity)
		{
			_context.Brands.Update(entity);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var brand = await GetByIdAsync(id);
			if (brand != null)
			{
				_context.Brands.Remove(brand);
				await _context.SaveChangesAsync();
			}
		}
		public async Task<IEnumerable<Models>> GetModelsByBrandIdAsync(int brandId)
		{
			return await _context.Models
								 .Where(m => m.BrandId == brandId)
								 .ToListAsync();
		}
	}

}
