using System;
using System.Collections.Generic;
using System.Text;

namespace Biblioteca.Entidades
{
    public class Persona
    {
        public string IDENTIFICACION { set; get; }
        public string NOMBRE { set; get; }
        public string APELLIDO1 { set; get; }
        public string APELLIDO2 { set; get; }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", NOMBRE, APELLIDO1, APELLIDO2);
        }
    }
}
