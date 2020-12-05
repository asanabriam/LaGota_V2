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
    public partial class FrmRegistroConsumo : Form
    {
        Datos D;
        List<Mes> Meses;
        List<Hidrometros> Hidrometros;

        public FrmRegistroConsumo()
        {
            D = new Datos();
            Meses = D.ObtenerMeses();
            Hidrometros = D.ObtenerHidrometros();
            InitializeComponent();
            llenarComboBoxMeses();
            llenarComboBoxNis();


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void llenarComboBoxNis()
        {
            foreach (Hidrometros dato in Hidrometros )
            {
                cmbNis.Items.Add(dato.NIS);
            }
        }
        private void llenarComboBoxMeses()
        {
            foreach (Mes dato in Meses)
            {
                cmbMes.Items.Add(dato);
            }
        }





        private void llenaDgvHidrometros(int Nis)
        {
            dgvHidrometros.Rows.Clear();
            int i = 0;
            foreach (HistorialConsumo dato in D.ObtenerLecturas(Nis))
            {
                dgvHidrometros.Rows.Add();
                dgvHidrometros.Rows[i].Cells[0].Value = dato.NIS;
                dgvHidrometros.Rows[i].Cells[1].Value = D.getMes(dato.MES);
                dgvHidrometros.Rows[i].Cells[2].Value = dato.FECHALECTURA;
                dgvHidrometros.Rows[i].Cells[3].Value = dato.LECTURA;
                i++;
            }
        }

        private void dgvHidrometros_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbNis_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenaDgvHidrometros(int.Parse(cmbNis.Text));
        }

        private void txtLectura_KeyPress(object sender, KeyPressEventArgs e)
        {
            soloNumeros(sender, e);
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



            else if (cmbNis.Text == String.Empty)
            {
                MessageBox.Show("Debe elegir un NIS!", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                vacio = true;
            }

            else if (cmbMes.Text == String.Empty)
            {
                MessageBox.Show("Debe elegir un Mes!", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                vacio = true;
            }

            else
                vacio = false; // Devolvemos el valor original a nuestra variable.

            return vacio;
        }

        private void soloNumeros(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Este campo solo admite números", "Solo números", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (validar(this) == false) // Se valida que nos campos no esten vacios
            {
                
                if (lecturaAnterior(int.Parse(cmbNis.Text))<int.Parse(txtLectura.Text))
                {
                    Mes m = (Mes)cmbMes.SelectedItem;
                    if (mesRepetido(int.Parse(cmbNis.Text), m.Id) == false)
                    {
                        HistorialConsumo hist = new HistorialConsumo();

                        hist.NIS = int.Parse(cmbNis.Text);
                        hist.MES = m.Id;
                        hist.FECHALECTURA = dateTimePicker1.Value.ToString("dd-MM-yyyy");
                        hist.LECTURA = int.Parse(txtLectura.Text);


                        if (D.RegistrarConsumo (hist))
                        {
                            MessageBox.Show("La categoria fue registrada exitosamente", "Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            llenaDgvHidrometros(int.Parse(cmbNis.Text));
                            txtLectura.Text = "";
                        }

                        else
                        {
                            MessageBox.Show("La categoría no se pudo registrar. \nRevise el Código, este no puede repetirse.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("El mes de consumo ya ha sido registrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                }
                else
                {
                    MessageBox.Show("La nueva lectura es menor que la ultima lectura", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
        }

        public int lecturaAnterior(int NIS)// Devuele la ultima Lectura del numero NIS
        {
            int lecAnterior = 0;

            foreach (HistorialConsumo dato in D.ObtenerLecturas(NIS))
            {
               if (dato.LECTURA > lecAnterior)
                {
                    lecAnterior = dato.LECTURA;
                }
            }
            return lecAnterior;
        }

        public bool mesRepetido(int NIS, int MES)// Devuele la ultima Lectura del numero NIS
        {
            bool repetido = false;

            foreach (HistorialConsumo dato in D.ObtenerLecturas(NIS))
            {
                if (dato.MES == MES)
                {
                    repetido = true;
                }
            }
            return repetido;
        }
    }
}
