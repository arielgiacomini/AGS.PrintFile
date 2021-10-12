using System;

namespace AGS.PrintFile.Worker.Entities
{
    public class ControlePDF
    {
        public int Id { get; set; }
        public string Pasta { get; set; }
        public string Arquivo { get; set; }
        public bool? Impresso { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataImpressao { get; set; }
    }
}