using System;
using System.Windows.Forms;

namespace WinFormsManual
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            // Inicializar favoritos antes de iniciar cualquier formulario
            try
            {
                FavoritosManager.Inicializar();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inicializando favoritos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Application.Run(new FormManual());
        }
    }
}
