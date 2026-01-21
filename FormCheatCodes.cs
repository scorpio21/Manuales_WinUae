using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsManual
{
    public partial class FormCheatCodes : Form
    {
        private string CheatCodesPath => idiomaEspanol ? 
            ObtenerRutaCheatCodes(true) : 
            ObtenerRutaCheatCodes(false);
        private string AvailableCheatCodesFile => Path.Combine(CheatCodesPath, "AvailableCheatCodes.ini");
        private Dictionary<string, string>? juegosDisponibles;
        private string contenidoOriginal = "";
        private bool esTraducido = false;
        private bool idiomaEspanol = false;
        private static readonly HttpClient httpClient = new HttpClient();

        public FormCheatCodes()
        {
            InitializeComponent();
            EstablecerIconoFormulario();
            CargarJuegosDisponibles();
            ConfigurarPlaceholder();
            MostrarMensajeBienvenida();
            
            // Forzar actualización de favoritos después de una pequeña demora
            this.Load += (s, e) => {
                Task.Delay(100).ContinueWith(_ => {
                    if (juegosDisponibles != null)
                    {
                        this.Invoke(new Action(ActualizarListaJuegos));
                    }
                });
            };
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
                "🎮 Cheat Codes - WinUAE",
                @"<div style='text-align: center; padding: 40px;'>
                    <div style='font-size: 24px; margin-bottom: 20px; color: #4682B4;'>🎮</div>
                    <h3 style='color: #2C3E50; margin-bottom: 15px;'>Bienvenido a la Base de Datos de Cheat Codes</h3>
                    <p style='color: #7F8C8D; line-height: 1.6;'>
                        Selecciona un juego de la lista para ver sus códigos de trampa.<br>
                        Puedes buscar por nombre parcial y traducir el contenido al español.
                    </p>
                    <div style='background: #ECF0F1; padding: 15px; border-radius: 8px; margin-top: 20px;'>
                        <strong style='color: #34495E;'>💡 Tip:</strong> Usa el buscador para encontrar juegos rápidamente.
                    </div>
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
        .cheat-line {{
            background: #F8F9FA;
            border-left: 4px solid #4682B4;
            padding: 8px 12px;
            margin: 8px 0;
            border-radius: 0 4px 4px 0;
        }}
        .tip {{
            background: #D1F2EB;
            border: 1px solid #A3E4D7;
            border-radius: 6px;
            padding: 12px;
            margin: 15px 0;
        }}
        .warning {{
            background: #FADBD8;
            border: 1px solid #F5B7B1;
            border-radius: 6px;
            padding: 12px;
            margin: 15px 0;
        }}
        pre {{
            background: #2C3E50;
            color: #ECF0F1;
            padding: 15px;
            border-radius: 6px;
            overflow-x: auto;
            font-size: 10pt;
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
                if (txtBusqueda.Text == "Ej: pool, sonic, mario...")
                {
                    txtBusqueda.Text = "";
                    txtBusqueda.ForeColor = System.Drawing.Color.Black;
                }
            };

            txtBusqueda.Leave += (s, e) => {
                if (string.IsNullOrWhiteSpace(txtBusqueda.Text))
                {
                    txtBusqueda.Text = "Ej: pool, sonic, mario...";
                    txtBusqueda.ForeColor = System.Drawing.Color.Gray;
                }
            };
        }

        private void CargarJuegosDisponibles()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"=== CARGANDO JUEGOS ===");
                System.Diagnostics.Debug.WriteLine($"Idioma español: {idiomaEspanol}");
                System.Diagnostics.Debug.WriteLine($"CheatCodesPath: {CheatCodesPath}");
                System.Diagnostics.Debug.WriteLine($"AvailableCheatCodesFile: {AvailableCheatCodesFile}");
                
                juegosDisponibles = new Dictionary<string, string>();
                
                if (!Directory.Exists(CheatCodesPath))
                {
                    MostrarErrorCheatCodesNoEncontrado();
                    return;
                }

                if (File.Exists(AvailableCheatCodesFile))
                {
                    var lineas = File.ReadAllLines(AvailableCheatCodesFile);
                    System.Diagnostics.Debug.WriteLine($"Líneas leídas: {lineas.Length}");

                    foreach (var linea in lineas)
                    {
                        if (string.IsNullOrWhiteSpace(linea)) continue;
                        if (linea.StartsWith("COPY/PASTE") || linea.StartsWith("NOTE:") ||
                            linea.StartsWith("AVAILABLE CHEAT CODES") || linea.StartsWith("from the") ||
                            linea.StartsWith("to be open")) continue;

                        var nombreJuego = linea.Trim();
                        if (!string.IsNullOrEmpty(nombreJuego))
                        {
                            juegosDisponibles[nombreJuego] = nombreJuego;
                            System.Diagnostics.Debug.WriteLine($"Juego agregado: '{nombreJuego}'");
                        }
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"No existe INI. Escaneando archivos .txt en: {CheatCodesPath}");
                    var juegosEscaneados = EscanearJuegosDesdeTxt(CheatCodesPath);

                    if (juegosEscaneados.Count == 0)
                    {
                        MessageBox.Show(
                            $"No se encontró el archivo de juegos disponibles ni archivos .txt en: {CheatCodesPath}",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    foreach (var juego in juegosEscaneados)
                    {
                        juegosDisponibles[juego] = juego;
                    }

                    try
                    {
                        GuardarAvailableCheatCodesIni(AvailableCheatCodesFile, juegosEscaneados);
                        System.Diagnostics.Debug.WriteLine($"INI reconstruido: {AvailableCheatCodesFile}");
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"No se pudo reconstruir INI: {ex.Message}");
                    }
                }

                ActualizarListaJuegos();
                
                System.Diagnostics.Debug.WriteLine($"Total juegos en lista: {lstJuegos.Items.Count}");
                System.Diagnostics.Debug.WriteLine($"=== FIN CARGA ===");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los juegos disponibles: {ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static List<string> EscanearJuegosDesdeTxt(string cheatCodesPath)
        {
            var juegos = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            
            var carpetas = new List<string> { "0" };
            for (char c = 'A'; c <= 'Z'; c++) carpetas.Add(c.ToString());

            foreach (var carpeta in carpetas)
            {
                var dir = Path.Combine(cheatCodesPath, carpeta);
                if (!Directory.Exists(dir)) continue;

                try
                {
                    foreach (var archivo in Directory.EnumerateFiles(dir, "*.txt", SearchOption.TopDirectoryOnly))
                    {
                        var nombre = Path.GetFileNameWithoutExtension(archivo)?.Trim();
                        if (!string.IsNullOrWhiteSpace(nombre))
                        {
                            juegos.Add(nombre);
                        }
                    }
                }
                catch
                {
                }
            }

            return juegos.OrderBy(x => x, StringComparer.OrdinalIgnoreCase).ToList();
        }

        private static void GuardarAvailableCheatCodesIni(string iniPath, List<string> juegosOrdenados)
        {
            var dir = Path.GetDirectoryName(iniPath);
            if (!string.IsNullOrEmpty(dir)) Directory.CreateDirectory(dir);

            var sb = new StringBuilder();
            sb.AppendLine("COPY/PASTE with right mouse click ");
            sb.AppendLine("from the AvailableCheatCodes list the cheat code ");
            sb.AppendLine("to be open to the commandline prompt. ");
            sb.AppendLine("NOTE: spaces after the copied name can affect the cheat codes display.");
            sb.AppendLine(" ");
            sb.AppendLine("AVAILABLE CHEAT CODES (add more cheat codes anytime on the folders with letter) ");
            sb.AppendLine(" ");

            foreach (var juego in juegosOrdenados)
            {
                sb.AppendLine(juego);
            }

            File.WriteAllText(iniPath, sb.ToString(), Encoding.UTF8);
        }

        private void comboIdioma_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboIdioma.SelectedIndex == 1) // Español
            {
                idiomaEspanol = true;
                btnTraducir.Visible = false; // Ocultar botón de traducción
            }
            else // Inglés
            {
                idiomaEspanol = false;
                btnTraducir.Visible = true; // Mostrar botón de traducción
            }
            
            // Recargar juegos con el nuevo idioma
            CargarJuegosDisponibles();
            
            // Limpiar selección actual
            lstJuegos.SelectedIndex = -1;
            contenidoOriginal = "";
            esTraducido = false;
            
            // Mostrar mensaje de bienvenida apropiado
            MostrarMensajeBienvenida();
        }

        private void comboFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarListaJuegos();
        }

        private void ActualizarListaJuegos()
        {
            lstJuegos.Items.Clear();
            if (juegosDisponibles == null) return;

            var filtro = comboFiltro.SelectedIndex;
            IEnumerable<string> juegosFiltrados;

            switch (filtro)
            {
                case 1: // Favoritos
                    juegosFiltrados = juegosDisponibles.Keys
                        .Where(juego => FavoritosManager.EsFavoritoCheatCode(juego));
                    break;
                case 2: // No favoritos
                    juegosFiltrados = juegosDisponibles.Keys
                        .Where(juego => !FavoritosManager.EsFavoritoCheatCode(juego));
                    break;
                default: // Todos
                    juegosFiltrados = juegosDisponibles.Keys;
                    break;
            }

            foreach (var juego in juegosFiltrados.OrderBy(x => x))
            {
                var esFavorito = FavoritosManager.EsFavoritoCheatCode(juego);
                var textoConEstrella = FavoritosManager.GetTextoConEstrella(juego, esFavorito);
                lstJuegos.Items.Add(textoConEstrella);
            }
        }

        private void btnFavorito_Click(object sender, EventArgs e)
        {
            if (lstJuegos.SelectedItem == null) return;

            var textoSeleccionado = lstJuegos.SelectedItem.ToString();
            var nombreJuego = FavoritosManager.GetNombreSinEstrella(textoSeleccionado);

            FavoritosManager.ToggleFavoritoCheatCode(nombreJuego);
            
            // Actualizar la lista para reflejar el cambio
            ActualizarListaJuegos();
            
            // Intentar mantener la selección
            for (int i = 0; i < lstJuegos.Items.Count; i++)
            {
                if (FavoritosManager.GetNombreSinEstrella(lstJuegos.Items[i].ToString()) == nombreJuego)
                {
                    lstJuegos.SelectedIndex = i;
                    break;
                }
            }

            // Actualizar el texto del botón
            ActualizarBotonFavorito(nombreJuego);
        }

        private void ActualizarBotonFavorito(string nombreJuego)
        {
            var esFavorito = FavoritosManager.EsFavoritoCheatCode(nombreJuego);
            btnFavorito.Text = esFavorito ? "Quitar de favoritos" : "Agregar a favoritos";
            btnFavorito.BackColor = esFavorito ? 
                System.Drawing.Color.FromArgb(220, 53, 69) : 
                System.Drawing.Color.FromArgb(255, 193, 7);
        }

        private void MostrarMensajeDepuracion(string mensaje)
        {
            // Mostrar mensaje en el WebBrowser para depuración
            string htmlDepuracion = CrearHtmlProfesional(
                "🔍 Depuración",
                $@"<div class='tip'>
                    <h3>Información de Depuración</h3>
                    <p><strong>Ruta cheat codes:</strong> {CheatCodesPath}</p>
                    <p><strong>Archivo AvailableCheatCodes.ini:</strong> {AvailableCheatCodesFile}</p>
                    <p><strong>Mensaje:</strong> {mensaje}</p>
                    <p><strong>Existe carpeta:</strong> {Directory.Exists(CheatCodesPath)}</p>
                    <p><strong>Existe archivo:</strong> {File.Exists(AvailableCheatCodesFile)}</p>
                </div>"
            );
            webContenido.DocumentText = htmlDepuracion;
        }

        private string ObtenerRutaCheatCodes(bool espanol)
        {
            // Nueva estructura: Docs\Cheat\AMIGACHEATCODES_ES o AMIGACHEATCODES
            string nuevaRuta = Path.Combine(Application.StartupPath, "Docs", "Cheat", 
                espanol ? "AMIGACHEATCODES_ES" : "AMIGACHEATCODES");
            
            // Estructura antigua: AMIGACHEATCODES_ES o AMIGACHEATCODES
            string rutaAntigua = Path.Combine(Application.StartupPath, 
                espanol ? "AMIGACHEATCODES_ES" : "AMIGACHEATCODES");
            
            // Priorizar nueva estructura si existe
            if (Directory.Exists(nuevaRuta))
            {
                System.Diagnostics.Debug.WriteLine($"Usando nueva ruta: {nuevaRuta}");
                return nuevaRuta;
            }
            
            // Fallback a estructura antigua
            if (Directory.Exists(rutaAntigua))
            {
                System.Diagnostics.Debug.WriteLine($"Usando ruta antigua: {rutaAntigua}");
                return rutaAntigua;
            }
            
            // Si ninguna existe, devolver la nueva ruta (para crearla si es necesario)
            System.Diagnostics.Debug.WriteLine($"Ninguna ruta existe, devolviendo nueva: {nuevaRuta}");
            return nuevaRuta;
        }

        private void MostrarErrorCheatCodesNoEncontrado()
        {
            string htmlError = CrearHtmlProfesional(
                "❌ Cheat Codes No Encontrados",
                $@"<div class='warning'>
                    <h3>📁 Carpeta AMIGACHEATCODES no encontrada</h3>
                    <p><strong>Ruta buscada:</strong> {CheatCodesPath}</p>
                    <p><strong>Solución:</strong></p>
                    <ul>
                        <li>Copia la carpeta <code>AMIGACHEATCODES</code> junto al ejecutable del programa</li>
                        <li>Asegúrate de que contenga las subcarpetas (A-Z, 0) y el archivo <code>AvailableCheatCodes.ini</code></li>
                    </ul>
                    <div style='background: #F8F9FA; padding: 10px; border-radius: 4px; margin-top: 15px;'>
                        <strong>📦 Estructura esperada:</strong><br>
                        <code>Manuales_WinUAE.exe<br>
                        AMIGACHEATCODES/<br>
                        ├── AvailableCheatCodes.ini<br>
                        ├── 0/<br>
                        ├── A/<br>
                        ├── B/<br>
                        └── ... (carpetas C-Z)</code>
                    </div>
                </div>"
            );
            webContenido.DocumentText = htmlError;
            
            // Deshabilitar búsqueda y lista
            lstJuegos.Enabled = false;
            txtBusqueda.Enabled = false;
            btnTraducir.Enabled = false;
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            var textoBusqueda = txtBusqueda.Text.ToLower().Trim();
            
            // No buscar si es el placeholder
            if (textoBusqueda == "ej: pool, sonic, mario...")
                return;
                
            lstJuegos.Items.Clear();

            if (juegosDisponibles == null) return;

            if (string.IsNullOrEmpty(textoBusqueda))
            {
                foreach (var juego in juegosDisponibles.Keys.OrderBy(x => x))
                {
                    lstJuegos.Items.Add(juego);
                }
            }
            else
            {
                // Búsqueda mejorada: divide el texto en palabras y busca coincidencias
                var palabrasBusqueda = textoBusqueda.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                
                var juegosFiltrados = juegosDisponibles.Keys
                    .Select(juego => new { 
                        Nombre = juego, 
                        Puntuacion = CalcularPuntuacionBusqueda(juego.ToLower(), palabrasBusqueda)
                    })
                    .Where(x => x.Puntuacion > 0)
                    .OrderByDescending(x => x.Puntuacion)
                    .ThenBy(x => x.Nombre)
                    .Select(x => x.Nombre);

                foreach (var juego in juegosFiltrados)
                {
                    lstJuegos.Items.Add(juego);
                }
            }
        }

        private int CalcularPuntuacionBusqueda(string nombreJuego, string[] palabrasBusqueda)
        {
            int puntuacion = 0;
            
            foreach (var palabra in palabrasBusqueda)
            {
                // Coincidencia exacta al inicio = más puntos
                if (nombreJuego.StartsWith(palabra))
                    puntuacion += 10;
                // Coincidencia exacta en cualquier parte = puntos medios
                else if (nombreJuego.Contains(palabra))
                    puntuacion += 5;
                
                // Búsqueda por palabras individuales en el nombre del juego
                var palabrasJuego = nombreJuego.Split(new[] { ' ', '-', ':', '(', ')', '.', ',', '\'' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var palabraJuego in palabrasJuego)
                {
                    if (palabraJuego.Equals(palabra))
                        puntuacion += 8; // Coincidencia de palabra exacta
                    else if (palabraJuego.StartsWith(palabra))
                        puntuacion += 3; // Comienzo de palabra
                }
            }
            
            return puntuacion;
        }

        private void lstJuegos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Depuración: mostrar qué juego se seleccionó
                if (lstJuegos.SelectedItem != null)
                {
                    System.Diagnostics.Debug.WriteLine($"Juego seleccionado: {lstJuegos.SelectedItem}");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("No hay juego seleccionado");
                }

                if (lstJuegos.SelectedItem == null || juegosDisponibles == null) return;

                var textoSeleccionado = lstJuegos.SelectedItem.ToString();
                var nombreJuego = FavoritosManager.GetNombreSinEstrella(textoSeleccionado);
                
                if (nombreJuego == null) return;
                
                // Actualizar el botón de favoritos
                ActualizarBotonFavorito(nombreJuego);
                
                var rutaArchivo = BuscarArchivoCheatCode(nombreJuego);

                if (File.Exists(rutaArchivo))
                {
                    try
                    {
                        contenidoOriginal = File.ReadAllText(rutaArchivo);
                        esTraducido = false;
                        btnTraducir.Text = "Traducir al español";
                        MostrarContenidoFormateado(nombreJuego, contenidoOriginal);
                    }
                    catch (Exception ex)
                    {
                        MostrarError($"Error al leer el archivo: {ex.Message}");
                    }
                }
                else
                {
                    MostrarError("No se encontró el archivo de códigos para este juego.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en selección de juego: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarContenidoFormateado(string nombreJuego, string contenido)
        {
            string contenidoHtml = FormatearContenidoHtml(contenido);
            string htmlCompleto = CrearHtmlProfesional($"🎮 {nombreJuego}", contenidoHtml);
            webContenido.DocumentText = htmlCompleto;
        }

        private string FormatearContenidoHtml(string contenido)
        {
            if (string.IsNullOrEmpty(contenido))
                return "<p>No hay contenido disponible.</p>";

            var lineas = contenido.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var html = new StringBuilder();
            var encoder = HtmlEncoder.Create(UnicodeRanges.All);

            foreach (var linea in lineas)
            {
                var lineaLimpia = linea.Trim();
                
                if (string.IsNullOrEmpty(lineaLimpia)) continue;

                // Detectar títulos (líneas que terminan con :)
                if (lineaLimpia.EndsWith(":"))
                {
                    html.AppendLine($"<h2>{encoder.Encode(lineaLimpia)}</h2>");
                }
                // Detectar códigos (líneas con números y guiones)
                else if (System.Text.RegularExpressions.Regex.IsMatch(lineaLimpia, @"^\d+[-\s].*"))
                {
                    html.AppendLine($"<div class='cheat-line'><code>{encoder.Encode(lineaLimpia)}</code></div>");
                }
                // Detectar líneas que parecen tips o advertencias
                else if (lineaLimpia.ToLower().Contains("tip") || lineaLimpia.ToLower().Contains("note") || 
                        lineaLimpia.ToLower().Contains("warning") || lineaLimpia.ToLower().Contains("important"))
                {
                    html.AppendLine($"<div class='tip'><strong>{encoder.Encode(lineaLimpia)}</strong></div>");
                }
                // Líneas largas que podrían ser descripciones
                else if (lineaLimpia.Length > 50)
                {
                    html.AppendLine($"<p>{encoder.Encode(lineaLimpia)}</p>");
                }
                // Líneas cortas
                else
                {
                    html.AppendLine($"<p><strong>{encoder.Encode(lineaLimpia)}</strong></p>");
                }
            }

            return html.ToString();
        }

        private void MostrarError(string mensaje)
        {
            string htmlError = CrearHtmlProfesional(
                "❌ Error",
                $@"<div class='warning'>
                    <h3>⚠️ {mensaje}</h3>
                    <p>Por favor, verifica que el juego seleccionado tenga códigos disponibles.</p>
                </div>"
            );
            webContenido.DocumentText = htmlError;
        }

        private async void btnTraducir_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(contenidoOriginal))
            {
                MessageBox.Show("Por favor, selecciona un juego primero.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (esTraducido)
            {
                // Volver al original
                var nombreJuego = lstJuegos.SelectedItem?.ToString() ?? "Juego seleccionado";
                MostrarContenidoFormateado(nombreJuego, contenidoOriginal);
                btnTraducir.Text = "Traducir al español";
                esTraducido = false;
            }
            else
            {
                // Traducir
                btnTraducir.Text = "Traduciendo...";
                btnTraducir.Enabled = false;

                try
                {
                    string textoTraducido = await TraducirTexto(contenidoOriginal);
                    var nombreJuego = lstJuegos.SelectedItem?.ToString() ?? "Juego seleccionado";
                    
                    string contenidoHtml = FormatearContenidoHtml(textoTraducido);
                    string htmlCompleto = CrearHtmlProfesional($"🎮 {nombreJuego} (Español)", contenidoHtml);
                    webContenido.DocumentText = htmlCompleto;
                    
                    btnTraducir.Text = "Ver original";
                    esTraducido = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al traducir: {ex.Message}", "Error de traducción", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                finally
                {
                    btnTraducir.Enabled = true;
                }
            }
        }

        private async Task<string> TraducirTexto(string texto)
        {
            try
            {
                // La API de MyMemory tiene un límite de 500 caracteres por consulta
                // Dividimos el texto en fragmentos más pequeños y los traducimos por partes
                const int limiteCaracteres = 450;
                
                if (texto.Length <= limiteCaracteres)
                {
                    // Texto corto, traducir directamente
                    return await TraducirFragmento(texto);
                }
                else
                {
                    // Texto largo, dividir en fragmentos
                    var fragmentos = DividirTextoEnFragmentos(texto, limiteCaracteres);
                    var resultadoTraducido = new StringBuilder();
                    int fragmentosTraducidos = 0;
                    int totalFragmentos = fragmentos.Count;
                    
                    for (int i = 0; i < fragmentos.Count; i++)
                    {
                        var fragmento = fragmentos[i];
                        
                        try
                        {
                            var textoTraducido = await TraducirFragmento(fragmento);
                            
                            // Verificar si realmente se tradujo (comparar con original)
                            if (textoTraducido != fragmento && !string.IsNullOrWhiteSpace(textoTraducido))
                            {
                                resultadoTraducido.Append(textoTraducido);
                                fragmentosTraducidos++;
                            }
                            else
                            {
                                // Si no se tradujo, intentar con limpieza de caracteres
                                var fragmentoLimpio = LimpiarTextoParaTraduccion(fragmento);
                                if (!string.IsNullOrEmpty(fragmentoLimpio))
                                {
                                    var traduccionLimpia = await TraducirFragmento(fragmentoLimpio);
                                    if (traduccionLimpia != fragmentoLimpio && !string.IsNullOrWhiteSpace(traduccionLimpia))
                                    {
                                        resultadoTraducido.Append(traduccionLimpia);
                                        fragmentosTraducidos++;
                                    }
                                    else
                                    {
                                        resultadoTraducido.Append(fragmento); // Mantener original
                                        fragmentosTraducidos++;
                                    }
                                }
                                else
                                {
                                    resultadoTraducido.Append(fragmento); // Mantener original
                                    fragmentosTraducidos++;
                                }
                            }
                            
                            // Pausa entre fragmentos para no sobrecargar la API
                            if (i < fragmentos.Count - 1)
                                await Task.Delay(200);
                        }
                        catch (Exception ex)
                        {
                            // Si falla un fragmento, mantener el original
                            resultadoTraducido.Append(fragmento);
                            fragmentosTraducidos++;
                        }
                    }
                    
                    string resultado = resultadoTraducido.ToString();
                    
                    // Añadir notas informativas
                    if (fragmentosTraducidos < totalFragmentos)
                    {
                        resultado += "\n\n[Nota: Algunas partes no pudieron ser traducidas y se mantienen en el idioma original.]";
                    }
                    else if (texto.Length > limiteCaracteres * 2)
                    {
                        resultado += "\n\n[Nota: El texto fue muy largo y se tradujo por fragmentos. Puede haber pequeñas inconsistencias.]";
                    }
                    
                    return resultado;
                }
            }
            catch (Exception ex)
            {
                // Si falla completamente, devolver texto con indicador
                return $"[Error en traducción: {ex.Message}]\n\n{texto}";
            }
        }

        private async Task<string> TraducirFragmento(string fragmento)
        {
            try
            {
                string url = $"https://api.mymemory.translated.net/get?q={Uri.EscapeDataString(fragmento)}&langpair=en|es";
                var response = await httpClient.GetStringAsync(url);
                
                using var document = JsonDocument.Parse(response);
                var root = document.RootElement;
                
                if (root.TryGetProperty("responseData", out var responseData) &&
                    responseData.TryGetProperty("translatedText", out var translatedText))
                {
                    var traduccion = translatedText.GetString();
                    
                    // Verificar que la traducción sea válida
                    if (!string.IsNullOrWhiteSpace(traduccion) && traduccion != fragmento)
                    {
                        return traduccion ?? fragmento;
                    }
                }
                
                return fragmento;
            }
            catch
            {
                return fragmento;
            }
        }

        private string LimpiarTextoParaTraduccion(string texto)
        {
            // Eliminar caracteres que pueden causar problemas en la traducción
            var textoLimpio = texto;
            
            // Reemplazar caracteres problemáticos
            textoLimpio = System.Text.RegularExpressions.Regex.Replace(textoLimpio, @"[^\w\s\-\.\,\:\;\!\?\(\)\[\]""'\/\\n\r]", "");
            
            // Eliminar múltiples espacios en blanco
            textoLimpio = System.Text.RegularExpressions.Regex.Replace(textoLimpio, @"\s+", " ").Trim();
            
            // Si después de limpiar queda muy corto, devolver vacío
            if (textoLimpio.Length < 3)
                return string.Empty;
            
            return textoLimpio;
        }

        private List<string> DividirTextoEnFragmentos(string texto, int maxLongitud)
        {
            var fragmentos = new List<string>();
            var lineas = texto.Split(new[] { '\r', '\n' }, StringSplitOptions.None);
            var fragmentoActual = new StringBuilder();
            
            foreach (var linea in lineas)
            {
                // Si la línea por sí sola es más larga que el máximo, hay que partirla
                if (linea.Length > maxLongitud)
                {
                    // Primero añadir el fragmento actual si tiene contenido
                    if (fragmentoActual.Length > 0)
                    {
                        fragmentos.Add(fragmentoActual.ToString());
                        fragmentoActual.Clear();
                    }
                    
                    // Partir la línea larga en trozos
                    var lineaPartida = PartirLineaLarga(linea, maxLongitud);
                    fragmentos.AddRange(lineaPartida);
                }
                else if (fragmentoActual.Length + linea.Length + 1 <= maxLongitud)
                {
                    // La línea cabe en el fragmento actual
                    if (fragmentoActual.Length > 0)
                        fragmentoActual.Append('\n');
                    fragmentoActual.Append(linea);
                }
                else
                {
                    // La línea no cabe, empezar nuevo fragmento
                    if (fragmentoActual.Length > 0)
                    {
                        fragmentos.Add(fragmentoActual.ToString());
                        fragmentoActual.Clear();
                    }
                    fragmentoActual.Append(linea);
                }
            }
            
            // Añadir el último fragmento si tiene contenido
            if (fragmentoActual.Length > 0)
            {
                fragmentos.Add(fragmentoActual.ToString());
            }
            
            return fragmentos;
        }

        private List<string> PartirLineaLarga(string linea, int maxLongitud)
        {
            var fragmentos = new List<string>();
            int inicio = 0;
            
            while (inicio < linea.Length)
            {
                int fin = Math.Min(inicio + maxLongitud, linea.Length);
                string fragmento = linea.Substring(inicio, fin - inicio);
                fragmentos.Add(fragmento);
                inicio = fin;
            }
            
            return fragmentos;
        }

        private string? BuscarArchivoCheatCode(string nombreJuego)
        {
            try
            {
                // Buscar en todas las carpetas de la A a la Z y en la carpeta 0
                var carpetas = Directory.GetDirectories(CheatCodesPath);
                
                foreach (var carpeta in carpetas)
                {
                    var rutaArchivo = Path.Combine(carpeta, $"{nombreJuego}.txt");
                    if (File.Exists(rutaArchivo))
                    {
                        return rutaArchivo;
                    }
                }

                // También buscar en la raíz por si acaso
                var rutaRaiz = Path.Combine(CheatCodesPath, $"{nombreJuego}.txt");
                if (File.Exists(rutaRaiz))
                {
                    return rutaRaiz;
                }

                return null;
            }
            catch (Exception ex)
            {
                // En caso de error, devolver null
                return null;
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
