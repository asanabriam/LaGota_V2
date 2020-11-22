using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.Entidades;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Biblioteca.Datos
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

        public List<Funcionarios> ObtenerFuncionarios()
        {
            List<Funcionarios> resultado = new List<Funcionarios>();
            string cadena = ConfigurationManager.ConnectionStrings["conexionGota"].ConnectionString;
            SqlConnection conexionSQL = new SqlConnection(cadena);
            SqlCommand comandoSQL = new SqlCommand();
            SqlDataReader lectorSQL;

            string sentenciaSQL = "select IDENTIFICACION, NOMBRE, APELLIDO1, APELLIDO2 from FUNCIONARIOS;";

            comandoSQL.CommandType = CommandType.Text;
            comandoSQL.CommandText = sentenciaSQL;
            comandoSQL.Connection = conexionSQL;

            conexionSQL.Open();
            lectorSQL = comandoSQL.ExecuteReader();

            while (lectorSQL.Read())
            {
                Funcionarios item = new Funcionarios();
                item.IDENTIFICACION = lectorSQL.GetString(0);
                item.NOMBRE = lectorSQL.GetString(1);
                item.APELLIDO1 = lectorSQL.GetString(2);
                item.APELLIDO2 = lectorSQL.GetString(3);
                resultado.Add(item);

            }
            conexionSQL.Close();
            return resultado;
        }

        public List<Categorias> ObtenerCategorias()
        {
            List<Categorias> resultado = new List<Categorias>();
            string cadena = ConfigurationManager.ConnectionStrings["conexionGota"].ConnectionString;
            SqlConnection conexionSQL = new SqlConnection(cadena);
            SqlCommand comandoSQL = new SqlCommand();
            SqlDataReader lectorSQL;

            string sentenciaSQL = "select CODCATEGORIA, DESCRIPCION from CATEGORIAS;";

            comandoSQL.CommandType = CommandType.Text;
            comandoSQL.CommandText = sentenciaSQL;
            comandoSQL.Connection = conexionSQL;

            conexionSQL.Open();
            lectorSQL = comandoSQL.ExecuteReader();

            while (lectorSQL.Read())
            {
                Categorias item = new Categorias();
                item.CODCATEGORIA = lectorSQL.GetString(0);
                item.DESCRIPCION = lectorSQL.GetString(1);
                resultado.Add(item);

            }
            conexionSQL.Close();
            return resultado;
        }

        public List<Hidrometros> ObtenerHidrometros()
        {
            List<Hidrometros> resultado = new List<Hidrometros>();
            string cadena = ConfigurationManager.ConnectionStrings["conexionGota"].ConnectionString;
            SqlConnection conexionSQL = new SqlConnection(cadena);
            SqlCommand comandoSQL = new SqlCommand();
            SqlDataReader lectorSQL;

            string sentenciaSQL = "select hid.NIS, hid.MARCA, hid.NUMSERIE, hid.CODCATEGORIA, hid.IDENTIFICACION, cat.CODCATEGORIA " +
                                    ",cat.DESCRIPCION, cli.IDENTIFICACION ,cli.NOMBRE, cli.APELLIDO1, cli.APELLIDO2 " +
                                    "FROM HIDROMETROS hid INNER JOIN CLIENTES cli on hid.IDENTIFICACION = cli.IDENTIFICACION " +
                                    "INNER JOIN CATEGORIAS cat on hid.CODCATEGORIA = cat.CODCATEGORIA";

            comandoSQL.CommandType = CommandType.Text;
            comandoSQL.CommandText = sentenciaSQL;
            comandoSQL.Connection = conexionSQL;

            conexionSQL.Open();
            lectorSQL = comandoSQL.ExecuteReader();

            while (lectorSQL.Read())
            {
                Hidrometros item = new Hidrometros();
                Clientes cli = new Clientes();
                Categorias cat = new Categorias();

                item.NIS = lectorSQL.GetInt32(0);
                item.MARCA = lectorSQL.GetString(1);
                item.NUMSERIE = lectorSQL.GetInt32(2);
                cat.CODCATEGORIA = lectorSQL.GetString(5);
                cat.DESCRIPCION = lectorSQL.GetString(6);
                cli.IDENTIFICACION = lectorSQL.GetString(7);
                cli.NOMBRE = lectorSQL.GetString(8);
                cli.APELLIDO1 = lectorSQL.GetString(9);
                cli.APELLIDO2 = lectorSQL.GetString(10);
                item.CLIENTES = cli;
                item.CATEGORIA = cat;
                resultado.Add(item);

            }
            conexionSQL.Close();
            return resultado;
        }


    }
}
