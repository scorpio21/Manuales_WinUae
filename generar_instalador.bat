@echo off
echo ========================================
echo Generando Instalador Manuales WinUAE
echo ========================================
echo.

echo 1. Limpiando compilaciones anteriores...
dotnet clean
if errorlevel 1 goto error

echo.
echo 2. Publicando aplicacion (single-file)...
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -o publish/win-x64-singlefile
if errorlevel 1 goto error

echo.
echo 3. Generando instalador con Inno Setup...
"C:\Program Files (x86)\Inno Setup 6\ISCC.exe" installer.iss
if errorlevel 1 goto error

echo.
echo ========================================
echo ¡INSTALADOR GENERADO EXITOSAMENTE!
echo ========================================
echo.
echo Busca el archivo en: publish\Manuales_WinUAE_0.1.6_Setup.exe
echo.
pause
exit /b 0

:error
echo.
echo ========================================
echo ERROR EN LA GENERACION DEL INSTALADOR
echo ========================================
echo.
pause
exit /b 1
