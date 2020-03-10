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
            Context.Parameters["assemblypath"] += " " + "\"" + Context.Parameters["ImageFilePath"] + "\"";
            base.OnBeforeInstall(savedState);
        }
    }
}
