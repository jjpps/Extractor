using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Imovel
    {
        public int id_imovel { get; set; }
        public string Codigo { get; set; }
        public string Categoria { get; set; }        
        public string Bairro { get; set; }
        public string Valor { get; set; }
        public string Desconto { get; set; }
        public int Vaga { get; set; }
        public int Dormitorio { get; set; }
        public string Tamanho { get; set; }

        
    }
}
