﻿namespace ChocolateySpreader
{
    partial class Form1
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
            this.SuspendLayout();
            // 
            // ISOPathLabel
            // 
            this.ISOPathLabel.AutoSize = true;
            this.ISOPathLabel.Location = new System.Drawing.Point(13, 13);
            this.ISOPathLabel.Name = "ISOPathLabel";
            this.ISOPathLabel.Size = new System.Drawing.Size(53, 13);
            this.ISOPathLabel.TabIndex = 0;
            this.ISOPathLabel.Text = "ISO Path:";
            // 
            // FolderPathLabel
            // 
            this.FolderPathLabel.AutoSize = true;
            this.FolderPathLabel.Location = new System.Drawing.Point(13, 42);
            this.FolderPathLabel.Name = "FolderPathLabel";
            this.FolderPathLabel.Size = new System.Drawing.Size(74, 13);
            this.FolderPathLabel.TabIndex = 1;
            this.FolderPathLabel.Text = "Output Folder:";
            // 
            // FolderPathBox
            // 
            this.FolderPathBox.Location = new System.Drawing.Point(93, 39);
            this.FolderPathBox.Name = "FolderPathBox";
            this.FolderPathBox.Size = new System.Drawing.Size(120, 20);
            this.FolderPathBox.TabIndex = 2;
            // 
            // ISOPathBox
            // 
            this.ISOPathBox.Location = new System.Drawing.Point(93, 9);
            this.ISOPathBox.Name = "ISOPathBox";
            this.ISOPathBox.Size = new System.Drawing.Size(120, 20);
            this.ISOPathBox.TabIndex = 3;
            // 
            // ISOSelectButton
            // 
            this.ISOSelectButton.Location = new System.Drawing.Point(219, 8);
            this.ISOSelectButton.Name = "ISOSelectButton";
            this.ISOSelectButton.Size = new System.Drawing.Size(75, 23);
            this.ISOSelectButton.TabIndex = 4;
            this.ISOSelectButton.Text = "Browse...";
            this.ISOSelectButton.UseVisualStyleBackColor = true;
            this.ISOSelectButton.Click += new System.EventHandler(this.ISOSelectButton_Click);
            // 
            // OutputFolderSelectButton
            // 
            this.OutputFolderSelectButton.Location = new System.Drawing.Point(219, 38);
            this.OutputFolderSelectButton.Name = "OutputFolderSelectButton";
            this.OutputFolderSelectButton.Size = new System.Drawing.Size(75, 23);
            this.OutputFolderSelectButton.TabIndex = 5;
            this.OutputFolderSelectButton.Text = "Browse...";
            this.OutputFolderSelectButton.UseVisualStyleBackColor = true;
            this.OutputFolderSelectButton.Click += new System.EventHandler(this.FolderSelectButton_Click);
            // 
            // ExtractISOButton
            // 
            this.ExtractISOButton.Location = new System.Drawing.Point(12, 74);
            this.ExtractISOButton.Name = "ExtractISOButton";
            this.ExtractISOButton.Size = new System.Drawing.Size(75, 23);
            this.ExtractISOButton.TabIndex = 6;
            this.ExtractISOButton.Text = "Extract ISO";
            this.ExtractISOButton.UseVisualStyleBackColor = true;
            this.ExtractISOButton.Click += new System.EventHandler(this.ExtractISOButton_Click);
            // 
            // OutputBox
            // 
            this.OutputBox.Location = new System.Drawing.Point(16, 295);
            this.OutputBox.Name = "OutputBox";
            this.OutputBox.ReadOnly = true;
            this.OutputBox.Size = new System.Drawing.Size(290, 143);
            this.OutputBox.TabIndex = 7;
            this.OutputBox.Text = "";
            // 
            // OutputLabel
            // 
            this.OutputLabel.AutoSize = true;
            this.OutputLabel.Location = new System.Drawing.Point(13, 279);
            this.OutputLabel.Name = "OutputLabel";
            this.OutputLabel.Size = new System.Drawing.Size(39, 13);
            this.OutputLabel.TabIndex = 8;
            this.OutputLabel.Text = "Output";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.OutputLabel);
            this.Controls.Add(this.OutputBox);
            this.Controls.Add(this.ExtractISOButton);
            this.Controls.Add(this.OutputFolderSelectButton);
            this.Controls.Add(this.ISOSelectButton);
            this.Controls.Add(this.ISOPathBox);
            this.Controls.Add(this.FolderPathBox);
            this.Controls.Add(this.FolderPathLabel);
            this.Controls.Add(this.ISOPathLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "ChocolateySpreader";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ISOPathLabel;
        private System.Windows.Forms.Label FolderPathLabel;
        private System.Windows.Forms.TextBox FolderPathBox;
        private System.Windows.Forms.TextBox ISOPathBox;
        private System.Windows.Forms.Button ISOSelectButton;
        private System.Windows.Forms.Button OutputFolderSelectButton;
        private System.Windows.Forms.Button ExtractISOButton;
        private System.Windows.Forms.RichTextBox OutputBox;
        private System.Windows.Forms.Label OutputLabel;
    }
}

