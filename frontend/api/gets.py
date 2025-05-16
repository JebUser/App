from datetime import datetime
import requests
import urllib3
import streamlit as st

urllib3.disable_warnings(urllib3.exceptions.InsecureRequestWarning)

BASE_URL = "https://localhost:57740/api"

def consultar_actividades():
    """Obtiene todas las actividades con sus participantes"""
    try:
        response = requests.get(f"{BASE_URL}/Actividades", verify=False)
        response.raise_for_status()
        return procesar_datos_actividades(response.json())
    except requests.exceptions.RequestException as e:
        st.error(f"Error al obtener actividades: {e}")
        return []

def procesar_datos_actividades(datos_api):
    """Procesa los datos crudos de la API a un formato estructurado"""
    datos_actividades = []
    for act in datos_api:
        participantes = procesar_participantes(act.get("beneficiarios", []))
        
        datos_actividades.append({
            "Nombre de la actividad": act.get("nombre", "Sin nombre"),
            "Lugar": act.get("lugar", {}).get("nombre", "Sin lugar"),
            "Fecha": act.get("fechaInicio", "Sin fecha"),
            "Participantes": participantes
        })
    return datos_actividades

def procesar_participantes(beneficiarios):
    """Procesa la lista de beneficiarios a participantes"""
    participantes = []
    for bene in beneficiarios:
        municipio = bene.get("municipio")
        sector = bene.get("sector")
        tipo_doc = bene.get("tipoiden")
        genero = bene.get("genero")
        rango_edad = bene.get("rangoedad")
        organizaciones = bene.get("organizaciones", [])

        organizacion_nombre = organizaciones[0]["nombre"] if organizaciones else "N/A"

        participante = {
            "Nombre": f"{bene.get('nombre1', '')} {bene.get('apellido1', '')} {bene.get('apellido2', '')}",
            "Documento": bene.get("identificacion", "N/A"),
            "Celular": bene.get("celular", "N/A"),
            "Edad": rango_edad["rango"] if rango_edad else "N/A",
            "Sexo": genero["nombre"] if genero else "N/A",
            "Municipio": municipio["nombre"] if municipio else "N/A",
            "Organizacion": organizacion_nombre,
            "Sector": sector["nombre"] if sector else "N/A",
            "Tipo Documento": tipo_doc["nombre"] if tipo_doc else "N/A"
        }
        participantes.append(participante)
    return participantes

# Obtener actividades recientes
def obtener_actividades_recientes():
    """Obtiene los eventos desde la API y devuelve solo los datos necesarios"""
    try:
        response = requests.get(f"{BASE_URL}/Actividades", verify=False)
        response.raise_for_status()
        eventos_api = response.json()
        
        # Procesamos solo los datos que necesitamos
        eventos_procesados = []
        for evento in eventos_api:
            eventos_procesados.append({
                "nombre": evento.get("nombre", "Sin nombre"),
                "fecha_inicio": format_fecha(evento.get("fechaInicio")),
                "fecha_fin": format_fecha(evento.get("fechaFinal")),
                "lugar": evento.get("lugar", {}).get("nombre", "Sin lugar")
            })
        
        return eventos_procesados
        
    except requests.exceptions.RequestException as e:
        st.error(f"Error al obtener eventos: {e}")
        return []

def format_fecha(fecha_str):
    """Formatea la fecha ISO a un formato más legible"""
    if not fecha_str:
        return "Sin fecha"
    
    try:
        fecha = datetime.fromisoformat(fecha_str.replace("Z", "+00:00"))
        return fecha.strftime("%d/%m/%Y")
    except ValueError:
        return fecha_str  # Si falla el parseo, devolver el original