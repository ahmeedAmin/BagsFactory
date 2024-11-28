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
	public partial class Login : Form
	{
		FactoryContext _context = new FactoryContext();
		public Login()
		{
			InitializeComponent();
		}

		private void Login_Load(object sender, EventArgs e)
		{

		}

		private void PicLogin_Click(object sender, EventArgs e)
		{

		}

		private async void Login_Button_Click(object sender, EventArgs e)
		{
			var UserName = UserName_TextBox.Text;
			var Password = Password_TextBox.Text;
			if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
			{
				MessageBox.Show("Both username and password are required.");
				UserName_TextBox.Text = null;
				Password_TextBox.Text = null;
			}
			else
			{
				Admins? admin = await _context.Admins.FirstOrDefaultAsync(A => A.Username == UserName);
				if (admin != null && admin.Password == Password)
				{
					MessageBox.Show("Login Succissfuly...");
					Home home = new Home();
					this.Hide();
					home.Show();
				}
				else
				{
					MessageBox.Show("Invalid username or password.");
					UserName_TextBox.Text = null;
					Password_TextBox.Text = null;
				}
			}
		}

		private void UserName_TextBox_TextChanged(object sender, EventArgs e)
		{

		}

		private void Password_TextBox_TextChanged(object sender, EventArgs e)
		{

		}
	}
}
