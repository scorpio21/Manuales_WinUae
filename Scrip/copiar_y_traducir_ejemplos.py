#!/usr/bin/env python3
"""
Script para copiar y traducir algunos archivos de ejemplo a español
"""

import os
import shutil
from pathlib import Path

# Configuración
ORIGEN_DIR = r"d:\xampp\htdocs\Manual_Winuae\WinFormsManual\AMIGACHEATCODES"
DESTINO_DIR = r"d:\xampp\htdocs\Manual_Winuae\WinFormsManual\AMIGACHEATCODES_ES"

# Archivos de ejemplo para traducir
EJEMPLOS = [
    ("0", "3D Pool.txt"),
    ("0", "4-Get-It.txt"),
    ("A", "A-Train.txt"),
    ("A", "Aaargh!.txt"),
    ("B", "Bubble Bobble.txt"),
]

# Traducciones simples de ejemplo
TRADUCCIONES = {
    "3D Pool": "Pool 3D",
    "4-Get-It": "4-Consiguelo",
    "A-Train": "A-Tren",
    "Aaargh!": "Aaaargh!",
    "Bubble Bobble": "Bubble Bobble",
    "Level": "Nivel",
    "Lives": "Vidas",
    "Time": "Tiempo",
    "Score": "Puntuación",
    "Infinite": "Infinito",
    "Infinite Lives": "Vidas Infinitas",
    "Infinite Time": "Tiempo Infinito",
    "Level Select": "Selección de Nivel",
    "Invincibility": "Invencibilidad",
    "All Weapons": "Todas las Armas",
    "Full Energy": "Energía Completa",
    "Skip Level": "Saltar Nivel",
    "Debug Mode": "Modo Depuración",
    "Cheat": "Truco",
    "Code": "Código",
    "Enter": "Presionar",
    "Press": "Presionar",
    "During": "Durante",
    "Game": "Juego",
    "Play": "Jugar",
    "Start": "Inicio",
    "Menu": "Menú",
    "Options": "Opciones",
    "Continue": "Continuar",
    "Exit": "Salir",
    "Pause": "Pausa",
    "Resume": "Reanudar",
    "Restart": "Reiniciar",
    "Quit": "Salir",
    "Help": "Ayuda",
    "About": "Acerca de",
    "Settings": "Configuración",
    "Controls": "Controles",
    "Graphics": "Gráficos",
    "Sound": "Sonido",
    "Music": "Música",
    "Effects": "Efectos",
    "Volume": "Volumen",
    "Quality": "Calidad",
    "Resolution": "Resolución",
    "Fullscreen": "Pantalla Completa",
    "Windowed": "Ventana",
    "Save": "Guardar",
    "Load": "Cargar",
    "Delete": "Eliminar",
    "New": "Nuevo",
    "Open": "Abrir",
    "Close": "Cerrar",
    "Cancel": "Cancelar",
    "OK": "Aceptar",
    "Yes": "Sí",
    "No": "No",
    "Warning": "Advertencia",
    "Error": "Error",
    "Information": "Información",
    "Success": "Éxito",
    "Failed": "Falló",
    "Loading": "Cargando",
    "Please Wait": "Espere Por Favor",
    "Processing": "Procesando",
    "Complete": "Completado",
    "Ready": "Listo",
    "Player": "Jugador",
    "Computer": "Computadora",
    "Multiplayer": "Multijugador",
    "Single Player": "Un Jugador",
    "Two Players": "Dos Jugadores",
    "Three Players": "Tres Jugadores",
    "Four Players": "Cuatro Jugadores",
    "Team": "Equipo",
    "Score": "Puntuación",
    "High Score": "Puntuación Máxima",
    "Best Score": "Mejor Puntuación",
    "Current Score": "Puntuación Actual",
    "Total Score": "Puntuación Total",
    "Bonus": "Bono",
    "Extra": "Extra",
    "Power": "Poder",
    "Speed": "Velocidad",
    "Strength": "Fuerza",
    "Defense": "Defensa",
    "Attack": "Ataque",
    "Magic": "Magia",
    "Health": "Salud",
    "Mana": "Maná",
    "Energy": "Energía",
    "Shield": "Escudo",
    "Armor": "Armadura",
    "Weapon": "Arma",
    "Sword": "Espada",
    "Gun": "Arma de Fuego",
    "Bullet": "Bala",
    "Bomb": "Bomba",
    "Explosion": "Explosión",
    "Fire": "Fuego",
    "Water": "Agua",
    "Earth": "Tierra",
    "Air": "Aire",
    "Ice": "Hielo",
    "Electric": "Eléctrico",
    "Poison": "Veneno",
    "Heal": "Curar",
    "Damage": "Daño",
    "Critical": "Crítico",
    "Miss": "Fallo",
    "Hit": "Golpe",
    "Kill": "Matar",
    "Death": "Muerte",
    "Alive": "Vivo",
    "Dead": "Muerto",
    "Ghost": "Fantasma",
    "Monster": "Monstruo",
    "Enemy": "Enemigo",
    "Friend": "Amigo",
    "Neutral": "Neutral",
    "Ally": "Aliado",
    "Boss": "Jefe",
    "Mini-Boss": "Mini-Jefe",
    "Final Boss": "Jefe Final",
    "Stage": "Etapa",
    "Round": "Ronda",
    "Match": "Partida",
    "Tournament": "Torneo",
    "Championship": "Campeonato",
    "League": "Liga",
    "Season": "Temporada",
    "Career": "Carrera",
    "Mission": "Misión",
    "Quest": "Búsqueda",
    "Adventure": "Aventura",
    "Story": "Historia",
    "Plot": "Trama",
    "Ending": "Final",
    "Beginning": "Comienzo",
    "Middle": "Mitad",
    "End": "Fin",
}

