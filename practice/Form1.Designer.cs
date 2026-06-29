namespace practice
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnToday = new Button();
            btnDate = new Button();
            datePicker = new DateTimePicker();
            listBoxRates = new ListBox();
            txtFilter = new TextBox();
            btnFilter = new Button();
            SuspendLayout();
            // 
            // btnToday
            // 
            btnToday.Location = new Point(282, 13);
            btnToday.Name = "btnToday";
            btnToday.Size = new Size(94, 29);
            btnToday.TabIndex = 0;
            btnToday.Text = "Сегодня";
            btnToday.UseVisualStyleBackColor = true;
            btnToday.Click += btnToday_Click;
            // 
            // btnDate
            // 
            btnDate.Location = new Point(182, 13);
            btnDate.Name = "btnDate";
            btnDate.Size = new Size(94, 29);
            btnDate.TabIndex = 1;
            btnDate.Text = "На дату";
            btnDate.UseVisualStyleBackColor = true;
            btnDate.Click += btnDate_Click;
            // 
            // datePicker
            // 
            datePicker.Format = DateTimePickerFormat.Short;
            datePicker.Location = new Point(12, 12);
            datePicker.Name = "datePicker";
            datePicker.Size = new Size(164, 27);
            datePicker.TabIndex = 2;
            // 
            // listBoxRates
            // 
            listBoxRates.Font = new Font("Book Antiqua", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 204);
            listBoxRates.FormattingEnabled = true;
            listBoxRates.Location = new Point(12, 45);
            listBoxRates.Name = "listBoxRates";
            listBoxRates.Size = new Size(1006, 564);
            listBoxRates.TabIndex = 3;
            // 
            // txtFilter
            // 
            txtFilter.Location = new Point(411, 12);
            txtFilter.Name = "txtFilter";
            txtFilter.Size = new Size(125, 27);
            txtFilter.TabIndex = 4;
            // 
            // btnFilter
            // 
            btnFilter.Location = new Point(542, 11);
            btnFilter.Name = "btnFilter";
            btnFilter.Size = new Size(94, 29);
            btnFilter.TabIndex = 5;
            btnFilter.Text = "Фильтр";
            btnFilter.UseVisualStyleBackColor = true;
            btnFilter.Click += btnFilter_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1275, 638);
            Controls.Add(btnFilter);
            Controls.Add(txtFilter);
            Controls.Add(listBoxRates);
            Controls.Add(datePicker);
            Controls.Add(btnDate);
            Controls.Add(btnToday);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnToday;
        private Button btnDate;
        private DateTimePicker datePicker;
        private ListBox listBoxRates;
        private TextBox txtFilter;
        private Button btnFilter;
    }
}
