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
                    FolderPathBox.Text = FolderSelectDialog.FileName;
                }
            }
        }
    }
}
