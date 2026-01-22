#!/usr/bin/env python3
"""
Script para traducir todos los cheat codes usando MyMemory API (gratuita)
"""

import os
import re
import time
import requests
from pathlib import Path

# Configuración
SCRIPT_DIR = Path(__file__).parent
DESTINO_DIR = SCRIPT_DIR / "AMIGACHEATCODES_ES"
API_URL = "https://api.mymemory.translated.net/get"

# Configuración de traducción
DELAY_SECONDS = 2  # Pausa entre peticiones
MAX_TEXT_LENGTH = 500  # Límite de la API

def traducir_texto_api(texto, retries=0):
    """Traducir texto usando MyMemory API"""
    try:
        if not texto.strip() or len(texto.strip()) < 3:
            return texto
        
        # Dividir texto si es muy largo
        if len(texto) > MAX_TEXT_LENGTH:
            chunks = []
            lines = texto.split('\n')
            current_chunk = ""
            
            for line in lines:
                if len(current_chunk + line + '\n') <= MAX_TEXT_LENGTH:
                    current_chunk += line + '\n'
                else:
                    if current_chunk.strip():
                        chunks.append(current_chunk.strip())
                    current_chunk = line + '\n'
            
            if current_chunk.strip():
                chunks.append(current_chunk.strip())
            
            # Traducir cada chunk
            translated_chunks = []
            for chunk in chunks:
                if len(chunk.strip()) < 3:
                    translated_chunks.append(chunk)
                    continue
                
                try:
                    params = {
                        'q': chunk,
                        'langpair': 'en|es'
                    }
                    response = requests.get(API_URL, params=params, timeout=10)
                    
                    if response.status_code == 200:
                        data = response.json()
                        if data.get('responseData') and data['responseData'].get('translatedText'):
                            translated_text = data['responseData']['translatedText']
                            # Verificar que la traducción sea razonable
                            if len(translated_text) >= len(chunk) * 0.3:
                                translated_chunks.append(translated_text)
                            else:
                                translated_chunks.append(chunk)
                        else:
                            translated_chunks.append(chunk)
                    else:
                        translated_chunks.append(chunk)
                    
                    time.sleep(DELAY_SECONDS)
                    
                except Exception as e:
                    print(f"⚠️ Error en chunk: {e}")
                    translated_chunks.append(chunk)
            
            return '\n'.join(translated_chunks)
        
        # Texto corto - traducir directamente
        else:
            params = {
                'q': texto,
                'langpair': 'en|es'
            }
            response = requests.get(API_URL, params=params, timeout=10)
            
            if response.status_code == 200:
                data = response.json()
                if data.get('responseData') and data['responseData'].get('translatedText'):
                    translated_text = data['responseData']['translatedText']
                    if len(translated_text) >= len(texto) * 0.3:
                        return translated_text
            
            return texto
            
    except Exception as e:
        if retries < 2:
            print(f"🔄 Reintentando (intento {retries + 1})")
            time.sleep(DELAY_SECONDS * (retries + 1))
            return traducir_texto_api(texto, retries + 1)
        else:
            print(f"❌ Error: {e}")
            return texto

def traducir_archivo(archivo_path):
    """Traducir un archivo individual"""
    try:
        with open(archivo_path, 'r', encoding='utf-8', errors='ignore') as f:
            contenido = f.read()
        
        if not contenido.strip():
            return False
        
        # Procesar línea por línea
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
                    linea_traducida = traducir_texto_api(linea)
                    lineas_traducidas.append(linea_traducida)
                else:
                    # Mantener códigos numéricos sin cambios
                    lineas_traducidas.append(linea)
            else:
                # Traducir líneas normales
                linea_traducida = traducir_texto_api(linea)
                lineas_traducidas.append(linea_traducida)
        
        contenido_traducido = '\n'.join(lineas_traducidas)
        
        # Guardar traducción
        with open(archivo_path, 'w', encoding='utf-8') as f:
            f.write(contenido_traducido)
        
        return True
        
    except Exception as e:
        print(f"❌ Error traduciendo {archivo_path.name}: {e}")
        return False

def traducir_lote():
    """Traducir todos los archivos por lotes"""
    print("🌍 TRADUCTOR AUTOMÁTICO DE CHEAT CODES")
    print("=" * 50)
    
    if not DESTINO_DIR.exists():
        print(f"❌ No existe: {DESTINO_DIR}")
        return
    
    # Encontrar todos los archivos .txt
    archivos_txt = list(DESTINO_DIR.rglob("*.txt"))
    print(f"📁 Archivos encontrados: {len(archivos_txt)}")
    
    # Excluir archivos ya traducidos (los que tienen texto en español)
    archivos_por_traducir = []
    
    for archivo in archivos_txt:
        try:
            with open(archivo, 'r', encoding='utf-8', errors='ignore') as f:
                contenido = f.read()
            
            # Verificar si ya está en español (palabras clave en español)
            palabras_espanol = ['el', 'la', 'los', 'las', 'de', 'en', 'con', 'para', 'por', 'nivel', 'vidas', 'puntuación']
            tiene_espanol = any(palabra in contenido.lower() for palabra in palabras_espanol)
            
            if not tiene_espanol and len(contenido.strip()) > 10:
                archivos_por_traducir.append(archivo)
        except:
            archivos_por_traducir.append(archivo)
    
    print(f"🎯 Archivos por traducir: {len(archivos_por_traducir)}")
    
    if not archivos_por_traducir:
        print("🎉 ¡Todos los archivos ya están traducidos!")
        return
    
    # Traducir archivos
    traducidos = 0
    fallidos = 0
    
    for i, archivo in enumerate(archivos_por_traducir, 1):  # Traducir todos
        print(f"\n[{i}/{len(archivos_por_traducir)}] Traduciendo: {archivo.name}")
        
        if traducir_archivo(archivo):
            traducidos += 1
            print(f"✅ Traducido: {archivo.name}")
        else:
            fallidos += 1
            print(f"❌ Falló: {archivo.name}")
        
        # Pausa cada 10 archivos
        if i % 10 == 0:
            print(f"⏸️ Pausa después de {i} archivos...")
            time.sleep(5)
    
    # Resumen
    print("\n" + "=" * 50)
    print("📊 RESUMEN DE TRADUCCIÓN")
    print(f"✅ Traducidos: {traducidos}")
    print(f"❌ Fallidos: {fallidos}")
    print(f"📁 Total procesados: {i}")
    
    if len(archivos_por_traducir) > 0:
        print(f"\n✅ Todos los archivos fueron procesados")
    else:
        print(f"\n⚠️ No se encontraron archivos para traducir")
    
    print("=" * 50)

if __name__ == "__main__":
    traducir_lote()
