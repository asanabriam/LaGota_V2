using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Biblioteca.ClienteServidor;
using Biblioteca.Entidades;

namespace La_Gota_Cliente
{
    public partial class Cliente : Form
    {
        public static ConexionTcp conexionTcp = new ConexionTcp();
        public static string IPADDRESS = "127.0.0.1";
        public const int PORT = 8900;

        public Cliente()
        {
            InitializeComponent();
            llenarComboBoxMeses();
        }


        private void MensajeRecibido(string datos)
        {
            var paquete = new Paquete(datos);
            string comando = paquete.Comando;
            if (comando == "resultado")
            {
                string contenido = paquete.Contenido;
                Invoke(new Action(() => lblResultado.Text = string.Format("¢ {0}", contenido)));
            }
        }

        private void Cliente_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }


        private void llenarComboBoxMeses()
        {
            List<Mes> Meses = new List<Mes>();
            Meses = ObtenerMeses();
            foreach (Mes dato in Meses)
            {
                cmbMes.Items.Add(dato);
            }
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

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            conexionTcp.OnDataRecieved += MensajeRecibido;

            if (!conexionTcp.Connectar(IPADDRESS, PORT))
            {
                MessageBox.Show("Error conectando con el servidor!");
                return;
            }
            else
            {
                lblEstado.Text = "Conectado";
                btnConsultar.Enabled = true;
            }
        }

        private void btnDesconectar_Click(object sender, EventArgs e)
        {
            conexionTcp.desconectar();
            lblEstado.Text = "Desconectado";
            btnConsultar.Enabled = false;
        }

        private void soloNumeros(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Este campo solo admite números", "Solo números", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtNis_KeyPress(object sender, KeyPressEventArgs e)
        {
            soloNumeros(sender, e);
        }

        private void btnConsultar_Click_1(object sender, EventArgs e)
        {
            if (txtNis.Text.Equals(""))
            {
                MessageBox.Show("Es necesario digitar un numero NIS", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (cmbMes.Text.Equals(""))
            {
                MessageBox.Show("Debe primero seleccionar un Mes", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    if (btnConsultar.Enabled == true)
                    {
                        Mes m = (Mes)cmbMes.SelectedItem;
                        var msgPack = new Paquete("consulta", string.Format("{0},{1}", txtNis.Text, m.Id.ToString()));
                        conexionTcp.EnviarPaquete(msgPack);
                    }
                    else MessageBox.Show("Es necesario estar conectado al servidor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
                catch(Exception)
                {
                     MessageBox.Show("No hay conexion con el servidor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }

            }
        }
    }
}
