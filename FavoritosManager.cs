using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WinFormsManual
{
    public class FavoritosManager
    {
        private static readonly string FavoritosFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "Manuales_WinUAE",
            "favoritos.json"
        );

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
                Directory.CreateDirectory(Path.GetDirectoryName(FavoritosFilePath)!);

                if (File.Exists(FavoritosFilePath))
                {
                    var json = File.ReadAllText(FavoritosFilePath);
                    var datos = JsonSerializer.Deserialize<FavoritosData>(json);
                    
                    _favoritosCheatCodes = datos?.CheatCodes ?? new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                    _favoritosManuales = datos?.Manuales ?? new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                }
                else
                {
                    _favoritosCheatCodes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                    _favoritosManuales = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                }
            }
            catch
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
            }
            catch
            {
                // Silenciar errores de guardado para no interrumpir la experiencia del usuario
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
        [JsonConverter(typeof(StringCaseInsensitiveHashSetConverter))]
        public HashSet<string> CheatCodes { get; set; } = new();

        [JsonConverter(typeof(StringCaseInsensitiveHashSetConverter))]
        public HashSet<string> Manuales { get; set; } = new();
    }

    // Convertidor personalizado para HashSet<string> con case insensitive
    public class StringCaseInsensitiveHashSetConverter : JsonConverter<HashSet<string>>
    {
        public override HashSet<string> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartArray)
                return new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            var set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
            {
                if (reader.TokenType == JsonTokenType.String)
                {
                    set.Add(reader.GetString() ?? string.Empty);
                }
            }
            return set;
        }

        public override void Write(Utf8JsonWriter writer, HashSet<string> value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            foreach (var item in value)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();
        }
    }
}