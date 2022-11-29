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
            @"iex ((New-Object System.Net.WebClient).DownloadString('https://community.chocolatey.org/install.ps1')); choco install " + InstallDrive + @"\setup\packages.config -y";
            instChoco.StartInfo.RedirectStandardOutput = false;
            instChoco.StartInfo.CreateNoWindow = false;
            instChoco.StartInfo.UseShellExecute = false;
            instChoco.Start();
            instChoco.WaitForExit();
            Console.WriteLine("Done. Chocolatey and your packages should now be installed.");
            Console.WriteLine("To update your packages, run 'choco upgrade all' from an elevated command prompt.");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }

}

