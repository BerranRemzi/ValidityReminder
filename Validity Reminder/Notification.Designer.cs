namespace Validity_Reminder
{
    partial class Notification
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
            this.lblExpiration = new System.Windows.Forms.Label();
            this.dataGridViewExcel = new System.Windows.Forms.DataGridView();
            this.btnSnooze = new System.Windows.Forms.Button();
            this.listSnooze = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.timerReminder = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExcel)).BeginInit();
            this.SuspendLayout();
            // 
            // lblExpiration
            // 
            this.lblExpiration.AutoSize = true;
            this.lblExpiration.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblExpiration.Location = new System.Drawing.Point(12, 15);
            this.lblExpiration.Name = "lblExpiration";
            this.lblExpiration.Size = new System.Drawing.Size(128, 16);
            this.lblExpiration.TabIndex = 0;
            this.lblExpiration.Text = "Expire in XX days";
            // 
            // dataGridViewExcel
            // 
            this.dataGridViewExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewExcel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewExcel.Location = new System.Drawing.Point(12, 41);
            this.dataGridViewExcel.Name = "dataGridViewExcel";
            this.dataGridViewExcel.Size = new System.Drawing.Size(768, 438);
            this.dataGridViewExcel.TabIndex = 1;
            this.dataGridViewExcel.Sorted += new System.EventHandler(this.dataGridViewExcel_Sorted);
            // 
            // btnSnooze
            // 
            this.btnSnooze.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSnooze.Location = new System.Drawing.Point(706, 12);
            this.btnSnooze.Name = "btnSnooze";
            this.btnSnooze.Size = new System.Drawing.Size(75, 23);
            this.btnSnooze.TabIndex = 3;
            this.btnSnooze.Text = "Snooze";
            this.btnSnooze.UseVisualStyleBackColor = true;
            this.btnSnooze.Click += new System.EventHandler(this.button2_Click);
            // 
            // listSnooze
            // 
            this.listSnooze.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.listSnooze.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.listSnooze.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.listSnooze.Location = new System.Drawing.Point(643, 14);
            this.listSnooze.Name = "listSnooze";
            this.listSnooze.Size = new System.Drawing.Size(57, 21);
            this.listSnooze.TabIndex = 4;
            this.listSnooze.SelectedIndexChanged += new System.EventHandler(this.ListSnooze_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(579, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Snooze for";
            // 
            // timerReminder
            // 
            this.timerReminder.Enabled = true;
            this.timerReminder.Tick += new System.EventHandler(this.TimerReminder_Tick);
            // 
            // Notification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(793, 491);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listSnooze);
            this.Controls.Add(this.btnSnooze);
            this.Controls.Add(this.dataGridViewExcel);
            this.Controls.Add(this.lblExpiration);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Notification";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Notification";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Notification_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExcel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblExpiration;
        private System.Windows.Forms.DataGridView dataGridViewExcel;
        private System.Windows.Forms.Button btnSnooze;
        private System.Windows.Forms.ComboBox listSnooze;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timerReminder;
    }
}