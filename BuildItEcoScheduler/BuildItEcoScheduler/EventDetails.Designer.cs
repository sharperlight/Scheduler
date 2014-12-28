namespace BuildItEcoScheduler
{
    partial class EventDetails
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
            this.buttonPrevious = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.buttonNext = new System.Windows.Forms.Button();
            this.labelMonth = new System.Windows.Forms.Label();
            this.textBoxCost = new System.Windows.Forms.TextBox();
            this.textBoxAddress = new System.Windows.Forms.TextBox();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.comboBoxBuilderName = new System.Windows.Forms.ComboBox();
            this.checkBoxConfirmed = new System.Windows.Forms.CheckBox();
            this.checkBoxCompleted = new System.Windows.Forms.CheckBox();
            this.labelBuilder = new System.Windows.Forms.Label();
            this.labelCost = new System.Windows.Forms.Label();
            this.labelAddress = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.buttonCU = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelYear = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonPrevious
            // 
            this.buttonPrevious.Location = new System.Drawing.Point(25, 79);
            this.buttonPrevious.Name = "buttonPrevious";
            this.buttonPrevious.Size = new System.Drawing.Size(38, 23);
            this.buttonPrevious.TabIndex = 0;
            this.buttonPrevious.Text = "<";
            this.buttonPrevious.UseVisualStyleBackColor = true;
            this.buttonPrevious.Click += new System.EventHandler(this.buttonPrevious_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(25, 108);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(272, 226);
            this.dataGridView1.TabIndex = 1;
            // 
            // buttonNext
            // 
            this.buttonNext.Location = new System.Drawing.Point(259, 79);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(38, 23);
            this.buttonNext.TabIndex = 2;
            this.buttonNext.Text = ">";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // labelMonth
            // 
            this.labelMonth.AutoSize = true;
            this.labelMonth.Location = new System.Drawing.Point(98, 84);
            this.labelMonth.Name = "labelMonth";
            this.labelMonth.Size = new System.Drawing.Size(37, 13);
            this.labelMonth.TabIndex = 3;
            this.labelMonth.Text = "Month";
            // 
            // textBoxCost
            // 
            this.textBoxCost.Location = new System.Drawing.Point(428, 164);
            this.textBoxCost.Name = "textBoxCost";
            this.textBoxCost.Size = new System.Drawing.Size(165, 20);
            this.textBoxCost.TabIndex = 4;
            // 
            // textBoxAddress
            // 
            this.textBoxAddress.Location = new System.Drawing.Point(428, 209);
            this.textBoxAddress.Name = "textBoxAddress";
            this.textBoxAddress.Size = new System.Drawing.Size(165, 20);
            this.textBoxAddress.TabIndex = 5;
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(428, 258);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(165, 20);
            this.textBoxDescription.TabIndex = 6;
            // 
            // comboBoxBuilderName
            // 
            this.comboBoxBuilderName.FormattingEnabled = true;
            this.comboBoxBuilderName.Location = new System.Drawing.Point(428, 108);
            this.comboBoxBuilderName.Name = "comboBoxBuilderName";
            this.comboBoxBuilderName.Size = new System.Drawing.Size(165, 21);
            this.comboBoxBuilderName.TabIndex = 7;
            // 
            // checkBoxConfirmed
            // 
            this.checkBoxConfirmed.AutoSize = true;
            this.checkBoxConfirmed.Location = new System.Drawing.Point(315, 317);
            this.checkBoxConfirmed.Name = "checkBoxConfirmed";
            this.checkBoxConfirmed.Size = new System.Drawing.Size(73, 17);
            this.checkBoxConfirmed.TabIndex = 8;
            this.checkBoxConfirmed.Text = "Confirmed";
            this.checkBoxConfirmed.UseVisualStyleBackColor = true;
            // 
            // checkBoxCompleted
            // 
            this.checkBoxCompleted.AutoSize = true;
            this.checkBoxCompleted.Location = new System.Drawing.Point(513, 317);
            this.checkBoxCompleted.Name = "checkBoxCompleted";
            this.checkBoxCompleted.Size = new System.Drawing.Size(76, 17);
            this.checkBoxCompleted.TabIndex = 9;
            this.checkBoxCompleted.Text = "Completed";
            this.checkBoxCompleted.UseVisualStyleBackColor = true;
            // 
            // labelBuilder
            // 
            this.labelBuilder.AutoSize = true;
            this.labelBuilder.Location = new System.Drawing.Point(312, 108);
            this.labelBuilder.Name = "labelBuilder";
            this.labelBuilder.Size = new System.Drawing.Size(39, 13);
            this.labelBuilder.TabIndex = 10;
            this.labelBuilder.Text = "Builder";
            // 
            // labelCost
            // 
            this.labelCost.AutoSize = true;
            this.labelCost.Location = new System.Drawing.Point(312, 164);
            this.labelCost.Name = "labelCost";
            this.labelCost.Size = new System.Drawing.Size(28, 13);
            this.labelCost.TabIndex = 11;
            this.labelCost.Text = "Cost";
            // 
            // labelAddress
            // 
            this.labelAddress.AutoSize = true;
            this.labelAddress.Location = new System.Drawing.Point(312, 212);
            this.labelAddress.Name = "labelAddress";
            this.labelAddress.Size = new System.Drawing.Size(45, 13);
            this.labelAddress.TabIndex = 12;
            this.labelAddress.Text = "Address";
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(312, 265);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(60, 13);
            this.labelDescription.TabIndex = 13;
            this.labelDescription.Text = "Description";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(28, 24);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(35, 13);
            this.labelName.TabIndex = 14;
            this.labelName.Text = "label6";
            // 
            // buttonCU
            // 
            this.buttonCU.Location = new System.Drawing.Point(365, 405);
            this.buttonCU.Name = "buttonCU";
            this.buttonCU.Size = new System.Drawing.Size(75, 23);
            this.buttonCU.TabIndex = 15;
            this.buttonCU.Text = "button1";
            this.buttonCU.UseVisualStyleBackColor = true;
            this.buttonCU.Click += new System.EventHandler(this.buttonCU_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(518, 405);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 16;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelYear
            // 
            this.labelYear.AutoSize = true;
            this.labelYear.Location = new System.Drawing.Point(191, 84);
            this.labelYear.Name = "labelYear";
            this.labelYear.Size = new System.Drawing.Size(29, 13);
            this.labelYear.TabIndex = 17;
            this.labelYear.Text = "Year";
            // 
            // EventDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 440);
            this.Controls.Add(this.labelYear);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonCU);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.labelAddress);
            this.Controls.Add(this.labelCost);
            this.Controls.Add(this.labelBuilder);
            this.Controls.Add(this.checkBoxCompleted);
            this.Controls.Add(this.checkBoxConfirmed);
            this.Controls.Add(this.comboBoxBuilderName);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.textBoxAddress);
            this.Controls.Add(this.textBoxCost);
            this.Controls.Add(this.labelMonth);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.buttonPrevious);
            this.Name = "EventDetails";
            this.Text = "EventDetails";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonPrevious;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Label labelMonth;
        private System.Windows.Forms.TextBox textBoxCost;
        private System.Windows.Forms.TextBox textBoxAddress;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.ComboBox comboBoxBuilderName;
        private System.Windows.Forms.CheckBox checkBoxConfirmed;
        private System.Windows.Forms.CheckBox checkBoxCompleted;
        private System.Windows.Forms.Label labelBuilder;
        private System.Windows.Forms.Label labelCost;
        private System.Windows.Forms.Label labelAddress;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Button buttonCU;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelYear;
    }
}