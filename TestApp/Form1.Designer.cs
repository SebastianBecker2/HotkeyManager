namespace TestApp
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
            components = new System.ComponentModel.Container();
            DgvHotkeys = new DataGridView();
            DgcHotkey = new DataGridViewTextBoxColumn();
            BtnRegisterHotkey = new Button();
            BtnRemoveHotkey = new Button();
            label1 = new Label();
            BtnRemoveAllHotkeys = new Button();
            TmrRowHighlight = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)DgvHotkeys).BeginInit();
            SuspendLayout();
            // 
            // DgvHotkeys
            // 
            DgvHotkeys.AllowUserToAddRows = false;
            DgvHotkeys.AllowUserToDeleteRows = false;
            DgvHotkeys.AllowUserToOrderColumns = true;
            DgvHotkeys.AllowUserToResizeColumns = false;
            DgvHotkeys.AllowUserToResizeRows = false;
            DgvHotkeys.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DgvHotkeys.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DgvHotkeys.Columns.AddRange(new DataGridViewColumn[] { DgcHotkey });
            DgvHotkeys.Location = new Point(12, 27);
            DgvHotkeys.MultiSelect = false;
            DgvHotkeys.Name = "DgvHotkeys";
            DgvHotkeys.RowHeadersVisible = false;
            DgvHotkeys.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DgvHotkeys.Size = new Size(281, 404);
            DgvHotkeys.TabIndex = 0;
            // 
            // DgcHotkey
            // 
            DgcHotkey.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DgcHotkey.HeaderText = "Hotkey";
            DgcHotkey.Name = "DgcHotkey";
            DgcHotkey.ReadOnly = true;
            // 
            // BtnRegisterHotkey
            // 
            BtnRegisterHotkey.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnRegisterHotkey.Location = new Point(299, 27);
            BtnRegisterHotkey.Name = "BtnRegisterHotkey";
            BtnRegisterHotkey.Size = new Size(127, 72);
            BtnRegisterHotkey.TabIndex = 1;
            BtnRegisterHotkey.Text = "Register Hotkey";
            BtnRegisterHotkey.UseVisualStyleBackColor = true;
            BtnRegisterHotkey.Click += BtnRegisterHotkey_Click;
            // 
            // BtnRemoveHotkey
            // 
            BtnRemoveHotkey.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnRemoveHotkey.Location = new Point(299, 105);
            BtnRemoveHotkey.Name = "BtnRemoveHotkey";
            BtnRemoveHotkey.Size = new Size(127, 72);
            BtnRemoveHotkey.TabIndex = 2;
            BtnRemoveHotkey.Text = "Remove  Hotkey";
            BtnRemoveHotkey.UseVisualStyleBackColor = true;
            BtnRemoveHotkey.Click += BtnRemoveHotkey_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(111, 15);
            label1.TabIndex = 3;
            label1.Text = "Registered Hotkeys:";
            // 
            // BtnRemoveAllHotkeys
            // 
            BtnRemoveAllHotkeys.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnRemoveAllHotkeys.Location = new Point(299, 183);
            BtnRemoveAllHotkeys.Name = "BtnRemoveAllHotkeys";
            BtnRemoveAllHotkeys.Size = new Size(127, 72);
            BtnRemoveAllHotkeys.TabIndex = 4;
            BtnRemoveAllHotkeys.Text = "Remove All Hotkeys";
            BtnRemoveAllHotkeys.UseVisualStyleBackColor = true;
            BtnRemoveAllHotkeys.Click += BtnRemoveAllHotkeys_Click;
            // 
            // TmrRowHighlight
            // 
            TmrRowHighlight.Interval = 1000;
            TmrRowHighlight.Tick += TmrRowHighlight_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(436, 443);
            Controls.Add(BtnRemoveAllHotkeys);
            Controls.Add(label1);
            Controls.Add(BtnRemoveHotkey);
            Controls.Add(BtnRegisterHotkey);
            Controls.Add(DgvHotkeys);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)DgvHotkeys).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView DgvHotkeys;
        private Button BtnRegisterHotkey;
        private Button BtnRemoveHotkey;
        private Label label1;
        private DataGridViewTextBoxColumn DgcHotkey;
        private Button BtnRemoveAllHotkeys;
        private System.Windows.Forms.Timer TmrRowHighlight;
    }
}
