using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ProjectB.Data;
using ProjectB.Pages;
using ProjectB.UserControlles;

namespace ProjectB
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			Homebar.InlogClicked += (obj, e) => Switch(inlog1);
			Homebar.HomeButtonClicked += (obj, e) => Switch(Page);
			Homebar.MenuButtonClicked += (obj, e) => Switch(menu1);
		}

		private void Switch(Control con)
		{
			foreach (Control child in Controls)
			{
				if (child is HomeBar)
					continue;
				if (child == con)
				{
					child.Show();
					child.Enabled = true;
					continue;
				}
				child.Enabled = false;
				child.Hide();
			}
		}

		private void Homebar_Load(object sender, EventArgs e)
		{

		}

		private void Homebar_Load_1(object sender, EventArgs e)
		{

		}

		private void Inlog1_Load(object sender, EventArgs e)
		{

		}
	}
}
