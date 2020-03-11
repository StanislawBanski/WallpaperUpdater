using System.Collections;
using System.Configuration.Install;

namespace WallPaperUpdater
{
    public class CustomParameters
    {
        public const string WallpaperParameterKey = "WallpaperImageFilePath";
        public const string LockScreenParameterKey = "LockScreenImageFilePath";

        public string WallpaperParameterValue { get; } = null;
        public string LockScreenParameterValue { get; } = null;

        public CustomParameters(InstallContext installContext)
        {
            WallpaperParameterValue = installContext.Parameters[WallpaperParameterKey];
            LockScreenParameterValue = installContext.Parameters[LockScreenParameterKey];
        }

        public CustomParameters(IDictionary savedState)
        {
            if (savedState != null)
            {
                if (savedState.Contains(WallpaperParameterKey) == true)
                {
                    WallpaperParameterValue = (string)savedState[WallpaperParameterKey];
                }

                if (savedState.Contains(LockScreenParameterKey) == true)
                {
                    LockScreenParameterValue = (string)savedState[LockScreenParameterKey];
                }
            } 
        }
    }
}