def traducir_texto_simple(texto):
    """Traducir texto usando diccionario simple"""
    texto_traducido = texto
    
    # Reemplazar palabras comunes
    for ingles, espanol in TRADUCCIONES.items():
        texto_traducido = texto_traducido.replace(ingles, espanol)
    
    return texto_traducido

def copiar_y_traducir_archivos():
    """Copiar y traducir archivos de ejemplo"""
    
    print("🌍 Copiando y traduciendo archivos de ejemplo...")
    
    # Crear estructura de carpetas si no existe
    Path(DESTINO_DIR).mkdir(exist_ok=True)
    for carpeta in ['0', 'A', 'B']:
        Path(DESTINO_DIR, carpeta).mkdir(exist_ok=True)
    
    # Copiar AvailableCheatCodes.ini
    origen_ini = Path(ORIGEN_DIR, "AvailableCheatCodes.ini")
    destino_ini = Path(DESTINO_DIR, "AvailableCheatCodes.ini")
    
    if origen_ini.exists():
        shutil.copy2(origen_ini, destino_ini)
        print(f"✅ Copiado: AvailableCheatCodes.ini")
    
    # Copiar y traducir archivos de ejemplo
    for carpeta, archivo in EJEMPLOS:
        origen = Path(ORIGEN_DIR, carpeta, archivo)
        destino = Path(DESTINO_DIR, carpeta, archivo)
        
        if origen.exists():
            try:
                # Leer contenido original
                with open(origen, 'r', encoding='utf-8', errors='ignore') as f:
                    contenido = f.read()
                
                # Traducir contenido
                contenido_traducido = traducir_texto_simple(contenido)
                
                # Guardar traducción
                with open(destino, 'w', encoding='utf-8') as f:
                    f.write(contenido_traducido)
                
                print(f"✅ Traducido: {carpeta}/{archivo}")
                
            except Exception as e:
                print(f"❌ Error traduciendo {archivo}: {e}")
                # Si falla la traducción, copiar original
                shutil.copy2(origen, destino)
                print(f"📋 Copiado original: {carpeta}/{archivo}")
        else:
            print(f"⚠️ No encontrado: {carpeta}/{archivo}")
    
    print("\n🎯 ¡Archivos de ejemplo creados!")
    print(f"📁 Directorio destino: {DESTINO_DIR}")
    print("\n📋 Archivos creados:")
    for carpeta, archivo in EJEMPLOS:
        ruta = Path(DESTINO_DIR, carpeta, archivo)
        if ruta.exists():
            print(f"   ✅ {carpeta}/{archivo}")
    
    print("\n🔍 Ahora puedes:")
    print("1. Abrir la aplicación")
    print("2. Seleccionar 'Español (Traducido)'")
    print("3. Buscar los juegos de ejemplo")

if __name__ == "__main__":
    copiar_y_traducir_archivos()
