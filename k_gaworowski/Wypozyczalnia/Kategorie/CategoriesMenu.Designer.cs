namespace Wypozyczalnia
{
    partial class CategoriesMenu
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
            this.listCategories = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listMovies = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listCategories
            // 
            this.listCategories.Font = new System.Drawing.Font("Courier New", 15.75F);
            this.listCategories.FormattingEnabled = true;
            this.listCategories.ItemHeight = 29;
            this.listCategories.Location = new System.Drawing.Point(16, 101);
            this.listCategories.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listCategories.Name = "listCategories";
            this.listCategories.Size = new System.Drawing.Size(408, 555);
            this.listCategories.TabIndex = 6;
            this.listCategories.SelectedIndexChanged += new System.EventHandler(this.listCategories_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Courier New", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(253, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(353, 67);
            this.label1.TabIndex = 5;
            this.label1.Text = "KATEGORIE";
            // 
            // listMovies
            // 
            this.listMovies.Font = new System.Drawing.Font("Courier New", 15.75F);
            this.listMovies.FormattingEnabled = true;
            this.listMovies.ItemHeight = 29;
            this.listMovies.Location = new System.Drawing.Point(458, 101);
            this.listMovies.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listMovies.Name = "listMovies";
            this.listMovies.Size = new System.Drawing.Size(437, 555);
            this.listMovies.TabIndex = 6;
            // 
            // CategoriesMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(912, 690);
            this.Controls.Add(this.listMovies);
            this.Controls.Add(this.listCategories);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "CategoriesMenu";
            this.Text = "CategoriesMenu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listCategories;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listMovies;
    }
}