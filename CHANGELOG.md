# Changelog

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
