using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.Entidades;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
    public class Datos
    {
        public List<Clientes> ObtenerClientes()
        {
            List<Clientes> resultado = new List<Clientes>();
            string cadena = ConfigurationManager.ConnectionStrings["conexionGota"].ConnectionString;
            SqlConnection conexionSQL = new SqlConnection(cadena);
            SqlCommand comandoSQL = new SqlCommand();
            SqlDataReader lectorSQL;

            string sentenciaSQL = "select IDENTIFICACION, NOMBRE, APELLIDO1, APELLIDO2, CORREOELECTRONICO,NUMCELULAR from CLIENTES;";

            comandoSQL.CommandType = CommandType.Text;
            comandoSQL.CommandText = sentenciaSQL;
            comandoSQL.Connection = conexionSQL;

            conexionSQL.Open();
            lectorSQL = comandoSQL.ExecuteReader();

            while (lectorSQL.Read())
            {
                Clientes item = new Clientes();
                item.IDENTIFICACION = lectorSQL.GetString(0);
                item.NOMBRE = lectorSQL.GetString(1);
                item.APELLIDO1 = lectorSQL.GetString(2);
                item.APELLIDO2 = lectorSQL.GetString(3);
                item.CORREOELECTRONICO = lectorSQL.GetString(4);
                item.NUMCELULAR = lectorSQL.GetString(5);
                resultado.Add(item);

            }
            conexionSQL.Close();
            return resultado;
        }




    }
}
