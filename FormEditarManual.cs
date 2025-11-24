using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WinFormsManual
{
    public partial class FormEditarManual : Form
    {
        private readonly List<PasoManual> _pasos = new();

        public Manual ManualCreado { get; private set; } = new Manual();

        public FormEditarManual()
        {
            InitializeComponent();
            EstablecerIconoFormulario();
            ActualizarListaPasos();
        }

        public FormEditarManual(Manual manualExistente) : this()
        {
            if (manualExistente == null)
            {
                return;
            }

            Text = "Editar manual";

            cuadroTituloManual.Text = manualExistente.Titulo;

            _pasos.Clear();
            foreach (var paso in manualExistente.Pasos)
            {
                _pasos.Add(new PasoManual
                {
                    TituloPaso = paso.TituloPaso,
                    Descripcion = paso.Descripcion,
                    NombreArchivoImagen = paso.NombreArchivoImagen
                });
            }

            ActualizarListaPasos();
        }

        private void listaPasos_SelectedIndexChanged(object sender, EventArgs e)
        {
            var pasoSeleccionado = listaPasos.SelectedItem as PasoManual;
            if (pasoSeleccionado == null)
            {
                return;
            }

            cuadroTituloPaso.Text = pasoSeleccionado.TituloPaso;
            cuadroDescripcionPaso.Text = pasoSeleccionado.Descripcion;
            cuadroRutaImagen.Text = pasoSeleccionado.NombreArchivoImagen;
        }

        private void botonBuscarImagen_Click(object sender, EventArgs e)
        {
            using var dialogo = new OpenFileDialog();
            dialogo.Filter = "Imágenes (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp";
            dialogo.Title = "Seleccionar imagen del paso";

            var baseDir = AppDomain.CurrentDomain.BaseDirectory;

            // 1) Buscar primero carpeta img junto al ejecutable instalado
            var rutaImg = Path.Combine(baseDir, "img");

            // 2) Si no existe (entorno de desarrollo), subir hasta Manual_Winuae/img
            if (!Directory.Exists(rutaImg))
            {
                rutaImg = Path.Combine(baseDir, "..", "..", "..", "..", "..", "img");
            }

            if (Directory.Exists(rutaImg))
            {
                dialogo.InitialDirectory = rutaImg;
            }

            if (dialogo.ShowDialog(this) == DialogResult.OK)
            {
                var rutaCompleta = dialogo.FileName;

                // Guardamos ruta relativa a la carpeta img (por ejemplo "Creación_HDF\\paso1.png")
                string valorAlmacenar = Path.GetFileName(rutaCompleta);
                try
                {
                    var raizImgCompleta = Path.GetFullPath(rutaImg).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                    var seleccionCompleta = Path.GetFullPath(rutaCompleta);

                    if (seleccionCompleta.StartsWith(raizImgCompleta + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase))
                    {
                        valorAlmacenar = seleccionCompleta.Substring(raizImgCompleta.Length + 1);
                    }
                }
                catch
                {
                    // Si algo falla, usamos solo el nombre de archivo
                }

                cuadroRutaImagen.Text = valorAlmacenar;
            }
        }

        private void EstablecerIconoFormulario()
        {
            try
            {
                var baseDir = AppDomain.CurrentDomain.BaseDirectory;

                // 1) Buscar primero img/logo junto al ejecutable instalado
                var rutaBase = Path.Combine(baseDir, "img", "logo");

                // 2) Si no existe (entorno de desarrollo), subir hasta Manual_Winuae/img/logo
                if (!Directory.Exists(rutaBase))
                {
                    rutaBase = Path.Combine(baseDir, "..", "..", "..", "..", "..", "img", "logo");
                }
                var rutaIcono = Path.GetFullPath(Path.Combine(rutaBase, "logo-multires.ico"));
                if (File.Exists(rutaIcono))
                {
                    Icon = new Icon(rutaIcono);
                }
            }
            catch
            {
                // Si falla, se mantiene el icono por defecto
            }
        }

        private void botonAgregarPaso_Click(object sender, EventArgs e)
        {
            var titulo = cuadroTituloPaso.Text.Trim();
            var descripcion = cuadroDescripcionPaso.Text.Trim();
            var nombreImagen = cuadroRutaImagen.Text.Trim();

            if (string.IsNullOrWhiteSpace(titulo) || string.IsNullOrWhiteSpace(descripcion))
            {
                MessageBox.Show(this, "El título y la descripción del paso son obligatorios.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var paso = new PasoManual
            {
                TituloPaso = titulo,
                Descripcion = descripcion,
                NombreArchivoImagen = nombreImagen
            };

            _pasos.Add(paso);
            ActualizarListaPasos();

            cuadroTituloPaso.Clear();
            cuadroDescripcionPaso.Clear();
            cuadroRutaImagen.Clear();
        }

        private void ActualizarListaPasos()
        {
            listaPasos.DataSource = null;
            listaPasos.DisplayMember = nameof(PasoManual.TituloPaso);
            listaPasos.DataSource = _pasos;
        }

        private void botonAceptar_Click(object sender, EventArgs e)
        {
            var tituloManual = cuadroTituloManual.Text.Trim();

            if (string.IsNullOrWhiteSpace(tituloManual))
            {
                MessageBox.Show(this, "El título del manual es obligatorio.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_pasos.Count == 0)
            {
                MessageBox.Show(this, "Debe agregar al menos un paso al manual.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ManualCreado = new Manual
            {
                Titulo = tituloManual,
                Pasos = new List<PasoManual>(_pasos)
            };

            DialogResult = DialogResult.OK;
            Close();
        }

        private void botonCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
