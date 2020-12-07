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


            try
            {
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
            }

            catch (SqlException ex)
            {
                StringBuilder errorMessages = new StringBuilder();
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                        "Message: " + ex.Errors[i].Message + "\n" +
                        "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                        "Source: " + ex.Errors[i].Source + "\n" +
                        "Procedure: " + ex.Errors[i].Procedure + "\n");
                }
                Console.WriteLine(errorMessages.ToString());
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
            try
            {
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
            }
            catch (Exception)
            {
            }
            conexionSQL.Close();
            return resultado;
        }

        public List<Mes> ObtenerMeses()
        {
            List<Mes> resultado = new List<Mes>();

            Mes ene = new Mes(1, "Enero");
            Mes feb = new Mes(2, "Febrero");
            Mes mar = new Mes(3, "Marzo");
            Mes abr = new Mes(4, "Abril");
            Mes may = new Mes(5, "Mayo");
            Mes jun = new Mes(6, "Junio");
            Mes jul = new Mes(7, "Julio");
            Mes ago = new Mes(8, "Agosto");
            Mes set = new Mes(9, "Setiembre");
            Mes oct = new Mes(10, "Octubre");
            Mes nov = new Mes(11, "Noviembre");
            Mes dic = new Mes(12, "Diciembre");

            resultado.Add(ene);
            resultado.Add(feb);
            resultado.Add(mar);
            resultado.Add(abr);
            resultado.Add(may);
            resultado.Add(jun);
            resultado.Add(jul);
            resultado.Add(ago);
            resultado.Add(set);
            resultado.Add(oct);
            resultado.Add(nov);
            resultado.Add(dic);

            return resultado;
        }

        public string getMes(int mes)
        {
            string diaMes = "";
            List<Mes> resultado = ObtenerMeses();

            foreach (Mes dato in resultado)
            {
                if (dato.Id == mes)
                {
                    diaMes = dato.Descripcion;
                }
            }
            return diaMes;
        }

        public List<HistorialConsumo> ObtenerLecturas(int Nis)
        {
            List<HistorialConsumo> resultado = new List<HistorialConsumo>();
            string cadena = ConfigurationManager.ConnectionStrings["conexionGota"].ConnectionString;
            SqlConnection conexionSQL = new SqlConnection(cadena);
            SqlCommand comandoSQL = new SqlCommand();
            SqlDataReader lectorSQL;

            string sentenciaSQL = "select NIS, MES, FECHALECTURA, LECTURA from HISTORIALCONSUMO where NIS = @NIS;";

            comandoSQL.CommandType = CommandType.Text;
            comandoSQL.CommandText = sentenciaSQL;
            comandoSQL.Connection = conexionSQL;
            comandoSQL.Parameters.AddWithValue("@NIS", Nis);

            conexionSQL.Open();
            lectorSQL = comandoSQL.ExecuteReader();

            while (lectorSQL.Read())
            {
                HistorialConsumo item = new HistorialConsumo();

                item.NIS = lectorSQL.GetInt32(0);
                item.MES = lectorSQL.GetInt32(1);
                item.FECHALECTURA = lectorSQL.GetDateTime(2).ToString("dd-MM-yyyy");
                item.LECTURA = lectorSQL.GetInt32(3);
                resultado.Add(item);

            }
            conexionSQL.Close();
            return resultado;
        }

        public Boolean RegistrarCategorias(Categorias c)
        {
            Boolean resultado = false;

            string cadena = ConfigurationManager.ConnectionStrings["conexionGota"].ConnectionString;
            SqlConnection conexionSQL = new SqlConnection(cadena);
            SqlCommand comandoSQL = new SqlCommand();
            string sentencia;

            sentencia = "insert Into CATEGORIAS (CODCATEGORIA, DESCRIPCION) values (@CODCATEGORIA, @DESCRIPCION)";

            comandoSQL.CommandType = CommandType.Text;
            comandoSQL.CommandText = sentencia;
            comandoSQL.Connection = conexionSQL;
            comandoSQL.Parameters.AddWithValue("@CODCATEGORIA", c.CODCATEGORIA);
            comandoSQL.Parameters.AddWithValue("@DESCRIPCION", c.DESCRIPCION);

            try
            {
                conexionSQL.Open();
                comandoSQL.ExecuteNonQuery();
                resultado = true;
            }
            catch
            {
                conexionSQL.Close();
            }

            conexionSQL.Close();
            return resultado;
        }

        public Boolean RegistrarCliente(Clientes c)
        {
            Boolean resultado = false;

            string cadena = ConfigurationManager.ConnectionStrings["conexionGota"].ConnectionString;
            SqlConnection conexionSQL = new SqlConnection(cadena);
            SqlCommand comandoSQL = new SqlCommand();
            string sentencia;

            sentencia = "insert Into CLIENTES (IDENTIFICACION, NOMBRE, APELLIDO1, APELLIDO2, CORREOELECTRONICO, NUMCELULAR) values (@IDENTIFICACION, @NOMBRE, @APELLIDO1, @APELLIDO2, @CORREOELECTRONICO, @NUMCELULAR)";

            comandoSQL.CommandType = CommandType.Text;
            comandoSQL.CommandText = sentencia;
            comandoSQL.Connection = conexionSQL;
            comandoSQL.Parameters.AddWithValue("@IDENTIFICACION", c.IDENTIFICACION);
            comandoSQL.Parameters.AddWithValue("@NOMBRE", c.NOMBRE);
            comandoSQL.Parameters.AddWithValue("@APELLIDO1", c.APELLIDO1);
            comandoSQL.Parameters.AddWithValue("@APELLIDO2", c.APELLIDO2);
            comandoSQL.Parameters.AddWithValue("@CORREOELECTRONICO", c.CORREOELECTRONICO);
            comandoSQL.Parameters.AddWithValue("@NUMCELULAR", c.NUMCELULAR);

            try
            {
                conexionSQL.Open();
                comandoSQL.ExecuteNonQuery();
                resultado = true;
            }
            catch
            {
                conexionSQL.Close();
            }

            conexionSQL.Close();
            return resultado;
        }

        public Boolean RegistrarFuncionario(Funcionarios f)
        {
            Boolean resultado = false;

            string cadena = ConfigurationManager.ConnectionStrings["conexionGota"].ConnectionString;
            SqlConnection conexionSQL = new SqlConnection(cadena);
            SqlCommand comandoSQL = new SqlCommand();
            string sentencia;

            sentencia = "insert Into FUNCIONARIOS (IDENTIFICACION, NOMBRE, APELLIDO1, APELLIDO2) values (@IDENTIFICACION, @NOMBRE, @APELLIDO1, @APELLIDO2)";

            comandoSQL.CommandType = CommandType.Text;
            comandoSQL.CommandText = sentencia;
            comandoSQL.Connection = conexionSQL;
            comandoSQL.Parameters.AddWithValue("@IDENTIFICACION", f.IDENTIFICACION);
            comandoSQL.Parameters.AddWithValue("@NOMBRE", f.NOMBRE);
            comandoSQL.Parameters.AddWithValue("@APELLIDO1", f.APELLIDO1);
            comandoSQL.Parameters.AddWithValue("@APELLIDO2", f.APELLIDO2);

            try
            {
                conexionSQL.Open();
                comandoSQL.ExecuteNonQuery();
                resultado = true;
            }
            catch
            {
                conexionSQL.Close();
            }

            conexionSQL.Close();
            return resultado;
        }

        public Boolean RegistrarHidrometro(Hidrometros h)
        {
            Boolean resultado = false;

            string cadena = ConfigurationManager.ConnectionStrings["conexionGota"].ConnectionString;
            SqlConnection conexionSQL = new SqlConnection(cadena);
            SqlCommand comandoSQL = new SqlCommand();
            string sentencia;

            sentencia = "insert Into HIDROMETROS(NIS, MARCA, NUMSERIE, CODCATEGORIA, IDENTIFICACION) values(@NIS, @MARCA, @NUMSERIE, @CODCATEGORIA, @IDENTIFICACION)";

            comandoSQL.CommandType = CommandType.Text;
            comandoSQL.CommandText = sentencia;
            comandoSQL.Connection = conexionSQL;
            comandoSQL.Parameters.AddWithValue("@NIS", h.NIS);
            comandoSQL.Parameters.AddWithValue("@MARCA", h.MARCA);
            comandoSQL.Parameters.AddWithValue("@NUMSERIE", h.NUMSERIE);
            comandoSQL.Parameters.AddWithValue("@CODCATEGORIA", h.CATEGORIA.CODCATEGORIA);
            comandoSQL.Parameters.AddWithValue("@IDENTIFICACION", h.CLIENTES.IDENTIFICACION);

            try
            {
                conexionSQL.Open();
                comandoSQL.ExecuteNonQuery();
                resultado = true;
            }
            catch
            {
                conexionSQL.Close();
            }

            conexionSQL.Close();
            return resultado;
        }

        public Boolean RegistrarConsumo(HistorialConsumo h)
        {
            Boolean resultado = false;

            string cadena = ConfigurationManager.ConnectionStrings["conexionGota"].ConnectionString;
            SqlConnection conexionSQL = new SqlConnection(cadena);
            SqlCommand comandoSQL = new SqlCommand();
            string sentencia;

            sentencia = "insert Into HISTORIALCONSUMO(NIS, MES, FECHALECTURA, LECTURA) values(@NIS, @MES, @FECHALECTURA, @LECTURA)";

            comandoSQL.CommandType = CommandType.Text;
            comandoSQL.CommandText = sentencia;
            comandoSQL.Connection = conexionSQL;
            comandoSQL.Parameters.AddWithValue("@NIS", h.NIS);
            comandoSQL.Parameters.AddWithValue("@MES", h.MES);
            comandoSQL.Parameters.AddWithValue("@FECHALECTURA", h.FECHALECTURA);
            comandoSQL.Parameters.AddWithValue("@LECTURA", h.LECTURA);

            try
            {
                conexionSQL.Open();
                comandoSQL.ExecuteNonQuery();
                resultado = true;
            }
            catch
            {
                conexionSQL.Close();
            }

            conexionSQL.Close();
            return resultado;
        }
    }
}

