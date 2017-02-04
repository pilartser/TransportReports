namespace TransportReports
{
    partial class Main
    {
                /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnRun = new System.Windows.Forms.Button();
            this.scReportListReportParam = new System.Windows.Forms.SplitContainer();
            this.tvReports = new System.Windows.Forms.TreeView();
            this.tcReportParams = new System.Windows.Forms.TabControl();
            this.tpDateActivePass = new System.Windows.Forms.TabPage();
            this.gbActivation = new System.Windows.Forms.GroupBox();
            this.lbActivationBeginDate = new System.Windows.Forms.Label();
            this.dtActivationEndDate = new System.Windows.Forms.DateTimePicker();
            this.lbActivationEndDate = new System.Windows.Forms.Label();
            this.dtActivationBeginDate = new System.Windows.Forms.DateTimePicker();
            this.gbPass = new System.Windows.Forms.GroupBox();
            this.lbPassBeginDate = new System.Windows.Forms.Label();
            this.dtPassEndDate = new System.Windows.Forms.DateTimePicker();
            this.lbPassEndDate = new System.Windows.Forms.Label();
            this.dtPassBeginDate = new System.Windows.Forms.DateTimePicker();
            this.tpPrivilege = new System.Windows.Forms.TabPage();
            this.tpTransportVehicle = new System.Windows.Forms.TabPage();
            this.tpTransportCard = new System.Windows.Forms.TabPage();
            this.tpTransaction = new System.Windows.Forms.TabPage();
            this.tpOrganisation = new System.Windows.Forms.TabPage();
            this.tpRoute = new System.Windows.Forms.TabPage();
            this.tpTerminal = new System.Windows.Forms.TabPage();
            this.tpEmpty = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.scReportListReportParam)).BeginInit();
            this.scReportListReportParam.Panel1.SuspendLayout();
            this.scReportListReportParam.Panel2.SuspendLayout();
            this.scReportListReportParam.SuspendLayout();
            this.tcReportParams.SuspendLayout();
            this.tpDateActivePass.SuspendLayout();
            this.gbActivation.SuspendLayout();
            this.gbPass.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRun
            // 
            this.btnRun.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnRun.Location = new System.Drawing.Point(0, 381);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(283, 23);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "Сформировать";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // scReportListReportParam
            // 
            this.scReportListReportParam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scReportListReportParam.Location = new System.Drawing.Point(0, 0);
            this.scReportListReportParam.Name = "scReportListReportParam";
            // 
            // scReportListReportParam.Panel1
            // 
            this.scReportListReportParam.Panel1.Controls.Add(this.tvReports);
            this.scReportListReportParam.Panel1.Controls.Add(this.btnRun);
            // 
            // scReportListReportParam.Panel2
            // 
            this.scReportListReportParam.Panel2.Controls.Add(this.tcReportParams);
            this.scReportListReportParam.Size = new System.Drawing.Size(616, 404);
            this.scReportListReportParam.SplitterDistance = 283;
            this.scReportListReportParam.TabIndex = 3;
            this.scReportListReportParam.TabStop = false;
            // 
            // tvReports
            // 
            this.tvReports.BackColor = System.Drawing.SystemColors.Control;
            this.tvReports.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvReports.Location = new System.Drawing.Point(0, 0);
            this.tvReports.Name = "tvReports";
            this.tvReports.Size = new System.Drawing.Size(283, 381);
            this.tvReports.TabIndex = 3;
            this.tvReports.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvReports_AfterSelect);
            // 
            // tcReportParams
            // 
            this.tcReportParams.Controls.Add(this.tpDateActivePass);
            this.tcReportParams.Controls.Add(this.tpPrivilege);
            this.tcReportParams.Controls.Add(this.tpTransportVehicle);
            this.tcReportParams.Controls.Add(this.tpTransportCard);
            this.tcReportParams.Controls.Add(this.tpTransaction);
            this.tcReportParams.Controls.Add(this.tpOrganisation);
            this.tcReportParams.Controls.Add(this.tpRoute);
            this.tcReportParams.Controls.Add(this.tpTerminal);
            this.tcReportParams.Controls.Add(this.tpEmpty);
            this.tcReportParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcReportParams.ItemSize = new System.Drawing.Size(0, 1);
            this.tcReportParams.Location = new System.Drawing.Point(0, 0);
            this.tcReportParams.Multiline = true;
            this.tcReportParams.Name = "tcReportParams";
            this.tcReportParams.Padding = new System.Drawing.Point(1, 1);
            this.tcReportParams.SelectedIndex = 0;
            this.tcReportParams.Size = new System.Drawing.Size(329, 404);
            this.tcReportParams.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tcReportParams.TabIndex = 3;
            this.tcReportParams.TabStop = false;
            // 
            // tpDateActivePass
            // 
            this.tpDateActivePass.Controls.Add(this.gbActivation);
            this.tpDateActivePass.Controls.Add(this.gbPass);
            this.tpDateActivePass.Location = new System.Drawing.Point(4, 5);
            this.tpDateActivePass.Name = "tpDateActivePass";
            this.tpDateActivePass.Padding = new System.Windows.Forms.Padding(3);
            this.tpDateActivePass.Size = new System.Drawing.Size(321, 395);
            this.tpDateActivePass.TabIndex = 0;
            this.tpDateActivePass.UseVisualStyleBackColor = true;
            // 
            // gbActivation
            // 
            this.gbActivation.Controls.Add(this.lbActivationBeginDate);
            this.gbActivation.Controls.Add(this.dtActivationEndDate);
            this.gbActivation.Controls.Add(this.lbActivationEndDate);
            this.gbActivation.Controls.Add(this.dtActivationBeginDate);
            this.gbActivation.Location = new System.Drawing.Point(12, 18);
            this.gbActivation.Margin = new System.Windows.Forms.Padding(9, 15, 3, 3);
            this.gbActivation.Name = "gbActivation";
            this.gbActivation.Size = new System.Drawing.Size(284, 132);
            this.gbActivation.TabIndex = 1;
            this.gbActivation.TabStop = false;
            this.gbActivation.Text = "Активация карты (включительно)";
            // 
            // lbActivationBeginDate
            // 
            this.lbActivationBeginDate.AutoSize = true;
            this.lbActivationBeginDate.Location = new System.Drawing.Point(6, 26);
            this.lbActivationBeginDate.Name = "lbActivationBeginDate";
            this.lbActivationBeginDate.Size = new System.Drawing.Size(130, 13);
            this.lbActivationBeginDate.TabIndex = 6;
            this.lbActivationBeginDate.Text = "Дата начала активации:";
            // 
            // dtActivationEndDate
            // 
            this.dtActivationEndDate.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            this.dtActivationEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtActivationEndDate.Location = new System.Drawing.Point(9, 96);
            this.dtActivationEndDate.Name = "dtActivationEndDate";
            this.dtActivationEndDate.Size = new System.Drawing.Size(200, 20);
            this.dtActivationEndDate.TabIndex = 4;
            // 
            // lbActivationEndDate
            // 
            this.lbActivationEndDate.AutoSize = true;
            this.lbActivationEndDate.Location = new System.Drawing.Point(6, 76);
            this.lbActivationEndDate.Name = "lbActivationEndDate";
            this.lbActivationEndDate.Size = new System.Drawing.Size(148, 13);
            this.lbActivationEndDate.TabIndex = 7;
            this.lbActivationEndDate.Text = "Дата окончания активации:";
            // 
            // dtActivationBeginDate
            // 
            this.dtActivationBeginDate.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            this.dtActivationBeginDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtActivationBeginDate.Location = new System.Drawing.Point(9, 46);
            this.dtActivationBeginDate.Name = "dtActivationBeginDate";
            this.dtActivationBeginDate.Size = new System.Drawing.Size(200, 20);
            this.dtActivationBeginDate.TabIndex = 3;
            // 
            // gbPass
            // 
            this.gbPass.Controls.Add(this.lbPassBeginDate);
            this.gbPass.Controls.Add(this.dtPassEndDate);
            this.gbPass.Controls.Add(this.lbPassEndDate);
            this.gbPass.Controls.Add(this.dtPassBeginDate);
            this.gbPass.Location = new System.Drawing.Point(12, 168);
            this.gbPass.Margin = new System.Windows.Forms.Padding(9, 15, 3, 3);
            this.gbPass.Name = "gbPass";
            this.gbPass.Size = new System.Drawing.Size(284, 132);
            this.gbPass.TabIndex = 2;
            this.gbPass.TabStop = false;
            this.gbPass.Text = "Проезд по картам (начальная дата включительно)";
            // 
            // lbPassBeginDate
            // 
            this.lbPassBeginDate.AutoSize = true;
            this.lbPassBeginDate.Location = new System.Drawing.Point(6, 26);
            this.lbPassBeginDate.Name = "lbPassBeginDate";
            this.lbPassBeginDate.Size = new System.Drawing.Size(172, 13);
            this.lbPassBeginDate.TabIndex = 8;
            this.lbPassBeginDate.Text = "Дата начала отчетного периода:";
            // 
            // dtPassEndDate
            // 
            this.dtPassEndDate.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            this.dtPassEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtPassEndDate.Location = new System.Drawing.Point(9, 96);
            this.dtPassEndDate.Name = "dtPassEndDate";
            this.dtPassEndDate.Size = new System.Drawing.Size(200, 20);
            this.dtPassEndDate.TabIndex = 6;
            // 
            // lbPassEndDate
            // 
            this.lbPassEndDate.AutoSize = true;
            this.lbPassEndDate.Location = new System.Drawing.Point(6, 76);
            this.lbPassEndDate.Name = "lbPassEndDate";
            this.lbPassEndDate.Size = new System.Drawing.Size(190, 13);
            this.lbPassEndDate.TabIndex = 9;
            this.lbPassEndDate.Text = "Дата окончания отчетного периода:";
            // 
            // dtPassBeginDate
            // 
            this.dtPassBeginDate.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            this.dtPassBeginDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtPassBeginDate.Location = new System.Drawing.Point(9, 46);
            this.dtPassBeginDate.Name = "dtPassBeginDate";
            this.dtPassBeginDate.Size = new System.Drawing.Size(200, 20);
            this.dtPassBeginDate.TabIndex = 5;
            // 
            // tpPrivilege
            // 
            this.tpPrivilege.Location = new System.Drawing.Point(4, 5);
            this.tpPrivilege.Name = "tpPrivilege";
            this.tpPrivilege.Padding = new System.Windows.Forms.Padding(3);
            this.tpPrivilege.Size = new System.Drawing.Size(320, 395);
            this.tpPrivilege.TabIndex = 1;
            this.tpPrivilege.UseVisualStyleBackColor = true;
            // 
            // tpTransportVehicle
            // 
            this.tpTransportVehicle.Location = new System.Drawing.Point(4, 5);
            this.tpTransportVehicle.Name = "tpTransportVehicle";
            this.tpTransportVehicle.Padding = new System.Windows.Forms.Padding(3);
            this.tpTransportVehicle.Size = new System.Drawing.Size(320, 395);
            this.tpTransportVehicle.TabIndex = 2;
            this.tpTransportVehicle.UseVisualStyleBackColor = true;
            // 
            // tpTransportCard
            // 
            this.tpTransportCard.Location = new System.Drawing.Point(4, 5);
            this.tpTransportCard.Name = "tpTransportCard";
            this.tpTransportCard.Padding = new System.Windows.Forms.Padding(3);
            this.tpTransportCard.Size = new System.Drawing.Size(320, 395);
            this.tpTransportCard.TabIndex = 3;
            this.tpTransportCard.UseVisualStyleBackColor = true;
            // 
            // tpTransaction
            // 
            this.tpTransaction.Location = new System.Drawing.Point(4, 5);
            this.tpTransaction.Name = "tpTransaction";
            this.tpTransaction.Padding = new System.Windows.Forms.Padding(3);
            this.tpTransaction.Size = new System.Drawing.Size(320, 395);
            this.tpTransaction.TabIndex = 4;
            this.tpTransaction.UseVisualStyleBackColor = true;
            // 
            // tpOrganisation
            // 
            this.tpOrganisation.Location = new System.Drawing.Point(4, 5);
            this.tpOrganisation.Name = "tpOrganisation";
            this.tpOrganisation.Padding = new System.Windows.Forms.Padding(3);
            this.tpOrganisation.Size = new System.Drawing.Size(320, 395);
            this.tpOrganisation.TabIndex = 5;
            this.tpOrganisation.UseVisualStyleBackColor = true;
            // 
            // tpRoute
            // 
            this.tpRoute.Location = new System.Drawing.Point(4, 5);
            this.tpRoute.Name = "tpRoute";
            this.tpRoute.Padding = new System.Windows.Forms.Padding(3);
            this.tpRoute.Size = new System.Drawing.Size(320, 395);
            this.tpRoute.TabIndex = 6;
            this.tpRoute.UseVisualStyleBackColor = true;
            // 
            // tpTerminal
            // 
            this.tpTerminal.Location = new System.Drawing.Point(4, 5);
            this.tpTerminal.Name = "tpTerminal";
            this.tpTerminal.Padding = new System.Windows.Forms.Padding(3);
            this.tpTerminal.Size = new System.Drawing.Size(320, 395);
            this.tpTerminal.TabIndex = 7;
            this.tpTerminal.UseVisualStyleBackColor = true;
            // 
            // tpEmpty
            // 
            this.tpEmpty.Location = new System.Drawing.Point(4, 5);
            this.tpEmpty.Name = "tpEmpty";
            this.tpEmpty.Padding = new System.Windows.Forms.Padding(3);
            this.tpEmpty.Size = new System.Drawing.Size(320, 395);
            this.tpEmpty.TabIndex = 8;
            this.tpEmpty.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 404);
            this.Controls.Add(this.scReportListReportParam);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Формирование отчетов";
            this.Load += new System.EventHandler(this.Main_Load);
            this.scReportListReportParam.Panel1.ResumeLayout(false);
            this.scReportListReportParam.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scReportListReportParam)).EndInit();
            this.scReportListReportParam.ResumeLayout(false);
            this.tcReportParams.ResumeLayout(false);
            this.tpDateActivePass.ResumeLayout(false);
            this.gbActivation.ResumeLayout(false);
            this.gbActivation.PerformLayout();
            this.gbPass.ResumeLayout(false);
            this.gbPass.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.SplitContainer scReportListReportParam;
        private System.Windows.Forms.TreeView tvReports;
        private System.Windows.Forms.TabControl tcReportParams;
        private System.Windows.Forms.TabPage tpDateActivePass;
        private System.Windows.Forms.GroupBox gbActivation;
        private System.Windows.Forms.Label lbActivationBeginDate;
        private System.Windows.Forms.DateTimePicker dtActivationEndDate;
        private System.Windows.Forms.Label lbActivationEndDate;
        private System.Windows.Forms.DateTimePicker dtActivationBeginDate;
        private System.Windows.Forms.GroupBox gbPass;
        private System.Windows.Forms.Label lbPassBeginDate;
        private System.Windows.Forms.DateTimePicker dtPassEndDate;
        private System.Windows.Forms.Label lbPassEndDate;
        private System.Windows.Forms.DateTimePicker dtPassBeginDate;
        private System.Windows.Forms.TabPage tpPrivilege;
        private System.Windows.Forms.TabPage tpRoute;
        private System.Windows.Forms.TabPage tpOrganisation;
        private System.Windows.Forms.TabPage tpTerminal;
        private System.Windows.Forms.TabPage tpTransaction;
        private System.Windows.Forms.TabPage tpTransportCard;
        private System.Windows.Forms.TabPage tpTransportVehicle;
        private System.Windows.Forms.TabPage tpEmpty;
    }
}