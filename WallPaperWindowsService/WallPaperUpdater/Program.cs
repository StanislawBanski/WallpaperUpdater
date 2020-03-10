using System.ServiceProcess;

namespace WallPaperUpdater
{
    static class Program
    {
        static void Main(string[] args)
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                args.Length > 0 ? new WallPaperUpdater(args[0]) : new WallPaperUpdater(string.Empty)
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
