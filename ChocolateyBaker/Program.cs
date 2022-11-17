using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

namespace ChocolateyBaker
{
    class Program
    {
        static void Main()
        {
            
            Console.WriteLine("Installing Chocolatey...\n");
            DriveInfo[] StorageDrives = DriveInfo.GetDrives();
            bool WindowsISODrive = false;
            //A process that runs Powershell and installs Chocolatey.
            Process chocoInstall = new Process();
            chocoInstall.StartInfo.FileName = "powershell.exe";
            chocoInstall.StartInfo.Arguments = "-NoProfile -Inputformat None -ExecutionPolicy Bypass -Command " +
                        "[System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072; " +
                        "iex ((New-Object System.Net.WebClient).DownloadString('https://community.chocolatey.org/install.ps1')) " +
                        "> C:\\chocoInstallLog.txt;";
            chocoInstall.StartInfo.Verb = "runas";
            chocoInstall.StartInfo.CreateNoWindow = false;
            chocoInstall.Start();
            chocoInstall.WaitForExit();


            //Get the amount of storage drives connected, including USB drives.
            for (int i = 0; i < StorageDrives.Count(); i++)
            {
                //If the drive contains an install.wim/.esd/.swm in the specified directory...
                //It must be done this way as its impossible to know what drive letter corresponds to which drive.
                //It's easier to ask every drive connected if they have this file in this directory.
                if (File.Exists(StorageDrives[i].Name + "\\sources\\install.wim") || 
                    File.Exists(StorageDrives[i].Name + "\\sources\\install.esd") || 
                    File.Exists(StorageDrives[i].Name + "\\sources\\install.swm"))
                {
                    WindowsISODrive = true;
                    Console.WriteLine("Installing packages. You should see the icons appear on your desktop...");
                    //Create a process that will call Chocolatey to install packages from a specified package list.
                    Process chocoPKGInstall = new Process();
                    chocoPKGInstall.StartInfo.FileName = "choco";
                    chocoPKGInstall.StartInfo.Arguments = "install " + StorageDrives[i].Name + "setup\\packages.config -y";
                    chocoPKGInstall.StartInfo.Verb = "runas";
                    chocoPKGInstall.StartInfo.CreateNoWindow = false;
                    chocoPKGInstall.Start();
                    chocoPKGInstall.WaitForExit();
                }
            }
            //If the drive containing the Windows install files could not be found...
            if (!WindowsISODrive)
            {
                Console.WriteLine("Could not find the drive containing the Windows install files!");
                Console.WriteLine("Please insert the drive that was used to install Windows and re-run this program.");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }

}

