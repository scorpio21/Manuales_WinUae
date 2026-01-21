using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;
using System.Reflection;
using System.Linq;

namespace WinFormsManual
{
    public class FavoritosManager
    {
        private static string FavoritosFilePath => Path.Combine(
            GetApplicationPath(),
            "json",
            "favoritos.json"
        );

        private static string GetApplicationPath()
        {
            // Detectar si estamos en modo desarrollo (bin/Debug) o en modo instalado
            var currentDir = AppContext.BaseDirectory;
            
            // Si estamos en bin/Debug, devolver la carpeta del proyecto
            if (currentDir.Contains("\\bin\\") || currentDir.Contains("/bin/"))
            {
                // Subir desde bin/Debug/net8.0-windows/win-x64 hasta la raíz del proyecto
                var projectDir = Path.GetFullPath(Path.Combine(currentDir, "..", "..", "..", ".."));
                return projectDir;
            }
            
            // Si no, estamos en modo instalado, usar la carpeta actual
            return currentDir;
        }

        // Método para inicializar después de que la aplicación esté completamente cargada
        public static void Inicializar()
        {
            try
            {
                CargarFavoritos();
                // Notificar que los favoritos han sido cargados
                FavoritosCargados?.Invoke(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                // En caso de error, inicializar con colecciones vacías
                _favoritosCheatCodes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                _favoritosManuales = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            }
        }

        // Evento para notificar cuando los favoritos se han cargado
        public static event EventHandler? FavoritosCargados;

        private static HashSet<string>? _favoritosCheatCodes;
        private static HashSet<string>? _favoritosManuales;
        private static bool _inicializado = false;

        private static void CargarFavoritos()
        {
            try
            {
                var appPath = GetApplicationPath();
                var favoritosPath = Path.Combine(appPath, "json", "favoritos.json");
                
                Directory.CreateDirectory(Path.GetDirectoryName(favoritosPath)!);

                if (File.Exists(favoritosPath))
                {
                    var json = File.ReadAllText(favoritosPath);
                    
                    var datos = JsonSerializer.Deserialize<FavoritosData>(json);
                    
                    _favoritosCheatCodes = datos?.CheatCodes != null ? new HashSet<string>(datos.CheatCodes, StringComparer.OrdinalIgnoreCase) : new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                    _favoritosManuales = datos?.Manuales != null ? new HashSet<string>(datos.Manuales, StringComparer.OrdinalIgnoreCase) : new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                }
                else
                {
                    _favoritosCheatCodes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                    _favoritosManuales = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                }
            }
            catch (Exception ex)
            {
                _favoritosCheatCodes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                _favoritosManuales = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            }
        }

        private static void GuardarFavoritos()
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(FavoritosFilePath)!);

                var datos = new FavoritosData
                {
                    CheatCodes = _favoritosCheatCodes?.ToList() ?? new List<string>(),
                    Manuales = _favoritosManuales?.ToList() ?? new List<string>()
                };

                var opciones = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var json = JsonSerializer.Serialize(datos, opciones);
                File.WriteAllText(FavoritosFilePath, json);
                
                // Debug: Verificar que se guardó
                System.Diagnostics.Debug.WriteLine($"Guardados favoritos en: {FavoritosFilePath}");
                System.Diagnostics.Debug.WriteLine($"JSON: {json}");
            }
            catch (Exception ex)
            {
                // Mostrar error en lugar de silenciarlo para depuración
                System.Diagnostics.Debug.WriteLine($"Error guardando favoritos: {ex.Message}");
                MessageBox.Show($"Error al guardar favoritos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Métodos para Cheat Codes
        public static bool EsFavoritoCheatCode(string nombreJuego)
        {
            return _favoritosCheatCodes?.Contains(nombreJuego) ?? false;
        }

        public static void ToggleFavoritoCheatCode(string nombreJuego)
        {
            if (_favoritosCheatCodes == null) return;

            if (_favoritosCheatCodes.Contains(nombreJuego))
            {
                _favoritosCheatCodes.Remove(nombreJuego);
            }
            else
            {
                _favoritosCheatCodes.Add(nombreJuego);
            }

            GuardarFavoritos();
        }

        public static HashSet<string> GetFavoritosCheatCodes()
        {
            return _favoritosCheatCodes ?? new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        }

        // Métodos para Manuales
        public static bool EsFavoritoManual(string nombreManual)
        {
            return _favoritosManuales?.Contains(nombreManual) ?? false;
        }

        public static void ToggleFavoritoManual(string nombreManual)
        {
            if (_favoritosManuales == null) return;

            if (_favoritosManuales.Contains(nombreManual))
            {
                _favoritosManuales.Remove(nombreManual);
            }
            else
            {
                _favoritosManuales.Add(nombreManual);
            }

            GuardarFavoritos();
        }

        public static HashSet<string> GetFavoritosManuales()
        {
            return _favoritosManuales ?? new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        }

        // Método para forzar recarga (para depuración)
        public static void ForzarRecarga()
        {
            System.Diagnostics.Debug.WriteLine("=== FORZANDO RECARGA MANUAL ===");
            CargarFavoritos();
        }

        // Método para obtener el texto con estrella
        public static string GetTextoConEstrella(string nombre, bool esFavorito)
        {
            return esFavorito ? $"★ {nombre}" : $"  {nombre}";
        }

        // Método para extraer el nombre sin la estrella
        public static string GetNombreSinEstrella(string textoConEstrella)
        {
            return textoConEstrella.StartsWith("★ ") ? textoConEstrella.Substring(2) :
                   textoConEstrella.StartsWith("  ") ? textoConEstrella.Substring(2) :
                   textoConEstrella;
        }
    }

    internal class FavoritosData
    {
        [JsonPropertyName("cheatCodes")]
        public List<string> CheatCodes { get; set; } = new();
        
        [JsonPropertyName("manuales")]
        public List<string> Manuales { get; set; } = new();
    }
}