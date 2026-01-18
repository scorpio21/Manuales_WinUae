# Manuales WinUAE - Aplicación WinForms (.NET 8)

## ¿Para qué sirve?

Manuales WinUAE es una aplicación de escritorio diseñada para:

- **Mostrar tutoriales y guías** sobre el uso de WinUAE (emulador de Amiga) de manera organizada y accesible.
- **Visualizar imágenes paso a paso** junto con instrucciones detalladas.
- **Gestionar múltiples manuales** desde una misma interfaz.
- **Editar contenido** directamente desde la aplicación.
- **Distribuirse fácilmente** como aplicación portable o instalable en Windows.

## Guía de uso

### Navegación básica

1. **Selecciona un manual** del menú desplegable superior.
2. **Elige un paso** de la lista de la izquierda para ver su contenido.
3. **Navega entre imágenes** usando los botones `>` y `<` cuando estén disponibles.
4. **Haz clic en cualquier imagen** para verla ampliada en una ventana nueva.
5. **Usa el menú superior** para salir o acceder a más opciones.

### Añadir o editar manuales

1. Haz clic en **Manuales → Agregar manual** para crear uno nuevo.
2. Completa el título y descripción.
3. Añade pasos con sus respectivas imágenes (guarda las imágenes en la carpeta `img/`).
4. Guarda los cambios para que persistan.

### Editar manuales

1. Haz clic en **Manuales → Editar manual** para editar uno existente.
2. Selecciona el manual que deseas editar.
3. Modifica el título y descripción.
4. Modifica los pasos con sus respectivas imágenes (guarda las imágenes en la carpeta `img/`).
5. Guarda los cambios para que persistan.

## Descripción

Aplicación de escritorio en C# (.NET 8, WinForms) para mostrar manuales de WinUAE con texto e imágenes, con soporte para varios manuales y edición desde la propia interfaz.

Repositorio GitHub: `scorpio21/Manuales_WinUae`

## Requisitos

- .NET SDK 8.x
- Windows 64 bits
- Visual Studio 2022 (recomendado) con soporte para desarrollo de escritorio .NET.

## Estructura del proyecto

- `WinFormsManual.sln` – solución principal.
- `WinFormsManual/WinFormsManual.csproj` – proyecto WinForms .NET 8, self-contained `win-x64`.
- `FormManual` – formulario principal:
  - Menú **Archivo → Salir**.
  - Menú **Manuales → Agregar manual**.
  - Botón **Editar manual**.
  - ComboBox de selección de manual.
  - Lista de pasos.
  - Texto descriptivo (RichTextBox).
  - Imagen asociada con navegación (`<`, `>`, indicador `< i de N >`).
  - **Clic en imagen para ampliar** - abre la imagen en ventana maximizada con controles de zoom.
- `FormEditarManual` – formulario para agregar/editar manuales:
  - Título del manual.
  - Alta y edición de pasos (título, descripción, imágenes).
  - Selector de imágenes desde la carpeta `img` (guardar ruta relativa, incluyendo subcarpetas por manual).
- `FormImagenAmpliada` – formulario para visualización ampliada de imágenes:
  - Ventana maximizada con fondo negro.
  - Controles de zoom con rueda del ratón y teclado (+/-/0).
  - Navegación con teclas (ESC para cerrar).
  - Soporte para arrastre de imagen.
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

1. Abrir `WinFormsManual.sln` en Visual Studio **o** situarse en la carpeta del proyecto `WinFormsManual/WinFormsManual`.
2. Compilar:

   ```bash
   dotnet build
   ```

3. Ejecutar desde la carpeta de salida Debug:

   ```bash
   bin/Debug/net8.0-windows/win-x64/WinFormsManual.exe
   ```

## Publicación self-contained `win-x64`

Desde `WinFormsManual/WinFormsManual`:

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

### Navegación básica

1. **Selecciona un manual** del menú desplegable superior.
2. **Elige un paso** de la lista de la izquierda para ver su contenido.
3. **Navega entre imágenes** usando los botones `>` y `<` cuando estén disponibles.
4. **Usa el menú superior** para salir o acceder a más opciones.

### Añadir o editar manuales

1. Haz clic en **Manuales → Agregar manual** para crear uno nuevo.
2. Completa el título y descripción.
3. Añade pasos con sus respectivas imágenes (guarda las imágenes en la carpeta `img/`).
4. Guarda los cambios para que persistan.

### Editar manuales

1. Haz clic en **Manuales → Editar manual** para editar uno existente.
2. Selecciona el manual que deseas editar.
3. Modifica el título y descripción.
4. Modifica los pasos con sus respectivas imágenes (guarda las imágenes en la carpeta `img/`).
5. Guarda los cambios para que persistan.

## Descripción

Aplicación de escritorio en C# (.NET 8, WinForms) para mostrar manuales de WinUAE con texto e imágenes, con soporte para varios manuales y edición desde la propia interfaz.

Repositorio GitHub: `scorpio21/Manuales_WinUae`

## Requisitos

- .NET SDK 8.x
- Windows 64 bits
- Visual Studio 2022 (recomendado) con soporte para desarrollo de escritorio .NET.

## Estructura del proyecto

- `WinFormsManual.sln` – solución principal.
- `WinFormsManual/WinFormsManual.csproj` – proyecto WinForms .NET 8, self-contained `win-x64`.
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
  - Selector de imágenes desde la carpeta `img` (guardar ruta relativa, incluyendo subcarpetas por manual).
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

1. Abrir `WinFormsManual.sln` en Visual Studio **o** situarse en la carpeta del proyecto `WinFormsManual/WinFormsManual`.
2. Compilar:

   ```bash
   dotnet build
   ```

3. Ejecutar desde la carpeta de salida Debug:

   ```bash
   bin/Debug/net8.0-windows/win-x64/WinFormsManual.exe
   ```

## Publicación self-contained `win-x64`

Desde `WinFormsManual/WinFormsManual`:

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
>>>>>>> 97922ff8cfb5d8f478aa5f81a7764a94af18ac34
