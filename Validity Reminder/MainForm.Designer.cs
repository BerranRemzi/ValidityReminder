namespace Validity_Reminder
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnReload = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnReminder = new System.Windows.Forms.Button();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.listColumns = new System.Windows.Forms.ComboBox();
            this.dataGridViewExcel = new System.Windows.Forms.DataGridView();
            this.timerReminder = new System.Windows.Forms.Timer(this.components);
            this.notifyIconReminder = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuReminder = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reminderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerFirstStart = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExcel)).BeginInit();
            this.contextMenuReminder.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnReload
            // 
            this.btnReload.Location = new System.Drawing.Point(12, 12);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(75, 23);
            this.btnReload.TabIndex = 0;
            this.btnReload.Text = "Reload";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.BtnReload_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSettings.Location = new System.Drawing.Point(640, 12);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(75, 23);
            this.btnSettings.TabIndex = 1;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.BtnSettings_Click);
            // 
            // btnReminder
            // 
            this.btnReminder.Location = new System.Drawing.Point(93, 12);
            this.btnReminder.Name = "btnReminder";
            this.btnReminder.Size = new System.Drawing.Size(75, 23);
            this.btnReminder.TabIndex = 2;
            this.btnReminder.Text = "Reminder";
            this.btnReminder.UseVisualStyleBackColor = true;
            this.btnReminder.Click += new System.EventHandler(this.BtnReminder_Click);
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(301, 14);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(100, 20);
            this.txtFilter.TabIndex = 3;
            this.txtFilter.TextChanged += new System.EventHandler(this.TxtFilter_TextChanged);
            // 
            // listColumns
            // 
            this.listColumns.FormattingEnabled = true;
            this.listColumns.Location = new System.Drawing.Point(174, 14);
            this.listColumns.Name = "listColumns";
            this.listColumns.Size = new System.Drawing.Size(121, 21);
            this.listColumns.TabIndex = 4;
            this.listColumns.SelectedIndexChanged += new System.EventHandler(this.ListColumns_SelectedIndexChanged);
            // 
            // dataGridViewExcel
            // 
            this.dataGridViewExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewExcel.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewExcel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewExcel.Location = new System.Drawing.Point(12, 41);
            this.dataGridViewExcel.Name = "dataGridViewExcel";
            this.dataGridViewExcel.ReadOnly = true;
            this.dataGridViewExcel.Size = new System.Drawing.Size(703, 388);
            this.dataGridViewExcel.TabIndex = 5;
            this.dataGridViewExcel.Sorted += new System.EventHandler(this.DataGridViewExcel_Sorted);
            // 
            // timerReminder
            // 
            this.timerReminder.Enabled = true;
            this.timerReminder.Tick += new System.EventHandler(this.timerReminder_Tick);
            // 
            // notifyIconReminder
            // 
            this.notifyIconReminder.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIconReminder.ContextMenuStrip = this.contextMenuReminder;
            this.notifyIconReminder.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIconReminder.Icon")));
            this.notifyIconReminder.Text = "Validity Reminder";
            this.notifyIconReminder.Visible = true;
            this.notifyIconReminder.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIconReminder_MouseDoubleClick);
            // 
            // contextMenuReminder
            // 
            this.contextMenuReminder.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showStripMenuItem,
            this.reminderToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.exitStripMenuItem});
            this.contextMenuReminder.Name = "contextMenuStrip1";
            this.contextMenuReminder.ShowImageMargin = false;
            this.contextMenuReminder.Size = new System.Drawing.Size(101, 92);
            this.contextMenuReminder.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ContextMenuReminder_ItemClicked);
            // 
            // showStripMenuItem
            // 
            this.showStripMenuItem.Name = "showStripMenuItem";
            this.showStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.showStripMenuItem.Text = "Show";
            // 
            // reminderToolStripMenuItem
            // 
            this.reminderToolStripMenuItem.Name = "reminderToolStripMenuItem";
            this.reminderToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.reminderToolStripMenuItem.Text = "Reminder";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // exitStripMenuItem
            // 
            this.exitStripMenuItem.Name = "exitStripMenuItem";
            this.exitStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.exitStripMenuItem.Text = "Exit";
            // 
            // timerFirstStart
            // 
            this.timerFirstStart.Enabled = true;
            this.timerFirstStart.Tick += new System.EventHandler(this.timerFirstStart_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 441);
            this.Controls.Add(this.dataGridViewExcel);
            this.Controls.Add(this.listColumns);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.btnReminder);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnReload);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "MainForm";
            this.Text = "Validity Reminder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExcel)).EndInit();
            this.contextMenuReminder.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnReminder;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.ComboBox listColumns;
        private System.Windows.Forms.DataGridView dataGridViewExcel;
        private System.Windows.Forms.Timer timerReminder;
        private System.Windows.Forms.NotifyIcon notifyIconReminder;
        private System.Windows.Forms.ContextMenuStrip contextMenuReminder;
        private System.Windows.Forms.ToolStripMenuItem showStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reminderToolStripMenuItem;
        private System.Windows.Forms.Timer timerFirstStart;
    }
}

