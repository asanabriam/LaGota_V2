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
    public partial class FrmRegistroHidrometros : Form
    {
        readonly Datos D;
        List<Categorias> cat = new List<Categorias>();
        List<Clientes> cli = new List<Clientes>();
        public FrmRegistroHidrometros()
        {
            D = new Datos();
            cat = D.ObtenerCategorias();
            cli = D.ObtenerClientes();
            
            InitializeComponent();

            llenaCmbCategoria();
            llenaCmbClientes();
            llenaDataGrid();
        }


        public void llenaCmbCategoria()
        {
            foreach (Categorias dato in cat)
            {
                cmbCategoria.Items.Add(dato);
            }
        }

        public void llenaCmbClientes()
        {
            foreach (Clientes dato in cli)
            {
                cmbCliente.Items.Add(dato);
            }
        }


        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void soloNumeros(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) )
            {
                e.Handled = true;
                MessageBox.Show("Este campo solo admite números", "Solo números", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }    
        }

        private void txtNis_KeyPress(object sender, KeyPressEventArgs e)
        {
            soloNumeros(sender, e);
        }

        private void textSerie_KeyPress(object sender, KeyPressEventArgs e)
        {
            soloNumeros(sender, e);
        }

        public void llenaDataGrid()
        {
            dgvHidrometros.DataSource = D.ObtenerHidrometros();
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



            else if (cmbCategoria.Text == String.Empty)
            {
                MessageBox.Show("Debe elegir una categoría!", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                vacio = true;
            }

            else if (cmbCliente.Text == String.Empty)
            {
                MessageBox.Show("Debe elegir un Cliente!", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                vacio = true;
            }

            else
                vacio = false; // Devolvemos el valor original a nuestra variable.

            return vacio;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            Hidrometros hid = new Hidrometros();


            if (!validar(this))
            {
                hid.NIS = Convert.ToInt32(txtNis.Text);
                hid.MARCA = txtMarca.Text;
                hid.NUMSERIE = Convert.ToInt32(textSerie.Text);
                hid.CATEGORIA = (Categorias) cmbCategoria.SelectedItem;
                hid.CLIENTES = (Clientes)cmbCliente.SelectedItem;


                if (D.RegistrarHidrometro(hid))
                {
                    MessageBox.Show("El Hidrómetro fue registrado exitosamente", "Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtMarca.Text = "";
                    txtNis.Text = "";
                    textSerie.Text = "";
                    llenaDataGrid();
                }

                else
                {
                    MessageBox.Show("La categoría no se pudo registrar. \nRevise el Código, este no puede repetirse.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
    }
}
