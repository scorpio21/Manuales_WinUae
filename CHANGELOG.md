# Changelog

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
