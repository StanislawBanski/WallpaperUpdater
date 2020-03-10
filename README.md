# WallpaperUpdater
Small windows service used to update wallpaper in the register

# Dependencies
- .NET Framework 4.7.2

# Installation
- Download source code via zip or clone repository
- Open solution with Visual Stuido
- Rebuild
- Install service using installutil.exe with CMD in administrator mode  
  Example call: c:\windows\microsoft.net\framework\v4.0.30319\installutil.exe /ImageFilePath="<full path to image>" WallPaperUpdater.exe  
  For /ImageFilePath provide path to your image
- Instller will ask for credentials for a service user
- Open Windows services and start the service (only needed for 1st time)
  
# Logs
- Open Windows Event log
- Navigate to Application group under Windows logs
- Look for source WallpaperUpdater

# Removal of service
- Run uninstall command with installutil.exe with CMD in administrator mode  
  Example call: c:\windows\microsoft.net\framework\v4.0.30319\installutil.exe /u WallPaperUpdater.exe
