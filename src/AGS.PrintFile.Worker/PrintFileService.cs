using AGS.PrintFile.Worker.Application;
using System.ServiceProcess;

namespace AGS.PrintFile.Worker
{
    public partial class PrintFileService : ServiceBase
    {
        public PrintFileService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {

        }

        protected override void OnStop()
        {

        }

        public void Initialize()
        {
            ImpressaoAutomaticaApplication service = new ImpressaoAutomaticaApplication();

            service.Execute();
        }
    }
}