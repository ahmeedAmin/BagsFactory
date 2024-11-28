using Factory.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
	public partial class AddModel : Form
	{
		FactoryContext _context = new FactoryContext();
		public AddModel()
		{
			InitializeComponent();
			AddBrand.BrandAdded += LoadBrands;
			LoadBrands(this, EventArgs.Empty);
		}
		public async void LoadBrands(object sender, EventArgs e)
		{
			var brands = await _context.Brands.Select(b => b.Name).ToListAsync();
			BrandName.DataSource = brands;
		}


		private string CopymodelImage(string sourcePath)
		{
			string wwwrootPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "wwwroot", "images", "Models");
			Directory.CreateDirectory(wwwrootPath);
			string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(sourcePath);
			string destinationPath = Path.Combine(wwwrootPath, uniqueFileName);

			File.Copy(sourcePath, destinationPath);
			return uniqueFileName;
		}

		private void Button_AddPic_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog openFileDialog = new OpenFileDialog())
			{
				openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					ModelPicPath.Text = openFileDialog.FileName;
				}
			}
		}

		private async void Button_AddNewModel_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(ModelName.Text) || string.IsNullOrWhiteSpace(BrandName.Text))
			{
				MessageBox.Show("Model name and brand are required.");
				return;
			}
			var brand = await _context.Brands.Include(b => b.Models).FirstOrDefaultAsync(b => b.Name == BrandName.Text);
			if (!string.IsNullOrEmpty(ModelPicPath.Text) && !File.Exists(ModelPicPath.Text))
			{
				MessageBox.Show("The selected image does not exist.");
				return;
			}
			if (brand != null && brand.Models.Any(m => m.Name.ToLower() == BrandName.Text.ToLower()))
			{
				MessageBox.Show("A Model with this name already exists in this Brand.");
				return;
			}

			var NewModel = new Models
			{
				Name = ModelName.Text,
				Description = Description.Text,
				ImagePath = string.IsNullOrWhiteSpace(ModelPicPath.Text) ? null : $" ",
				BrandId = brand.Id
			};

			if (NewModel.ImagePath != null)
			{
				try
				{
					NewModel.ImagePath = CopymodelImage(ModelPicPath.Text);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
					return;
				}
			}

			try
			{
				_context.Models.Add(NewModel);
				_context.SaveChanges();
				MessageBox.Show("Model added successfully!");
				this.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"An error occurred while saving data...");
			}
		}

		private void Reset_Click(object sender, EventArgs e)
		{
			ModelName.Text = null;
			ModelPicPath.Text = null;
			Description.Text = null;
			BrandName.Text = null;
		}
	}
}
