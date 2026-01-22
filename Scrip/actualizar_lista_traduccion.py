#!/usr/bin/env python3
"""
Script para actualizar la lista de juegos traducidos y parcialmente traducidos
"""

from pathlib import Path

DESTINO_DIR = Path(__file__).parent / "AMIGACHEATCODES_ES"
LISTA_FILE = DESTINO_DIR / "JUEGOS_POR_TRADUCIR.txt"

def actualizar_lista():
    """Actualizar la lista con estado actual de traducción"""
    print("📝 ACTUALIZANDO LISTA DE TRADUCCIÓN")
    print("=" * 50)
    
    # Palabras clave en español
    palabras_espanol = ['el ', 'la ', 'los ', 'las ', 'de ', 'en ', 'con ', 'para ', 'por ', 
                       'nivel', 'vidas', 'puntuación', 'truco', 'código', 'presiona',
                       'haz clic', 'selecciona', 'juego', 'puntos', 'energía', 'tiempo',
                       'infinito', 'inmortal', 'invencible', 'arma', 'escudo']
    
    # Obtener todos los archivos
    archivos_txt = list(DESTINO_DIR.rglob("*.txt"))
    
    # Clasificar juegos
    completamente_traducidos = []
    parcialmente_traducidos = []
    no_traducidos = []
    
    print("🔍 Analizando archivos...")
    
    for archivo in archivos_txt:
        if archivo.name == "JUEGOS_POR_TRADUCIR.txt":
            continue
            
        try:
            with open(archivo, 'r', encoding='utf-8', errors='ignore') as f:
                contenido = f.read().lower()
            
            if not contenido.strip():
                continue
            
            # Contar palabras en español
            palabras_es = sum(1 for palabra in palabras_espanol if palabra in contenido)
            nombre_juego = archivo.stem
            
            # Clasificar
            if palabras_es >= 5:  # Múltiples palabras en español
                completamente_traducidos.append(nombre_juego)
            elif palabras_es >= 2:  # Algunas palabras en español
                parcialmente_traducidos.append(nombre_juego)
            else:
                no_traducidos.append(nombre_juego)
                
        except Exception as e:
            print(f"⚠️ Error procesando {archivo.name}: {e}")
            no_traducidos.append(archivo.stem)
    
    # Ordenar listas
    completamente_traducidos.sort()
    parcialmente_traducidos.sort()
    no_traducidos.sort()
    
    print(f"✅ Completamente traducidos: {len(completamente_traducidos)}")
    print(f"🔄 Parcialmente traducidos: {len(parcialmente_traducidos)}")
    print(f"❌ No traducidos: {len(no_traducidos)}")
    
    # Escribir nueva lista
    with open(LISTA_FILE, 'w', encoding='utf-8') as f:
        f.write("ESTADO ACTUAL DE TRADUCCIÓN DE CHEAT CODES\n")
        f.write("=" * 60 + "\n\n")
        
        f.write(f"📊 RESUMEN:\n")
        f.write(f"✅ Completamente traducidos: {len(completamente_traducidos)} juegos\n")
        f.write(f"🔄 Parcialmente traducidos: {len(parcialmente_traducidos)} juegos\n")
        f.write(f"❌ No traducidos: {len(no_traducidos)} juegos\n")
        f.write(f"📁 Total archivos: {len(completamente_traducidos) + len(parcialmente_traducidos) + len(no_traducidos)}\n\n")
        
        f.write("✅ COMPLETAMENTE TRADUCIDOS:\n")
        f.write("-" * 40 + "\n")
        for juego in completamente_traducidos:
            f.write(f"✅ {juego}\n")
        
        f.write(f"\n🔄 PARCIALMENTE TRADUCIDOS:\n")
        f.write("-" * 40 + "\n")
        for juego in parcialmente_traducidos:
            f.write(f"🔄 {juego}\n")
        
        f.write(f"\n❌ SIN TRADUCIR:\n")
        f.write("-" * 40 + "\n")
        for juego in no_traducidos:
            f.write(f"❌ {juego}\n")
        
        f.write(f"\n" + "=" * 60 + "\n")
        f.write("💡 NOTA:\n")
        f.write("• Los juegos con ✅ están completamente en español\n")
        f.write("• Los juegos con 🔄 tienen algunas palabras en español\n")
        f.write("• Los juegos con ❌ están completamente en inglés\n")
        f.write("• Usa 'traducir_con_api_simple.py' para traducir automáticamente\n")
    
    print(f"\n✅ Lista actualizada en: {LISTA_FILE}")
    print(f"📋 Total de líneas: {len(completamente_traducidos) + len(parcialmente_traducidos) + len(no_traducidos) + 20}")
    
    # Mostrar algunos ejemplos
    print(f"\n📋 EJEMPLOS:")
    print(f"✅ Completamente traducidos: {completamente_traducidos[:5]}")
    print(f"🔄 Parcialmente traducidos: {parcialmente_traducidos[:5]}")
    print(f"❌ No traducidos: {no_traducidos[:5]}")

if __name__ == "__main__":
    actualizar_lista()
