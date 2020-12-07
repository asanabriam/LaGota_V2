using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Entidades
{
    public class Categorias
    {
        public string CODCATEGORIA { set; get; }
        public string DESCRIPCION { set; get; }

  
        public override string ToString()
        {
            return DESCRIPCION;
        }

        public int MONTO {

            get
            {
                if (CODCATEGORIA == "1")
                    return  1000;

                if (CODCATEGORIA == "2")
                    return 2000;

                if (CODCATEGORIA == "3")
                    return 3000;

                if (CODCATEGORIA == "4")
                    return 4000;

                else
                    return 0;

            }
        
        }
    }
}
