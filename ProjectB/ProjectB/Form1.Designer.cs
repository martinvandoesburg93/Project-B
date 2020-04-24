using System.Windows.Forms;

namespace ProjectB
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.Page = new ProjectB.Pages.Home();
            this.Homebar = new ProjectB.UserControlles.HomeBar();
            this.inlog1 = new ProjectB.UserControlles.Inlog();
            this.menu1 = new ProjectB.UserControlles.Menu();
            this.SuspendLayout();
            // 
            // Page
            // 
            this.Page.BackColor = System.Drawing.SystemColors.Control;
            this.Page.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Page.Location = new System.Drawing.Point(0, 75);
            this.Page.Name = "Page";
            this.Page.Size = new System.Drawing.Size(1280, 645);
            this.Page.TabIndex = 1;
            // 
            // Homebar
            // 
            this.Homebar.Dock = System.Windows.Forms.DockStyle.Top;
            this.Homebar.Location = new System.Drawing.Point(0, 0);
            this.Homebar.Name = "Homebar";
            this.Homebar.Size = new System.Drawing.Size(1280, 75);
            this.Homebar.TabIndex = 0;
            this.Homebar.Load += new System.EventHandler(this.Homebar_Load_1);
            // 
            // inlog1
            // 
            this.inlog1.BackColor = System.Drawing.SystemColors.Control;
            this.inlog1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inlog1.Enabled = false;
            this.inlog1.Location = new System.Drawing.Point(0, 75);
            this.inlog1.Name = "inlog1";
            this.inlog1.Size = new System.Drawing.Size(1280, 645);
            this.inlog1.TabIndex = 2;
            this.inlog1.Visible = false;
            this.inlog1.Load += new System.EventHandler(this.Inlog1_Load);
            // 
            // menu1
            // 
            this.menu1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menu1.Enabled = false;
            this.menu1.Location = new System.Drawing.Point(0, 75);
            this.menu1.Name = "menu1";
            this.menu1.Size = new System.Drawing.Size(1280, 645);
            this.menu1.TabIndex = 3;
            this.menu1.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.menu1);
            this.Controls.Add(this.inlog1);
            this.Controls.Add(this.Page);
            this.Controls.Add(this.Homebar);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);

		}

		#endregion

		private UserControlles.HomeBar Homebar;
        private UserControlles.Inlog inlog1;
        private Pages.Home Page;
        private UserControlles.Menu menu1;
    }
}

