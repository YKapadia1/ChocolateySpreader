using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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

        //When the user has clicked the button to browse for an ISO file...
        private void ISOSelectButton_Click(object sender, EventArgs e) 
        {
            //Set up the file dialog with the initial directory, filters and disable multiselect.
            using (OpenFileDialog ISOPathDialog = new OpenFileDialog())
            {
                ISOPathDialog.InitialDirectory = "C:\\";
                ISOPathDialog.Filter = "ISO Files (*.iso)|*.iso|All Files (*.*)|*.*";
                ISOPathDialog.FilterIndex = 1;
                ISOPathDialog.RestoreDirectory = true; //For some reason, this attribute seems to also affect the CommonOpenFileDialog.
                ISOPathDialog.Multiselect = false;
                

                //Show the dialog, and if it was successful...
                if (ISOPathDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the file path of the ISO, and put it into the text box.
                    ISOPathBox.Text = ISOPathDialog.FileName;
                }
            }
        }

        //When the user has clicked the button to browse for an ISO file...
        private void FolderSelectButton_Click(object sender, EventArgs e)
        {
            //If the user has not yet specified an ISO file...
            if (ISOPathBox.Text == "")
            {
                MessageBox.Show("Please specify an ISO before specifiying the output folder.", "No ISO file",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //Set up the file dialog by specifying its a folder picker, and set the initial directory.
                using (CommonOpenFileDialog FolderSelectDialog = new CommonOpenFileDialog())
                {
                    FolderSelectDialog.IsFolderPicker = true;
                    //FolderSelectDialog.InitialDirectory = Environment.SpecialFolder.MyComputer.ToString();

                    //Show the dialog, and if it was successful...
                    if (FolderSelectDialog.ShowDialog() == CommonFileDialogResult.Ok)
                    {
                        //Get the folder path, and put it into the text box.
                        FolderPathBox.Text = FolderSelectDialog.FileName;
                    }
                }
            }
        }

        //When the user clicks the button to extract the ISO...
        private void ExtractISOButton_Click(object sender, EventArgs e)
        {
            //If the user has not selected an ISO...
            if (ISOPathBox.Text == "")
            {
                MessageBox.Show("You have not specified an ISO file to extract.", "No ISO file", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //If the user has not selected an output folder...
            else if (FolderPathBox.Text == "")
            {
                MessageBox.Show("You have not specified an output folder to extract to.", "No output folder",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                bool SevenZipInstalled = File.Exists("C:\\Program Files\\7-Zip\\7z.exe");
                if (!SevenZipInstalled)
                {
                    MessageBox.Show("7-Zip is not installed. Get it from 7-zip.org.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Process.Start("https://www.7-zip.org/");
                }
                else
                {
                    MessageBox.Show("Add code to extract the stuff!", "So close!",
                   MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        
        
        //Don't touch, or everything will break!
        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
