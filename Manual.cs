using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace WinFormsManual
{
    public class PasoManual
    {
        public string TituloPaso { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;

        // Imagen principal (para compatibilidad con datos antiguos)
        public string NombreArchivoImagen { get; set; } = string.Empty;

        // Lista de imágenes asociadas al paso (subcarpetas incluidas)
        public List<string> Imagenes { get; set; } = new();
    }

    public class Manual
    {
        public string Titulo { get; set; } = string.Empty;
        public List<PasoManual> Pasos { get; set; } = new();
    }

    public static class ManualesPredefinidos
    {
        public static Manual CrearManualWinUaeCreacionHdf()
        {
            var manual = new Manual
            {
                Titulo = "WinUAE - Creación HDF y particionado de HDD bajo WB 3.1",
                Pasos = new List<PasoManual>()
            };

            // Paso 1: configuración básica del A1200 en WinUAE (6 imágenes)
            manual.Pasos.Add(new PasoManual
            {
                TituloPaso = "Configurar A1200 básico en WinUAE",
                Descripcion = "Lo primero es configurar un A1200 básico en WinUAE.",
                NombreArchivoImagen = "Creación_HDF" + Path.DirectorySeparatorChar + "paso1.png",
                Imagenes = new List<string>
                {
                    "Creación_HDF" + Path.DirectorySeparatorChar + "paso1.png",
                    "Creación_HDF" + Path.DirectorySeparatorChar + "paso2.png",
                    "Creación_HDF" + Path.DirectorySeparatorChar + "paso3.png",
                    "Creación_HDF" + Path.DirectorySeparatorChar + "paso4.png",
                    "Creación_HDF" + Path.DirectorySeparatorChar + "paso5.png",
                    "Creación_HDF" + Path.DirectorySeparatorChar + "paso6.png"
                }
            });

            // Paso 2: creación del HDF de 3GB (2 imágenes)
            manual.Pasos.Add(new PasoManual
            {
                TituloPaso = "Crear disco duro virtual HDF de 3GB",
                Descripcion = "Tenemos que crear el disco duro virtual ... un archivo HDF de 3GB (3072MB).",
                NombreArchivoImagen = "Creación_HDF" + Path.DirectorySeparatorChar + "paso7.png",
                Imagenes = new List<string>
                {
                    "Creación_HDF" + Path.DirectorySeparatorChar + "paso7.png",
                    "Creación_HDF" + Path.DirectorySeparatorChar + "paso8.png"
                }
            });

            // Paso 3: poner el HDF en modo RDB (1 imagen)
            manual.Pasos.Add(new PasoManual
            {
                TituloPaso = "Poner el HDF en modo RDB",
                Descripcion = "Una vez creado el disco duro, hay que poner el HDF en modo RDB ...",
                NombreArchivoImagen = "Creación_HDF" + Path.DirectorySeparatorChar + "paso9.png",
                Imagenes = new List<string>
                {
                    "Creación_HDF" + Path.DirectorySeparatorChar + "paso9.png"
                }
            });

            // Paso 4: comprobar device uaehdf.device (1 imagen)
            manual.Pasos.Add(new PasoManual
            {
                TituloPaso = "Comprobar device uaehdf.device",
                Descripcion = "Fijaos que como device está seleccionado el uaehdf.device.",
                NombreArchivoImagen = "Creación_HDF" + Path.DirectorySeparatorChar + "paso10.png",
                Imagenes = new List<string>
                {
                    "Creación_HDF" + Path.DirectorySeparatorChar + "paso10.png"
                }
            });

            // Paso 5: disco duro virtual listo (1 imagen)
            manual.Pasos.Add(new PasoManual
            {
                TituloPaso = "Disco duro virtual listo",
                Descripcion = "Ya está nuestro disco duro virtual listo ....",
                NombreArchivoImagen = "Creación_HDF" + Path.DirectorySeparatorChar + "paso11.png",
                Imagenes = new List<string>
                {
                    "Creación_HDF" + Path.DirectorySeparatorChar + "paso11.png"
                }
            });

            // Paso 6: configurar la controladora de discos (1 imagen)
            manual.Pasos.Add(new PasoManual
            {
                TituloPaso = "Configurar controladora de discos",
                Descripcion = "Configuramos la controladora de discos ....",
                NombreArchivoImagen = "Creación_HDF" + Path.DirectorySeparatorChar + "paso12.png",
                Imagenes = new List<string>
                {
                    "Creación_HDF" + Path.DirectorySeparatorChar + "paso12.png"
                }
            });

            // Paso 7: guardar y recargar configuración (2 imágenes)
            manual.Pasos.Add(new PasoManual
            {
                TituloPaso = "Guardar y recargar configuración",
                Descripcion = "Guardamos la configuración y la volvemos a cargar.",
                NombreArchivoImagen = "Creación_HDF" + Path.DirectorySeparatorChar + "paso13.png",
                Imagenes = new List<string>
                {
                    "Creación_HDF" + Path.DirectorySeparatorChar + "paso13.png",
                    "Creación_HDF" + Path.DirectorySeparatorChar + "paso14.png"
                }
            });

            // Paso 8: insertar disquetes necesarios (1 imagen)
            manual.Pasos.Add(new PasoManual
            {
                TituloPaso = "Insertar disquetes necesarios",
                Descripcion = "Ahora metemos unos cuantos disquetes ... el HDInstTools 6.9 y mi disco FileSystems los tenéis en el adjunto, el WB lo ponéis vosotros.",
                NombreArchivoImagen = "Creación_HDF" + Path.DirectorySeparatorChar + "paso15.png",
                Imagenes = new List<string>
                {
                    "Creación_HDF" + Path.DirectorySeparatorChar + "paso15.png"
                }
            });

            // Paso 9: iniciar el sistema desde disquete (1 imagen)
            manual.Pasos.Add(new PasoManual
            {
                TituloPaso = "Iniciar sistema desde disquete",
                Descripcion = "Iniciamos ....",
                NombreArchivoImagen = "Creación_HDF" + Path.DirectorySeparatorChar + "paso16.png",
                Imagenes = new List<string>
                {
                    "Creación_HDF" + Path.DirectorySeparatorChar + "paso16.png"
                }
            });

            // Paso 10: montar FileSystems.adf (1 imagen)
            manual.Pasos.Add(new PasoManual
            {
                TituloPaso = "Montar disquete FileSystems",
                Descripcion = "Mi FileSystems.adf aparece como 'ADF_merlinkv_Tools' pero le ponéis el nombre que os parezca ... da igual ....",
                NombreArchivoImagen = "Creación_HDF" + Path.DirectorySeparatorChar + "paso17.png",
                Imagenes = new List<string>
                {
                    "Creación_HDF" + Path.DirectorySeparatorChar + "paso17.png"
                }
            });

            // Paso 11: secuencia de imágenes de HDInstTools, particiones, etc. (17 imágenes)
            manual.Pasos.Add(new PasoManual
            {
                TituloPaso = "Crear y particionar el HDF con las herramientas",
                Descripcion = "Bueno ... ahora 'al loro' porque lo que sigue son todo imágenes .....",
                NombreArchivoImagen = "Creación_HDF" + Path.DirectorySeparatorChar + "paso18.png",
                Imagenes = new List<string>
                {
                    "Creación_HDF" + Path.DirectorySeparatorChar + "paso18.png",
                    "Creación_HDF" + Path.DirectorySeparatorChar + "paso19.png",
                    "Creación_HDF" + Path.DirectorySeparatorChar + "paso20.png",
                    "Creación_HDF" + Path.DirectorySeparatorChar + "paso21.png",
                    "Creación_HDF" + Path.DirectorySeparatorChar + "paso22.png",
                    "Creación_HDF" + Path.DirectorySeparatorChar + "paso23.png",
                    "Creación_HDF" + Path.DirectorySeparatorChar + "paso24.png",
                    "Creación_HDF" + Path.DirectorySeparatorChar + "paso25.png",
                    "Creación_HDF" + Path.DirectorySeparatorChar + "paso26.png",
                    "Creación_HDF" + Path.DirectorySeparatorChar + "paso27.png",
                    "Creación_HDF" + Path.DirectorySeparatorChar + "paso28.png",
                    "Creación_HDF" + Path.DirectorySeparatorChar + "paso29.png",
                    "Creación_HDF" + Path.DirectorySeparatorChar + "paso30.png",
                    "Creación_HDF" + Path.DirectorySeparatorChar + "paso31.png",
                    "Creación_HDF" + Path.DirectorySeparatorChar + "paso32.png",
                    "Creación_HDF" + Path.DirectorySeparatorChar + "paso33.png",
                    "Creación_HDF" + Path.DirectorySeparatorChar + "paso34.png"
                }
            });

            // Paso 12: formatear partición DH0 (1 imagen)
            manual.Pasos.Add(new PasoManual
            {
                TituloPaso = "Formatear partición DH0 de 100MB",
                Descripcion = "Se reinicia el sistema y formateamos la partición DH0 de 100MB ....",
                NombreArchivoImagen = "Creación_HDF" + Path.DirectorySeparatorChar + "paso35.png",
                Imagenes = new List<string>
                {
                    "Creación_HDF" + Path.DirectorySeparatorChar + "paso35.png"
                }
            });

            // Paso 13: formatear partición DH1 (2 imágenes)
            manual.Pasos.Add(new PasoManual
            {
                TituloPaso = "Formatear partición DH1",
                Descripcion = "Hacemos lo mismo con DH1: que, como podemos ver, tiene 2971MB de capacidad.",
                NombreArchivoImagen = "Creación_HDF" + Path.DirectorySeparatorChar + "paso36.png",
                Imagenes = new List<string>
                {
                    "Creación_HDF" + Path.DirectorySeparatorChar + "paso36.png",
                    "Creación_HDF" + Path.DirectorySeparatorChar + "paso37.png"
                }
            });

            // Paso 14: comprobar capacidad de DH1 (1 imagen)
            manual.Pasos.Add(new PasoManual
            {
                TituloPaso = "Comprobar capacidad de DH1",
                Descripcion = "Comprobamos que se está usando toda la capacidad de DH1:",
                NombreArchivoImagen = "Creación_HDF" + Path.DirectorySeparatorChar + "paso38.png",
                Imagenes = new List<string>
                {
                    "Creación_HDF" + Path.DirectorySeparatorChar + "paso38.png"
                }
            });

            // Paso 15: finalización y uso de FileSystems (1 imagen)
            manual.Pasos.Add(new PasoManual
            {
                TituloPaso = "Finalizar y preparar FileSystems",
                Descripcion = "Y bueno, ya está .... he renombrado uno de mis adf's como FileSystems ....",
                NombreArchivoImagen = "Creación_HDF" + Path.DirectorySeparatorChar + "paso39.png",
                Imagenes = new List<string>
                {
                    "Creación_HDF" + Path.DirectorySeparatorChar + "paso39.png"
                }
            });

            return manual;
        }
    }

    public static class RepositorioManuales
    {
        private const string NombreArchivo = "manuales.json";

        private static string ObtenerRutaArchivo()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, NombreArchivo);
        }

        public static List<Manual> CargarManuales()
        {
            var ruta = ObtenerRutaArchivo();
            if (!File.Exists(ruta))
            {
                return new List<Manual>();
            }

            try
            {
                var json = File.ReadAllText(ruta);
                var opciones = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var lista = JsonSerializer.Deserialize<List<Manual>>(json, opciones);
                return lista ?? new List<Manual>();
            }
            catch
            {
                return new List<Manual>();
            }
        }

        public static void GuardarManuales(IEnumerable<Manual> manuales)
        {
            var ruta = ObtenerRutaArchivo();

            var opciones = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(manuales, opciones);
            File.WriteAllText(ruta, json);
        }
    }
}
