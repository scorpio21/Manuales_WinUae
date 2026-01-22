#!/usr/bin/env python3
"""
Script para copiar TODOS los juegos de cheat codes al español (sin traducción automática)
Copia todos los archivos y genera una lista para traducción manual
"""

import os
import shutil
from pathlib import Path

# Configuración
SCRIPT_DIR = Path(__file__).parent
ORIGEN_DIR = SCRIPT_DIR / "AMIGACHEATCODES"
DESTINO_DIR = SCRIPT_DIR / "AMIGACHEATCODES_ES"

# Juegos ya traducidos (excluir)
JUEGOS_YA_TRADUCIDOS = {
    "3D Pool",
    "4-Get-It", 
    "A-Train",
    "Aaargh!",
    "Bubble Bobble"
}

def obtener_lista_juegos():
    """Obtener lista de juegos desde AvailableCheatCodes.ini"""
    ini_file = ORIGEN_DIR / "AvailableCheatCodes.ini"
    
    if not ini_file.exists():
        print(f"❌ No existe: {ini_file}")
        return []
    
    juegos = []
    with open(ini_file, 'r', encoding='utf-8', errors='ignore') as f:
        for linea in f:
            linea = linea.strip()
            if (linea and 
                not linea.startswith("COPY/PASTE") and 
                not linea.startswith("NOTE:") and 
                not linea.startswith("AVAILABLE") and 
                not linea.startswith("from") and 
                not linea.startswith("to be")):
                juegos.append(linea)
    
    return juegos

def copiar_archivo(origen_path, destino_path, nombre_juego):
    """Copiar un archivo individual"""
    try:
        print(f"📋 Copiando: {nombre_juego}")
        
        with open(origen_path, 'r', encoding='utf-8', errors='ignore') as f:
            contenido = f.read()
        
        # Guardar en destino
        with open(destino_path, 'w', encoding='utf-8') as f:
            f.write(contenido)
        
        print(f"✅ Copiado: {nombre_juego}")
        return True
        
    except Exception as e:
        print(f"❌ Error copiando {nombre_juego}: {e}")
        return False

def main():
    """Función principal"""
    print("📁 COPIADOR COMPLETO DE CHEAT CODES")
    print("=" * 50)
    
    # Verificar carpetas
    if not ORIGEN_DIR.exists():
        print(f"❌ No existe carpeta origen: {ORIGEN_DIR}")
        return
    
    DESTINO_DIR.mkdir(exist_ok=True)
    
    # Crear subcarpetas
    for carpeta in ['0'] + [chr(ord('A') + i) for i in range(26)]:
        (DESTINO_DIR / carpeta).mkdir(exist_ok=True)
    
    # Obtener lista de juegos
    juegos = obtener_lista_juegos()
    print(f"📁 Total juegos encontrados: {len(juegos)}")
    
    # Filtrar juegos ya traducidos
    juegos_por_copiar = [j for j in juegos if j not in JUEGOS_YA_TRADUCIDOS]
    print(f"🎯 Juegos por copiar: {len(juegos_por_copiar)}")
    print(f"✅ Juegos ya traducidos: {len(JUEGOS_YA_TRADUCIDOS)}")
    
    if not juegos_por_copiar:
        print("🎉 ¡Todos los juegos ya están copiados!")
        return
    
    # Copiar archivos
    copiados = 0
    fallidos = 0
    
    for i, nombre_juego in enumerate(juegos_por_copiar, 1):
        print(f"\n[{i}/{len(juegos_por_copiar)}] Procesando: {nombre_juego}")
        
        # Buscar archivo original
        archivo_origen = None
        for carpeta in ['0'] + [chr(ord('A') + i) for i in range(26)]:
            posible_origen = ORIGEN_DIR / carpeta / f"{nombre_juego}.txt"
            if posible_origen.exists():
                archivo_origen = posible_origen
                break
        
        if not archivo_origen:
            print(f"⚠️ No encontrado: {nombre_juego}.txt")
            fallidos += 1
            continue
        
        # Determinar carpeta destino
        primera_letra = nombre_juego[0].upper()
        if primera_letra.isdigit():
            carpeta_destino = '0'
        else:
            carpeta_destino = primera_letra
        
        archivo_destino = DESTINO_DIR / carpeta_destino / f"{nombre_juego}.txt"
        
        # Copiar archivo
        if copiar_archivo(archivo_origen, archivo_destino, nombre_juego):
            copiados += 1
        else:
            fallidos += 1
    
    # Actualizar AvailableCheatCodes.ini con TODOS los juegos
    print("\n📋 Actualizando AvailableCheatCodes.ini con TODOS los juegos...")
    with open(DESTINO_DIR / "AvailableCheatCodes.ini", 'w', encoding='utf-8') as f:
        f.write("COPY/PASTE with right mouse click \n")
        f.write("from the AvailableCheatCodes list the cheat code \n")
        f.write("to be open to the commandline prompt. \n")
        f.write("NOTE: spaces after the copied name can affect the cheat codes display.\n")
        f.write(" \n")
        f.write("AVAILABLE CHEAT CODES (add more cheat codes anytime on the folders with letter) \n")
        f.write(" \n")
        
        for juego in juegos:
            f.write(f"{juego}\n")
    
    # Generar lista de juegos para traducir
    print("\n📝 Generando lista de juegos para traducir manualmente...")
    with open(DESTINO_DIR / "JUEGOS_POR_TRADUCIR.txt", 'w', encoding='utf-8') as f:
        f.write("JUEGOS QUE NECESITAN TRADUCCIÓN MANUAL:\n")
        f.write("=" * 40 + "\n\n")
        
        for juego in juegos_por_copiar:
            f.write(f"- {juego}\n")
        
        f.write(f"\nTotal: {len(juegos_por_copiar)} juegos\n")
        f.write(f"\nJuegos ya traducidos: {len(JUEGOS_YA_TRADUCIDOS)}\n")
        for juego in sorted(JUEGOS_YA_TRADUCIDOS):
            f.write(f"  ✅ {juego}\n")
    
    # Resumen
    print("\n" + "=" * 50)
    print("📊 RESUMEN DE COPIA COMPLETA")
    print(f"✅ Copiados exitosamente: {copiados}")
    print(f"❌ Fallidos: {fallidos}")
    print(f"📁 Total procesados: {len(juegos_por_copiar)}")
    print(f"🎯 Directorio español: {DESTINO_DIR}")
    print("=" * 50)
    
    if fallidos == 0:
        print("🎉 ¡Todos los juegos copiados exitosamente!")
        print("\n📝 AHORA PUEDES:")
        print("1. Abrir los archivos .txt en AMIGACHEATCODES_ES")
        print("2. Traducirlos manualmente al español")
        print("3. El programa ya cargará todos los juegos en español")
    else:
        print(f"⚠️ {fallidos} juegos no pudieron ser copiados")
    
    # Copiar a ejecutable
    print("\n📦 Copiando a carpeta de ejecución...")
    ejecutable_dir = SCRIPT_DIR / "bin" / "Debug" / "net8.0-windows" / "win-x64" / "AMIGACHEATCODES_ES"
    if ejecutable_dir.exists():
        shutil.rmtree(ejecutable_dir)
    shutil.copytree(DESTINO_DIR, ejecutable_dir)
    print("✅ Copiado a carpeta de ejecución")
    
    print(f"\n📋 Lista de juegos para traducir guardada en:")
    print(f"   {DESTINO_DIR / 'JUEGOS_POR_TRADUCIR.txt'}")

if __name__ == "__main__":
    main()
