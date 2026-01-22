#!/usr/bin/env python3
"""
Script para generar paths.ini con rutas relativas
"""

import os
from pathlib import Path

# Configuración
CHEAT_CODES_DIR = r"d:\xampp\htdocs\Manual_Winuae\WinFormsManual\AMIGACHEATCODES"
OUTPUT_FILE = os.path.join(CHEAT_CODES_DIR, "paths_rel.ini")

def generate_relative_paths():
    """Generar archivo paths.ini con rutas relativas"""
    
    print("🔧 Generando paths.ini con rutas relativas...")
    
    # Encontrar todos los archivos TXT
    txt_files = []
    for root, dirs, files in os.walk(CHEAT_CODES_DIR):
        for file in files:
            if file.endswith('.txt'):
                full_path = os.path.join(root, file)
                relative_path = os.path.relpath(full_path, CHEAT_CODES_DIR)
                txt_files.append(relative_path)
    
    # Ordenar archivos
    txt_files.sort()
    
    # Generar archivo
    with open(OUTPUT_FILE, 'w', encoding='utf-8') as f:
        for txt_file in txt_files:
            # Ruta relativa desde AMIGACHEATCODES
            f.write(f"AMIGACHEATCODES\\{txt_file.replace('/', '\\')}\n")
    
    print(f"✅ Generado: {OUTPUT_FILE}")
    print(f"📁 Total archivos: {len(txt_files)}")
    
    # Mostrar primeros ejemplos
    print("\n📋 Ejemplos de rutas generadas:")
    for i, txt_file in enumerate(txt_files[:5]):
        print(f"   AMIGACHEATCODES\\{txt_file.replace('/', '\\')}")
    
    if len(txt_files) > 5:
        print(f"   ... y {len(txt_files) - 5} archivos más")

if __name__ == "__main__":
    generate_relative_paths()
