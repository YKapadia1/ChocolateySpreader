using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
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

        
        //Strings for the various dialogs presented to the user.
        const string DEFAULT_7ZIP_LOCATION = "C:\\Program Files\\7-Zip\\7z.exe";
        
        const string ISO_SELECT_WINDOW_TITLE = "Select Windows ISO";
        const string ISO_SELECT_WINDOW_DIRECTORY = "C:\\";
        const string ISO_SELECT_WINDOW_FILTER = "ISO Files (*.iso)|*.iso|All Files (*.*)|*.*";
        
        const string OUTPUT_FOLDER_SELECT_TITLE = "Select folder to extract ISO to:";

        const string ZIP_SELECT_WINDOW_TITLE = "Locate 7z.exe";
        const string ZIP_SELECT_WINDOW_DIRECTORY = "C:\\Program Files";
        const string ZIP_SELECT_WINDOW_FILTER = "7-Zip|7z.exe";


        //Strings containing the various messages that may be displayed to the user.
        //Strings related to selecting an ISO and an output folder.
        const string ERR_NO_ISO_SPECIFIED_TITLE = "No ISO file";
        const string ERR_NO_ISO_SPECIFIED = "You have not specified an ISO file to extract.";
        const string ERR_NO_OUTPUT_SPECIFIED_TITLE = "No output folder";
        const string ERR_NO_OUTPUT_SPECIFIED = "You have not specified an output folder to extract to.";
        const string ERR_ISO_EXTRACT_FATAL = "A fatal error occured when extracting the ISO.";
        const string ERR_ISO_EXTRACT_CLI = "A command line error occured when extracting the ISO.";
        const string ERR_ISO_EXTRACT_NO_MEMORY = "There is not enough free memory available to extract the ISO.";
        const string ERR_ISO_EXTRACT_USER_ABORT = "The user cancelled the operation.";
        
        const string WARN_SPECIFY_ISO_BEFORE_OUTPUT = "Please specify an ISO before specifiying the output folder.";
        const string WARN_SPECIFY_ISO_BEFORE_OUTPUT_TITLE = "No ISO file";
        
        const string INFO_ISO_EXTRACT_SUCCESS = "ISO extracted successfully!";
        const string INFO_ISO_EXTRACT_WARNING = "ISO extracted with warnings. Please check for any corrupt/missing files.";

        const string ERR_INVALID_7Z_EXE = "Invalid 7z.exe supplied!";
        const string INFO_7Z_INSTALL_TIP = "If you do not have 7-Zip installed, you can get it from 7-zip.org.";








        private void OutputLog(object sendingProcess, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                BeginInvoke(new MethodInvoker(() => { OutputBox.AppendText(e.Data + Environment.NewLine); }));
            }
        }

        void ExtractISO(ref string SevenZipLocation)
        {
            ExtractISOButton.Enabled = false;
            ISOSelectButton.Enabled = false;
            OutputFolderSelectButton.Enabled = false;
            Process ExtractISO = new Process(); //Create a new process that we will start.
                                                //Set the file path to where 7-Zip is usually located.
            ExtractISO.StartInfo.FileName = SevenZipLocation;
            ExtractISO.StartInfo.Arguments = " -o" + FolderPathBox.Text + " -aoa x " + ISOPathBox.Text; 
            //Create the arguments necessary.
            //-o switch specifies output directory, -aoa specifies to replace any existing files without user interaction.
            //x specifies to extract files from a given archive and keep folder structure.
            ExtractISO.StartInfo.CreateNoWindow = true;
            ExtractISO.StartInfo.RedirectStandardOutput = true;
            ExtractISO.StartInfo.UseShellExecute = false;
            ExtractISO.OutputDataReceived += new DataReceivedEventHandler(OutputLog);
            ExtractISO.Start(); //Start the process.
            ExtractISO.BeginOutputReadLine(); //Begin asynchronously reading the output.
            while (!ExtractISO.HasExited) //If the process has not yet exited...
            {
                Application.DoEvents(); //Keep Form1 active to the text box can be updated.
            }
            switch (ExtractISO.ExitCode) //Check the exit code.
            {
                case 0: //If there was no error...
                    MessageBox.Show(INFO_ISO_EXTRACT_SUCCESS, this.Text,
                        MessageBoxButtons.OK, MessageBoxIcon.Information) ;
                    break;
                case 1: //If there were non-fatal errors/warnings...
                    MessageBox.Show(INFO_ISO_EXTRACT_WARNING, this.Text,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 2: //If there was a fatal error...
                    MessageBox.Show(ERR_ISO_EXTRACT_FATAL, this.Text,
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case 7: //If there was a command line error.
                    MessageBox.Show(ERR_ISO_EXTRACT_CLI, this.Text,
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case 8: //If we have ran out of memory...
                    MessageBox.Show(ERR_ISO_EXTRACT_NO_MEMORY, this.Text,
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case -1073741510: //If the user closed the window before it was completed or pressed CTRL+C...
                    MessageBox.Show(ERR_ISO_EXTRACT_USER_ABORT, this.Text,
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }


        void CreateOpenFileDialog(string name, string directory, string filter, TextBox InputBox)
        {
            using (OpenFileDialog OpenDialog = new OpenFileDialog())
            {
                //Set up the file dialog with the name, initial directory, filters and disable multiselect.
                OpenDialog.Title = name;
                OpenDialog.InitialDirectory = directory;
                OpenDialog.Filter = filter;
                OpenDialog.FilterIndex = 1;
                OpenDialog.RestoreDirectory = false;  //For some reason, this attribute seems to also affect the CommonOpenFileDialog.
                OpenDialog.Multiselect = false;

                //Show the dialog, and if the user has supplied a file...
                if (OpenDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the file path, and put it into the text box.
                    OpenDialog.FileName = InputBox.Text;
                }
            }
            
        }


        //When the user has clicked the button to browse for an ISO file...
        private void ISOSelectButton_Click(object sender, EventArgs e) 
        {
            CreateOpenFileDialog(ISO_SELECT_WINDOW_TITLE, ISO_SELECT_WINDOW_DIRECTORY,ISO_SELECT_WINDOW_FILTER, ISOPathBox);
        }

        //When the user has clicked the button to browse for an ISO file...
        private void FolderSelectButton_Click(object sender, EventArgs e)
        {
            //If the user has not yet specified an ISO file...
            if (ISOPathBox.Text == "")
            {
                MessageBox.Show(WARN_SPECIFY_ISO_BEFORE_OUTPUT, WARN_SPECIFY_ISO_BEFORE_OUTPUT_TITLE,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //Set up the file dialog by specifying its a folder picker, and set the initial directory.
                using (CommonOpenFileDialog OutputFolderSelectDialog = new CommonOpenFileDialog())
                {
                    OutputFolderSelectDialog.IsFolderPicker = true;
                    OutputFolderSelectDialog.Title = OUTPUT_FOLDER_SELECT_TITLE;

                    //Show the dialog, and if the user has supplied a folder...
                    if (OutputFolderSelectDialog.ShowDialog() == CommonFileDialogResult.Ok)
                    {
                        //Get the folder path, and put it into the text box.
                        FolderPathBox.Text = OutputFolderSelectDialog.FileName;
                    }
                }
            }
        }

        //When the user clicks the button to extract the ISO...
        private void ExtractISOButton_Click(object sender, EventArgs e)
        {
            string SevenZipLocation = DEFAULT_7ZIP_LOCATION;
            //If the user has not selected an ISO...
            if (ISOPathBox.Text == "")
            {
                MessageBox.Show(ERR_NO_ISO_SPECIFIED, ERR_NO_ISO_SPECIFIED_TITLE,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //If the user has not selected an output folder...
            else if (FolderPathBox.Text == "")
            {
                MessageBox.Show(ERR_NO_OUTPUT_SPECIFIED, ERR_NO_OUTPUT_SPECIFIED_TITLE,
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //Check if 7-Zip is installed. This should work on both 32 and 64 bit systems.
                //This will fail if the user has specified an alternate install location!
                bool SevenZipInstalled = File.Exists(DEFAULT_7ZIP_LOCATION);
                //If 7-Zip is not installed in the usual location...
                if (!SevenZipInstalled)
                {
                    bool LookingFor7Z = true; //Set a boolean to indicate that the user is looking for 7-Zip.
                    while (LookingFor7Z)
                    {
                        using (OpenFileDialog ZipSelectDialog = new OpenFileDialog())
                        {
                            ZipSelectDialog.Title = ZIP_SELECT_WINDOW_TITLE; //Set the title of the window.
                            ZipSelectDialog.InitialDirectory = ZIP_SELECT_WINDOW_DIRECTORY; //Set the initial directory of the selection window.
                            ZipSelectDialog.Filter = ZIP_SELECT_WINDOW_FILTER; //Set the filter so that it only shows exe files called 7z.
                            ZipSelectDialog.FilterIndex = 1; //Set the selected filter to be 1.
                            ZipSelectDialog.Multiselect = false; //Disallow the user from selecting multiple files.
                            //Show the dialog, and if the user has supplied a file...
                            if (ZipSelectDialog.ShowDialog() == DialogResult.OK)
                            {
                                //Check that the user has supplied a valid 7z.exe.
                                if (ZipSelectDialog.FileName.EndsWith("7z.exe") != true)
                                {
                                    MessageBox.Show(ERR_INVALID_7Z_EXE, this.Text,
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    SevenZipLocation = ZipSelectDialog.FileName;
                                    LookingFor7Z = false;
                                    SevenZipInstalled = true;
                                }
                            }
                            else //If the user has not supplied a file...
                            {
                                LookingFor7Z = false; //Assume they do not have 7-Zip installed.
                                MessageBox.Show(INFO_7Z_INSTALL_TIP, this.Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //Open the 7-Zip website.
                                Process.Start("https://www.7-zip.org/");
                            }
                        }
                    }
                }
                if (SevenZipInstalled)
                {

                    ExtractISO(ref SevenZipLocation);
                    ExtractISOButton.Enabled = true;
                    OutputFolderSelectButton.Enabled = true;
                    ISOSelectButton.Enabled = true;
                    //Reenable the buttons after the extraction process has exited.
                }
            }
        }

    }
}
