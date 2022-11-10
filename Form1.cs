using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Configuration;
using System.IO;
using System.Windows.Forms;


namespace ChocolateySpreader
{

    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            Form1 FuncForm = this;
        }

        Font ChocoPresence = new Font("Microsoft Sans Serif", 8.25f, style: FontStyle.Bold);
        ProgramFunctions Functions = new ProgramFunctions();
        
        void Checkfor7Z(ref string SevenZipLocation)
        {
            //Check if 7-Zip is installed. This should work on both 32 and 64 bit systems.
            //This will fail if the user has specified an alternate install location!
            bool SevenZipInstalled = File.Exists(ProgramStrings.DEFAULT_7ZIP_LOCATION);
            //If 7-Zip is not installed in the usual location...
            if (!SevenZipInstalled)
            {
                bool LookingFor7Z = true; //Set a boolean to indicate that the user is looking for 7-Zip.
                while (LookingFor7Z)
                {
                    using (OpenFileDialog ZipSelectDialog = new OpenFileDialog())
                    {
                        //Set the title, filter, and directory of the open file dialog.
                        ZipSelectDialog.Title = ProgramStrings.ZIP_SELECT_WINDOW_TITLE;
                        ZipSelectDialog.InitialDirectory = ProgramStrings.ZIP_SELECT_WINDOW_DIRECTORY; 
                        ZipSelectDialog.Filter = ProgramStrings.ZIP_SELECT_WINDOW_FILTER;
                        ZipSelectDialog.FilterIndex = 1; //Set the selected filter to be 1.
                        ZipSelectDialog.Multiselect = false; //Disallow the user from selecting multiple files.
                                                             //Show the dialog, and if the user has supplied a file...
                        if (ZipSelectDialog.ShowDialog() == DialogResult.OK)
                        {
                            //Check that the user has supplied a valid 7z.exe.
                            if (ZipSelectDialog.FileName.EndsWith("7z.exe") != true)
                            {
                                MessageBox.Show(ProgramStrings.ERR_INVALID_7Z_EXE, this.Text,
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
                            MessageBox.Show(ProgramStrings.INFO_7Z_INSTALL_TIP, this.Text,
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //Open the 7-Zip website.
                            Process.Start("https://www.7-zip.org/");
                        }
                    }
                }
            }
            if (SevenZipInstalled)
            {
                Functions.ExtractISO(SevenZipLocation, FolderPathBox.Text, ISOPathBox.Text,this);
                //Reenable the buttons after the extraction process has exited.
            }
        }

        public void OutputLog(object sendingProcess, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                BeginInvoke(new MethodInvoker(() => {OutputBox.AppendText(e.Data + Environment.NewLine); }));
                //Invoke the UI thread and update the text box. It must be done this way to ensure asynchronous operation.
                //If this was done synchronously, the UI would freeze.
            }
        }

        //When the user has clicked the button to browse for an ISO file...
        private void ISOSelectButton_Click(object sender, EventArgs e) 
        {
            ISOPathBox.Text = Functions.CreateOpenFileDialog(ProgramStrings.ISO_SELECT_WINDOW_TITLE, ProgramStrings.ISO_SELECT_WINDOW_DIRECTORY
                ,ProgramStrings.ISO_SELECT_WINDOW_FILTER);
        }

        //When the user has clicked the button to browse for an ISO file...
        private void FolderSelectButton_Click(object sender, EventArgs e)
        {
            //If the user has not yet specified an ISO file...
            if (ISOPathBox.Text == "")
            {
                MessageBox.Show(ProgramStrings.WARN_SPECIFY_ISO_BEFORE_OUTPUT, ProgramStrings.WARN_SPECIFY_ISO_BEFORE_OUTPUT_TITLE,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                FolderPathBox.Text = Functions.CreateOpenFolderDialog(ProgramStrings.OUTPUT_FOLDER_SELECT_TITLE);
            }
        }

        //When the user clicks the button to extract the ISO...
        private void ExtractISOButton_Click(object sender, EventArgs e)
        {
            string SevenZipLocation = ProgramStrings.DEFAULT_7ZIP_LOCATION;
            //If the user has not selected an ISO...
            if (ISOPathBox.Text == "")
            {
                MessageBox.Show(ProgramStrings.ERR_NO_ISO_SPECIFIED, ProgramStrings.ERR_NO_ISO_SPECIFIED_TITLE,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //If the user has not selected an output folder...
            else if (FolderPathBox.Text == "")
            {
                MessageBox.Show(ProgramStrings.ERR_NO_OUTPUT_SPECIFIED, ProgramStrings.ERR_NO_OUTPUT_SPECIFIED_TITLE,
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //INFO: This is some janky ass shit right here... Try to make this neater!
            else if (Directory.GetFileSystemEntries(FolderPathBox.Text).Length != 0)
            {
                switch (MessageBox.Show(ProgramStrings.WARN_OUTPUT_FOLDER_NOT_EMPTY, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    case DialogResult.Yes:
                        Checkfor7Z(ref SevenZipLocation);
                        break;
                }
            }
            else
            {
                Functions.ExtractISO(SevenZipLocation, FolderPathBox.Text, ISOPathBox.Text, this);
            }
        }
        private void ChocoSpreadButton_Click(object sender, EventArgs e)
        {
            if (ISOFolderBox.Text == "")
            {
                MessageBox.Show(ProgramStrings.ERR_NO_ISO_FOLDER_SPECIFIED, ProgramStrings.ERR_NO_ISO_FOLDER_SPECIFIED_TITLE,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //If the user has not selected an output folder...
            else if (PKGListBox.Text == "")
            {
                MessageBox.Show(ProgramStrings.ERR_NO_PACKAGE_LIST_SPECIFIED, ProgramStrings.ERR_NO_PACKAGE_LIST_SPECIFIED_TITLE,
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ISOFolderButton_Click(object sender, EventArgs e)
        {
           ISOFolderBox.Text = Functions.CreateOpenFolderDialog(ProgramStrings.ISO_FOLDER_SELECT_TITLE);
        }

        private void PKGListButton_Click(object sender, EventArgs e)
        {
            PKGListBox.Text = Functions.CreateOpenFileDialog(ProgramStrings.ISO_FOLDER_SELECT_TITLE, 
                ProgramStrings.ISO_FOLDER_SELECT_WINDOW_DIRECTORY, ProgramStrings.ISO_FOLDER_SELECT_WINDOW_FILTER);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string chocoEnv = "";
            //When the program starts, get the environment variable for Chocolatey.
            chocoEnv = Environment.ExpandEnvironmentVariables("%ChocolateyInstall%");
            //If the environment variable is not found, it will just expand to %ChocolateyInstall%, which won't do anything.
            //If the environment variable is found, it will expand to where chocolatey is installed.
            
            
            //Use the expanded environment variable and the provided path to determine if Chocolatey is installed.
            if (File.Exists(chocoEnv + "\\choco.exe"))
            {
                MessageBox.Show(ProgramStrings.CHOCO_DETECTED_MSG1 + ProgramStrings.CHOCO_DETECTED_MSG2,
                    this.Text,MessageBoxButtons.OK,MessageBoxIcon.Information);
                //Inform the user that Chocolatey has been detected, and what they can do with it.
                ChocoDetectLabel.Text = ProgramStrings.CHOCO_DETECTED_LABEL;
                ChocoDetectLabel.Font = ChocoPresence;
                ChocoDetectLabel.ForeColor = Color.Green;
                //Change the label text, font and colour.
                ChocoDetectLabel.Location = new Point(447, 19);
                //Change the position of the label for better presentation.
            }
            else
            {
                MessageBox.Show(ProgramStrings.CHOCO_NOT_DETECTED_MSG1 + ProgramStrings.CHOCO_NOT_DETECTED_MSG2,
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ChocoDetectLabel.Text = ProgramStrings.CHOCO_NOT_DETECTED_LABEL;
                ChocoDetectLabel.Font = ChocoPresence;
                ChocoDetectLabel.ForeColor = Color.Red;
                ChocoDetectLabel.Location = new Point(360, 9);
                ChocoExportButton.Enabled = false;
            }
        }
    }
}
