﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.ClienteServidor
{
    // Clase que serializa y desserealiza los paquetes que se van a transmitir
    public class Formato
    {
        public static string Serializar(List<string> lista)
        {
            if (lista.Count == 0)
            {
                return null;
            }

            bool esElPrimero = true;
            var salida = new StringBuilder();

            foreach (var linea in lista)
            {
                if (esElPrimero)
                {
                    salida.Append(linea);
                    esElPrimero = false;
                }
                else
                {
                    salida.Append(string.Format(",{0}", linea));
                }
            }
            return salida.ToString();
        }

        public static List<string> Deserializar(string entrada)
        {
            string str = entrada;
            var lista = new List<string>();

            if (string.IsNullOrEmpty(str))
            {
                return lista;
            }

            try
            {
                foreach (string linea in entrada.Split(','))
                {
                    lista.Add(linea);
                }
            }
            catch (Exception)
            {
                return null;
            }

            return lista;
        }

    }
}
