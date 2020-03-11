using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;

namespace WallPaperUpdater
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        protected override void OnBeforeInstall(IDictionary savedState)
        {
            Context.Parameters["assemblypath"] += " " + "\"" + Context.Parameters["WallpaperImageFilePath"] + "\" " + "\"" + Context.Parameters["LockScreenImageFilePath"] + "\" ";
            base.OnBeforeInstall(savedState);
        }

        public override void Install(IDictionary stateSaver)
        {
            CustomParameters customParameters = new CustomParameters(Context);

            SaveCustomParametersInStateSaverDictionary(stateSaver, customParameters);

            base.Install(stateSaver);
        }

        private void SaveCustomParametersInStateSaverDictionary(IDictionary stateSaver, CustomParameters customParameters)
        {
            if (stateSaver.Contains(CustomParameters.WallpaperParameterKey) == true)
            {
                stateSaver[CustomParameters.WallpaperParameterKey] = customParameters.WallpaperParameterValue;
            }              
            else
            {
                stateSaver.Add(CustomParameters.WallpaperParameterKey, customParameters.WallpaperParameterValue);
            }
                

            if (stateSaver.Contains(CustomParameters.LockScreenParameterKey) == true)
            {
                stateSaver[CustomParameters.LockScreenParameterKey] = customParameters.LockScreenParameterValue;
            }            
            else
            {
                stateSaver.Add(CustomParameters.LockScreenParameterKey, customParameters.LockScreenParameterValue);
            }
        }

        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);
        }
    }
}
