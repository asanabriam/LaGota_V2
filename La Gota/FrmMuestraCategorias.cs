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
    public partial class FrmMuestraCategorias : Form
    {
        readonly Datos D;
        readonly List<Categorias> resultado;

        public FrmMuestraCategorias()
        {
            D = new Datos();
            resultado = D.ObtenerCategorias();

            InitializeComponent();

            llenaComboBoxCategorias();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void bunifuSeparator1_Load(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void llenaComboBoxCategorias()
        {
            foreach (Categorias dato in resultado)
            {
                CombCategorias.Items.Add(dato);
            }
        }

        private void CombCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            Categorias c = (Categorias)CombCategorias.SelectedItem;
            txtCodigo.Text = c.CODCATEGORIA;
            txtDescripcion.Text = c.DESCRIPCION;

        }
    }
}
