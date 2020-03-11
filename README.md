# WallpaperUpdater
Small windows service used to update wallpaper in the register

# Dependencies
- .NET Framework 4.7.2

# Prepare source
- Download source code via zip or clone repository
- Open solution with Visual Stuido
- Build both projects

# Install exe with installlutil
- Install service using installutil.exe with CMD in administrator mode  
  Example call: c:\windows\microsoft.net\framework\v4.0.30319\installutil.exe /WallpaperImageFilePath="C:="<full path to image>" /LockScreenImageFilePath="C:="<full path to image>" WallPaperUpdater.exe  
  For /ImageFilePath provide path to your image
- Installer will ask for credentials for a service user
- Open Windows services and start the service (only needed for 1st time)
  
# Install with msi
- Run msi
- Provide file paths to images
- Provide installation location
- Installer will ask for credentials for a service user
- Open Windows services and start the service (only needed for 1st time)
  
# Logs
- Open Windows Event log
- Navigate to Application group under Windows logs
- Look for source WallpaperUpdater

# Removal of service
- Run uninstall command with installutil.exe with CMD in administrator mode  
  Example call: c:\windows\microsoft.net\framework\v4.0.30319\installutil.exe /u WallPaperUpdater.exe
- If used MSI just run it again and select remove
