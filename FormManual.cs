using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WinFormsManual
{
    public partial class FormManual : Form
    {
        private readonly BindingList<Manual> _manuales = new();
        private int _indiceImagenActual = 0;
        private PasoManual? _pasoActual;

        public FormManual()
        {
            InitializeComponent();

            EstablecerIconoFormulario();
            // Mostrar nombre y versión de la aplicación en la barra de título (sin sufijo de hash)
            var versionLimpia = Application.ProductVersion.Split('+')[0];
            Text = $"{Application.ProductName} v{versionLimpia}";
            CargarManuales();
        }

        private void CargarManuales()
        {
            var manualesDesdeArchivo = RepositorioManuales.CargarManuales();

            if (manualesDesdeArchivo.Count == 0)
            {
                // Si no hay archivo o está vacío, inicializamos con el manual de WinUAE
                var manualWinUae = ManualesPredefinidos.CrearManualWinUaeCreacionHdf();
                _manuales.Add(manualWinUae);
                RepositorioManuales.GuardarManuales(_manuales);
            }
            else
            {
                foreach (var manual in manualesDesdeArchivo)
                {
                    _manuales.Add(manual);
                }
            }

            comboManuales.DisplayMember = nameof(Manual.Titulo);
            comboManuales.DataSource = _manuales;
        }

        private void EstablecerIconoFormulario()
        {
            try
            {
                // 1) Buscar primero img/logo junto al ejecutable instalado
                var baseDir = AppDomain.CurrentDomain.BaseDirectory;
                var rutaBase = Path.Combine(baseDir, "img", "logo");

                // 2) Si no existe (entorno de desarrollo), subir hasta Manuales_WinUae/img/logo
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

        private void comboManuales_SelectedIndexChanged(object sender, EventArgs e)
        {
            var manualSeleccionado = comboManuales.SelectedItem as Manual;
            if (manualSeleccionado == null) return;

            listaPasos.DisplayMember = nameof(PasoManual.TituloPaso);
            listaPasos.DataSource = manualSeleccionado.Pasos;
        }

        private void listaPasos_SelectedIndexChanged(object sender, EventArgs e)
        {
            var paso = listaPasos.SelectedItem as PasoManual;
            if (paso == null) return;

            _pasoActual = paso;
            _indiceImagenActual = 0;

            cuadroTextoPaso.Text = paso.Descripcion;

            CargarImagenActual();
        }

        private void menuSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void menuCheatCodes_Click(object sender, EventArgs e)
        {
            Hide(); // Ocultar la ventana del manual
            using var formulario = new FormCheatCodes();
            formulario.ShowDialog(); // Mostrar como diálogo modal
            Show(); // Mostrar nuevamente la ventana del manual
        }

        private void menuCheatDocumentacion_Click(object sender, EventArgs e)
        {
            Hide();
            using var formulario = new FormDocumentacion();
            formulario.ShowDialog();
            Show();
        }

        private void CargarImagenActual()
        {
            if (_pasoActual == null)
            {
                imagenPaso.Image = null;
                etiquetaIndiceImagen.Text = string.Empty;
                botonImagenAnterior.Visible = false;
                botonImagenSiguiente.Visible = false;
                return;
            }

            // Determinar la ruta de la imagen según el índice actual
            string? rutaRelativa = null;

            var totalImagenes = _pasoActual.Imagenes != null ? _pasoActual.Imagenes.Count : 0;

            if (totalImagenes > 0)
            {
                if (_indiceImagenActual < 0 || _indiceImagenActual >= totalImagenes)
                {
                    _indiceImagenActual = 0;
                }
                rutaRelativa = _pasoActual.Imagenes[_indiceImagenActual];
            }
            else if (!string.IsNullOrWhiteSpace(_pasoActual.NombreArchivoImagen))
            {
                rutaRelativa = _pasoActual.NombreArchivoImagen;
            }

            if (!string.IsNullOrWhiteSpace(rutaRelativa))
            {
                try
                {
                    var baseDir = AppDomain.CurrentDomain.BaseDirectory;

                    // 1) Buscar primero carpeta img junto al ejecutable
                    var rutaBase = Path.Combine(baseDir, "img");

                    // 2) Si no existe (entorno de desarrollo), subir hasta Manuales_WinUae/img
                    if (!Directory.Exists(rutaBase))
                    {
                        rutaBase = Path.Combine(baseDir, "..", "..", "..", "..", "..", "img");
                    }
                    var rutaImagen = Path.GetFullPath(Path.Combine(rutaBase, rutaRelativa));

                    if (File.Exists(rutaImagen))
                    {
                        using var imagenTemp = System.Drawing.Image.FromFile(rutaImagen);
                        imagenPaso.Image?.Dispose();
                        imagenPaso.Image = (System.Drawing.Image)imagenTemp.Clone();
                    }
                    else
                    {
                        imagenPaso.Image = null;
                    }
                }
                catch
                {
                    imagenPaso.Image = null;
                }
            }
            else
            {
                imagenPaso.Image = null;
            }

            // Actualizar visibilidad de flechas y texto de índice
            if (totalImagenes > 1)
            {
                botonImagenAnterior.Visible = true;
                botonImagenSiguiente.Visible = true;
                etiquetaIndiceImagen.Visible = true;
                etiquetaIndiceImagen.Text = $"< {_indiceImagenActual + 1} de {totalImagenes} >";
            }
            else
            {
                botonImagenAnterior.Visible = false;
                botonImagenSiguiente.Visible = false;
                etiquetaIndiceImagen.Visible = false;
                etiquetaIndiceImagen.Text = string.Empty;
            }
        }

        private void botonImagenAnterior_Click(object sender, EventArgs e)
        {
            if (_pasoActual == null || _pasoActual.Imagenes == null || _pasoActual.Imagenes.Count <= 1)
            {
                return;
            }

            _indiceImagenActual--;
            if (_indiceImagenActual < 0)
            {
                _indiceImagenActual = _pasoActual!.Imagenes.Count - 1;
            }

            CargarImagenActual();
        }

        private void botonImagenSiguiente_Click(object sender, EventArgs e)
        {
            if (_pasoActual == null || _pasoActual.Imagenes == null || _pasoActual.Imagenes.Count <= 1)
            {
                return;
            }

            _indiceImagenActual++;
            if (_indiceImagenActual >= _pasoActual.Imagenes.Count)
            {
                _indiceImagenActual = 0;
            }

            CargarImagenActual();
        }

        private void botonAgregarManual_Click(object sender, EventArgs e)
        {
            using var editor = new FormEditarManual();
            if (editor.ShowDialog(this) == DialogResult.OK && editor.ManualCreado != null)
            {
                _manuales.Add(editor.ManualCreado);
                comboManuales.SelectedItem = editor.ManualCreado;
                RepositorioManuales.GuardarManuales(_manuales);
            }
        }

        private void botonEditarManual_Click(object sender, EventArgs e)
        {
            var manualActual = comboManuales.SelectedItem as Manual;
            if (manualActual == null)
            {
                return;
            }

            using var editor = new FormEditarManual(manualActual);
            if (editor.ShowDialog(this) == DialogResult.OK && editor.ManualCreado != null)
            {
                var indice = _manuales.IndexOf(manualActual);
                if (indice >= 0)
                {
                    _manuales[indice] = editor.ManualCreado;
                    comboManuales.SelectedItem = editor.ManualCreado;
                    RepositorioManuales.GuardarManuales(_manuales);
                }
            }
        }

        private void imagenPaso_Click(object sender, EventArgs e)
        {
            if (imagenPaso.Image != null)
            {
                using var formAmpliada = new FormImagenAmpliada(imagenPaso.Image);
                formAmpliada.ShowDialog(this);
            }
        }
    }
}
