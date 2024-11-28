using Factory.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory.Repository
{
	public class AdminRepository : IRepository<Admins>
	{
		private readonly FactoryContext _context;

		public AdminRepository(FactoryContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Admins>> GetAllAsync()
		{
			return await _context.Admins.ToListAsync();
		}

		public async Task<Admins> GetByIdAsync(int id)
		{
			return await _context.Admins.FindAsync(id);
		}

		public async Task AddAsync(Admins entity)
		{
			await _context.Admins.AddAsync(entity);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Admins entity)
		{
			_context.Admins.Update(entity);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var admin = await GetByIdAsync(id);
			if (admin != null)
			{
				_context.Admins.Remove(admin);
				await _context.SaveChangesAsync();
			}
		}
	}
}
