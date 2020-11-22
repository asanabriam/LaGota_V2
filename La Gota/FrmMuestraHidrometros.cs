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
    public partial class FrmMuestraHidrometros : Form
    {
        readonly Datos D;
        readonly Clientes Cli;
        readonly Categorias Cat;
        readonly List<Hidrometros> resultado;

        public FrmMuestraHidrometros()
        {
            D = new Datos();

            resultado = D.ObtenerHidrometros();

            InitializeComponent();

            llenaComboBoxHidrometros();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void llenaComboBoxHidrometros()
        {
            foreach (Hidrometros dato in resultado)
            {
                CombHidrometros.Items.Add(dato);
            }
        }

        private void CombHidrometros_SelectedIndexChanged(object sender, EventArgs e)
        {
            Hidrometros h = (Hidrometros)CombHidrometros.SelectedItem;

            txtNis.Text = h.NIS.ToString();
            txtMarca.Text = h.MARCA;
            txtCategoria.Text = h.CATEGORIA.DESCRIPCION;
            txtCliente.Text = h.CLIENTES.ToString();

        }
    }
}
