# ChocolateySpreader
An application to inject automatic package installers into Windows installation media to be executed as a post-install script.
This readme is under construction, and will be updated as the project progresses.

## System Requirements

To use this application, you will need at least the following:

- Windows 7 or later (64-bit)
- .NET Core 6 or later
- [7-Zip](https://7-zip.org)
- Windows Deployment Tools, found in the [Windows Assessment and Deployment Kit](https://aka.ms/windows/adk)
- (optional) Chocolatey, to enable the use of exporting the list of currently installed packages.

## How Does It Work?

This application inserts the following files into Windows installation files that allow the automatic installation of programs when the operating system is installed and the user lands on the desktop.
- OOBE.cmd: This file sets up RunOnceEx, which will call ChocolateyBaker with admin rights to install the packages.
- ChocolateyBaker: This will download the latest version of Chocolatey and install the packages located in packages.config.
Note that unless specified in packages.config, Chocolatey will install the latest version of the packages.


## How To Use

This section and subsequent sections are under construction.