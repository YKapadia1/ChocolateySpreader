using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;



namespace ChocolateySpreader
{
    
    public partial class Form1 : Form
    {
        //Used to determine if the user has actually selected a file/folder or not.
        string ISOPath = null;
        string FolderPath = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void ISOSelectButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ISOPathDialog = new OpenFileDialog())
            {
                ISOPathDialog.InitialDirectory = "C:\\";
                ISOPathDialog.Filter = "ISO Files (*.iso)|*.iso|All Files (*.*)|*.*";
                ISOPathDialog.FilterIndex = 1;
                ISOPathDialog.RestoreDirectory = true;
                ISOPathDialog.Multiselect = false;
                

                if (ISOPathDialog.ShowDialog() == DialogResult.OK)
                {
                    ISOPath = ISOPathDialog.FileName;
                    ISOPathBox.Text = ISOPathDialog.FileName;
                }
            }
        }

        private void FolderSelectButton_Click(object sender, EventArgs e)
        {
            using (CommonOpenFileDialog FolderSelectDialog = new CommonOpenFileDialog())
            {
                FolderSelectDialog.IsFolderPicker = true;
                FolderSelectDialog.DefaultDirectory = Environment.SpecialFolder.MyComputer.ToString();
                if (FolderSelectDialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    FolderPath = FolderSelectDialog.FileName;
                    FolderPathBox.Text = FolderSelectDialog.FileName;
                }
            }
        }

        private void ExtractISOButton_Click(object sender, EventArgs e)
        {
            if (ISOPath == null)
            {
                MessageBox.Show("You have not specified an ISO file to extract.", "No ISO file", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (FolderPath == null)
            {
                MessageBox.Show("You have not specified an output folder to extract to.", "No output folder",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
