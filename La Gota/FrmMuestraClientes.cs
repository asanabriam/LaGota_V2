using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Biblioteca.Datos;
using Biblioteca.Entidades;

namespace La_Gota
{
    public partial class FrmMuestraClientes : Form
    {
        readonly Datos D;
        readonly List<Clientes> resultado;


        public FrmMuestraClientes()
        {
            
            D = new Datos();
            resultado = D.ObtenerClientes();

            InitializeComponent();
            llenaComboBoxClientes();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }


        private void llenaComboBoxClientes()
        {
            foreach (Clientes dato in resultado)
            {
                CombClientes.Items.Add(dato);
            }
        }

        private void CombClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            Clientes c =  (Clientes) CombClientes.SelectedItem;
            txtCedula.Text = c.IDENTIFICACION;
            txtNombre.Text = c.NOMBRE;
            txtApellido1.Text = c.APELLIDO1;
            txtApellido2.Text = c.APELLIDO2;
            txtCelular.Text = c.NUMCELULAR;
            txtEmail.Text = c.CORREOELECTRONICO;

        }
    }
}
