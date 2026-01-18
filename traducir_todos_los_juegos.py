#!/usr/bin/env python3
"""
Script para traducir TODOS los juegos de cheat codes al español
Usa Google Translate API para traducción automática
"""

import os
import re
import time
from pathlib import Path
from googletrans import Translator

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

# Configuración de traducción
MAX_RETRIES = 3
DELAY_SECONDS = 1
MAX_TEXT_LENGTH = 4500

def limpiar_texto_para_traduccion(text):
    """Limpiar texto para mejor traducción"""
    # Eliminar caracteres problemáticos pero mantener formato
    text = re.sub(r'[^\w\s\-\.\,\:\;\!\?\(\)\[\]\"\'\/\n\r\t\*\=\+\#\%]', '', text)
    # Eliminar múltiples espacios y líneas
    text = re.sub(r'\n\s*\n', '\n\n', text)
    text = re.sub(r'[ \t]+', ' ', text)
    return text.strip()

def dividir_texto_en_chunks(text, max_length=MAX_TEXT_LENGTH):
    """Dividir texto en chunks para traducción"""
    if len(text) <= max_length:
        return [text]
    
    chunks = []
    lines = text.split('\n')
    current_chunk = ""
    
    for line in lines:
        # Si la línea es muy larga, partirla
        if len(line) > max_length:
            if current_chunk:
                chunks.append(current_chunk.strip())
                current_chunk = ""
            
            # Partir línea larga
            words = line.split(' ')
            temp_line = ""
            for word in words:
                if len(temp_line + word + ' ') <= max_length:
                    temp_line += word + ' '
                else:
                    if temp_line:
                        chunks.append(temp_line.strip())
                    temp_line = word + ' '
            
            if temp_line:
                current_chunk = temp_line + '\n'
        else:
            if len(current_chunk + line + '\n') <= max_length:
                current_chunk += line + '\n'
            else:
                if current_chunk:
                    chunks.append(current_chunk.strip())
                current_chunk = line + '\n'
    
    if current_chunk.strip():
        chunks.append(current_chunk.strip())
    
    return chunks

def traducir_texto(text, translator, retries=0):
    """Traducir texto con reintentos"""
    try:
        if not text.strip():
            return text
        
        # Dividir en chunks si es muy largo
        chunks = dividir_texto_en_chunks(text)
        translated_chunks = []
        
        for i, chunk in enumerate(chunks):
            if len(chunk.strip()) < 3:
                translated_chunks.append(chunk)
                continue
            
            try:
                # Traducir chunk
                result = translator.translate(chunk, src='en', dest='es')
                translated_text = result.text
                
                # Verificar que la traducción sea razonable
                if len(translated_text) < len(chunk) * 0.3:  # Si es mucho más corta, mantener original
                    translated_chunks.append(chunk)
                else:
                    translated_chunks.append(translated_text)
                
                time.sleep(DELAY_SECONDS)  # Evitar límite de API
                
            except Exception as e:
                print(f"⚠️ Error traduciendo chunk {i+1}/{len(chunks)}: {e}")
                translated_chunks.append(chunk)  # Mantener original
        
        return '\n'.join(translated_chunks)
        
    except Exception as e:
        if retries < MAX_RETRIES:
            print(f"🔄 Reintentando traducción (intento {retries + 1}/{MAX_RETRIES})")
            time.sleep(DELAY_SECONDS * (retries + 1))
            return traducir_texto(text, translator, retries + 1)
        else:
            print(f"❌ Error en traducción después de {MAX_RETRIES} intentos: {e}")
            return text  # Devolver original si falla

