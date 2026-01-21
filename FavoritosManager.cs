using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;
using System.Reflection;

namespace WinFormsManual
{
    public class FavoritosManager
    {
        private static readonly string FavoritosFilePath = Path.Combine(
            GetApplicationPath(),
            "json",
            "favoritos.json"
        );

        private static string GetApplicationPath()
        {
            // En single-file apps, Assembly.Location no funciona
            // Usar AppContext.BaseDirectory que es más fiable
            var basePath = AppContext.BaseDirectory;
            
            if (!string.IsNullOrEmpty(basePath) && Directory.Exists(basePath))
            {
                return basePath;
            }
            
            // Fallback a otras opciones
            var paths = new[]
            {
                AppDomain.CurrentDomain.BaseDirectory,
                Environment.CurrentDirectory,
                Application.StartupPath
            };

            foreach (var path in paths)
            {
                if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
                {
                    return path;
                }
            }

            // Último recurso
            return ".";
        }

        private static HashSet<string>? _favoritosCheatCodes;
        private static HashSet<string>? _favoritosManuales;

        static FavoritosManager()
        {
            CargarFavoritos();
        }

        private static void CargarFavoritos()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"Intentando cargar favoritos desde: {FavoritosFilePath}");
                
                Directory.CreateDirectory(Path.GetDirectoryName(FavoritosFilePath)!);

                if (File.Exists(FavoritosFilePath))
                {
                    var json = File.ReadAllText(FavoritosFilePath);
                    System.Diagnostics.Debug.WriteLine($"JSON leído: {json}");
                    
                    var datos = JsonSerializer.Deserialize<FavoritosData>(json);
                    System.Diagnostics.Debug.WriteLine($"Datos deserializados - CheatCodes: {datos?.CheatCodes?.Count ?? 0}, Manuales: {datos?.Manuales?.Count ?? 0}");
                    
                    _favoritosCheatCodes = datos?.CheatCodes ?? new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                    _favoritosManuales = datos?.Manuales ?? new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                    
                    System.Diagnostics.Debug.WriteLine($"Favoritos cargados - CheatCodes: {_favoritosCheatCodes.Count}, Manuales: {_favoritosManuales.Count}");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Archivo de favoritos no existe, creando nuevos");
                    _favoritosCheatCodes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                    _favoritosManuales = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error cargando favoritos: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"StackTrace: {ex.StackTrace}");
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
                    CheatCodes = _favoritosCheatCodes ?? new HashSet<string>(StringComparer.OrdinalIgnoreCase),
                    Manuales = _favoritosManuales ?? new HashSet<string>(StringComparer.OrdinalIgnoreCase)
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
        public HashSet<string> CheatCodes { get; set; } = new();
        public HashSet<string> Manuales { get; set; } = new();
    }
}