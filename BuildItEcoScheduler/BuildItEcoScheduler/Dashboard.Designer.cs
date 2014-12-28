namespace BuildItEcoScheduler
{
    partial class Dashboard
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
            this.DashTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.BuilderButton = new System.Windows.Forms.Button();
            this.ContractorButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DashTableLayout
            // 
            this.DashTableLayout.ColumnCount = 2;
            this.DashTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.DashTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.DashTableLayout.Location = new System.Drawing.Point(13, 62);
            this.DashTableLayout.Name = "DashTableLayout";
            this.DashTableLayout.RowCount = 2;
            this.DashTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.DashTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.DashTableLayout.Size = new System.Drawing.Size(336, 88);
            this.DashTableLayout.TabIndex = 0;
            // 
            // BuilderButton
            // 
            this.BuilderButton.Location = new System.Drawing.Point(170, 13);
            this.BuilderButton.Name = "BuilderButton";
            this.BuilderButton.Size = new System.Drawing.Size(75, 23);
            this.BuilderButton.TabIndex = 1;
            this.BuilderButton.Text = "Builder";
            this.BuilderButton.UseVisualStyleBackColor = true;
            // 
            // ContractorButton
            // 
            this.ContractorButton.Location = new System.Drawing.Point(274, 13);
            this.ContractorButton.Name = "ContractorButton";
            this.ContractorButton.Size = new System.Drawing.Size(75, 23);
            this.ContractorButton.TabIndex = 2;
            this.ContractorButton.Text = "Contractor";
            this.ContractorButton.UseVisualStyleBackColor = true;
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 777);
            this.Controls.Add(this.ContractorButton);
            this.Controls.Add(this.BuilderButton);
            this.Controls.Add(this.DashTableLayout);
            this.Name = "Dashboard";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Dashboard_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel DashTableLayout;
        private System.Windows.Forms.Button BuilderButton;
        private System.Windows.Forms.Button ContractorButton;
    }
}

