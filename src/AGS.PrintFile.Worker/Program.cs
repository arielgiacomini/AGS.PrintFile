namespace AGS.PrintFile.Worker
{
    public static class Program
    {
        public static void Main()
        {
#if (!DEBUG)
            ServiceBase[] ServicesToRun;

            ServicesToRun = new ServiceBase[]
            {
                new PrintFileService()
            };

            ServiceBase.Run(ServicesToRun);
#else
            PrintFileService service = new PrintFileService();

            service.Initialize();

            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
#endif
        }
    }
}