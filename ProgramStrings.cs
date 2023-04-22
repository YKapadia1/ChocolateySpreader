using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocolateySpreader
{
    public class ProgramStrings
    {
        //Strings for the various file dialogs presented to the user.
        public const string WINDOW_TITLE = "ChocolateySpreader";

        public const string DEFAULT_7ZIP_LOCATION = @"C:\Program Files\7-Zip\7z.exe";

        public const string ISO_SELECT_WINDOW_TITLE = "Select Windows ISO";
        public const string ISO_SELECT_WINDOW_DIRECTORY = "C:\\";
        public const string ISO_SELECT_WINDOW_FILTER = "ISO Files|*.iso|All Files|*.*";

        public const string ISO_FOLDER_SELECT_WINDOW_TITLE = "Select folder where ISO files are located:";

        public const string PKG_LIST_SELECT_WINDOW_TITLE = "Select packages.config";
        public const string PKG_LIST_SELECT_WINDOW_DIRECTORY = "C:\\";
        public const string PKG_LIST_SELECT_WINDOW_FILTER = "Package List|packages.config|All Files(*.*)|*.*";

        public const string OUTPUT_ISO_SELECT_TITLE = "Choose where to save the final ISO:";

        public const string OUTPUT_FOLDER_SELECT_TITLE = "Select folder to extract ISO to:";
        public const string PKG_LIST_OUTPUT_SELECT_TITLE = "Choose location to save package list:";

        public const string ZIP_SELECT_WINDOW_TITLE = "Locate 7z.exe";
        public const string ZIP_SELECT_WINDOW_DIRECTORY = @"C:\Program Files";
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
        public const string ERR_ISO_EXTRACT_CLI = "A command line error occured when extracting the ISO.\n The file path may be too long. Try moving the ISO to the root of the storage drive.";
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
            "If Chocolatey is installed on this machine, press the \"Export Package List\" button at the top.";
        public const string ISO_FOLDER_SELECT_TITLE = "Select folder with Windows ISO files:";
        public const string ERR_NO_OUTPUT_ISO_PATH_SPECIFIED = "You have not specified a location to save the finalised ISO.";


        //Strings related to Chocolatey presence detection.
        public const string CHOCO_DETECTED_MSG1 = "Chocolatey presence found!\n";
        public const string CHOCO_DETECTED_MSG2 = "You will be able to easily export a list of installed packages currently on this machine.";
        public const string CHOCO_DETECTED_LABEL = "Chocolatey Detected!";

        public const string CHOCO_NOT_DETECTED_MSG1 = "Chocolatey presence not found.\n";
        public const string CHOCO_NOT_DETECTED_MSG2 = "Chocolatey must be installed to export a list of installed packages currently on this machine.";
        public const string CHOCO_NOT_DETECTED_LABEL = "Chocolatey Not Detected";


        //Strings related to exporting the package list.
        public const string INFO_CHOCO_PKG_LIST_EXPORT_SUCCESS = "Package list successfully exported!";
        public const string ERR_CHOCO_PKG_LIST_EXPORT_FAIL = "An error occured while exporting the package list.";


        //Strings informing the user what is going to happen when they press the "Insert Scripts" button.
        public const string CHOICE_INSERT_OOBE_OPERATION = "This will insert OOBE.cmd into the following location:\n";
        public const string CHOICE_INSERT_OOBE_LOCATION = "\\sources\\$OEM\\$$\\Setup\\Scripts\n";
        public const string CHOICE_INSERT_OOBE_EXPLANATION = "OOBE.cmd will run ChocolateyBaker and install your packages.";

        public const string CHOICE_INSERT_PKG_FILE = "ChocolateyBaker and your specified package list will be inserted into the following location.\n";
        public const string CHOICE_INSERT_PKG_FILE_LOCATION = "\\setup\n";

        public const string CHOICE_ISO_CREATION_TEXT = "Once the files are inserted into the folders, a new Windows ISO will be created.";
        public const string CHOICE_INSERT_FILES_QUESTION = "Do you wish to continue?";


        //The string related to selecting a package list.
        public const string ERR_PKGLIST_PARSE_ERROR = "An error occured when attempting to parse the package file.";
        public const string ERR_PKGLIST_CONTAINS_WHITESPACE = "The package list contains a whitespace as its first character. Please remove it.";
        public const string ERR_PKGLIST_INVALID = "Please specify a valid .config file.";


        //Strings related to creating the ISO.
        public const string ERR_DIRECTORY_NOT_FOUND = "The directory ";
        public const string ERR_ISO_CREATOR_NOT_FOUND = "The Windows Assessment and Deployment Kit does not appear to be installed. You can install it from here: https://learn.microsoft.com/en-us/windows-hardware/get-started/adk-install";
        public const string ERR_USER_UNAUTHORISED = "This user account is not authorised to write to the location you specified. Try running this program as an administrator.";
        public const string ERR_FILE_NOT_FOUND1 = "The file ";
        public const string ERR_FILE_NOT_FOUND2 = " could not be found. Please check it exists.";
        public const string ERR_PATH_TOO_LONG = "The path to the ISO files is too long. Try shortening it by moving it out of sub-folders or by reducing the length of the folder name.";
        public const string ERR_ISO_CREATION_ERROR = "An error occured when creating the ISO. Please check the Output window for details.";
        public const string INFO_ISO_CREATION_SUCCESS1 = "Your ISO was successfully created! Please note the following things however:\n";
        public const string INFO_ISO_CREATION_SUCCESS2 = "An active Internet connection is required throughout the setup process.\n";
        public const string INFO_ISO_CREATION_SUCCESS3 = "The auto-install scripts will not work on a Ventoy enabled USB drive. You will need to extract the setup folder from this ISO onto the root of the USB drive.";



        //Strings related to providing the user with help in case they are stuck.
        public const string HELP_ISO_PATH = "This is where your Windows ISO is located.";
        public const string HELP_OUTPUT_FOLDER_LOCATION = "This is where you want the extracted ISO files to go.";
        public const string HELP_ISO_FOLDER_LOCATION = "This is where your extracted ISO files are located. It should have a setup.exe application, as well as a sources folder. If you cannot find such a folder, you may need to extract a Windows ISO by going to the Extract ISO tab.";
        public const string HELP_PKG_LIST_LOCATION = "This is where your Chocolatey package list is located. If you do not have one, you can do one of the following:\n\nIf you already have Chocolatey and some packages installed on this machine, you can use the button below to export one if you have Chocolatey installed.\n\nIf you do not have Chocolatey installed on this machine, you can go to https://community.chocolatey.org/packages and create one there. More detailed instructions can be found in the README file.";
        public const string HELP_FINAL_ISO_LOCATION = "This is the location where you want the new ISO to be saved to. It is recommended to save it in a place that you have easy access to. You will need some other program like Rufus in order to create bootable installation media using this ISO.";

    }
}
