#!/usr/bin/env python3
"""
Script para copiar y traducir archivos de ejemplo - VERSIÓN PORTABLE
Usa rutas relativas para funcionar en cualquier PC
"""

import os
import shutil
from pathlib import Path

# Configuración PORTABLE - usa rutas relativas
SCRIPT_DIR = Path(__file__).parent
ORIGEN_DIR = SCRIPT_DIR / "AMIGACHEATCODES"
DESTINO_DIR = SCRIPT_DIR / "AMIGACHEATCODES_ES"

# Archivos de ejemplo para traducir
EJEMPLOS = [
    ("0", "3D Pool.txt"),
    ("0", "4-Get-It.txt"),
    ("A", "A-Train.txt"),
    ("A", "Aaargh!.txt"),
    ("B", "Bubble Bobble.txt"),
]

def copiar_y_traducir_archivos():
    """Copiar y traducir archivos de ejemplo con rutas relativas"""
    
    print("🌍 Copiando y traduciendo archivos de ejemplo (PORTABLE)...")
    print(f"📁 Script dir: {SCRIPT_DIR}")
    print(f"📂 Origen: {ORIGEN_DIR}")
    print(f"📂 Destino: {DESTINO_DIR}")
    
    # Verificar que existe el origen
    if not ORIGEN_DIR.exists():
        print(f"❌ ERROR: No existe la carpeta origen: {ORIGEN_DIR}")
        return False
    
    # Crear estructura de carpetas
    DESTINO_DIR.mkdir(exist_ok=True)
    for carpeta in ['0', 'A', 'B']:
        (DESTINO_DIR / carpeta).mkdir(exist_ok=True)
    
    # Copiar AvailableCheatCodes.ini
    origen_ini = ORIGEN_DIR / "AvailableCheatCodes.ini"
    destino_ini = DESTINO_DIR / "AvailableCheatCodes.ini"
    
    if origen_ini.exists():
        shutil.copy2(origen_ini, destino_ini)
        print(f"✅ Copiado: AvailableCheatCodes.ini")
    else:
        print(f"⚠️ No encontrado: AvailableCheatCodes.ini")
    
    # Copiar archivos traducidos ya existentes
    for carpeta, archivo in EJEMPLOS:
        origen = ORIGEN_DIR / carpeta / archivo
        destino = DESTINO_DIR / carpeta / archivo
        
        if origen.exists():
            # Si ya existe una versión traducida, copiarla
            if destino.exists():
                print(f"✅ Ya existe traducido: {carpeta}/{archivo}")
            else:
                # Copiar original para traducir manualmente
                shutil.copy2(origen, destino)
                print(f"📋 Copiado para traducir: {carpeta}/{archivo}")
        else:
            print(f"⚠️ No encontrado: {carpeta}/{archivo}")
    
    print("\n🎯 ¡Archivos copiados!")
    print("📝 NOTA: Los archivos están listos para traducción manual")
    print(f"📁 Directorio destino: {DESTINO_DIR}")
    
    return True

if __name__ == "__main__":
    copiar_y_traducir_archivos()
