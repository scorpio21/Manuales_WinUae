[Setup]
AppName=Manual WinUAE
AppVersion=1.0.0
DefaultDirName={pf64}\Manual WinUAE
DefaultGroupName=Manual WinUAE
OutputBaseFilename=Manual_WinUAE_Setup
Compression=lzma
SolidCompression=yes

[Files]
; Ejecutable y dependencias publicados (ajusta la ruta si cambias la estructura)
Source="bin\\Release\\net8.0-windows\\win-x64\\publish\\*"; DestDir="{app}"; Flags=recursesubdirs
; Copiar carpeta de imágenes si se desea incluir
Source="..\\img\\*"; DestDir="{app}\\img"; Flags=recursesubdirs

[Icons]
Name="{group}\Manual WinUAE"; Filename="{app}\\WinFormsManual.exe"
