# Manuales WinUAE - Aplicación WinForms (.NET 8)

Aplicación de escritorio en C# (.NET 8, WinForms) para mostrar manuales de WinUAE con texto e imágenes, con soporte para varios manuales y edición desde la propia interfaz.

Repositorio GitHub: `scorpio21/Manuales_WinUae`

## Requisitos

- .NET SDK 8.x
- Windows 64 bits
- Visual Studio 2022 (recomendado) con soporte para desarrollo de escritorio .NET.

## Estructura del proyecto

- `WinFormsManual.csproj` – proyecto WinForms .NET 8, self-contained `win-x64`.
- `Program.cs` – punto de entrada, abre directamente `FormManual`.
- `FormManual` – formulario principal:
  - Menú **Archivo → Salir**.
  - Menú **Manuales → Agregar manual**.
  - Botón **Editar manual**.
  - ComboBox de selección de manual.
  - Lista de pasos.
  - Texto descriptivo (RichTextBox).
  - Imagen asociada con navegación (`<`, `>`, indicador `< i de N >`).
- `FormEditarManual` – formulario para agregar/editar manuales:
  - Título del manual.
  - Alta y edición de pasos (título, descripción, imágenes).
  - Selector de imágenes desde la carpeta `img` (ruta relativa, incluyendo subcarpetas por manual).
- `Manual.cs` – modelos y datos:
  - `Manual`, `PasoManual` (con lista de imágenes por paso).
  - `ManualesPredefinidos` con el manual:
    - **WinUAE - Creación HDF y particionado de HDD bajo WB 3.1**.
  - `RepositorioManuales` – carga/guarda `manuales.json` junto al ejecutable.
- `DM2BuilderGUI_CS.iss` – script de Inno Setup para generar el instalador.
- `CHANGELOG.md` – historial de versiones.
- Carpeta `img` (a nivel de `d:\xampp\htdocs\Manual_Winuae`):
  - `img/Creación_HDF` – capturas del manual de HDF (paso1.png .. paso39.png).
  - `img/logo` – iconos (`logo-multires.ico`, PNG varios).

## Manuales incluidos

Versión `v0.1.0` incluye:

- **WinUAE - Creación HDF y particionado de HDD bajo WB 3.1**
  - 15 pasos con texto en español.
  - 39 capturas de pantalla.
  - Navegación por varias imágenes dentro de un mismo paso.

## Compilación y ejecución (Debug)

Desde la carpeta del proyecto `d:\xampp\htdocs\Manual_Winuae\WinFormsManual`:

```bash
 dotnet build
 bin/Debug/net8.0-windows/win-x64/WinFormsManual.exe
```

## Publicación self-contained `win-x64`

```bash
dotnet publish -c Release -r win-x64 --self-contained true
```

Salida (publicación):

`bin/Release/net8.0-windows/win-x64/publish/`

En esa carpeta deben estar:

- `WinFormsManual.exe` (icono `logo-multires.ico`).
- Dependencias .NET (self-contained).
- `manuales.json` (se genera al ejecutar la app por primera vez).
- Carpeta `img` copiada con la misma estructura que en desarrollo.

## Instalador con Inno Setup

El script `DM2BuilderGUI_CS.iss` está preparado para:

- Tomar como origen la carpeta `bin/Release/net8.0-windows/win-x64/publish/`.
- Copiar el ejecutable y sus dependencias a `{app}`.
- Copiar la carpeta de imágenes `img` a `{app}\img`.

Pasos:

1. Abrir `DM2BuilderGUI_CS.iss` en Inno Setup.
2. Verificar/ajustar la ruta `Source` si cambiaste la estructura.
3. Compilar el script (**Build → Compile**).
4. Se generará un `Setup.exe` (nombre definido en `OutputBaseFilename`).

## Publicación en GitHub

- Repositorio: `https://github.com/scorpio21/Manuales_WinUae`.
- Rama principal recomendada: `main`.
- Para crear la versión `v0.1.0`:
  1. Realizar `git commit` con el código actual.
  2. Crear el tag:

     ```bash
     git tag v0.1.0
     git push origin main
     git push origin v0.1.0
     ```

  3. En GitHub, crear un **Release** basado en el tag `v0.1.0` y adjuntar, si se desea, el `Setup.exe` generado con Inno Setup.
