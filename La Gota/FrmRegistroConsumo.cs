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
    }
}
