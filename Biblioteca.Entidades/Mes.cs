using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Entidades
{
    public class Mes
    {
        
        public int Id { set; get; }
        public string Descripcion { set; get; }

        public Mes(int Id, string Descripcion)
        {
            this.Id = Id;
            this.Descripcion = Descripcion;
        }
        public override string ToString()
        {
            return Descripcion;
        }
    }
}
