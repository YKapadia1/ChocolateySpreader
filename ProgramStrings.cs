using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocolateySpreader
{
    public class ProgramStrings
    {
        //Strings for the various dialogs presented to the user.
        public const string DEFAULT_7ZIP_LOCATION = "C:\\Program Files\\7-Zip\\7z.exe";

        public const string ISO_SELECT_WINDOW_TITLE = "Select Windows ISO";
        public const string ISO_SELECT_WINDOW_DIRECTORY = "C:\\";
        public const string ISO_SELECT_WINDOW_FILTER = "ISO Files (*.iso)|*.iso|All Files (*.*)|*.*";

        public const string ISO_FOLDER_SELECT_WINDOW_TITLE = "Select packages.config";
        public const string ISO_FOLDER_SELECT_WINDOW_DIRECTORY = "C:\\";
        public const string ISO_FOLDER_SELECT_WINDOW_FILTER = "Package List (packages.config)|packages.config|All Files(*.*)|*.*";

        public const string OUTPUT_FOLDER_SELECT_TITLE = "Select folder to extract ISO to:";

        public const string ZIP_SELECT_WINDOW_TITLE = "Locate 7z.exe";
        public const string ZIP_SELECT_WINDOW_DIRECTORY = "C:\\Program Files";
        public const string ZIP_SELECT_WINDOW_FILTER = "7-Zip|7z.exe";


        //Strings containing the various messages that may be displayed to the user.
        //Strings related to selecting an ISO and an output folder.
        public const string ERR_NO_ISO_SPECIFIED_TITLE = "No ISO file";
        public const string ERR_NO_ISO_SPECIFIED = "You have not specified an ISO file to extract.";
        public const string ERR_NO_ISO_FOLDER_SPECIFIED_TITLE = "No ISO folder";
        public const string ERR_NO_ISO_FOLDER_SPECIFIED = "You have not specified the folder where the Windows ISO files are located.";
        public const string ERR_NO_OUTPUT_SPECIFIED_TITLE = "No output folder";
        public const string ERR_NO_OUTPUT_SPECIFIED = "You have not specified an output folder to extract to.";

        public const string WARN_SPECIFY_ISO_BEFORE_OUTPUT = "Please specify an ISO before specifiying the output folder.";
        public const string WARN_SPECIFY_ISO_BEFORE_OUTPUT_TITLE = "No ISO file";
        public const string WARN_OUTPUT_FOLDER_NOT_EMPTY = "There appear to be files/folders inside the output folder you specified.\n" +
            "Would you like to extract to this folder anyway?";

        //Strings related to the status of the extraction process.
        public const string INFO_ISO_EXTRACT_SUCCESS = "ISO extracted successfully!";
        public const string INFO_ISO_EXTRACT_WARNING = "ISO extracted with warnings. Please check for any corrupt/missing files.";
        public const string ERR_ISO_EXTRACT_FATAL = "A fatal error occured when extracting the ISO.";
        public const string ERR_ISO_EXTRACT_CLI = "A command line error occured when extracting the ISO.";
        public const string ERR_ISO_EXTRACT_NO_MEMORY = "There is not enough free memory available to extract the ISO.";
        public const string ERR_ISO_EXTRACT_USER_ABORT = "The user cancelled the operation.";

        //Strings related to locating 7-Zip.
        public const string ERR_INVALID_7Z_EXE = "Invalid 7z.exe supplied!";
        public const string INFO_7Z_INSTALL_TIP = "If you do not have 7-Zip installed, you can get it from 7-zip.org.";

        //Strings related to selecting a folder with the ISO files and a packages list file.
        public const string ERR_INVALID_ISO_FOLDER_SPECIFIED_TITLE = "Invalid Windows ISO folder";
        public const string ERR_INVALID_ISO_FOLDER_SPECIFIED = "Could not find install.esd/.wim/.swm. Please check that the folder contains valid Windows ISO files.";
        public const string ERR_NO_PACKAGE_LIST_SPECIFIED_TITLE = "No packages.config file";
        public const string ERR_NO_PACKAGE_LIST_SPECIFIED = "You have not specified the location to your Chocolatey package list. You can create one by going to https://community.chocolatey.org/packages.\n" +
            "If Chocolatey is installed on this machine, press the \"Export Packages\" button at the top.";
        public const string ISO_FOLDER_SELECT_TITLE = "Select folder with Windows ISO files:";
    }
}
