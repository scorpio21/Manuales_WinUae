; Inno Setup script for Manuales WinUAE
; Requires Inno Setup 6.x

#define MyAppName "Manuales WinUAE"
#ifndef MyAppVersion
 #define MyAppVersion "0.1.6"
#endif
#define MyAppPublisher "scorpio21"
#define MyAppExeName "Manuales_WinUAE.exe"

; Carpeta de publicación del ejecutable single-file
#define PublishDir "publish\\win-x64-singlefile"

[Setup]
AppId={{D5B36F9E-3E9B-4B1B-9F33-8F4C9C0F9D21}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={commonpf64}\{#MyAppName}
DefaultGroupName={#MyAppName}
DisableDirPage=no
DisableProgramGroupPage=no
OutputBaseFilename=Manuales_WinUAE_{#MyAppVersion}_Setup
OutputDir=.\\publish
Compression=lzma
SolidCompression=yes
WizardStyle=modern
ArchitecturesInstallIn64BitMode=x64os

[Languages]
Name: "spanish"; MessagesFile: "compiler:Languages\\Spanish.isl"

[Tasks]
Name: "desktopicon"; Description: "Crear icono en el escritorio"; GroupDescription: "Tareas adicionales:"; Flags: unchecked

[Files]
; Archivos de la aplicación (single-file publish)
Source: "{#PublishDir}\\*"; DestDir: "{app}"; Flags: recursesubdirs ignoreversion

; Carpeta img con todas sus subcarpetas y archivos (dentro del repositorio/proyecto)
Source: "img\\*"; DestDir: "{app}\\img"; Flags: recursesubdirs createallsubdirs ignoreversion

; Carpeta AMIGACHEATCODES con todas las subcarpetas y archivos de cheat codes
Source: "AMIGACHEATCODES\\*"; DestDir: "{app}\\AMIGACHEATCODES"; Flags: recursesubdirs createallsubdirs ignoreversion

; Carpeta AMIGACHEATCODES_ES con todas las subcarpetas y archivos de cheat codes en español
Source: "AMIGACHEATCODES_ES\\*"; DestDir: "{app}\\AMIGACHEATCODES_ES"; Flags: recursesubdirs createallsubdirs ignoreversion

[Icons]
Name: "{group}\\{#MyAppName}"; Filename: "{app}\\{#MyAppExeName}"
Name: "{group}\\Desinstalar {#MyAppName}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\\{#MyAppName}"; Filename: "{app}\\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\\{#MyAppExeName}"; Description: "Ejecutar {#MyAppName}"; Flags: nowait postinstall skipifsilent
