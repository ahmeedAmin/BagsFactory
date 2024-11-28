using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory.Model
{
	public class FactoryContext : DbContext
	{	
		public DbSet<Admins> Admins { get; set; }
		public DbSet<Brands> Brands { get; set; }
		public DbSet<Models> Models { get; set; }
		public DbSet<Orders> Orders { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Server=Minoo;Database=FactoryDB;Encrypt=false;Trusted_Connection=True;TrustServerCertificate=True");
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Admins>().ToTable("Admins");
			modelBuilder.Entity<Brands>().ToTable("Brands");
			modelBuilder.Entity<Models>().ToTable("Models");
			modelBuilder.Entity<Orders>().ToTable("Orders");
			modelBuilder.Entity<Brands>()
			.HasIndex(b => b.Name)
			.IsUnique();
			modelBuilder.Entity<Models>()
			.HasIndex(m => new { m.BrandId, m.Name })
			.IsUnique();

			modelBuilder.Entity<Brands>()
			.HasMany(b => b.Models)
			.WithOne(m => m.Brand)
			.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Models>()
				.HasMany(m => m.Orders)
				.WithOne(o => o.Model)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}

}
