using Factory.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Factory
{
	public partial class AddOrder : Form
	{
		FactoryContext _context = new FactoryContext();
		public AddOrder()
		{
			InitializeComponent();
			AddBrand.BrandAdded += LoadBrands;
			LoadBrands(this, EventArgs.Empty);
		}

		private void LoadBrands(object sender, EventArgs e)
		{
			var brands = _context.Brands.Select(b => b.Name).ToList();
			BrandName.DataSource = brands;
		}

		private void LoadModel(string brand)
		{
			if (string.IsNullOrWhiteSpace(brand))
			{
				ModelNamee.DataSource = null;
				return;
			}
			var models = _context.Models.Where(m => m.Brand.Name == brand).Select(m => m.Name).ToList();
			ModelNamee.DataSource = models;
		}
		private void BrandName_SelectedIndexChanged(object sender, EventArgs e)
		{
			LoadModel(BrandName.SelectedItem.ToString());
		}

		private void AddOrder_Load(object sender, EventArgs e)
		{

		}

		private async void Button_AddNewOrder_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(Colorr.Text) ||
			string.IsNullOrWhiteSpace(Quantity.Text) ||
			string.IsNullOrWhiteSpace(Price.Text))
			{
				MessageBox.Show("All fields are required...(Color - Quantity - Price)");
				return;
			}

			if (!int.TryParse(Quantity.Text, out int quantity) || quantity <= 0)
			{
				MessageBox.Show("Quantity must be a positive number.");
				return;
			}

			if (!decimal.TryParse(Price.Text, out decimal price) || price < 0)
			{
				MessageBox.Show("Price must be a positive number.");
				return;
			}
			decimal? cost = null;
			if (!string.IsNullOrWhiteSpace(Cost.Text))
			{
				if (!decimal.TryParse(Cost.Text, out decimal parsedCost) || parsedCost < 0)
				{
					MessageBox.Show("Cost must be a positive number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				cost = parsedCost; // تعيين قيمة التكلفة
			}
			var selectedModelName = ModelNamee.SelectedItem?.ToString();
			var model = await _context.Models.FirstOrDefaultAsync(m => m.Name == selectedModelName);

			if (model == null)
			{
				MessageBox.Show("Selected model not found.");
				return;
			}

			var newOrder = new Orders
			{
				ModelId = model.Id,
				Color = Colorr.Text,
				Quantity = quantity,
				Price = price,
				Cost = cost,
				Description = Description.Text
			};

			try
			{
				_context.Orders.Add(newOrder);
				await _context.SaveChangesAsync();
				MessageBox.Show("Order saved successfully!");
				this.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"An error occurred while saving the order...");
			}
		}

		private void Reset_Click(object sender, EventArgs e)
		{
			BrandName.Text = null;
			ModelNamee.Text = null;
			Colorr.Text = null;
			Quantity.TabIndex = default;
			Price.TabIndex = default;
			Cost.TabIndex = default;
			Description.Text = null;
		}

		private void Colorr_TextChanged(object sender, EventArgs e)
		{

		}
	}
}
