using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsManual
{
    public partial class FormImagenAmpliada : Form
    {
        private readonly Image _imagenOriginal;
        private float _zoomFactor = 1.0f;
        private Point _ultimaPosicionRaton;
        private bool _arrastrando = false;

        public FormImagenAmpliada(Image imagen)
        {
            _imagenOriginal = imagen;
            InitializeComponent();
            ConfigurarFormulario();
        }

        private void ConfigurarFormulario()
        {
            // Configuración básica del formulario
            Text = "Imagen Ampliada";
            BackColor = Color.Black;
            KeyPreview = true;
            DoubleBuffered = true;
            StartPosition = FormStartPosition.CenterScreen;

            // Configurar tamaño inicial basado en la imagen y pantalla
            var pantalla = Screen.PrimaryScreen.WorkingArea;
            var maxAncho = pantalla.Width * 0.9; // 90% del ancho de pantalla
            var maxAlto = pantalla.Height * 0.9; // 90% del alto de pantalla

            var imagenAncho = _imagenOriginal.Width;
            var imagenAlto = _imagenOriginal.Height;

            // Ajustar tamaño para que quepa en pantalla manteniendo proporción
            if (imagenAncho > maxAncho || imagenAlto > maxAlto)
            {
                var ratioAncho = maxAncho / imagenAncho;
                var ratioAlto = maxAlto / imagenAlto;
                var ratio = Math.Min(ratioAncho, ratioAlto);

                ClientSize = new Size((int)(imagenAncho * ratio), (int)(imagenAlto * ratio));
            }
            else
            {
                ClientSize = new Size(imagenAncho, imagenAlto);
            }

            // Crear el PictureBox para la imagen
            var pictureBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Black
            };
            pictureBox.Image = _imagenOriginal;
            pictureBox.MouseDown += PictureBox_MouseDown;
            pictureBox.MouseMove += PictureBox_MouseMove;
            pictureBox.MouseUp += PictureBox_MouseUp;
            pictureBox.MouseWheel += PictureBox_MouseWheel;

            Controls.Add(pictureBox);

            // Eventos de teclado para cerrar y zoom
            KeyDown += FormImagenAmpliada_KeyDown;
        }

        private void FormImagenAmpliada_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    break;
                case Keys.Add:
                case Keys.Oemplus:
                    AplicarZoom(1.2f);
                    break;
                case Keys.Subtract:
                case Keys.OemMinus:
                    AplicarZoom(0.8f);
                    break;
                case Keys.D0:
                case Keys.NumPad0:
                    RestablecerZoom();
                    break;
            }
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _arrastrando = true;
                _ultimaPosicionRaton = e.Location;
                Cursor = Cursors.Hand;
            }
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (_arrastrando)
            {
                var pictureBox = (PictureBox)sender;
                var deltaX = e.Location.X - _ultimaPosicionRaton.X;
                var deltaY = e.Location.Y - _ultimaPosicionRaton.Y;

                // Implementar pan si la imagen está ampliada
                if (_zoomFactor > 1.0f)
                {
                    // Lógica de desplazamiento aquí si es necesario
                }

                _ultimaPosicionRaton = e.Location;
            }
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            _arrastrando = false;
            Cursor = Cursors.Default;
        }

        private void PictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                AplicarZoom(1.1f);
            }
            else
            {
                AplicarZoom(0.9f);
            }
        }

        private void AplicarZoom(float factor)
        {
            _zoomFactor *= factor;
            
            // Limitar el zoom entre 0.1x y 10x
            _zoomFactor = Math.Max(0.1f, Math.Min(10.0f, _zoomFactor));

            var pictureBox = Controls[0] as PictureBox;
            if (pictureBox != null && _imagenOriginal != null)
            {
                var nuevoAncho = (int)(_imagenOriginal.Width * _zoomFactor);
                var nuevoAlto = (int)(_imagenOriginal.Height * _zoomFactor);
                
                var bitmapRedimensionado = new Bitmap(nuevoAncho, nuevoAlto);
                using (var g = Graphics.FromImage(bitmapRedimensionado))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(_imagenOriginal, 0, 0, nuevoAncho, nuevoAlto);
                }

                pictureBox.Image?.Dispose();
                pictureBox.Image = bitmapRedimensionado;
                pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
            }

            // Actualizar el título para mostrar el nivel de zoom
            Text = $"Imagen Ampliada - Zoom: {_zoomFactor:P0}";
        }

        private void RestablecerZoom()
        {
            _zoomFactor = 1.0f;
            var pictureBox = Controls[0] as PictureBox;
            if (pictureBox != null)
            {
                pictureBox.Image?.Dispose();
                pictureBox.Image = _imagenOriginal;
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            Text = "Imagen Ampliada";
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // No dispose _imagenOriginal ya que pertenece al formulario principal
            }
            base.Dispose(disposing);
        }
    }
}
