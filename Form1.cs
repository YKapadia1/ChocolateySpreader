using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;


namespace ChocolateySpreader
{

    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

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

                ExtractISO(SevenZipLocation);
                //Reenable the buttons after the extraction process has exited.
            }
        }

        private void OutputLog(object sendingProcess, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                BeginInvoke(new MethodInvoker(() => { OutputBox.AppendText(e.Data + Environment.NewLine);}));
                //Invoke the UI thread and update the text box. It must be done this way to ensure asynchronous operation.
                //If this was done synchronously, the UI would freeze.
            }
        }

        void ExtractISO(string SevenZipLocation)
        {
            ExtractISOButton.Enabled = false;
            ISOSelectButton.Enabled = false;
            ISOFolderButton.Enabled = false;
            PKGListButton.Enabled = false;
            OutputFolderSelectButton.Enabled = false;
            Process ExtractISO = new Process(); //Create a new process that we will start.
                                                //Set the file path to where 7-Zip has been located.
            ExtractISO.StartInfo.FileName = SevenZipLocation;
            ExtractISO.StartInfo.Arguments = " -o" + FolderPathBox.Text + " -aoa -bsp1 x " + ISOPathBox.Text; 
            //Create the arguments necessary.
            //-o switch specifies output directory, -aoa specifies to replace any existing files without user interaction.
            //x specifies to extract files from a given archive and keep folder structure.
            //-bsp1 redirects the standard output so the progress can be shown.
            ExtractISO.StartInfo.CreateNoWindow = true;
            ExtractISO.StartInfo.RedirectStandardOutput = true;
            ExtractISO.StartInfo.UseShellExecute = false;
            
            //Send any output to the event handler responsible for updating the text box.
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
                    MessageBox.Show(ProgramStrings.INFO_ISO_EXTRACT_SUCCESS, this.Text,
                        MessageBoxButtons.OK, MessageBoxIcon.Information) ;
                    break;
                case 1: //If there were non-fatal errors/warnings...
                    MessageBox.Show(ProgramStrings.INFO_ISO_EXTRACT_WARNING, this.Text,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 2: //If there was a fatal error...
                    MessageBox.Show(ProgramStrings.ERR_ISO_EXTRACT_FATAL, this.Text,
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case 7: //If there was a command line error.
                    MessageBox.Show(ProgramStrings.ERR_ISO_EXTRACT_CLI, this.Text,
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case 8: //If we have ran out of memory...
                    MessageBox.Show(ProgramStrings.ERR_ISO_EXTRACT_NO_MEMORY, this.Text,
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case -1073741510: //If the user closed the window before it was completed or pressed CTRL+C...
                    MessageBox.Show(ProgramStrings.ERR_ISO_EXTRACT_USER_ABORT, this.Text,
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
            ExtractISOButton.Enabled = true;
            OutputFolderSelectButton.Enabled = true;
            ISOSelectButton.Enabled = true;
        }


        string CreateOpenFileDialog(string name, string directory, string filter)
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
                    return OpenDialog.FileName;
                }
                return null;
            }
            
        }

        string CreateOpenFolderDialog(string name)
        {
            //Set up the file dialog by specifying its a folder picker, and set the initial directory.
            using (CommonOpenFileDialog FolderSelectDialog = new CommonOpenFileDialog())
            {
                FolderSelectDialog.IsFolderPicker = true;
                FolderSelectDialog.Title = name;

                //Show the dialog, and if the user has supplied a folder...
                if (FolderSelectDialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    //Return the folder path.
                    return FolderSelectDialog.FileName;
                }
            }
            return null; //Return null if the user pressed Cancel.
        }


        //When the user has clicked the button to browse for an ISO file...
        private void ISOSelectButton_Click(object sender, EventArgs e) 
        {
            ISOPathBox.Text = CreateOpenFileDialog(ProgramStrings.ISO_SELECT_WINDOW_TITLE, ProgramStrings.ISO_SELECT_WINDOW_DIRECTORY
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
                FolderPathBox.Text = CreateOpenFolderDialog(ProgramStrings.OUTPUT_FOLDER_SELECT_TITLE);
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
                ExtractISO(SevenZipLocation);

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
           ISOFolderBox.Text = CreateOpenFolderDialog(ProgramStrings.ISO_FOLDER_SELECT_TITLE);
        }

        private void PKGListButton_Click(object sender, EventArgs e)
        {
            PKGListBox.Text = CreateOpenFileDialog(ProgramStrings.ISO_FOLDER_SELECT_TITLE, 
                ProgramStrings.ISO_FOLDER_SELECT_WINDOW_DIRECTORY, ProgramStrings.ISO_FOLDER_SELECT_WINDOW_FILTER);
        }
    }
}
