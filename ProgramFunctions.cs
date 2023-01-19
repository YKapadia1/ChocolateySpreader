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

        //Various constants related to file locations or program arguments.
        public const string ISOCreatorEXE = "C:\\Program Files (x86)\\Windows Kits\\10\\Assessment and Deployment Kit\\Deployment Tools\\amd64\\Oscdimg\\oscdimg.exe";
        const string OOBEFolder = @"\sources\$OEM$\$$\Setup\Scripts";
        const string ChocoBakerFolder = @"\setup";
        
        //The arguments to be passed to the ISO creator.
        //-m ignores the maximum file size an image can have, -o enables MD5 hashing.
        //-u2 creates the image using the UDF file system, -udfver102 uses UDF version 1.02.
        //-l specifies the volume label. In this case, it will be "ESD-ISO".
        //-bootdata specifies the number of boot entries on the ISO. In this case, 2 are provided.
        //One for non-UEFI systems, and one for UEFI enabled systems.
        const string ISOCreatorArgs1 = @"-w4 -m -o -u2 -udfver102 -lESD-ISO -bootdata:2#p0,e,b";
        const string ISOCreatorArgs2 = @"\boot\etfsboot.com#pEF,e,b";
        const string ISOCreatorArgs3 = @"\efi\microsoft\boot\efisys.bin ";


        
        //Two DLL imports to enable the synchronisation of scrolling between multiple controls.
        [DllImport("User32.dll")]
        public extern static int GetScrollPos(IntPtr hWnd, int nBar);


        [DllImport("User32.dll")]
        public extern static int SendMessage(IntPtr hWnd, int msg, int wParam, ref Point lParam);

        
        
        public void Checkfor7Z(ref string SevenZipLocation, Form1 form1)
        {
            //Check if 7-Zip is installed. This should work on both 32 and 64 bit systems.
            //This will fail if the user has installed 7-Zip in an alternate location.
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
                                MessageBox.Show(ProgramStrings.ERR_INVALID_7Z_EXE, form1.Text,
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
                            MessageBox.Show(ProgramStrings.INFO_7Z_INSTALL_TIP, form1.Text,
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //Open the 7-Zip website.
                            Process.Start("https://www.7-zip.org/");
                        }
                    }
                }
            }
            if (SevenZipInstalled)
            {
                ExtractISO(SevenZipLocation, form1.FolderPathBox.Text, form1.ISOPathBox.Text, form1);
                //Extract the ISO and re-enable the buttons after the extraction process has exited.
            }
        }

        public void ExtractISO(string SevenZipLocation, string OutputPath, string ISOPath, Form1 form1)
        {
            //Disable the buttons to prevent accidental user input.
            //For every control in the form...
            foreach (Control control in form1.Controls)
            {
                //If the control is a button...
                if (control is Button)
                {
                    //Flip its enabled state.
                    //If enabled, it will be flipped to disabled, and vice versa.
                    control.Enabled = !control.Enabled;
                }
            }
           
            
            
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
            foreach (Control control in form1.Controls)
            {
                if (control is Button)
                {
                    control.Enabled = !control.Enabled;
                }
            }
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
            //Clear any text inside the output window.
            Output.Clear();
            
            try //Try to...
            {
                //Insert text into the output window letting the user know what the program is doing.
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

                //For every file to be copied in the FileInfo object...
                foreach (FileInfo file in OOBEFiles)
                {
                    //Insert the file into the directory.
                    file.CopyTo(OOBEDest.FullName + @"\" + file.Name, true);
                    //Insert text into the output window of what file was copied.
                    Output.AppendText(file + "\n");
                }

                Output.AppendText("Inserting ChocolateyBaker into " + Destination.Text + ChocoBakerFolder + "...\n");

                foreach (FileInfo file in ChocoBakerFiles)
                {
                    file.CopyTo(ChocoBakerDest.FullName + @"\" + file.Name, true);
                    Output.AppendText("Inserted " + file.Name + "\n");
                }
                
                //If a packages.config file already exists...
                if (File.Exists(Destination.Text + @"\setup\packages.config"))
                {
                    //Delete it. This does not have any user warning, consider adding one.
                    File.Delete(Destination.Text + @"\setup\packages.config");
                }
                //Copy the packages.config file.
                File.Copy(PKGListLocation, Destination.Text + @"\setup\packages.config");
            }
            catch (UnauthorizedAccessException)
            //If the user does not have sufficient privileges...
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
            //If oscdimg.exe exists...
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
