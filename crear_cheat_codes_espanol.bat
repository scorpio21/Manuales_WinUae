@echo off
echo ========================================
echo Creando Cheat Codes en Español
echo ========================================
echo.

echo 1. Creando carpeta AMIGACHEATCODES_ES...
if not exist "AMIGACHEATCODES_ES" mkdir "AMIGACHEATCODES_ES"

echo 2. Creando subcarpetas...
for %%d in (0 A B C D E F G H I J K L M N O P Q R S T U V W X Y Z) do (
    if not exist "AMIGACHEATCODES_ES\%%d" mkdir "AMIGACHEATCODES_ES\%%d"
)

echo 3. Copiando AvailableCheatCodes.ini...
copy "AMIGACHEATCODES\AvailableCheatCodes.ini" "AMIGACHEATCODES_ES\" >nul

echo 4. Generando plantilla para traducción manual...
echo.
echo Para traducir los cheat codes al español:
echo 1. Abre cada archivo TXT en AMIGACHEATCODES\
echo 2. Traduce el contenido al español
echo 3. Guarda la traducción en AMIGACHEATCODES_ES\ con el mismo nombre
echo.
echo Ejemplo:
echo   AMIGACHEATCODES\0\3D Pool.txt  --traducir-->  AMIGACHEATCODES_ES\0\3D Pool.txt
echo.

echo 5. Creando lista de archivos para traducir...
echo Lista de archivos a traducir:
dir /s /b "AMIGACHEATCODES\*.txt" > archivos_a_traducir.txt
findstr /v "AMIGACHEATCODES_ES" archivos_a_traducir.txt > temp.txt
move /y temp.txt archivos_a_traducir.txt >nul

echo.
echo Total de archivos a traducir:
for /f %%i in ('type archivos_a_traducir.txt ^| find /c /v ""') do echo %%i archivos

echo.
echo ========================================
echo ¡Estructura creada!
echo ========================================
echo.
echo Siguientes pasos:
echo 1. Traduce los archivos manualmente o usa una herramienta de traducción
echo 2. Copia las traducciones a AMIGACHEATCODES_ES\
echo 3. Modifica el programa para cargar desde AMIGACHEATCODES_ES
echo.
pause
