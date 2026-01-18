# Changelog

## v0.1.8 - Documentación (Docs\\Manuales) + Menú actualizado

- **Nuevo menú**:
  - `Cheat Codes` ahora incluye:
    - `Cheat Codes` (existente)
    - `Documentación` (nuevo)
- **Nueva pantalla de Documentación**:
  - Nuevo formulario `FormDocumentacion`.
  - Lista documentos `.txt` desde `Docs\\Manuales\\AMIGADOCS` / `Docs\\Manuales\\AMIGADOCS_ES`.
  - Selector de idioma (Inglés/Español).
  - Fallback a estructura antigua (`AMIGADOCS` / `AMIGADOCS_ES`) si existe.
- **Instalador**:
  - Incluye `Docs\\Manuales` para distribuir la documentación junto a la app.

## v0.1.7 - Cheat Codes en Docs\\Cheat + Instalador/CI actualizados

- **Reorganización de Cheat Codes**:
  - Soporte para nueva estructura `Docs\\Cheat\\AMIGACHEATCODES` y `Docs\\Cheat\\AMIGACHEATCODES_ES`
  - Fallback automático a la estructura antigua (`AMIGACHEATCODES` / `AMIGACHEATCODES_ES`) para compatibilidad
- **Instalador actualizado**:
  - Incluye la nueva carpeta `Docs\\Cheat` además de las rutas antiguas
- **CI/CD actualizado**:
  - Copia `Docs\\Cheat` al directorio `publish\\win-x64-singlefile` cuando existe

## v0.1.6 - Cheat Codes Profesional con Traducción

- **Nueva interfaz profesional para Cheat Codes**:
  - Diseño moderno con WebBrowser y formato HTML profesional
  - Gradiente de fondo, tarjetas blancas y sombras elegantes
  - Tipografía Segoe UI y paleta de colores coherente
  - Formateo inteligente de contenido (títulos, códigos, tips)
- **Sistema de traducción avanzado**:
  - Traducción al español con API MyMemory
  - División inteligente de textos largos en fragmentos
  - Limpieza de caracteres problemáticos
  - Verificación de traducciones válidas
  - Manejo robusto de errores y fallbacks
- **Mejoras de portabilidad**:
  - Rutas relativas usando `Application.StartupPath`
  - Verificación automática de carpeta AMIGACHEATCODES
  - Mensaje de error claro si no se encuentran los cheat codes
  - Instrucciones detalladas para el usuario
- **Características técnicas**:
  - Búsqueda instantánea mejorada con puntuación
  - Placeholder en campo de búsqueda
  - Botones modernos con estilo flat
  - Toggle entre original/traducción
- **Experiencia de usuario mejorada**:
  - Mensaje de bienvenida informativo
  - Indicadores de estado en traducción
  - Notas informativas sobre traducciones por fragmentos
  - Deshabilitado controles si no hay cheat codes

## v0.1.5 - Visualizador de Cheat Codes

- **Nueva funcionalidad de Cheat Codes**:
  - Nuevo botón "Cheat Codes" en el formulario principal para acceder a la base de datos de códigos de trampa.
  - Nuevo formulario `FormCheatCodes` con interfaz completa para visualizar códigos:
    - Campo de búsqueda para filtrar juegos por nombre en tiempo real.
    - Lista de juegos disponibles cargada desde `AvailableCheatCodes.ini`.
    - Panel de contenido que muestra los códigos de trampa del juego seleccionado.
    - Búsqueda automática de archivos TXT en todas las carpetas (A-Z y 0).
  - **Características técnicas**:
    - Carga de más de 1000 juegos desde la base de datos de FS-UAE.
    - Búsqueda instantánea mientras escribes.
    - Visualización de contenido de archivos TXT con formato preservado.
    - Manejo de errores robusto para archivos no encontrados.
- **Mejoras de interfaz**:
  - Diseño responsive con panel izquierdo para búsqueda/lista y panel derecho para contenido.
  - Botón "Cerrar" para volver al menú principal.
  - Tamaño mínimo de ventana para asegurar buena experiencia de usuario.

