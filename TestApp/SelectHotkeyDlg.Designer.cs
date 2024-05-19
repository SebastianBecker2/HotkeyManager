namespace TestApp
{
    partial class SelectHotkeyDlg
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
            BtnOK = new Button();
            BtnCancel = new Button();
            label1 = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            LblHotkey = new Label();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // BtnOK
            // 
            BtnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            BtnOK.DialogResult = DialogResult.OK;
            BtnOK.Location = new Point(136, 157);
            BtnOK.Name = "BtnOK";
            BtnOK.Size = new Size(75, 23);
            BtnOK.TabIndex = 0;
            BtnOK.TabStop = false;
            BtnOK.Text = "OK";
            BtnOK.UseVisualStyleBackColor = true;
            // 
            // BtnCancel
            // 
            BtnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            BtnCancel.DialogResult = DialogResult.Cancel;
            BtnCancel.Location = new Point(217, 157);
            BtnCancel.Name = "BtnCancel";
            BtnCancel.Size = new Size(75, 23);
            BtnCancel.TabIndex = 1;
            BtnCancel.TabStop = false;
            BtnCancel.Text = "Cancel";
            BtnCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Location = new Point(99, 27);
            label1.Name = "label1";
            label1.Size = new Size(82, 15);
            label1.TabIndex = 2;
            label1.Text = "Select Hotkey:";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(LblHotkey, 0, 1);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Location = new Point(12, 12);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(280, 139);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // LblHotkey
            // 
            LblHotkey.Anchor = AnchorStyles.None;
            LblHotkey.AutoSize = true;
            LblHotkey.Location = new Point(134, 96);
            LblHotkey.Name = "LblHotkey";
            LblHotkey.Size = new Size(12, 15);
            LblHotkey.TabIndex = 3;
            LblHotkey.Text = "-";
            // 
            // SelectHotkeyDlg
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(304, 192);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(BtnCancel);
            Controls.Add(BtnOK);
            Name = "SelectHotkeyDlg";
            Text = "SelectHotkeyDlg";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button BtnOK;
        private Button BtnCancel;
        private Label label1;
        private TableLayoutPanel tableLayoutPanel1;
        private Label LblHotkey;
    }
}