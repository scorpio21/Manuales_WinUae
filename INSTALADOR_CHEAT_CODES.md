# Instalador con Cheat Codes - Instrucciones Actualizadas

## Cambios en v0.1.6

El instalador ahora incluye la carpeta `AMIGACHEATCODES` completa con todos los cheat codes.

## Script del Instalador (`installer.iss`)

### Cambios realizados:

1. **Versión actualizada**: `#define MyAppVersion "0.1.6"`
2. **Inclusión de AMIGACHEATCODES**:
   ```iss
   ; Carpeta AMIGACHEATCODES con todas las subcarpetas y archivos de cheat codes
   Source: "AMIGACHEATCODES\\*"; DestDir: "{app}\\AMIGACHEATCODES"; Flags: recursesubdirs createallsubdirs ignoreversion
   ```

## Generar el Instalador

### Método 1: Automático (Recomendado)

Ejecuta el script `generar_instalador.bat`:

```bash
generar_instalador.bat
```

Este script:
1. Limpia compilaciones anteriores
2. Publica la aplicación en modo single-file
3. Genera el instalador con Inno Setup
4. Muestra el resultado en `publish/Manuales_WinUAE_0.1.6_Setup.exe`

### Método 2: Manual

1. **Publicar la aplicación**:
   ```bash
   dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -o publish/win-x64-singlefile
   ```

2. **Generar el instalador**:
   ```bash
   "C:\Program Files (x86)\Inno Setup 6\ISCC.exe" installer.iss
   ```

## Estructura después de la instalación

El instalador creará la siguiente estructura:

```
C:\Program Files\Manuales WinUAE\
├── Manuales_WinUAE.exe          ← Ejecutable principal
├── AMIGACHEATCODES/            ← 🎮 Cheat codes (NUEVO)
│   ├── AvailableCheatCodes.ini
│   ├── 0/
│   │   └── 3D Pool.txt
│   ├── A/
│   │   ├── A-Train.txt
│   │   └── ...
│   ├── B/
│   │   └── ...
│   └── ... (carpetas C-Z)
└── img/                         ← Imágenes del manual
    └── logo/
        └── logo-multires.ico
```

## Verificación

Después de instalar, verifica que:

1. ✅ El ejecutable `Manuales_WinUAE.exe` funciona
2. ✅ Los manuales se muestran correctamente
3. ✅ **Menú → Manuales → Cheat Codes** funciona
4. ✅ La búsqueda de juegos encuentra resultados
5. ✅ La traducción al español funciona
6. ✅ Los cheat codes se muestran con formato profesional

## Solución de Problemas

### Si los Cheat Codes no funcionan:

1. **Verifica la carpeta**: Debe existir `AMIGACHEATCODES/` junto al `.exe`
2. **Reinstala**: Ejecuta el instalador nuevamente
3. **Manual**: Copia la carpeta `AMIGACHEATCODES` manualmente si es necesario

### Mensaje de error esperado:

Si no se encuentra la carpeta, el programa mostrará:
> 📁 Carpeta AMIGACHEATCODES no encontrada  
> Ruta buscada: [ruta donde busca]  
> Solución: Copia la carpeta AMIGACHEATCODES junto al ejecutable

## Portable vs Instalador

### Versión Portable:
- Copia manualmente la carpeta completa
- Incluye `AMIGACHEATCODES/` junto al `.exe`

### Versión Instalador:
- El instalador copia automáticamente todo
- Más fácil para usuarios finales
- Incluye desinstalador

## Requisitos del Sistema

- Windows 10/11 (64-bit)
- .NET 8 Runtime (incluido en el instalador self-contained)
- Conexión a internet (para traducción - opcional)

## Tamaño del Instalador

- **Sin AMIGACHEATCODES**: ~15 MB
- **Con AMIGACHEATCODES**: ~25-30 MB (por los ~1000 archivos TXT)

El aumento de tamaño es necesario para incluir todos los cheat codes y hacer la aplicación completamente funcional sin dependencias externas.
