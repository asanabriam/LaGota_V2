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
    public partial class FrmRegistroCliente : Form
    {
        readonly Datos D = new Datos();

        public FrmRegistroCliente()
        {
            InitializeComponent();
            llenaDataGrid();
        }
        public void llenaDataGrid()
        {
            dgvClientes.DataSource = D.ObtenerClientes();
            dgvClientes.Columns[0].HeaderText = "EMAIL";
            dgvClientes.Columns[1].HeaderText = "CELULAR";
            dgvClientes.Columns[2].HeaderText = "CEDULA";
            dgvClientes.Columns[0].DisplayIndex = 5;
            dgvClientes.Columns[1].DisplayIndex = 5;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Dispose();
        }
        private bool validar(Form formulario)
        {
            bool vacio = false;

            foreach (Control oControls in formulario.Controls) // Buscamos en cada TextBox de nuestro Formulario.
            {
                if (oControls is TextBox & oControls.Text == String.Empty) // Verificamos que no este vacio.
                {
                    vacio = true; // Si esta vacio el TextBox asignamos el valor True a nuestra variable.
                }
            }
            if (vacio == true) MessageBox.Show("Favor de llenar todos los campos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); // Si nuestra variable es verdadera mostramos un mensaje.

            else
                vacio = false; // Devolvemos el valor original a nuestra variable.

            return vacio;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            Clientes cli = new Clientes();


            if (!validar(this))
            {
                cli.IDENTIFICACION = txtCedula.Text;
                cli.NOMBRE = txtNombre.Text;
                cli.APELLIDO1 = txtApellido1.Text;
                cli.APELLIDO2 = txtApellido2.Text;
                cli.CORREOELECTRONICO = txtEmail.Text;
                cli.NUMCELULAR = txtCelular.Text;

                if (D.RegistrarCliente(cli))
                {
                    MessageBox.Show("El cliente fue registrado exitosamente", "Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    llenaDataGrid();
                    txtCedula.Text = ""; ;
                    txtNombre.Text = ""; ;
                    txtApellido1.Text = ""; ;
                    txtApellido2.Text = ""; ;
                    txtEmail.Text = ""; ;
                    txtCelular.Text = ""; ;
                }

                else
                {
                    MessageBox.Show("El Cliente no se pudo registrar. \nRevise el Código, este no puede repetirse.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
