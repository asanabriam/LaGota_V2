using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Entidades
{
    public class Hidrometros
    {
        public int NIS { set; get; }
        public string MARCA { set; get; }
        public int NUMSERIE { set; get; }
        public Categorias CATEGORIA { set; get; }
        public Clientes CLIENTES { set; get; }

        public override string ToString()
        {
            return NIS.ToString();
        }
    }
}
