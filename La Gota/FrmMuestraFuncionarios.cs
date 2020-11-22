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
    public partial class FrmMuestraFuncionarios : Form
    {
        readonly Datos D;
        readonly List<Funcionarios> resultado;

        public FrmMuestraFuncionarios()
        {
            D = new Datos();
            resultado = D.ObtenerFuncionarios();
            InitializeComponent();
            llenaComboBoxFuncionarios();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void llenaComboBoxFuncionarios()
        {
            foreach (Funcionarios dato in resultado)
            {
                CombFuncionarios.Items.Add(dato);
            }
        }

        private void CombClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            Funcionarios c = (Funcionarios)CombFuncionarios.SelectedItem;
            txtCedula.Text = c.IDENTIFICACION;
            txtNombre.Text = c.NOMBRE;
            txtApellido1.Text = c.APELLIDO1;
            txtApellido2.Text = c.APELLIDO2;
        }
    }
}
