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
                args.Length == 2 ? new WallPaperUpdater(args[0], args[1]) : new WallPaperUpdater(string.Empty, string.Empty)
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
