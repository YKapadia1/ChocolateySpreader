using System;
using System.Diagnostics;
using System.IO;

namespace ChocolateyBaker
{
    class Program
    {
        static void Main()
        {
            string InstallDrive = null;
            Console.WriteLine("Installing Chocolatey and your packages now. You should see the icons appear on your desktop...\n");
            //While no Windows install drive has been found...
            while (InstallDrive == null)
            {
                DriveInfo[] currentDrives = DriveInfo.GetDrives();
                for (int i = 0; i < currentDrives.Length; i++)
                {
                    if (File.Exists(currentDrives[i].Name + "\\sources\\install.esd")
                        || File.Exists(currentDrives[i].Name + "\\sources\\install.wim")
                        || File.Exists(currentDrives[i].Name + "\\sources\\install.swm"))
                    {
                        InstallDrive = currentDrives[i].Name;
                    }

                }
                //If the drive used to install Windows could not be found...
                //Unlikely this would happen, as this program would not run if that was the case.
                if (InstallDrive == null)
                {
                    Console.WriteLine("Could not find the drive used to install Windows!");
                    Console.WriteLine("Please insert the drive that was used to install Windows and press any key to continue...\n");
                    Console.ReadKey();
                }
            }
            Process instChoco = new Process();
            instChoco.StartInfo.FileName = "powershell.exe";
            instChoco.StartInfo.Arguments = "-NoProfile -Inputformat None -ExecutionPolicy Bypass -Command " +
            "[System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072; " +
            @"iex ((New-Object System.Net.WebClient).DownloadString('https://community.chocolatey.org/install.ps1'))";
            instChoco.StartInfo.CreateNoWindow = false;
            instChoco.StartInfo.UseShellExecute = false;
            instChoco.Start();
            instChoco.WaitForExit();


            Process instPackages = new Process();
            instPackages.StartInfo.FileName = "powershell.exe";
            instPackages.StartInfo.Arguments = "choco install " + InstallDrive + @"setup\packages.config -y";
            instPackages.StartInfo.RedirectStandardOutput = false;
            instPackages.StartInfo.CreateNoWindow = false;
            instPackages.StartInfo.UseShellExecute = false;
            instPackages.Start();
            instPackages.WaitForExit();

            Console.WriteLine("Done. Chocolatey and your specified packages should now be installed.");
            Console.WriteLine("To update your packages, run the command 'choco upgrade all' in an elevated command prompt.");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }

}

