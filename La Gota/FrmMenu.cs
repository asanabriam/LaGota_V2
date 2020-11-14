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
    public partial class FrmMenu : Form
    {
        public FrmMenu()
        {

            Datos D = new Datos();

            List<Clientes> resultado = D.ObtenerClientes();

            InitializeComponent();
            DataGrid1.DataSource = resultado;
            DataGrid1.Columns[4].Width = 200;
            DataGrid1.Columns[0].HeaderText = "CEDULA";
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton10_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel1_Click(object sender, EventArgs e)
        {

        }

        private void DataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
