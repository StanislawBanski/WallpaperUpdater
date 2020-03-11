using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Timers;

namespace WallPaperUpdater
{
    public partial class WallPaperUpdater : ServiceBase
    {
        private EventLog eventLog;
        private int eventId = 1;
        private Timer timer;

        private string wallpaperImageFilePath;
        private string lockScreenImageFilePath;

        private const string userRoot = "HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System";
        private const string wallPaperKey = "Wallpaper";

        private const string machineRoot = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows\\Personalization";
        private const string lockScreenKey = "LockScreenImage";

        public WallPaperUpdater(string wpFilePath, string lsFilePath)
        {
            InitializeComponent();
            setupEventLog();
            eventLog.WriteEntry($"Received wallpaper image file path: {wpFilePath}", EventLogEntryType.Information, eventId++);
            eventLog.WriteEntry($"Received lockscreen image file path: {lsFilePath}", EventLogEntryType.Information, eventId++);
            wallpaperImageFilePath = wpFilePath;
            lockScreenImageFilePath = lsFilePath;
        }

        private void setupEventLog()
        {
            eventLog = new EventLog();
            if (!EventLog.SourceExists("WallpaperUpdater"))
            {
                EventLog.CreateEventSource("WallpaperUpdater", "");
            }
            eventLog.Source = "WallpaperUpdater";
            eventLog.Log = "";
        }

        protected override void OnStart(string[] args)
        {
            eventLog.WriteEntry("Starting...", EventLogEntryType.Information, eventId++);

            if (string.IsNullOrEmpty(wallpaperImageFilePath) || !File.Exists(wallpaperImageFilePath))
            {
                eventLog.WriteEntry($"Invalid wallpaper file path {wallpaperImageFilePath}", EventLogEntryType.Error, eventId++);
            } 
            else if (string.IsNullOrEmpty(lockScreenImageFilePath) || !File.Exists(lockScreenImageFilePath))
            {
                eventLog.WriteEntry($"Invalid lock screen file path {wallpaperImageFilePath}", EventLogEntryType.Error, eventId++);
            }
            else
            {
                eventLog.WriteEntry("Setup timer...", EventLogEntryType.Information, eventId++);
                SetupTimer();
                UpdateValues();
            }
        }

        protected override void OnStop()
        {
            eventLog.WriteEntry("Stopping...", EventLogEntryType.Information, eventId++);
        }

        private void SetupTimer()
        {
            timer = new Timer();
            timer.Interval = 600000;
            timer.Elapsed += new ElapsedEventHandler(OnTimer);
            timer.Start();
        }

        public void OnTimer(object sender, ElapsedEventArgs args)
        {
            UpdateValues();
        }

        private void UpdateValues()
        {
            try
            {
                var currentRegValue = Registry.GetValue(userRoot, wallPaperKey, string.Empty).ToString();

                if (currentRegValue != wallpaperImageFilePath)
                {
                    Registry.SetValue(userRoot, wallPaperKey, wallpaperImageFilePath);
                }

                currentRegValue = Registry.GetValue(machineRoot, lockScreenKey, string.Empty).ToString();

                if (currentRegValue != lockScreenImageFilePath)
                {
                    Registry.SetValue(machineRoot, lockScreenKey, lockScreenImageFilePath);
                }
            }
            catch (System.Exception e)
            {
                eventLog.WriteEntry($"Update of the registry key failed {e.Message}. Stack trace: {e.StackTrace}", EventLogEntryType.Error, eventId++);
            }
        }
    }
}
