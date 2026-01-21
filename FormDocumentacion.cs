using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsManual
{
    public partial class FormDocumentacion : Form
    {
        private bool idiomaEspanol = false;
        private Dictionary<string, string>? documentosDisponibles;

        private string DocumentacionPath => idiomaEspanol ?
            ObtenerRutaDocumentacion(true) :
            ObtenerRutaDocumentacion(false);

        public FormDocumentacion()
        {
            InitializeComponent();
            EstablecerIconoFormulario();
            ConfigurarPlaceholder();
            MostrarMensajeBienvenida();
            CargarDocumentosDisponibles();
        }

        private void EstablecerIconoFormulario()
        {
            try
            {
                var baseDir = AppDomain.CurrentDomain.BaseDirectory;
                var rutaBase = Path.Combine(baseDir, "img", "logo");

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
            }
        }

        private void MostrarMensajeBienvenida()
        {
            string htmlBienvenida = CrearHtmlProfesional(
                "📚 Documentación",
                @"<div style='text-align: center; padding: 40px;'>
                    <div style='font-size: 24px; margin-bottom: 20px; color: #4682B4;'>📚</div>
                    <h3 style='color: #2C3E50; margin-bottom: 15px;'>Bienvenido a la Documentación</h3>
                    <p style='color: #7F8C8D; line-height: 1.6;'>
                        Selecciona un documento de la lista para verlo.<br>
                        Puedes buscar por nombre parcial.
                    </p>
                </div>"
            );

            webContenido.DocumentText = htmlBienvenida;
        }

        private string CrearHtmlProfesional(string titulo, string contenido)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <title>{titulo}</title>
    <style>
        body {{
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            font-size: 11pt;
            line-height: 1.6;
            color: #2C3E50;
            margin: 0;
            padding: 20px;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
        }}
        .container {{
            background: white;
            border-radius: 12px;
            padding: 25px;
            box-shadow: 0 10px 30px rgba(0,0,0,0.1);
            max-width: 100%;
            margin: 0 auto;
        }}
        h1 {{
            color: #2C3E50;
            border-bottom: 3px solid #4682B4;
            padding-bottom: 10px;
            margin-bottom: 20px;
            font-size: 18pt;
        }}
        h2 {{
            color: #34495E;
            margin-top: 25px;
            margin-bottom: 15px;
            font-size: 14pt;
        }}
        h3 {{
            color: #4682B4;
            margin-top: 20px;
            margin-bottom: 10px;
            font-size: 12pt;
        }}
        code {{
            background: #F8F9FA;
            border: 1px solid #E9ECEF;
            border-radius: 4px;
            padding: 2px 6px;
            font-family: 'Courier New', monospace;
            color: #E74C3C;
            font-weight: bold;
        }}
        pre {{
            background: #2C3E50;
            color: #ECF0F1;
            padding: 15px;
            border-radius: 6px;
            overflow-x: auto;
            font-size: 10pt;
        }}
        .warning {{
            background: #FADBD8;
            border: 1px solid #F5B7B1;
            border-radius: 6px;
            padding: 12px;
            margin: 15px 0;
        }}
        ::selection {{
            background: #4682B4;
            color: white;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <h1>{titulo}</h1>
        {contenido}
    </div>
</body>
</html>";
        }

        private void ConfigurarPlaceholder()
        {
            txtBusqueda.Enter += (s, e) => {
                if (txtBusqueda.Text == "Ej: zool, zork, prince...")
                {
                    txtBusqueda.Text = "";
                    txtBusqueda.ForeColor = System.Drawing.Color.Black;
                }
            };

            txtBusqueda.Leave += (s, e) => {
                if (string.IsNullOrWhiteSpace(txtBusqueda.Text))
                {
                    txtBusqueda.Text = "Ej: zool, zork, prince...";
                    txtBusqueda.ForeColor = System.Drawing.Color.Gray;
                }
            };
        }

        private void CargarDocumentosDisponibles()
        {
            try
            {
                documentosDisponibles = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                var rutaDocumentacion = DocumentacionPath;

                if (!Directory.Exists(rutaDocumentacion))
                {
                    MostrarError($"No se encontró la carpeta de documentación en: {rutaDocumentacion}");
                    lstDocumentos.Items.Clear();
                    return;
                }

                foreach (var archivo in Directory.EnumerateFiles(rutaDocumentacion, "*.txt", SearchOption.AllDirectories))
                {
                    var nombre = Path.GetFileNameWithoutExtension(archivo)?.Trim();
                    if (string.IsNullOrWhiteSpace(nombre))
                    {
                        continue;
                    }

                    if (!documentosDisponibles.ContainsKey(nombre))
                    {
                        documentosDisponibles[nombre] = archivo;
                    }
                }

                ActualizarListaDocumentos();

                if (documentosDisponibles.Count == 0)
                {
                    MostrarError($"No se encontraron documentos .txt en: {rutaDocumentacion}");
                }
            }
            catch (Exception ex)
            {
                MostrarError($"Error al cargar la documentación: {ex.Message}");
                lstDocumentos.Items.Clear();
            }
        }

        private void comboIdioma_SelectedIndexChanged(object sender, EventArgs e)
        {
            idiomaEspanol = comboIdioma.SelectedIndex == 1;
            MostrarMensajeBienvenida();
            CargarDocumentosDisponibles();
            lstDocumentos.SelectedIndex = -1;
        }

        private void comboFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarListaDocumentos();
        }

        private void ActualizarListaDocumentos()
        {
            lstDocumentos.Items.Clear();
            if (documentosDisponibles == null) return;

            var filtro = comboFiltro.SelectedIndex;
            IEnumerable<string> documentosFiltrados;

            switch (filtro)
            {
                case 1: // Favoritos
                    documentosFiltrados = documentosDisponibles.Keys
                        .Where(doc => FavoritosManager.EsFavoritoManual(doc));
                    break;
                case 2: // No favoritos
                    documentosFiltrados = documentosDisponibles.Keys
                        .Where(doc => !FavoritosManager.EsFavoritoManual(doc));
                    break;
                default: // Todos
                    documentosFiltrados = documentosDisponibles.Keys;
                    break;
            }

            foreach (var documento in documentosFiltrados.OrderBy(x => x, StringComparer.OrdinalIgnoreCase))
            {
                var esFavorito = FavoritosManager.EsFavoritoManual(documento);
                var textoConEstrella = FavoritosManager.GetTextoConEstrella(documento, esFavorito);
                lstDocumentos.Items.Add(textoConEstrella);
            }
        }

        private void btnFavorito_Click(object sender, EventArgs e)
        {
            if (lstDocumentos.SelectedItem == null) return;

            var textoSeleccionado = lstDocumentos.SelectedItem.ToString();
            var nombreDocumento = FavoritosManager.GetNombreSinEstrella(textoSeleccionado);

            FavoritosManager.ToggleFavoritoManual(nombreDocumento);
            
            // Actualizar la lista para reflejar el cambio
            ActualizarListaDocumentos();
            
            // Intentar mantener la selección
            for (int i = 0; i < lstDocumentos.Items.Count; i++)
            {
                if (FavoritosManager.GetNombreSinEstrella(lstDocumentos.Items[i].ToString()) == nombreDocumento)
                {
                    lstDocumentos.SelectedIndex = i;
                    break;
                }
            }

            // Actualizar el texto del botón
            ActualizarBotonFavorito(nombreDocumento);
        }

        private void ActualizarBotonFavorito(string nombreDocumento)
        {
            var esFavorito = FavoritosManager.EsFavoritoManual(nombreDocumento);
            btnFavorito.Text = esFavorito ? "Quitar de favoritos" : "Agregar a favoritos";
            btnFavorito.BackColor = esFavorito ? 
                System.Drawing.Color.FromArgb(220, 53, 69) : 
                System.Drawing.Color.FromArgb(255, 193, 7);
        }

        private static string? BuscarDirectorioAscendiendo(string directorioInicio, string rutaRelativa)
        {
            try
            {
                var actual = new DirectoryInfo(directorioInicio);
                int niveles = 0;

                while (actual != null && niveles < 12)
                {
                    var candidato = Path.GetFullPath(Path.Combine(actual.FullName, rutaRelativa));
                    if (Directory.Exists(candidato))
                    {
                        return candidato;
                    }

                    actual = actual.Parent;
                    niveles++;
                }
            }
            catch
            {
            }

            return null;
        }

        private string ObtenerRutaDocumentacion(bool espanol)
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;

            var carpeta = espanol ? "AMIGADOCS_ES" : "AMIGADOCS";

            var rutaNuevaJuntoExe = Path.GetFullPath(Path.Combine(baseDir, "Docs", "Manuales", carpeta));
            if (Directory.Exists(rutaNuevaJuntoExe))
            {
                return rutaNuevaJuntoExe;
            }

            var rutaNuevaAscendiendo = BuscarDirectorioAscendiendo(baseDir, Path.Combine("Docs", "Manuales", carpeta));
            if (!string.IsNullOrWhiteSpace(rutaNuevaAscendiendo))
            {
                return rutaNuevaAscendiendo;
            }

            var rutaAntiguaJuntoExe = Path.GetFullPath(Path.Combine(baseDir, carpeta));
            if (Directory.Exists(rutaAntiguaJuntoExe))
            {
                return rutaAntiguaJuntoExe;
            }

            var rutaAntiguaAscendiendo = BuscarDirectorioAscendiendo(baseDir, carpeta);
            if (!string.IsNullOrWhiteSpace(rutaAntiguaAscendiendo))
            {
                return rutaAntiguaAscendiendo;
            }

            return rutaNuevaJuntoExe;
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            if (documentosDisponibles == null)
            {
                return;
            }

            var texto = txtBusqueda.Text?.Trim() ?? string.Empty;
            if (texto == "Ej: zool, zork, prince...")
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(texto))
            {
                lstDocumentos.Items.Clear();
                foreach (var nombre in documentosDisponibles.Keys.OrderBy(x => x, StringComparer.OrdinalIgnoreCase))
                {
                    lstDocumentos.Items.Add(nombre);
                }
                return;
            }

            var palabrasBusqueda = texto.ToLower().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var resultados = documentosDisponibles.Keys
                .Select(nombre => new { Nombre = nombre, Puntuacion = CalcularPuntuacionBusqueda(nombre.ToLower(), palabrasBusqueda) })
                .Where(x => x.Puntuacion > 0)
                .OrderByDescending(x => x.Puntuacion)
                .ThenBy(x => x.Nombre, StringComparer.OrdinalIgnoreCase)
                .Take(200)
                .ToList();

            lstDocumentos.Items.Clear();
            foreach (var resultado in resultados)
            {
                lstDocumentos.Items.Add(resultado.Nombre);
            }
        }

        private int CalcularPuntuacionBusqueda(string nombre, string[] palabrasBusqueda)
        {
            int puntuacion = 0;

            foreach (var palabra in palabrasBusqueda)
            {
                if (nombre.StartsWith(palabra))
                {
                    puntuacion += 10;
                }
                else if (nombre.Contains(palabra))
                {
                    puntuacion += 5;
                }

                var palabrasNombre = nombre.Split(new[] { ' ', '-', ':', '(', ')', '.', ',', '\'' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var palabraNombre in palabrasNombre)
                {
                    if (palabraNombre.Equals(palabra))
                    {
                        puntuacion += 8;
                    }
                    else if (palabraNombre.StartsWith(palabra))
                    {
                        puntuacion += 3;
                    }
                }
            }

            return puntuacion;
        }

        private void lstDocumentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstDocumentos.SelectedItem == null || documentosDisponibles == null)
            {
                return;
            }

            var textoSeleccionado = lstDocumentos.SelectedItem.ToString();
            var nombre = FavoritosManager.GetNombreSinEstrella(textoSeleccionado);
            
            if (string.IsNullOrWhiteSpace(nombre))
            {
                return;
            }

            // Actualizar el botón de favoritos
            ActualizarBotonFavorito(nombre);

            if (!documentosDisponibles.TryGetValue(nombre, out var rutaArchivo))
            {
                MostrarError("No se encontró el archivo del documento.");
                return;
            }

            if (!File.Exists(rutaArchivo))
            {
                MostrarError("No se encontró el archivo del documento.");
                return;
            }

            try
            {
                var contenido = File.ReadAllText(rutaArchivo);
                MostrarContenidoFormateado(nombre, contenido);
            }
            catch (Exception ex)
            {
                MostrarError($"Error al leer el archivo: {ex.Message}");
            }
        }

        private void MostrarContenidoFormateado(string titulo, string contenido)
        {
            string contenidoHtml = FormatearContenidoHtml(contenido);
            string htmlCompleto = CrearHtmlProfesional($"📚 {titulo}", contenidoHtml);
            webContenido.DocumentText = htmlCompleto;
        }

        private string FormatearContenidoHtml(string contenido)
        {
            if (string.IsNullOrEmpty(contenido))
            {
                return "<p>No hay contenido disponible.</p>";
            }

            var lineas = contenido.Split(new[] { '\r', '\n' }, StringSplitOptions.None);
            var html = new StringBuilder();
            var encoder = HtmlEncoder.Create(UnicodeRanges.All);

            bool dentroDePre = false;

            foreach (var linea in lineas)
            {
                var lineaOriginal = linea ?? string.Empty;
                var lineaLimpia = lineaOriginal.TrimEnd();

                if (lineaLimpia.StartsWith(" ") || lineaLimpia.StartsWith("\t"))
                {
                    if (!dentroDePre)
                    {
                        html.AppendLine("<pre>");
                        dentroDePre = true;
                    }
                    html.AppendLine(encoder.Encode(lineaOriginal));
                    continue;
                }

                if (dentroDePre)
                {
                    html.AppendLine("</pre>");
                    dentroDePre = false;
                }

                if (string.IsNullOrWhiteSpace(lineaLimpia))
                {
                    continue;
                }

                if (lineaLimpia.EndsWith(":") && lineaLimpia.Length < 80)
                {
                    html.AppendLine($"<h2>{encoder.Encode(lineaLimpia)}</h2>");
                }
                else if (lineaLimpia.Length > 100)
                {
                    html.AppendLine($"<p>{encoder.Encode(lineaLimpia)}</p>");
                }
                else
                {
                    html.AppendLine($"<p><strong>{encoder.Encode(lineaLimpia)}</strong></p>");
                }
            }

            if (dentroDePre)
            {
                html.AppendLine("</pre>");
            }

            return html.ToString();
        }

        private void MostrarError(string mensaje)
        {
            string htmlError = CrearHtmlProfesional(
                "❌ Error",
                $@"<div class='warning'>
                    <h3>⚠️ {mensaje}</h3>
                    <p>Verifica que exista la carpeta Docs\\Manuales y que contenga documentos .txt.</p>
                </div>"
            );

            webContenido.DocumentText = htmlError;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
