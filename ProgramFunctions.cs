using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ChocolateySpreader
{
    public class ProgramFunctions
    {
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
    }

    
}
