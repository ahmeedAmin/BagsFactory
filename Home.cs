//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace Factory
//{
//	public partial class Home : Form
//	{
//		public Home()
//		{
//			InitializeComponent();
//		}
//		bool BrandExpand = false;
//		bool ModelExpand = false;
//		bool OrderExpand = false;
//		private void BrandTransition_Tick(object sender, EventArgs e)
//		{
//			if (BrandExpand == false)
//			{
//				BrandContainar.Height += 10;
//				if (BrandContainar.Height >= 154)
//				{
//					BrandTransition.Stop();
//					BrandExpand = true;
//				}
//			}
//			else
//			{
//				BrandContainar.Height -= 10;
//				if (BrandContainar.Height <= 53)
//				{
//					BrandTransition.Stop();
//					BrandExpand = false;
//				}
//			}
//		}

//		private void Button_Brand_Click(object sender, EventArgs e)
//		{
//			BrandTransition.Start();
//		}

//		private void ModelTransition_Tick(object sender, EventArgs e)
//		{
//			if (ModelExpand == false)
//			{
//				ModelContainar.Height = 154;
//				if (ModelContainar.Height >= 154)
//				{
//					ModelTransition.Stop();
//					ModelExpand = true;
//				}
//			}
//			else
//			{
//				ModelContainar.Height = 53;
//				if (ModelContainar.Height <= 53)
//				{
//					ModelTransition.Stop();
//					ModelExpand = false;
//				}
//			}
//		}

//		private void Button_Model_Click(object sender, EventArgs e)
//		{
//			ModelTransition.Start();
//		}

//		private void OrderTranition_Tick(object sender, EventArgs e)
//		{
//			if (OrderExpand == false)
//			{
//				OrderContainar.Height = 154;
//				if (OrderContainar.Height >= 154)
//				{
//					OrderTranition.Stop();
//					OrderExpand = true;
//				}
//			}
//			else
//			{
//				OrderContainar.Height = 53;
//				if (OrderContainar.Height <= 53)
//				{
//					OrderTranition.Stop();
//					OrderExpand = false;
//				}
//			}
//		}

//		private void Button_Order_Click(object sender, EventArgs e)
//		{
//			OrderTranition.Start();
//		}
//	}
//}

using System;
using System.Windows.Forms;

namespace Factory
{
	public partial class Home : Form
	{
		AddBrand AddB;
		AddModel AddM;
		AddOrder AddO;
		public Home()
		{
			InitializeComponent();
		}

		private bool isBrandExpanded = false;
		private bool isModelExpanded = false;
		private bool isOrderExpanded = false;

		private void BrandTransition_Tick(object sender, EventArgs e)
		{
			ToggleContainer(BrandContainar, ref isBrandExpanded);
		}

		private void Button_Brand_Click(object sender, EventArgs e)
		{
			BrandTransition.Start();
		}

		private void ModelTransition_Tick(object sender, EventArgs e)
		{
			ToggleContainer(ModelContainar, ref isModelExpanded);
		}

		private void Button_Model_Click(object sender, EventArgs e)
		{
			ModelTransition.Start();
		}

		private void OrderTranition_Tick(object sender, EventArgs e)
		{
			ToggleContainer(OrderContainar, ref isOrderExpanded);
		}

		private void Button_Order_Click(object sender, EventArgs e)
		{
			OrderTranition.Start();
		}

		private void ToggleContainer(Panel container, ref bool isExpanded)
		{
			if (!isExpanded)
			{
				container.Height = 155;
				if (container.Height >= 155)
				{
					container.Height = 155; // ضبط الارتفاع النهائي
					isExpanded = true;
					GetTimerByContainer(container).Stop();
				}
			}
			else
			{
				container.Height = 50;
				if (container.Height <= 50)
				{
					container.Height = 50; // ضبط الارتفاع النهائي
					isExpanded = false;
					GetTimerByContainer(container).Stop();
				}
			}
		}

		private System.Windows.Forms.Timer GetTimerByContainer(Panel container)
		{
			if (container == BrandContainar) return BrandTransition;
			if (container == ModelContainar) return ModelTransition;
			if (container == OrderContainar) return OrderTranition;
			return null;
		}

		private void Form_FormClosed<T>(object? sender, FormClosedEventArgs e, ref T formReference) where T : class
		{
			formReference = null;
		}

		private void Button_AddBrand_Click(object sender, EventArgs e)
		{
			if (AddB == null)
			{
				AddB = new AddBrand();
				AddB.FormClosed += (s, e) => Form_FormClosed(s, e, ref AddB);
				AddB.MdiParent = this;
				AddB.Dock = DockStyle.Fill;
				AddB.Show();
			}
			else
			{
				AddB.Activate();
			}
		}

		private void Button_AddModel_Click(object sender, EventArgs e)
		{
			if (AddM == null)
			{
				AddM = new AddModel();
				AddM.FormClosed += (s, e) => Form_FormClosed(s, e, ref AddM);
				AddM.MdiParent = this;
				AddM.Dock = DockStyle.Fill;
				AddM.Show();
			}
			else
			{
				AddM.Activate();
			}
		}

		private void Button_AddOrder_Click(object sender, EventArgs e)
		{
			if (AddO == null)
			{
				AddO = new AddOrder();
				AddO.FormClosed += (s, e) => Form_FormClosed(s, e, ref AddO);
				AddO.MdiParent = this;
				AddO.Dock = DockStyle.Fill;
				AddO.Show();
			}
			else
			{
				AddO.Activate();
			}
		}
	}
}

