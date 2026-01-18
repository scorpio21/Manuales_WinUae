#!/usr/bin/env python3
"""
Script para mostrar resumen del estado de traducción
"""

from pathlib import Path

DESTINO_DIR = Path(__file__).parent / "AMIGACHEATCODES_ES"

# Juegos ya traducidos manualmente
JUEGOS_TRADUCIDOS_MANUAL = {
    "3D Pool",
    "A-Train", 
    "Bubble Bobble"
}

def analizar_traduccion():
    """Analizar estado de traducción"""
    print("📊 ANÁLISIS DE TRADUCCIÓN DE CHEAT CODES")
    print("=" * 60)
    
    # Contar archivos totales
    archivos_txt = list(DESTINO_DIR.rglob("*.txt"))
    total_archivos = len(archivos_txt)
    
    print(f"📁 Total archivos .txt: {total_archivos}")
    
    # Analizar contenido para detectar traducción
    traducidos_completos = 0
    parcialmente_traducidos = 0
    no_traducidos = 0
    
    palabras_espanol = ['el ', 'la ', 'los ', 'las ', 'de ', 'en ', 'con ', 'para ', 'por ', 
                       'nivel', 'vidas', 'puntuación', 'truco', 'código', 'presiona',
                       'haz clic', 'selecciona', 'juego', 'puntos', 'energía', 'tiempo']
    
    print("\n🔍 Analizando contenido...")
    
    for archivo in archivos_txt:
        try:
            with open(archivo, 'r', encoding='utf-8', errors='ignore') as f:
                contenido = f.read().lower()
            
            if not contenido.strip():
                continue
            
            # Contar palabras en español
            palabras_es = sum(1 for palabra in palabras_espanol if palabra in contenido)
            
            # Determinar estado
            if palabras_es >= 5:  # Múltiples palabras en español
                traducidos_completos += 1
            elif palabras_es >= 2:  # Algunas palabras en español
                parcialmente_traducidos += 1
            else:
                no_traducidos += 1
                
        except:
            no_traducidos += 1
    
    print(f"\n📈 RESULTADOS:")
    print(f"✅ Completamente traducidos: {traducidos_completos}")
    print(f"🔄 Parcialmente traducidos: {parcialmente_traducidos}")
    print(f"❌ No traducidos: {no_traducidos}")
    
    # Mostrar ejemplos
    print(f"\n📋 EJEMPLOS POR CATEGORÍA:")
    
    # Buscar ejemplos
    ej_completos = []
    ej_parciales = []
    ej_no_traducidos = []
    
    for archivo in archivos_txt:
        if len(ej_completos) >= 5 and len(ej_parciales) >= 5 and len(ej_no_traducidos) >= 5:
            break
            
        try:
            with open(archivo, 'r', encoding='utf-8', errors='ignore') as f:
                contenido = f.read().lower()
            
            if not contenido.strip():
                continue
            
            palabras_es = sum(1 for palabra in palabras_espanol if palabra in contenido)
            nombre = archivo.stem
            
            if palabras_es >= 5 and len(ej_completos) < 5:
                ej_completos.append(nombre)
            elif palabras_es >= 2 and len(ej_parciales) < 5:
                ej_parciales.append(nombre)
            elif palabras_es < 2 and len(ej_no_traducidos) < 5:
                ej_no_traducidos.append(nombre)
                
        except:
            continue
    
    print(f"\n✅ COMPLETAMENTE TRADUCIDOS:")
    for juego in ej_completos:
        print(f"   • {juego}")
    
    print(f"\n🔄 PARCIALMENTE TRADUCIDOS:")
    for juego in ej_parciales:
        print(f"   • {juego}")
    
    print(f"\n❌ SIN TRADUCIR:")
    for juego in ej_no_traducidos:
        print(f"   • {juego}")
    
    print(f"\n🎯 JUEGOS TRADUCIDOS MANUALMENTE:")
    for juego in sorted(JUEGOS_TRADUCIDOS_MANUAL):
        print(f"   ⭐ {juego}")
    
    print(f"\n💡 RECOMENDACIONES:")
    print(f"   • Hay {no_traducidos} juegos que necesitan traducción completa")
    print(f"   • Hay {parcialmente_traducidos} juegos con traducción parcial")
    print(f"   • Usa 'traducir_con_api_simple.py' para traducir automáticamente")
    print(f"   • Los juegos más populares deberían traducirse manualmente")
    
    print("\n" + "=" * 60)

if __name__ == "__main__":
    analizar_traduccion()
