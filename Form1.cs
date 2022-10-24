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
                //Check if 7-Zip is installed. This should work on both 32 and 64 bit systems.
                //This will fail if the user has specified an alternate install location!
                bool SevenZipInstalled = File.Exists("C:\\Program Files\\7-Zip\\7z.exe");
                //If 7-Zip is not installed...
                if (!SevenZipInstalled)
                {
                    //Let the user know
                    MessageBox.Show("7-Zip is not installed. Get it from 7-zip.org.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //Open the 7-Zip website.
                    Process.Start("https://www.7-zip.org/");
                }
                else
                {
                    Process ExtractISO = new Process(); //Create a new process that we will start.
                    //Set the file path to where 7-Zip is usually located.
                    //This will break if the user has specified an alternate install location!
                    ExtractISO.StartInfo.FileName =  "C:\\Program Files\\7-Zip\\7z.exe"; 
                    ExtractISO.StartInfo.Arguments = " -o" + FolderPathBox.Text + " x " + ISOPathBox.Text; //Create the arguments necessary.
                    ExtractISO.Start(); //Start the process.
                    while (!ExtractISO.HasExited) //If the process has not yet exited...
                    {
                        //Do nothing.
                    }
                    switch (ExtractISO.ExitCode) //Check the exit code.
                    {
                        case 0: //If there was no error...
                            MessageBox.Show("ISO extracted successfully!", "ChocolateySpreader",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case 1: //If there were non-fatal errors/warnings...
                            MessageBox.Show("ISO extracted successfully!", "ChocolateySpreader",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case 2: //If there was a fatal error...
                            MessageBox.Show("A fatal error occured when extracting the ISO.", "ChocolateySpreader",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case 7: //If there was a command line error.
                            MessageBox.Show("A command line error occured when extracting the ISO.", "ChocolateySpreader",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case 8: //If we have ran out of memory...
                            MessageBox.Show("There is not enough free memory available to extract the ISO.", "ChocolateySpreader",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case 255: //If the user closed the window before it was completed or pressed CTRL+C...
                            MessageBox.Show("The user cancelled the operation.", "ChocolateySpreader",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }

                }
            }
        }

        
        
        //Don't touch, or everything will break!
        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
