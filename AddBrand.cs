using Factory.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Factory
{
	public partial class AddBrand : Form
	{
		FactoryContext _context = new FactoryContext();
		public static event EventHandler BrandAdded;

		public AddBrand()
		{
			InitializeComponent();
		}

		private void AddBrand_Load(object sender, EventArgs e)
		{

		}

		private bool ValidateBrand(Brands brand)
		{
			var validationResults = new List<ValidationResult>();
			var validationContext = new ValidationContext(brand);

			if (!string.IsNullOrWhiteSpace(brand.Email) && !Validator.TryValidateProperty(brand.Email, new ValidationContext(brand) { MemberName = "Email" }, validationResults))
			{
				MessageBox.Show(string.Join("\n", validationResults.Select(vr => vr.ErrorMessage)));
				return false;
			}

			if (!string.IsNullOrWhiteSpace(brand.PhoneNumber) && !Validator.TryValidateProperty(brand.PhoneNumber, new ValidationContext(brand) { MemberName = "PhoneNumber" }, validationResults))
			{
				MessageBox.Show(string.Join("\n", validationResults.Select(vr => vr.ErrorMessage)));
				return false;
			}

			return true;
		}

		private string CopyBrandImage(string sourcePath)
		{
			string wwwrootPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "wwwroot", "images", "Brands");
			Directory.CreateDirectory(wwwrootPath);
			string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(sourcePath);
			string destinationPath = Path.Combine(wwwrootPath, uniqueFileName);

			File.Copy(sourcePath, destinationPath);
			return uniqueFileName;
		}

		private void Button_AddLogo_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog openFileDialog = new OpenFileDialog())
			{
				openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					BrandLogoPath.Text = openFileDialog.FileName;
				}
			}
		}

		private void Button_AddNewBrand_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(BrandName?.Text))
			{
				MessageBox.Show("The Name Of Brand Is Required");
				return;
			}

			if (!string.IsNullOrEmpty(BrandLogoPath.Text) && !File.Exists(BrandLogoPath.Text))
			{
				MessageBox.Show("The File Not Exist");
				return;
			}

			if (_context.Brands.Any(b => b.Name.ToLower() == BrandName.Text.ToLower()))
			{
				MessageBox.Show("A brand with this name already exists.");
				return;
			}


			var newBrand = new Brands
			{
				Name = BrandName.Text,
				LogoPath = string.IsNullOrWhiteSpace(BrandLogoPath.Text) ? null : $" ",
				Email = BrandEmail.Text,
				PhoneNumber = BrandPhone.Text,
				Info = BrandInfo.Text,
			};

			if (!ValidateBrand(newBrand))
			{
				return;
			}

			if (newBrand.LogoPath != null)
			{
				try
				{
					newBrand.LogoPath = CopyBrandImage(BrandLogoPath.Text);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
					return;
				}
			}

			try
			{
				_context.Brands.Add(newBrand);
				_context.SaveChanges();
				MessageBox.Show("Brand added successfully!");
				BrandAdded?.Invoke(this, EventArgs.Empty);
				Reset_Click(this, EventArgs.Empty);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"An error occurred while saving data...");
			}
		}

		private void BrandName_TextChanged(object sender, EventArgs e)
		{

		}

		private void BrandEmail_TextChanged(object sender, EventArgs e)
		{

		}

		private void BrandPhone_TextChanged(object sender, EventArgs e)
		{

		}

		private void BrandInfo_TextChanged(object sender, EventArgs e)
		{

		}

		private void BrandLogoPath_TextChanged(object sender, EventArgs e)
		{

		}

		private void Reset_Click(object sender, EventArgs e)
		{
			BrandName.Text = null;
			BrandPhone.Text = null;
			BrandLogoPath.Text = null;
			BrandEmail.Text = null;
			BrandInfo.Text = null;
		}
	}
}