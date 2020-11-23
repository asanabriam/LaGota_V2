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
    public partial class FrmRegistroFuncionarios : Form
    {
        readonly Datos D = new Datos();
        public FrmRegistroFuncionarios()
        {
            InitializeComponent();
            llenaDataGrid();
        }
        public void llenaDataGrid()
        {
            dgvFuncionarios.DataSource = D.ObtenerFuncionarios();
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
            Funcionarios Fun = new Funcionarios();


            if (!validar(this))
            {
                Fun.IDENTIFICACION = txtCedula.Text;
                Fun.NOMBRE = txtNombre.Text;
                Fun.APELLIDO1 = txtApellido1.Text;
                Fun.APELLIDO2 = txtApellido2.Text;

                if (D.RegistrarFuncionario(Fun))
                {
                    MessageBox.Show("El Funcionario fue registrado exitosamente", "Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    llenaDataGrid();
                    txtCedula.Text = ""; ;
                    txtNombre.Text = ""; ;
                    txtApellido1.Text = ""; ;
                    txtApellido2.Text = ""; ;
                }

                else
                {
                    MessageBox.Show("El Funcionario no se pudo registrar. \nRevise el Código, este no puede repetirse.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
