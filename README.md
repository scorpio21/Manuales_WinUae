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

### Cheat Codes (Códigos de Trampa)

1. Haz clic en **Cheat Codes → Cheat Codes** para acceder a la base de datos.
2. **Busca juegos** escribiendo parcialmente el nombre en el campo de búsqueda.
3. **Selecciona un juego** para ver sus códigos de trampa formateados profesionalmente.
4. **Haz clic en "Traducir al español"** para traducir el contenido al español.
5. **Usa "Ver original"** para volver al texto en inglés.

### Documentación

1. Haz clic en **Cheat Codes → Documentación**.
2. **Busca documentos** escribiendo parcialmente el nombre en el campo de búsqueda.
3. **Selecciona un documento** para visualizar su contenido.
4. Puedes alternar entre **Inglés** y **Español** desde el selector de idioma.

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

## Estructura de Carpetas de Cheat Codes

La aplicación soporta dos estructuras de carpetas para los cheat codes:

### Nueva Estructura (Recomendada)

```text
Docs/
└── Cheat/
    ├── AMIGACHEATCODES/     # Cheat codes en inglés
    └── AMIGACHEATCODES_ES/ # Cheat codes en español
```

**Comportamiento:**

- La aplicación buscará primero la nueva estructura (`Docs\Cheat\`).
- Si no existe, hará fallback a la estructura antigua (carpetas en la raíz).
- Esto garantiza compatibilidad con instalaciones existentes.

## Estructura de Carpetas de Documentación

La aplicación soporta dos estructuras de carpetas para la documentación:

### Nueva Estructura (Recomendada) - Documentación

```text
Docs/
└── Manuales/
    ├── AMIGADOCS/     # Documentación en inglés
    └── AMIGADOCS_ES/ # Documentación en español
```

### Estructura Antigua (Con Soporte de Compatibilidad) - Documentación

```text
AMIGADOCS/     # Documentación en inglés
AMIGADOCS_ES/ # Documentación en español
```

**Comportamiento:**

- La aplicación buscará primero la nueva estructura (`Docs\Manuales\`).
- Si no existe, hará fallback a la estructura antigua (carpetas en la raíz).

## Descripción

Aplicación de escritorio en C# (.NET 8, WinForms) para mostrar manuales de WinUAE con texto e imágenes, con soporte para varios manuales y edición desde la propia interfaz.

Repositorio GitHub: `scorpio21/Manuales_WinUae`

## Requisitos

- **Windows 10+** (recomendado Windows 11)
- **.NET 8.0 Runtime** (se instala automáticamente con el instalador)
- **2 GB RAM mínimo** (4 GB recomendado)
- **100 MB espacio en disco**

## Compilación

### Prerrequisitos

- Visual Studio 2022 con workload **.NET desktop development**
- SDK .NET 8.0
- Git (para clonar el repositorio)

### Pasos

1. Clonar el repositorio:

```bash
git clone https://github.com/scorpio21/Manuales_WinUae.git
cd Manuales_WinUae/WinFormsManual
```

2. Abrir `WinFormsManual.sln` en Visual Studio **o** situarse en la carpeta del proyecto `WinFormsManual/WinFormsManual`.
3. Compilar:

```bash
dotnet build
```

4. Ejecutar desde la carpeta de salida Debug:

```bash
bin/Debug/net8.0-windows/win-x64/WinFormsManual.exe
```

## Publicación self-contained `win-x64`

Desde `WinFormsManual/WinFormsManual`:

```bash
dotnet publish -c Release -r win-x64 --self-contained true
```

**Salida (publicación):**

`bin/Release/net8.0-windows/win-x64/publish/`

**En esa carpeta deben estar:**

- `WinFormsManual.exe` (icono `logo-multires.ico`).
- Dependencias .NET (self-contained).
- `manuales.json` (se genera al ejecutar la app por primera vez).
- Carpeta `img` copiada con la misma estructura que en desarrollo.
- Carpetas `AMIGACHEATCODES` y `AMIGACHEATCODES_ES` o `Docs\Cheat\`.

## Instalador con Inno Setup

El script `installer.iss` está preparado para:

- Tomar como origen la carpeta `publish/win-x64-singlefile/`.
- Copiar el ejecutable y sus dependencias a `{app}`.
- Copiar la carpeta de imágenes `img` a `{app}\img`.
- Copiar las carpetas de cheat codes (ambas estructuras soportadas).

**Pasos:**

1. Abrir `installer.iss` en Inno Setup.
2. Verificar/ajustar la ruta `Source` si cambiaste la estructura.
3. Compilar el script (**Build → Compile**).
4. Se generará un `Setup.exe` (nombre definido en `OutputBaseFilename`).

## Publicación en GitHub

- Repositorio: `https://github.com/scorpio21/Manuales_WinUae`.
- Rama principal recomendada: `main`.
- Para crear la versión `v0.1.7`:
  1. Realizar `git commit` con el código actual.
  2. Crear el tag:

     ```bash
     git tag v0.1.7
     git push origin main
     git push origin v0.1.7
     ```

  3. En GitHub, crear un **Release** basado en el tag `v0.1.7` y adjuntar los artefactos generados por el workflow.

## Licencia

MIT License - ver archivo LICENSE para más detalles.
