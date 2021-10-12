namespace AGS.PrintFile.Worker
{
    public static class Program
    {
        public static void Main()
        {
#if (!DEBUG)
            PrintFileService[] ServicesToRun;

            ServicesToRun = new PrintFileService[]
            {
                new PrintFileService()
            };

            PrintFileService.Run(ServicesToRun);
#else
            PrintFileService service = new PrintFileService();

            service.Initialize();

            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
#endif
        }
    }
}