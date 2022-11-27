﻿namespace ChocolateySpreader
{
    public partial class Form1
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
            this.ISOPathLabel = new System.Windows.Forms.Label();
            this.FolderPathLabel = new System.Windows.Forms.Label();
            this.FolderPathBox = new System.Windows.Forms.TextBox();
            this.ISOPathBox = new System.Windows.Forms.TextBox();
            this.ISOSelectButton = new System.Windows.Forms.Button();
            this.OutputFolderSelectButton = new System.Windows.Forms.Button();
            this.ExtractISOButton = new System.Windows.Forms.Button();
            this.OutputBox = new System.Windows.Forms.RichTextBox();
            this.OutputLabel = new System.Windows.Forms.Label();
            this.ChocoSpreadButton = new System.Windows.Forms.Button();
            this.PKGListButton = new System.Windows.Forms.Button();
            this.ISOFolderButton = new System.Windows.Forms.Button();
            this.ISOFolderBox = new System.Windows.Forms.TextBox();
            this.PKGListBox = new System.Windows.Forms.TextBox();
            this.PKGListLabel = new System.Windows.Forms.Label();
            this.ISOFolderLabel = new System.Windows.Forms.Label();
            this.ChocoDetectLabel = new System.Windows.Forms.Label();
            this.ChocoExportButton = new System.Windows.Forms.Button();
            this.PKGListViewBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PKGListVersionBox = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.FinalISOButton = new System.Windows.Forms.Button();
            this.FinalISOPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ISOPathLabel
            // 
            this.ISOPathLabel.AutoSize = true;
            this.ISOPathLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ISOPathLabel.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.ISOPathLabel.Location = new System.Drawing.Point(13, 13);
            this.ISOPathLabel.Name = "ISOPathLabel";
            this.ISOPathLabel.Size = new System.Drawing.Size(54, 13);
            this.ISOPathLabel.TabIndex = 0;
            this.ISOPathLabel.Text = "ISO Path:";
            // 
            // FolderPathLabel
            // 
            this.FolderPathLabel.AutoSize = true;
            this.FolderPathLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FolderPathLabel.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.FolderPathLabel.Location = new System.Drawing.Point(14, 50);
            this.FolderPathLabel.Name = "FolderPathLabel";
            this.FolderPathLabel.Size = new System.Drawing.Size(84, 13);
            this.FolderPathLabel.TabIndex = 1;
            this.FolderPathLabel.Text = "Output Folder:";
            // 
            // FolderPathBox
            // 
            this.FolderPathBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.FolderPathBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FolderPathBox.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FolderPathBox.ForeColor = System.Drawing.SystemColors.Window;
            this.FolderPathBox.Location = new System.Drawing.Point(15, 66);
            this.FolderPathBox.Name = "FolderPathBox";
            this.FolderPathBox.ReadOnly = true;
            this.FolderPathBox.Size = new System.Drawing.Size(221, 22);
            this.FolderPathBox.TabIndex = 2;
            // 
            // ISOPathBox
            // 
            this.ISOPathBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ISOPathBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ISOPathBox.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ISOPathBox.ForeColor = System.Drawing.SystemColors.Window;
            this.ISOPathBox.Location = new System.Drawing.Point(15, 29);
            this.ISOPathBox.Name = "ISOPathBox";
            this.ISOPathBox.ReadOnly = true;
            this.ISOPathBox.Size = new System.Drawing.Size(221, 22);
            this.ISOPathBox.TabIndex = 3;
            // 
            // ISOSelectButton
            // 
            this.ISOSelectButton.CausesValidation = false;
            this.ISOSelectButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ISOSelectButton.Location = new System.Drawing.Point(242, 28);
            this.ISOSelectButton.Name = "ISOSelectButton";
            this.ISOSelectButton.Size = new System.Drawing.Size(75, 22);
            this.ISOSelectButton.TabIndex = 4;
            this.ISOSelectButton.Text = "Browse...";
            this.ISOSelectButton.UseVisualStyleBackColor = true;
            this.ISOSelectButton.Click += new System.EventHandler(this.ISOSelectButton_Click);
            // 
            // OutputFolderSelectButton
            // 
            this.OutputFolderSelectButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutputFolderSelectButton.Location = new System.Drawing.Point(242, 66);
            this.OutputFolderSelectButton.Name = "OutputFolderSelectButton";
            this.OutputFolderSelectButton.Size = new System.Drawing.Size(75, 22);
            this.OutputFolderSelectButton.TabIndex = 5;
            this.OutputFolderSelectButton.Text = "Browse...";
            this.OutputFolderSelectButton.UseVisualStyleBackColor = true;
            this.OutputFolderSelectButton.Click += new System.EventHandler(this.FolderSelectButton_Click);
            // 
            // ExtractISOButton
            // 
            this.ExtractISOButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExtractISOButton.Location = new System.Drawing.Point(103, 94);
            this.ExtractISOButton.Name = "ExtractISOButton";
            this.ExtractISOButton.Size = new System.Drawing.Size(75, 23);
            this.ExtractISOButton.TabIndex = 6;
            this.ExtractISOButton.Text = "Extract ISO";
            this.ExtractISOButton.UseVisualStyleBackColor = true;
            this.ExtractISOButton.Click += new System.EventHandler(this.ExtractISOButton_Click);
            // 
            // OutputBox
            // 
            this.OutputBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.OutputBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OutputBox.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutputBox.ForeColor = System.Drawing.SystemColors.Window;
            this.OutputBox.Location = new System.Drawing.Point(17, 317);
            this.OutputBox.Name = "OutputBox";
            this.OutputBox.ReadOnly = true;
            this.OutputBox.Size = new System.Drawing.Size(255, 159);
            this.OutputBox.TabIndex = 7;
            this.OutputBox.Text = "";
            this.OutputBox.TextChanged += new System.EventHandler(this.OutputBox_TextChanged);
            // 
            // OutputLabel
            // 
            this.OutputLabel.AutoSize = true;
            this.OutputLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutputLabel.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.OutputLabel.Location = new System.Drawing.Point(13, 301);
            this.OutputLabel.Name = "OutputLabel";
            this.OutputLabel.Size = new System.Drawing.Size(45, 13);
            this.OutputLabel.TabIndex = 8;
            this.OutputLabel.Text = "Output";
            // 
            // ChocoSpreadButton
            // 
            this.ChocoSpreadButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChocoSpreadButton.Location = new System.Drawing.Point(60, 256);
            this.ChocoSpreadButton.Name = "ChocoSpreadButton";
            this.ChocoSpreadButton.Size = new System.Drawing.Size(163, 23);
            this.ChocoSpreadButton.TabIndex = 15;
            this.ChocoSpreadButton.Text = "Insert Scripts and Create ISO";
            this.ChocoSpreadButton.UseVisualStyleBackColor = true;
            this.ChocoSpreadButton.Click += new System.EventHandler(this.ChocoSpreadButton_Click);
            // 
            // PKGListButton
            // 
            this.PKGListButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PKGListButton.Location = new System.Drawing.Point(242, 189);
            this.PKGListButton.Name = "PKGListButton";
            this.PKGListButton.Size = new System.Drawing.Size(74, 23);
            this.PKGListButton.TabIndex = 14;
            this.PKGListButton.Text = "Browse...";
            this.PKGListButton.UseVisualStyleBackColor = true;
            this.PKGListButton.Click += new System.EventHandler(this.PKGListButton_Click);
            // 
            // ISOFolderButton
            // 
            this.ISOFolderButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ISOFolderButton.Location = new System.Drawing.Point(242, 152);
            this.ISOFolderButton.Name = "ISOFolderButton";
            this.ISOFolderButton.Size = new System.Drawing.Size(75, 22);
            this.ISOFolderButton.TabIndex = 13;
            this.ISOFolderButton.Text = "Browse...";
            this.ISOFolderButton.UseVisualStyleBackColor = true;
            this.ISOFolderButton.Click += new System.EventHandler(this.ISOFolderButton_Click);
            // 
            // ISOFolderBox
            // 
            this.ISOFolderBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ISOFolderBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ISOFolderBox.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ISOFolderBox.ForeColor = System.Drawing.SystemColors.Window;
            this.ISOFolderBox.Location = new System.Drawing.Point(15, 153);
            this.ISOFolderBox.Name = "ISOFolderBox";
            this.ISOFolderBox.ReadOnly = true;
            this.ISOFolderBox.Size = new System.Drawing.Size(221, 22);
            this.ISOFolderBox.TabIndex = 12;
            // 
            // PKGListBox
            // 
            this.PKGListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.PKGListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PKGListBox.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PKGListBox.ForeColor = System.Drawing.SystemColors.Window;
            this.PKGListBox.Location = new System.Drawing.Point(16, 190);
            this.PKGListBox.Name = "PKGListBox";
            this.PKGListBox.ReadOnly = true;
            this.PKGListBox.Size = new System.Drawing.Size(221, 22);
            this.PKGListBox.TabIndex = 11;
            // 
            // PKGListLabel
            // 
            this.PKGListLabel.AutoSize = true;
            this.PKGListLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PKGListLabel.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.PKGListLabel.Location = new System.Drawing.Point(14, 174);
            this.PKGListLabel.Name = "PKGListLabel";
            this.PKGListLabel.Size = new System.Drawing.Size(77, 13);
            this.PKGListLabel.TabIndex = 10;
            this.PKGListLabel.Text = "Packages List:";
            // 
            // ISOFolderLabel
            // 
            this.ISOFolderLabel.AutoSize = true;
            this.ISOFolderLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ISOFolderLabel.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.ISOFolderLabel.Location = new System.Drawing.Point(13, 136);
            this.ISOFolderLabel.Name = "ISOFolderLabel";
            this.ISOFolderLabel.Size = new System.Drawing.Size(64, 13);
            this.ISOFolderLabel.TabIndex = 9;
            this.ISOFolderLabel.Text = "ISO Folder:";
            // 
            // ChocoDetectLabel
            // 
            this.ChocoDetectLabel.AutoSize = true;
            this.ChocoDetectLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChocoDetectLabel.ForeColor = System.Drawing.Color.Green;
            this.ChocoDetectLabel.Location = new System.Drawing.Point(533, 9);
            this.ChocoDetectLabel.Name = "ChocoDetectLabel";
            this.ChocoDetectLabel.Size = new System.Drawing.Size(118, 13);
            this.ChocoDetectLabel.TabIndex = 17;
            this.ChocoDetectLabel.Text = "Chocolatey Detected!";
            // 
            // ChocoExportButton
            // 
            this.ChocoExportButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChocoExportButton.Location = new System.Drawing.Point(520, 25);
            this.ChocoExportButton.Name = "ChocoExportButton";
            this.ChocoExportButton.Size = new System.Drawing.Size(147, 23);
            this.ChocoExportButton.TabIndex = 18;
            this.ChocoExportButton.Text = "Export Package List";
            this.ChocoExportButton.UseVisualStyleBackColor = true;
            this.ChocoExportButton.Click += new System.EventHandler(this.ChocoExportButton_Click);
            // 
            // PKGListViewBox
            // 
            this.PKGListViewBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.PKGListViewBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PKGListViewBox.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PKGListViewBox.ForeColor = System.Drawing.SystemColors.Window;
            this.PKGListViewBox.Location = new System.Drawing.Point(292, 317);
            this.PKGListViewBox.Name = "PKGListViewBox";
            this.PKGListViewBox.ReadOnly = true;
            this.PKGListViewBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.PKGListViewBox.Size = new System.Drawing.Size(190, 159);
            this.PKGListViewBox.TabIndex = 19;
            this.PKGListViewBox.Text = "No package list loaded.";
            this.PKGListViewBox.VScroll += new System.EventHandler(this.PKGListViewBox_VScroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(289, 301);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Packages:";
            // 
            // PKGListVersionBox
            // 
            this.PKGListVersionBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.PKGListVersionBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PKGListVersionBox.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PKGListVersionBox.ForeColor = System.Drawing.SystemColors.Window;
            this.PKGListVersionBox.Location = new System.Drawing.Point(488, 317);
            this.PKGListVersionBox.Name = "PKGListVersionBox";
            this.PKGListVersionBox.ReadOnly = true;
            this.PKGListVersionBox.Size = new System.Drawing.Size(175, 159);
            this.PKGListVersionBox.TabIndex = 21;
            this.PKGListVersionBox.Text = "No package list loaded.";
            this.PKGListVersionBox.VScroll += new System.EventHandler(this.PKGListVersionBox_VScroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label2.Location = new System.Drawing.Point(485, 301);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Version:";
            // 
            // FinalISOButton
            // 
            this.FinalISOButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FinalISOButton.Location = new System.Drawing.Point(242, 227);
            this.FinalISOButton.Name = "FinalISOButton";
            this.FinalISOButton.Size = new System.Drawing.Size(74, 22);
            this.FinalISOButton.TabIndex = 25;
            this.FinalISOButton.Text = "Browse...";
            this.FinalISOButton.UseVisualStyleBackColor = true;
            this.FinalISOButton.Click += new System.EventHandler(this.FinalISOButton_Click);
            // 
            // FinalISOPath
            // 
            this.FinalISOPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.FinalISOPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FinalISOPath.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FinalISOPath.ForeColor = System.Drawing.SystemColors.Window;
            this.FinalISOPath.Location = new System.Drawing.Point(16, 228);
            this.FinalISOPath.Name = "FinalISOPath";
            this.FinalISOPath.ReadOnly = true;
            this.FinalISOPath.Size = new System.Drawing.Size(221, 22);
            this.FinalISOPath.TabIndex = 24;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label3.Location = new System.Drawing.Point(14, 212);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Output ISO:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(670, 488);
            this.Controls.Add(this.FinalISOButton);
            this.Controls.Add(this.FinalISOPath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PKGListVersionBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PKGListViewBox);
            this.Controls.Add(this.ChocoExportButton);
            this.Controls.Add(this.ChocoDetectLabel);
            this.Controls.Add(this.ChocoSpreadButton);
            this.Controls.Add(this.PKGListButton);
            this.Controls.Add(this.ISOFolderButton);
            this.Controls.Add(this.ISOFolderBox);
            this.Controls.Add(this.PKGListBox);
            this.Controls.Add(this.PKGListLabel);
            this.Controls.Add(this.ISOFolderLabel);
            this.Controls.Add(this.OutputLabel);
            this.Controls.Add(this.OutputBox);
            this.Controls.Add(this.ExtractISOButton);
            this.Controls.Add(this.OutputFolderSelectButton);
            this.Controls.Add(this.ISOSelectButton);
            this.Controls.Add(this.ISOPathBox);
            this.Controls.Add(this.FolderPathBox);
            this.Controls.Add(this.FolderPathLabel);
            this.Controls.Add(this.ISOPathLabel);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "ChocolateySpreader";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label ISOPathLabel;
        public System.Windows.Forms.Label FolderPathLabel;
        public System.Windows.Forms.TextBox FolderPathBox;
        public System.Windows.Forms.TextBox ISOPathBox;
        public System.Windows.Forms.Button ISOSelectButton;
        public System.Windows.Forms.Button OutputFolderSelectButton;
        public System.Windows.Forms.Button ExtractISOButton;
        public System.Windows.Forms.RichTextBox OutputBox;
        public System.Windows.Forms.Label OutputLabel;
        public System.Windows.Forms.Button ChocoSpreadButton;
        public System.Windows.Forms.Button PKGListButton;
        public System.Windows.Forms.Button ISOFolderButton;
        public System.Windows.Forms.TextBox ISOFolderBox;
        public System.Windows.Forms.TextBox PKGListBox;
        public System.Windows.Forms.Label PKGListLabel;
        public System.Windows.Forms.Label ISOFolderLabel;
        public System.Windows.Forms.Label ChocoDetectLabel;
        public System.Windows.Forms.Button ChocoExportButton;
        public System.Windows.Forms.RichTextBox PKGListViewBox;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.RichTextBox PKGListVersionBox;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Button FinalISOButton;
        public System.Windows.Forms.TextBox FinalISOPath;
        public System.Windows.Forms.Label label3;
    }
}