## v0.1.4 - Ampliación de imágenes al hacer clic

- **Nueva funcionalidad de ampliación de imágenes**:
  - Al hacer clic en cualquier imagen del manual, se abre una ventana maximizada para visualización ampliada.
  - Nuevo formulario `FormImagenAmpliada` con controles de zoom:
    - Zoom con rueda del ratón
    - Zoom con teclado (+ para ampliar, - para reducir, 0 para restablecer)
    - Límites de zoom entre 0.1x y 10x
    - Indicador del nivel de zoom en el título de la ventana
  - Controles de navegación:
    - Tecla ESC para cerrar la ventana
    - Cursor tipo mano al pasar sobre las imágenes
    - Soporte para arrastre de imagen (preparado para implementación futura)
- **Mejoras de experiencia de usuario**:
  - Cursor cambia a mano (Cursors.Hand) al pasar sobre las imágenes para indicar que son clicables.
  - La ventana de ampliación se abre maximizada para mejor visualización.
  - Fondo negro en la ventana de ampliada para mejor contraste.
- **Actualizaciones de documentación**:
  - README.md actualizado con instrucciones de uso de la nueva funcionalidad.
  - Descripción detallada de los controles de zoom y navegación.

## v0.1.3 - CI/CD y empaquetado mejorados

- Se incluye la carpeta `img/` dentro del repositorio para que las compilaciones en CI encuentren los recursos.
- Se corrigen las rutas de iconos y de imágenes en:
  - `WinFormsManual.csproj` (icono de aplicación).
  - `installer.iss` (copiado de la carpeta `img` al directorio de instalación).
  - Formularios `FormManual` y `FormEditarManual` para que busquen primero `img` junto al `.exe` y, en desarrollo, la carpeta del proyecto.
- Se ajusta el instalador Inno Setup para:
  - Usar el ejecutable `Manuales_WinUAE.exe` generado por `dotnet publish`.
  - Incluir correctamente todas las capturas del manual HDF y los logos.
- Se define un flujo claro para crear una versión **portable** (ZIP) con:
  - `Manuales_WinUAE.exe` en single-file.
  - Carpeta `img` completa empaquetada junto al ejecutable.
- Se añade la guía `Guia_Compilacion_Instalador.txt` con los pasos para compilar, publicar y generar instalador/portable.
- Se actualiza el workflow de GitHub Actions `build-and-release` para:
  - Publicar la aplicación en modo single-file `win-x64`.
  - Construir el instalador con Inno Setup en CI.
  - Aceptar una versión al lanzar el workflow manualmente o al hacer push de un tag `vX.Y.Z`.
  - Crear automáticamente **tag** y **release** en GitHub usando `CHANGELOG.md` como cuerpo y adjuntando ZIP + instalador.

## v0.1.0 - Primera versión

- Aplicación WinForms en C# (.NET 8, self-contained `win-x64`) para manuales de WinUAE.
- Manual incluido:
  - **WinUAE - Creación HDF y particionado de HDD bajo WB 3.1**.
  - 15 pasos con texto en español.
  - 39 capturas de pantalla organizadas en `img/Creación_HDF`.
- Visor de manuales:
  - ComboBox de selección de manual.
  - Lista de pasos.
  - Texto del paso (RichTextBox).
  - Imagen asociada con navegación por varias capturas (`<` / `>` y "< i de N >").
- Editor de manuales:
  - Menú **Manuales → Agregar manual**.
  - Botón **Editar manual**.
  - Posibilidad de asignar varias imágenes por paso desde la carpeta `img`.
- Persistencia:
  - Manuales guardados en `manuales.json` junto al ejecutable.
- Iconos y recursos:
  - Icono multi-resolución `logo-multires.ico` en `img/logo` (usado por el `.exe` y los formularios).
- Scripts y packaging:
  - Script de Inno Setup `DM2BuilderGUI_CS.iss` para generar el instalador `Setup.exe`.
