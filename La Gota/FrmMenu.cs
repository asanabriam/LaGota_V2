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


            InitializeComponent();

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
            panel5.Controls.Clear();
            AbrirFormularios<FrmMuestraHidrometros>();
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

        private void btnMosCliente_Click(object sender, EventArgs e)
        {
            panel5.Controls.Clear();
            AbrirFormularios<FrmMuestraClientes>();

        }

        private void AbrirFormularios<FormularioAbrir>() where FormularioAbrir:Form, new()
        {
            Form Formularios;
            Formularios = panel5.Controls.OfType<FormularioAbrir>().FirstOrDefault();

            if (Formularios == null)
            {
                Formularios = new FormularioAbrir
                {
                    TopLevel = false,
                    Dock = DockStyle.Fill        
                };

            panel5.Controls.Add(Formularios);
            panel5.Tag = Formularios;
            Formularios.Show();
            Formularios.BringToFront();

            }
        }

        private void btnMosCategoria_Click(object sender, EventArgs e)
        {
            panel5.Controls.Clear();
            AbrirFormularios<FrmMuestraCategorias>();
        }

        private void btnMosFuncionario_Click(object sender, EventArgs e)
        {
            panel5.Controls.Clear();
            AbrirFormularios<FrmMuestraFuncionarios>();
        }
    }
}
