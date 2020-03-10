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
        private string imageFilePath;
        private const string userRoot = "HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System";
        private const string wallPaperKey = "Wallpaper";

        public WallPaperUpdater(string filePath)
        {
            InitializeComponent();
            setupEventLog();
            eventLog.WriteEntry($"Received image file path: {filePath}", EventLogEntryType.Information, eventId++);
            imageFilePath = filePath;
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
            eventLog.WriteEntry("Starting...");

            if (string.IsNullOrEmpty(imageFilePath) || !File.Exists(imageFilePath))
            {
                eventLog.WriteEntry($"Invalid file path {imageFilePath}", EventLogEntryType.Error, eventId++);
            } 
            else
            {
                eventLog.WriteEntry("Setup timer", EventLogEntryType.Information, eventId++);
                SetupTimer();
                UpdateValue();
            }
        }

        protected override void OnStop()
        {
            eventLog.WriteEntry("Stopping...");
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
            UpdateValue();
        }

        private void UpdateValue()
        {
            try
            {
                var currentRegValue = Registry.GetValue(userRoot, wallPaperKey, string.Empty).ToString();

                if (currentRegValue != imageFilePath)
                {
                    Registry.SetValue(userRoot, wallPaperKey, imageFilePath);
                }
            }
            catch (System.Exception e)
            {
                eventLog.WriteEntry($"Update of the registry key failed {e.Message}. Stack trace: {e.StackTrace}", EventLogEntryType.Error, eventId++);
            }
        }
    }
}
