namespace ColeBrightSchedulingAppC969
{
    partial class Main
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
            this.btnExit = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btMonthApp = new System.Windows.Forms.Button();
            this.dgvAppts = new System.Windows.Forms.DataGridView();
            this.btnAddAppt = new System.Windows.Forms.Button();
            this.btnModifyAppt = new System.Windows.Forms.Button();
            this.btnDeleteAppt = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btSchedule = new System.Windows.Forms.Button();
            this.dgvCustomers = new System.Windows.Forms.DataGridView();
            this.btnAddCust = new System.Windows.Forms.Button();
            this.btnModifyCust = new System.Windows.Forms.Button();
            this.btnDeleteCust = new System.Windows.Forms.Button();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.rbWeek = new System.Windows.Forms.RadioButton();
            this.rbMonth = new System.Windows.Forms.RadioButton();
            this.btLoginRec = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppts)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(1123, 372);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 31);
            this.btnExit.TabIndex = 18;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btMonthApp);
            this.groupBox2.Controls.Add(this.dgvAppts);
            this.groupBox2.Controls.Add(this.btnAddAppt);
            this.groupBox2.Controls.Add(this.btnModifyAppt);
            this.groupBox2.Controls.Add(this.btnDeleteAppt);
            this.groupBox2.Location = new System.Drawing.Point(780, 60);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(429, 295);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Appointments";
            // 
            // btMonthApp
            // 
            this.btMonthApp.Location = new System.Drawing.Point(262, 241);
            this.btMonthApp.Name = "btMonthApp";
            this.btMonthApp.Size = new System.Drawing.Size(168, 48);
            this.btMonthApp.TabIndex = 11;
            this.btMonthApp.Text = "Number of Appointment Types By Month";
            this.btMonthApp.UseVisualStyleBackColor = true;
            this.btMonthApp.Click += new System.EventHandler(this.btMonthApp_Click);
            // 
            // dgvAppts
            // 
            this.dgvAppts.AllowUserToAddRows = false;
            this.dgvAppts.AllowUserToDeleteRows = false;
            this.dgvAppts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAppts.Location = new System.Drawing.Point(16, 74);
            this.dgvAppts.Name = "dgvAppts";
            this.dgvAppts.ReadOnly = true;
            this.dgvAppts.RowHeadersWidth = 51;
            this.dgvAppts.RowTemplate.Height = 24;
            this.dgvAppts.Size = new System.Drawing.Size(413, 150);
            this.dgvAppts.TabIndex = 1;
            this.dgvAppts.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvAppts_CellClick);
            // 
            // btnAddAppt
            // 
            this.btnAddAppt.Location = new System.Drawing.Point(16, 241);
            this.btnAddAppt.Name = "btnAddAppt";
            this.btnAddAppt.Size = new System.Drawing.Size(75, 31);
            this.btnAddAppt.TabIndex = 10;
            this.btnAddAppt.Text = "Add";
            this.btnAddAppt.UseVisualStyleBackColor = true;
            this.btnAddAppt.Click += new System.EventHandler(this.BtnAddAppt_Click);
            // 
            // btnModifyAppt
            // 
            this.btnModifyAppt.Location = new System.Drawing.Point(97, 241);
            this.btnModifyAppt.Name = "btnModifyAppt";
            this.btnModifyAppt.Size = new System.Drawing.Size(75, 31);
            this.btnModifyAppt.TabIndex = 12;
            this.btnModifyAppt.Text = "Modify";
            this.btnModifyAppt.UseVisualStyleBackColor = true;
            this.btnModifyAppt.Click += new System.EventHandler(this.BtnModifyAppt_Click);
            // 
            // btnDeleteAppt
            // 
            this.btnDeleteAppt.Location = new System.Drawing.Point(181, 241);
            this.btnDeleteAppt.Name = "btnDeleteAppt";
            this.btnDeleteAppt.Size = new System.Drawing.Size(75, 31);
            this.btnDeleteAppt.TabIndex = 11;
            this.btnDeleteAppt.Text = "Delete";
            this.btnDeleteAppt.UseVisualStyleBackColor = true;
            this.btnDeleteAppt.Click += new System.EventHandler(this.BtnDeleteAppt_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(318, 38);
            this.label1.TabIndex = 16;
            this.label1.Text = "Appointment Manager";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btSchedule);
            this.groupBox1.Controls.Add(this.dgvCustomers);
            this.groupBox1.Controls.Add(this.btnAddCust);
            this.groupBox1.Controls.Add(this.btnModifyCust);
            this.groupBox1.Controls.Add(this.btnDeleteCust);
            this.groupBox1.Location = new System.Drawing.Point(9, 53);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(445, 302);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Customers";
            // 
            // btSchedule
            // 
            this.btSchedule.Location = new System.Drawing.Point(254, 248);
            this.btSchedule.Name = "btSchedule";
            this.btSchedule.Size = new System.Drawing.Size(168, 31);
            this.btSchedule.TabIndex = 10;
            this.btSchedule.Text = "Consultant Schedule";
            this.btSchedule.UseVisualStyleBackColor = true;
            this.btSchedule.Click += new System.EventHandler(this.btSchedule_Click);
            // 
            // dgvCustomers
            // 
            this.dgvCustomers.AllowUserToAddRows = false;
            this.dgvCustomers.AllowUserToDeleteRows = false;
            this.dgvCustomers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCustomers.Location = new System.Drawing.Point(0, 68);
            this.dgvCustomers.Name = "dgvCustomers";
            this.dgvCustomers.ReadOnly = true;
            this.dgvCustomers.RowHeadersWidth = 51;
            this.dgvCustomers.RowTemplate.Height = 24;
            this.dgvCustomers.Size = new System.Drawing.Size(433, 150);
            this.dgvCustomers.TabIndex = 0;
            this.dgvCustomers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvCustomers_CellContentClick);
            // 
            // btnAddCust
            // 
            this.btnAddCust.Location = new System.Drawing.Point(1, 248);
            this.btnAddCust.Name = "btnAddCust";
            this.btnAddCust.Size = new System.Drawing.Size(75, 31);
            this.btnAddCust.TabIndex = 7;
            this.btnAddCust.Text = "Add";
            this.btnAddCust.UseVisualStyleBackColor = true;
            this.btnAddCust.Click += new System.EventHandler(this.BtnAddCust_Click);
            // 
            // btnModifyCust
            // 
            this.btnModifyCust.Location = new System.Drawing.Point(85, 248);
            this.btnModifyCust.Name = "btnModifyCust";
            this.btnModifyCust.Size = new System.Drawing.Size(75, 31);
            this.btnModifyCust.TabIndex = 8;
            this.btnModifyCust.Text = "Modify";
            this.btnModifyCust.UseVisualStyleBackColor = true;
            this.btnModifyCust.Click += new System.EventHandler(this.BtnModifyCust_Click);
            // 
            // btnDeleteCust
            // 
            this.btnDeleteCust.Location = new System.Drawing.Point(173, 248);
            this.btnDeleteCust.Name = "btnDeleteCust";
            this.btnDeleteCust.Size = new System.Drawing.Size(75, 31);
            this.btnDeleteCust.TabIndex = 9;
            this.btnDeleteCust.Text = "Delete";
            this.btnDeleteCust.UseVisualStyleBackColor = true;
            this.btnDeleteCust.Click += new System.EventHandler(this.BtnDeleteCust_Click);
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(466, 93);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 19;
            this.monthCalendar1.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateChanged);
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.Checked = true;
            this.rbAll.Location = new System.Drawing.Point(466, 33);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(233, 21);
            this.rbAll.TabIndex = 20;
            this.rbAll.TabStop = true;
            this.rbAll.Text = "All Appointments and Customers";
            this.rbAll.UseVisualStyleBackColor = true;
            this.rbAll.CheckedChanged += new System.EventHandler(this.RbAll_CheckedChanged);
            // 
            // rbWeek
            // 
            this.rbWeek.AutoSize = true;
            this.rbWeek.Location = new System.Drawing.Point(466, 60);
            this.rbWeek.Name = "rbWeek";
            this.rbWeek.Size = new System.Drawing.Size(98, 21);
            this.rbWeek.TabIndex = 21;
            this.rbWeek.Text = "Week View";
            this.rbWeek.UseVisualStyleBackColor = true;
            this.rbWeek.CheckedChanged += new System.EventHandler(this.RbWeek_CheckedChanged);
            this.rbWeek.Click += new System.EventHandler(this.rbWeek_Click);
            // 
            // rbMonth
            // 
            this.rbMonth.AutoSize = true;
            this.rbMonth.Location = new System.Drawing.Point(627, 60);
            this.rbMonth.Name = "rbMonth";
            this.rbMonth.Size = new System.Drawing.Size(101, 21);
            this.rbMonth.TabIndex = 22;
            this.rbMonth.Text = "Month View";
            this.rbMonth.UseVisualStyleBackColor = true;
            this.rbMonth.CheckedChanged += new System.EventHandler(this.RbMonth_CheckedChanged);
            this.rbMonth.Click += new System.EventHandler(this.rbMonth_Click);
            // 
            // btLoginRec
            // 
            this.btLoginRec.Location = new System.Drawing.Point(12, 372);
            this.btLoginRec.Name = "btLoginRec";
            this.btLoginRec.Size = new System.Drawing.Size(157, 31);
            this.btLoginRec.TabIndex = 23;
            this.btLoginRec.Text = "User Login Records";
            this.btLoginRec.UseVisualStyleBackColor = true;
            this.btLoginRec.Click += new System.EventHandler(this.BtLoginRec_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1221, 415);
            this.Controls.Add(this.btLoginRec);
            this.Controls.Add(this.rbMonth);
            this.Controls.Add(this.rbWeek);
            this.Controls.Add(this.rbAll);
            this.Controls.Add(this.monthCalendar1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Main";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppts)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvAppts;
        private System.Windows.Forms.Button btnAddAppt;
        private System.Windows.Forms.Button btnModifyAppt;
        private System.Windows.Forms.Button btnDeleteAppt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvCustomers;
        private System.Windows.Forms.Button btnAddCust;
        private System.Windows.Forms.Button btnModifyCust;
        private System.Windows.Forms.Button btnDeleteCust;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.RadioButton rbWeek;
        private System.Windows.Forms.RadioButton rbMonth;
        private System.Windows.Forms.Button btMonthApp;
        private System.Windows.Forms.Button btSchedule;
        private System.Windows.Forms.Button btLoginRec;
    }
}