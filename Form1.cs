using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Configuration;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace ChocolateySpreader
{

    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        //Create a new Font object to be used for detecting Chocolatey presence.
        Font ChocoPresence = new Font("Segoe UI", 8.25f, style: FontStyle.Bold);
        
        //Initialise a new functions object so the methods can be called.
        ProgramFunctions Functions = new ProgramFunctions();





        //Code found at https://stackoverflow.com/questions/1827323/synchronize-scroll-position-of-two-richtextboxes
        //Create constants necessary for program operation.
        const int WM_USER = 0x400;
        const int EM_GETSCROLLPOS = WM_USER + 221;
        const int EM_SETSCROLLPOS = WM_USER + 222;
        //Code found at https://stackoverflow.com/questions/1827323/synchronize-scroll-position-of-two-richtextboxes


        string chocoEnv = ""; //Initialise to null so it can be used later.


        //An asynchronous method to update a control without freezing the UI.
        public void OutputLog(object sendingProcess, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                if (e.Data.Contains("%"))
                {
                    BeginInvoke(new MethodInvoker(() => { OutputBox.AppendText(e.Data); }));
                }
                else
                {
                    BeginInvoke(new MethodInvoker(() => { OutputBox.AppendText(e.Data + Environment.NewLine); }));
                    //Invoke the UI thread and update the text box. It must be done this way to ensure asynchronous operation.
                    //If this was done synchronously, the UI would freeze.
                }
            }
        }

        //When the user has clicked the button to browse for an ISO file...
        private void ISOSelectButton_Click(object sender, EventArgs e)
        {
            //Create an open file dialog, and set the text of the text box to the selected file's path.
            ISOPathBox.Text = Functions.CreateOpenFileDialog(ProgramStrings.ISO_SELECT_WINDOW_TITLE, ProgramStrings.ISO_SELECT_WINDOW_DIRECTORY
                , ProgramStrings.ISO_SELECT_WINDOW_FILTER);
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
                //Show a message box to the user.
            }
            //If the user has not selected an output folder...
            else if (FolderPathBox.Text == "")
            {
                MessageBox.Show(ProgramStrings.ERR_NO_OUTPUT_SPECIFIED, ProgramStrings.ERR_NO_OUTPUT_SPECIFIED_TITLE,
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //If the folder the user has selected already contains files...
            else if (Directory.GetFileSystemEntries(FolderPathBox.Text).Length != 0)
            {
                switch (MessageBox.Show(ProgramStrings.WARN_OUTPUT_FOLDER_NOT_EMPTY, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    case DialogResult.Yes:
                        Functions.Checkfor7Z(ref SevenZipLocation, this);
                        break;
                }
            }
            else
            {
                Functions.Checkfor7Z(ref SevenZipLocation, this);
            }
        }
        
        //If the user has clicked the button to insert the scripts and create the ISO...
        private void ChocoSpreadButton_Click(object sender, EventArgs e)
        {
            //If the user has not specified the folder where the ISO files are located...
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
            //If the user has not specified a location to save the final ISO...
            else if (FinalISOPath.Text == "")
            {
                MessageBox.Show(ProgramStrings.ERR_NO_OUTPUT_ISO_PATH_SPECIFIED, this.Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!File.Exists(ProgramFunctions.ISOCreatorEXE))
            {
                MessageBox.Show(ProgramStrings.ERR_ISO_CREATOR_NOT_FOUND, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Process.Start("https://learn.microsoft.com/en-us/windows-hardware/get-started/adk-install");
                //Open the following website. This will open the user's default browser.
            }
            //Final check: If the user has supplied a folder path containing valid Windows ISO files...
            else if (File.Exists(ISOFolderBox.Text + @"\sources\install.wim") || File.Exists(ISOFolderBox.Text + @"\sources\install.esd") || File.Exists(ISOFolderBox.Text + @"\sources\install.swm"))
            {
                MessageBox.Show(ProgramStrings.CHOICE_INSERT_OOBE_OPERATION + ISOFolderBox.Text +
                    ProgramStrings.CHOICE_INSERT_OOBE_LOCATION +
                    ProgramStrings.CHOICE_INSERT_OOBE_EXPLANATION, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);


                if (MessageBox.Show(ProgramStrings.CHOICE_INSERT_PKG_FILE + ISOFolderBox.Text +
                    ProgramStrings.CHOICE_INSERT_PKG_FILE_LOCATION +
                    ProgramStrings.CHOICE_INSERT_FILES_QUESTION, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    foreach (Control control in this.Controls)
                    {
                        if (control is Button)
                        {
                            control.Enabled = !control.Enabled;
                        }
                    }


                    Functions.InsertFiles(ISOFolderBox, ISOFolderBox.Text, FinalISOPath.Text, PKGListBox.Text, OutputBox, this);
                    foreach (Control control in this.Controls)
                    {
                        if (control is Button)
                        {
                            control.Enabled = !control.Enabled;
                        }
                    }
                }
            }
            else
            //If not...
            {
                MessageBox.Show(ProgramStrings.ERR_INVALID_ISO_FOLDER_SPECIFIED, ProgramStrings.ERR_INVALID_ISO_FOLDER_SPECIFIED_TITLE,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ISOFolderButton_Click(object sender, EventArgs e)
        {
            //Set the text of the text box by opening a open folder dialog and getting the folder path from that.
            ISOFolderBox.Text = Functions.CreateOpenFolderDialog(ProgramStrings.ISO_FOLDER_SELECT_TITLE);
        }

        private void PKGListButton_Click(object sender, EventArgs e)
        {
            //Set the text of the text box by opening a open file dialog and getting the file path from that.
            PKGListBox.Text = Functions.CreateOpenFileDialog(ProgramStrings.PKG_LIST_SELECT_WINDOW_TITLE,
                ProgramStrings.PKG_LIST_SELECT_WINDOW_DIRECTORY, ProgramStrings.PKG_LIST_SELECT_WINDOW_FILTER);

            //If the text box is not empty...
            if (PKGListBox.Text != String.Empty)
            {
                XmlReaderSettings settings = new XmlReaderSettings(); //Create a new XML parser settings instance.
                settings.IgnoreWhitespace = true; //Ignore any whitespaces.
                PKGListViewBox.Clear();
                PKGListVersionBox.Clear();
                try //Try to...
                {
                    using (var fileStream = File.OpenText(PKGListBox.Text)) //Open the package file.    
                    using (XmlReader reader = XmlReader.Create(fileStream, settings))
                    //Create an XML parser instance.
                    {
                        while (reader.Read()) //While the reader is reading through the file...
                        {
                            switch (reader.NodeType)
                            {
                                case XmlNodeType.Element: //If the reader has found an element...
                                    if (reader.GetAttribute("id") != null) //If the value returned is not null...
                                    {
                                        PKGListViewBox.AppendText(reader.GetAttribute("id") + "\n"); //Put it into the text box.
                                    }
                                    if (reader.GetAttribute("version") != null)
                                    {
                                        PKGListVersionBox.AppendText(reader.GetAttribute("version") + "\n");
                                    }
                                    //These if statements are necessary, otherwise a blank line will be inserted, causing the positions of the
                                    //text boxes to be out of sync when scrolling.
                                    break;
                            }
                        }
                    }
                }
                catch (ArgumentException) //If the filestream throws an argument exception...
                {
                    PKGListViewBox.Text = "No package list loaded.";
                    PKGListVersionBox.Text = PKGListViewBox.Text;
                    //Reset the text of the text boxes, as they get cleared before the XML reader tries to open the file.
                    return; //Handle it and do nothing else. This is usually caused when a user cancels the open file dialog.
                    //The if statement above shouldn't let the code run to this point, but I added it just in case.
                    
                }
                catch (XmlException) //If the parser instance has thrown an exception...
                                     //This could be due to a bad packages.config file, or the user has given a file that is not a package list.
                {
                    MessageBox.Show(ProgramStrings.ERR_PKGLIST_PARSE_ERROR, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //When the program starts, get the environment variable for Chocolatey.
            chocoEnv = Environment.ExpandEnvironmentVariables("%ChocolateyInstall%");
            //If the environment variable is not found, it will just expand to %ChocolateyInstall%, which won't do anything.
            //If the environment variable is found, it will expand to where chocolatey is installed.


            //Use the expanded environment variable and the provided path to determine if Chocolatey is installed.
            if (File.Exists(chocoEnv + "\\choco.exe"))
            {
                MessageBox.Show(ProgramStrings.CHOCO_DETECTED_MSG1 + ProgramStrings.CHOCO_DETECTED_MSG2,
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Inform the user that Chocolatey has been detected, and what they can do with it.
                ChocoDetectLabel.Text = ProgramStrings.CHOCO_DETECTED_LABEL;
                ChocoDetectLabel.Font = ChocoPresence;
                ChocoDetectLabel.ForeColor = Color.LimeGreen;
                //Change the label text, font and colour.
                ChocoDetectLabel.Location = new Point(535, 9);
                //Change the position of the label for better presentation.
            }
            else
            {
                MessageBox.Show(ProgramStrings.CHOCO_NOT_DETECTED_MSG1 + ProgramStrings.CHOCO_NOT_DETECTED_MSG2,
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ChocoDetectLabel.Text = ProgramStrings.CHOCO_NOT_DETECTED_LABEL;
                ChocoDetectLabel.Font = ChocoPresence;
                ChocoDetectLabel.ForeColor = Color.Red;
                ChocoDetectLabel.Location = new Point(525, 9);
                ChocoExportButton.Enabled = false;
            }
        }

        //When the user clicks the button to export their package list...
        private void ChocoExportButton_Click(object sender, EventArgs e)
        {
            string PKGListOutput = null;
            //Create a save file dialog.
            using (SaveFileDialog SavePKGList = new SaveFileDialog())
            {
                SavePKGList.Title = ProgramStrings.PKG_LIST_OUTPUT_SELECT_TITLE; //Set the title of the dialog.
                SavePKGList.InitialDirectory = "C:\\"; //Set the initial directory.
                SavePKGList.Filter = ProgramStrings.PKG_LIST_SELECT_WINDOW_FILTER; //Set the filters
                SavePKGList.FilterIndex = 1; //Set the default filter.
                SavePKGList.RestoreDirectory = false; //Do not restore the directory the user was on if this dialog was opened before.
                SavePKGList.OverwritePrompt = true; //Ask the user if they want to overwrite the file they selected.
                if (SavePKGList.ShowDialog() == DialogResult.OK)
                {
                    PKGListOutput = SavePKGList.FileName;
                }
            }
            if (PKGListOutput != null)
            {
                //Create a new process instance that will call Chocolatey and export the list of installed packages with their version numbers.
                Process ChocoExportPKGList = new Process();
                ChocoExportPKGList.StartInfo.FileName = chocoEnv + "\\choco.exe"; //The path to the Chocolatey executable.
                ChocoExportPKGList.StartInfo.Arguments = "export " + PKGListOutput + " --include-version-numbers"; //The arguments required.
                ChocoExportPKGList.StartInfo.CreateNoWindow = true; //INFO: This can probably be removed, since a new window gets created anyway...
                ChocoExportPKGList.StartInfo.Verb = "runas"; //Run the process as an administrator.
                ChocoExportPKGList.Start();
                ChocoExportPKGList.WaitForExit();
                switch (ChocoExportPKGList.ExitCode) //Check the exit code.
                {
                    case 0: //If there was no error...
                        MessageBox.Show(ProgramStrings.INFO_CHOCO_PKG_LIST_EXPORT_SUCCESS, this.Text,
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case 1: //If there was an error...
                        MessageBox.Show(ProgramStrings.ERR_CHOCO_PKG_LIST_EXPORT_FAIL, this.Text,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case -1: //If there was a fatal error...
                        MessageBox.Show(ProgramStrings.ERR_CHOCO_PKG_LIST_EXPORT_FAIL, this.Text,
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
        }

        //Code found at https://stackoverflow.com/questions/1827323/synchronize-scroll-position-of-two-richtextboxes
        private void PKGListViewBox_VScroll(object sender, EventArgs e)
        {
            Point pt = new Point();

            ProgramFunctions.SendMessage(PKGListViewBox.Handle, EM_GETSCROLLPOS, 0, ref pt);

            ProgramFunctions.SendMessage(PKGListVersionBox.Handle, EM_SETSCROLLPOS, 0, ref pt);
        }

        private void PKGListVersionBox_VScroll(object sender, EventArgs e)
        {
            Point pt = new Point();

            ProgramFunctions.SendMessage(PKGListVersionBox.Handle, EM_GETSCROLLPOS, 0, ref pt);

            ProgramFunctions.SendMessage(PKGListViewBox.Handle, EM_SETSCROLLPOS, 0, ref pt);
        }
        //Code found at https://stackoverflow.com/questions/1827323/synchronize-scroll-position-of-two-richtextboxes


        
        private void FinalISOButton_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog SaveFinalISO = new SaveFileDialog())
            {
                SaveFinalISO.Title = ProgramStrings.OUTPUT_ISO_SELECT_TITLE;
                SaveFinalISO.InitialDirectory = "C:\\";
                SaveFinalISO.Filter = ProgramStrings.ISO_SELECT_WINDOW_FILTER;
                SaveFinalISO.FilterIndex = 1;
                SaveFinalISO.RestoreDirectory = false;
                SaveFinalISO.OverwritePrompt = true;
                if (SaveFinalISO.ShowDialog() == DialogResult.OK)
                {
                    FinalISOPath.Text = SaveFinalISO.FileName;
                }
            }
        }

        //If the text in the output window has been changed...
        private void OutputBox_TextChanged(object sender, EventArgs e)
        {
            if (OutputBox.Text.Length != 0)
            {
                OutputBox.SelectionStart = OutputBox.Text.Length - 1; //Go to the end of the output box.
            }
        }
    }
}
