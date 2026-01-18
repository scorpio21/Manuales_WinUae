using System;
using System.Windows.Forms;

namespace WinFormsManual
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void botonAbrirManual_Click(object sender, EventArgs e)
        {
            using var formulario = new FormManual();
            formulario.ShowDialog(this);
        }

        private void botonCheatCodes_Click(object sender, EventArgs e)
        {
            using var formulario = new FormCheatCodes();
            formulario.ShowDialog(this);
        }
    }
}
