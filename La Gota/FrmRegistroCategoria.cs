using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Biblioteca.Entidades;
using Biblioteca.Datos;

namespace La_Gota
{
    public partial class FrmRegistroCategoria : Form
    {
        Datos D = new Datos();
        public FrmRegistroCategoria()
        {
            InitializeComponent();
            dgvCategorias.DataSource = D.ObtenerCategorias();
            dgvCategorias.Columns[0].HeaderText = "CODIGO";
            dgvCategorias.Columns[1].HeaderText = "DESCRIPCION";
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
            if (vacio == true) MessageBox.Show("Favor de llenar todos los campos.","Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); // Si nuestra variable es verdadera mostramos un mensaje.
            
            else
            vacio = false; // Devolvemos el valor original a nuestra variable.

            return vacio;
        }


        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            Categorias cat = new Categorias();


            if(!validar(this))
            {
                cat.CODCATEGORIA = txtCodigo.Text;
                cat.DESCRIPCION = txtDescripcion.Text;

                if (D.RegistrarCategorias(cat))
                {
                    MessageBox.Show("La categoria fue registrada exitosamente", "Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvCategorias.DataSource = D.ObtenerCategorias();
                    txtCodigo.Text="";
                    txtDescripcion.Text="";
                }

                else
                {
                    MessageBox.Show("La categoría no se pudo registrar. \nRevise el Código, este no puede repetirse.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
