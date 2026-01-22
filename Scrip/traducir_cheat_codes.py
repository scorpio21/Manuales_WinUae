#!/usr/bin/env python3
"""
Script para traducir todos los archivos TXT de cheat codes al español
Usa Google Translate API (requiere configuración)
"""

import os
import re
import time
from pathlib import Path
from googletrans import Translator

# Configuración
CHEAT_CODES_DIR = r"d:\xampp\htdocs\Manual_Winuae\WinFormsManual\AMIGACHEATCODES"
ESPANOL_DIR = r"d:\xampp\htdocs\Manual_Winuae\WinFormsManual\AMIGACHEATCODES_ES"
MAX_RETRIES = 3
DELAY_SECONDS = 1

def create_spanish_directory():
    """Crear directorio para traducciones en español"""
    Path(ESPANOL_DIR).mkdir(exist_ok=True)
    
    # Crear subcarpetas
    for folder in ['0'] + [chr(ord('A') + i) for i in range(26)]:
        Path(ESPANOL_DIR, folder).mkdir(exist_ok=True)
    
    print(f"✅ Directorio español creado: {ESPANOL_DIR}")

def clean_text_for_translation(text):
    """Limpiar texto para mejor traducción"""
    # Eliminar caracteres problemáticos
    text = re.sub(r'[^\w\s\-\.\,\:\;\!\?\(\)\[\]""\'\/\\n\r]', '', text)
    # Eliminar múltiples espacios
    text = re.sub(r'\s+', ' ', text).strip()
    return text

def split_text_chunks(text, max_length=4500):
    """Dividir texto en chunks para traducción"""
    if len(text) <= max_length:
        return [text]
    
    chunks = []
    lines = text.split('\n')
    current_chunk = ""
    
    for line in lines:
        if len(current_chunk + line + '\n') <= max_length:
            current_chunk += line + '\n'
        else:
            if current_chunk:
                chunks.append(current_chunk.strip())
            current_chunk = line + '\n'
    
    if current_chunk:
        chunks.append(current_chunk.strip())
    
    return chunks

def translate_text(text, translator, retries=0):
    """Traducir texto con reintentos"""
    try:
        if not text.strip():
            return text
            
        # Dividir en chunks si es muy largo
        chunks = split_text_chunks(text)
        translated_chunks = []
        
        for chunk in chunks:
            if len(chunk.strip()) < 3:
                translated_chunks.append(chunk)
                continue
                
            try:
                result = translator.translate(chunk, src='en', dest='es')
                translated_chunks.append(result.text)
                time.sleep(DELAY_SECONDS)  # Evitar límite de API
            except Exception as e:
                print(f"⚠️ Error traduciendo chunk: {e}")
                translated_chunks.append(chunk)  # Mantener original
        
        return '\n'.join(translated_chunks)
        
    except Exception as e:
        if retries < MAX_RETRIES:
            print(f"🔄 Reintentando traducción (intento {retries + 1}/{MAX_RETRIES})")
            time.sleep(DELAY_SECONDS * (retries + 1))
            return translate_text(text, translator, retries + 1)
        else:
            print(f"❌ Error en traducción después de {MAX_RETRIES} intentos: {e}")
            return text  # Devolver original si falla

def translate_file(file_path, translator):
    """Traducir un archivo individual"""
    try:
        with open(file_path, 'r', encoding='utf-8', errors='ignore') as f:
            content = f.read()
        
        if not content.strip():
            print(f"⚠️ Archivo vacío: {file_path}")
            return False
        
        # Mantener formato original para códigos
        lines = content.split('\n')
        translated_lines = []
        
        for line in lines:
            line = line.strip()
            if not line:
                translated_lines.append('')
                continue
                
            # Detectar si es una línea de código (números al inicio)
            if re.match(r'^\d+[-\s]', line):
                # Para códigos, traducir solo si es texto descriptivo
                if len(line) > 20 and not re.match(r'^\d+[-\s][\d\-\s]+$', line):
                    translated_line = translate_text(line, translator)
                    translated_lines.append(translated_line)
                else:
                    translated_lines.append(line)  # Mantener códigos numéricos
            else:
                # Traducir líneas normales
                translated_line = translate_text(line, translator)
                translated_lines.append(translated_line)
        
        translated_content = '\n'.join(translated_lines)
        
        # Guardar traducción
        relative_path = Path(file_path).relative_to(CHEAT_CODES_DIR)
        output_path = Path(ESPANOL_DIR, relative_path)
        
        with open(output_path, 'w', encoding='utf-8') as f:
            f.write(translated_content)
        
        print(f"✅ Traducido: {relative_path}")
        return True
        
    except Exception as e:
        print(f"❌ Error traduciendo {file_path}: {e}")
        return False

def main():
    """Función principal"""
    print("🌍 Traductor de Cheat Codes - Inglés a Español")
    print("=" * 50)
    
    # Crear directorio español
    create_spanish_directory()
    
    # Inicializar traductor
    try:
        translator = Translator()
        print("✅ Traductor Google inicializado")
    except Exception as e:
        print(f"❌ Error inicializando traductor: {e}")
        print("💡 Asegúrate de tener instalado: pip install googletrans==4.0.0-rc1")
        return
    
    # Contar archivos totales
    txt_files = list(Path(CHEAT_CODES_DIR).rglob("*.txt"))
    total_files = len(txt_files)
    
    print(f"📁 Encontrados {total_files} archivos TXT para traducir")
    print(f"🎯 Objetivo: {ESPANOL_DIR}")
    print()
    
    # Traducir archivos
    translated_count = 0
    failed_count = 0
    
    for i, file_path in enumerate(txt_files, 1):
        print(f"[{i}/{total_files}] Traduciendo: {file_path.name}")
        
        if translate_file(file_path, translator):
            translated_count += 1
        else:
            failed_count += 1
        
        # Pausa cada 10 archivos para no sobrecargar la API
        if i % 10 == 0:
            print(f"⏸️ Pausa después de {i} archivos...")
            time.sleep(2)
    
    # Copiar archivos .ini
    print("\n📋 Copiando archivos .ini...")
    ini_files = list(Path(CHEAT_CODES_DIR).glob("*.ini"))
    for ini_file in ini_files:
        dest = Path(ESPANOL_DIR, ini_file.name)
        import shutil
        shutil.copy2(ini_file, dest)
        print(f"✅ Copiado: {ini_file.name}")
    
    # Resumen
    print("\n" + "=" * 50)
    print("📊 RESUMEN DE TRADUCCIÓN")
    print(f"✅ Traducidos: {translated_count}")
    print(f"❌ Fallidos: {failed_count}")
    print(f"📁 Total archivos: {total_files}")
    print(f"🎯 Directorio español: {ESPANOL_DIR}")
    print("=" * 50)
    
    if failed_count == 0:
        print("🎉 ¡Todos los archivos traducidos exitosamente!")
    else:
        print(f"⚠️ {failed_count} archivos no pudieron ser traducidos")

if __name__ == "__main__":
    main()
