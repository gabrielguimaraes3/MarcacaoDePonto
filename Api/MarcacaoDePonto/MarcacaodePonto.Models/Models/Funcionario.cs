using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarcacaoDePonto.Models.Models
{
    public class Funcionario
    {
        public int FuncionarioId { get; set; }
        public string NomeDoFuncionario { get; set; }
        public string Cpf { get; set; }
        public DateTime NascimentoFuncionario { get; set; }
        public DateTime DataDeAdmissao { get; set; }
        public string CelularFuncionario { get; set; }
        public string EmailFuncionario { get; set; }
        public int CargoId { get; set; }
    }
}
