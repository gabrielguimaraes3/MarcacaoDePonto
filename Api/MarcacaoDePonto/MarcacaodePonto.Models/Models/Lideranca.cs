using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarcacaoDePonto.Models.Models
{
    public class Lideranca
    {
        public int LiderancaId { get; set; }
        public int FuncionarioId { get; set; }
        public string DescricaoEquipe { get; set; }
    }
}