def traducir_archivo(origen_path, destino_path, translator, nombre_juego):
    """Traducir un archivo individual"""
    try:
        print(f"📖 Traduciendo: {nombre_juego}")
        
        with open(origen_path, 'r', encoding='utf-8', errors='ignore') as f:
            contenido = f.read()
        
        if not contenido.strip():
            print(f"⚠️ Archivo vacío: {nombre_juego}")
            return False
        
        # Mantener estructura para códigos numéricos
        lineas = contenido.split('\n')
        lineas_traducidas = []
        
        for linea in lineas:
            linea = linea.rstrip()
            
            if not linea:
                lineas_traducidas.append('')
                continue
            
            # Detectar líneas de código (números al inicio)
            if re.match(r'^\d+[-\s]', linea) or re.match(r'^\s*\d+\.', linea):
                # Para códigos, traducir solo si es texto descriptivo largo
                if len(linea) > 30 and not re.match(r'^\d+[-\s][\d\-\s\.\,]+$', linea):
                    linea_limpia = limpiar_texto_para_traduccion(linea)
                    linea_traducida = traducir_texto(linea_limpia, translator)
                    lineas_traducidas.append(linea_traducida)
                else:
                    # Mantener códigos numéricos sin cambios
                    lineas_traducidas.append(linea)
            else:
                # Traducir líneas normales
                linea_limpia = limpiar_texto_para_traduccion(linea)
                linea_traducida = traducir_texto(linea_limpia, translator)
                lineas_traducidas.append(linea_traducida)
        
        contenido_traducido = '\n'.join(lineas_traducidas)
        
        # Guardar traducción
        with open(destino_path, 'w', encoding='utf-8') as f:
            f.write(contenido_traducido)
        
        print(f"✅ Traducido: {nombre_juego}")
        return True
        
    except Exception as e:
        print(f"❌ Error traduciendo {nombre_juego}: {e}")
        return False

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

def main():
    """Función principal"""
    print("🌍 TRADUCTOR COMPLETO DE CHEAT CODES")
    print("=" * 50)
    
    # Verificar carpetas
    if not ORIGEN_DIR.exists():
        print(f"❌ No existe carpeta origen: {ORIGEN_DIR}")
        return
    
    DESTINO_DIR.mkdir(exist_ok=True)
    
    # Crear subcarpetas
    for carpeta in ['0'] + [chr(ord('A') + i) for i in range(26)]:
        (DESTINO_DIR / carpeta).mkdir(exist_ok=True)
    
    # Inicializar traductor
    try:
        translator = Translator()
        print("✅ Traductor Google inicializado")
    except Exception as e:
        print(f"❌ Error inicializando traductor: {e}")
        print("💡 Instala: pip install googletrans==4.0.0-rc1")
        return
    
    # Obtener lista de juegos
    juegos = obtener_lista_juegos()
    print(f"📁 Total juegos encontrados: {len(juegos)}")
    
    # Filtrar juegos ya traducidos
    juegos_por_traducir = [j for j in juegos if j not in JUEGOS_YA_TRADUCIDOS]
    print(f"🎯 Juegos por traducir: {len(juegos_por_traducir)}")
    print(f"✅ Juegos ya traducidos: {len(JUEGOS_YA_TRADUCIDOS)}")
    
    if not juegos_por_traducir:
        print("🎉 ¡Todos los juegos ya están traducidos!")
        return
    
    # Traducir archivos
    traducidos = 0
    fallidos = 0
    
    for i, nombre_juego in enumerate(juegos_por_traducir, 1):
        print(f"\n[{i}/{len(juegos_por_traducir)}] Procesando: {nombre_juego}")
        
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
        
        # Traducir archivo
        if traducir_archivo(archivo_origen, archivo_destino, translator, nombre_juego):
            traducidos += 1
        else:
            fallidos += 1
        
        # Pausa cada 10 archivos
        if i % 10 == 0:
            print(f"⏸️ Pausa después de {i} archivos...")
            time.sleep(3)
    
    # Actualizar AvailableCheatCodes.ini
    print("\n📋 Actualizando AvailableCheatCodes.ini...")
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
    
    # Resumen
    print("\n" + "=" * 50)
    print("📊 RESUMEN DE TRADUCCIÓN COMPLETA")
    print(f"✅ Traducidos exitosamente: {traducidos}")
    print(f"❌ Fallidos: {fallidos}")
    print(f"📁 Total procesados: {len(juegos_por_traducir)}")
    print(f"🎯 Directorio español: {DESTINO_DIR}")
    print("=" * 50)
    
    if fallidos == 0:
        print("🎉 ¡Todos los juegos traducidos exitosamente!")
    else:
        print(f"⚠️ {fallidos} juegos no pudieron ser traducidos")
    
    # Copiar a ejecutable
    print("\n📦 Copiando a carpeta de ejecución...")
    ejecutable_dir = SCRIPT_DIR / "bin" / "Debug" / "net8.0-windows" / "win-x64" / "AMIGACHEATCODES_ES"
    if ejecutable_dir.exists():
        import shutil
        shutil.rmtree(ejecutable_dir)
    shutil.copytree(DESTINO_DIR, ejecutable_dir)
    print("✅ Copiado a carpeta de ejecución")

if __name__ == "__main__":
    main()
