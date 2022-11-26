using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ChocolateySpreader
{
    public class ProgramFunctions
    {
        const string OOBEFolder = @"\sources\$OEM$\$$\Setup\Scripts";
        const string ChocoBakerFolder = @"\setup";
        public const string ISOCreatorEXE = "C:\\Program Files (x86)\\Windows Kits\\10\\Assessment and Deployment Kit\\Deployment Tools\\amd64\\Oscdimg\\oscdimg.exe";
        const string ISOCreatorArgs1 = @"-m -o -u2 -udfver102 -bootdata:2#p0,e,b";
        const string ISOCreatorArgs2 = @"\boot\etfsboot.com#pEF,e,b";
        const string ISOCreatorArgs3 = @"\efi\microsoft\boot\efisys.bin ";


        [DllImport("User32.dll")]
        public extern static int GetScrollPos(IntPtr hWnd, int nBar);


        [DllImport("User32.dll")]
        public extern static int SendMessage(IntPtr hWnd, int msg, int wParam, ref Point lParam);

        public void ExtractISO(string SevenZipLocation, string OutputPath, string ISOPath, Form1 form1)
        {
            //Disable the buttons to prevent accidental user input.
            form1.ExtractISOButton.Enabled = false;
            form1.ISOSelectButton.Enabled = false;
            form1.ISOFolderButton.Enabled = false;
            form1.PKGListButton.Enabled = false;
            form1.ChocoSpreadButton.Enabled = false;
            form1.OutputFolderSelectButton.Enabled = false;
            Process ExtractISO = new Process(); //Create a new process that we will start.
                                                //Set the file path to where 7-Zip has been located.
            ExtractISO.StartInfo.FileName = SevenZipLocation;
            ExtractISO.StartInfo.Arguments = " -o" + OutputPath + " -aoa -bsp1 x " + form1.ISOPathBox.Text;
            //Create the arguments necessary.
            //-o switch specifies output directory, -aoa specifies to replace any existing files without user interaction.
            //x specifies to extract files from a given archive and keep folder structure.
            //-bsp1 redirects the standard output so the progress can be shown.
            ExtractISO.StartInfo.CreateNoWindow = true;
            ExtractISO.StartInfo.RedirectStandardOutput = true;
            ExtractISO.StartInfo.UseShellExecute = false;

            //Send any output to the event handler responsible for updating the text box.
            ExtractISO.OutputDataReceived += new DataReceivedEventHandler(form1.OutputLog);
            ExtractISO.Start(); //Start the process.
            ExtractISO.BeginOutputReadLine(); //Begin asynchronously reading the output.
            while (!ExtractISO.HasExited) //If the process has not yet exited...
            {
                Application.DoEvents(); //Keep Form1 active to the text box can be updated.
            }
            switch (ExtractISO.ExitCode) //Check the exit code.
            {
                case 0: //If there was no error...
                    MessageBox.Show(ProgramStrings.INFO_ISO_EXTRACT_SUCCESS, form1.Text,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 1: //If there were non-fatal errors/warnings...
                    MessageBox.Show(ProgramStrings.INFO_ISO_EXTRACT_WARNING, form1.Text,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 2: //If there was a fatal error...
                    MessageBox.Show(ProgramStrings.ERR_ISO_EXTRACT_FATAL, form1.Text,
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case 7: //If there was a command line error.
                    MessageBox.Show(ProgramStrings.ERR_ISO_EXTRACT_CLI, form1.Text,
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case 8: //If we have ran out of memory...
                    MessageBox.Show(ProgramStrings.ERR_ISO_EXTRACT_NO_MEMORY, form1.Text,
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case -1073741510: //If the user closed the window before it was completed or pressed CTRL+C...
                    MessageBox.Show(ProgramStrings.ERR_ISO_EXTRACT_USER_ABORT, form1.Text,
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
            //Reenable the buttons once the operation is finished.
            form1.ExtractISOButton.Enabled = true;
            form1.OutputFolderSelectButton.Enabled = true;
            form1.ISOSelectButton.Enabled = true;
            form1.PKGListButton.Enabled = true;
            form1.ChocoSpreadButton.Enabled = true;
            form1.ISOFolderButton.Enabled = true;
        }

        public string CreateOpenFileDialog(string name, string directory, string filter)
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

        public string CreateOpenFolderDialog(string name)
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

        public void InsertFiles(TextBox Destination, string ISOFolderLocation, string OutputISOLocation, string PKGListLocation, RichTextBox Output, Form1 form1)
        {

            Output.Clear();
            try
            {
                Output.AppendText("Inserting OOBE.cmd into " + Destination.Text + OOBEFolder + "...\n");

                //Create the directories necessary. If they already exist, this will do nothing.
                Directory.CreateDirectory(Destination.Text + OOBEFolder);
                Directory.CreateDirectory(Destination.Text + ChocoBakerFolder);



                //Create new DirectoryInfo objects for the source files and the destinations.
                DirectoryInfo OOBESrc = new DirectoryInfo("./files");
                DirectoryInfo ChocoBakerSrc = new DirectoryInfo("./net6.0");
                DirectoryInfo OOBEDest = new DirectoryInfo(Destination.Text + OOBEFolder);
                DirectoryInfo ChocoBakerDest = new DirectoryInfo(Destination.Text + @"\setup");



                //Create new FileInfo objects based on the files in the directories the DirectoryInfo objects hold.
                FileInfo[] OOBEFiles = OOBESrc.GetFiles();
                FileInfo[] ChocoBakerFiles = ChocoBakerSrc.GetFiles();

                foreach (FileInfo file in OOBEFiles)
                {
                    file.CopyTo(OOBEDest.FullName + @"\" + file.Name, true);
                    Output.AppendText("Inserted OOBE.cmd\n");
                }

                Output.AppendText("Inserting ChocolateyBaker into " + Destination.Text + ChocoBakerFolder + "...\n");

                foreach (FileInfo file in ChocoBakerFiles)
                {
                    file.CopyTo(ChocoBakerDest.FullName + @"\" + file.Name, true);
                    Output.AppendText("Inserted " + file.Name + "\n");
                }
                if (File.Exists(Destination.Text + @"\setup\packages.config"))
                {
                    File.Delete(Destination.Text + @"\setup\packages.config");
                }
                File.Copy(PKGListLocation, Destination.Text + @"\setup\packages.config");
            }
            catch (UnauthorizedAccessException)
            //This exception shouldn't be thrown as this program is written to always be run as an Administrator.
            {
                MessageBox.Show(ProgramStrings.ERR_USER_UNAUTHORISED, ProgramStrings.WINDOW_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FileNotFoundException fe)
            //If a file to be copied could not be found...
            {
                MessageBox.Show(ProgramStrings.ERR_FILE_NOT_FOUND1 + fe.FileName + ProgramStrings.ERR_FILE_NOT_FOUND2, ProgramStrings.WINDOW_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (PathTooLongException)
            //If the path to the ISO's or the files to be copied are too long...
            {
                MessageBox.Show(ProgramStrings.ERR_PATH_TOO_LONG, ProgramStrings.WINDOW_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (File.Exists(ISOCreatorEXE))
            {
                Process CreateISO = new Process();
                CreateISO.StartInfo.FileName = ISOCreatorEXE;
                CreateISO.StartInfo.Arguments = ISOCreatorArgs1 + ISOFolderLocation + ISOCreatorArgs2 +
                    ISOFolderLocation + ISOCreatorArgs3 + ISOFolderLocation + " " + OutputISOLocation;
                form1.OutputBox.AppendText(CreateISO.StartInfo.Arguments);
                CreateISO.StartInfo.CreateNoWindow = true;
                CreateISO.StartInfo.RedirectStandardOutput = true;
                CreateISO.StartInfo.RedirectStandardError = true;
                CreateISO.StartInfo.UseShellExecute = false;

                CreateISO.OutputDataReceived += new DataReceivedEventHandler(form1.OutputLog);
                CreateISO.Start();
                CreateISO.BeginOutputReadLine();
                while (!CreateISO.HasExited)
                {
                    Application.DoEvents();
                }
                if (CreateISO.ExitCode == 1)
                {
                    MessageBox.Show(ProgramStrings.ERR_ISO_CREATION_ERROR, form1.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(ProgramStrings.INFO_ISO_CREATION_SUCCESS1 + ProgramStrings.INFO_ISO_CREATION_SUCCESS2 + ProgramStrings.INFO_ISO_CREATION_SUCCESS3, form1.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }
    }
}
