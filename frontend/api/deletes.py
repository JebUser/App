import requests
import urllib3
import streamlit as st

urllib3.disable_warnings(urllib3.exceptions.InsecureRequestWarning)

BASE_URL = "https://localhost:57740/api"

def eliminar_actividad(actividad_id:int):
    """Modifica una actividad"""
    try:
        response = requests.delete(f"{BASE_URL}/Actividades/{actividad_id}", verify=False)
        response.raise_for_status()
        return response.status_code
    except requests.exceptions.RequestException as e:
        st.error(f"Error al eliminar la actividad: {e}")
        return None

def eliminar_beneficiario(beneficiario_id:int):
    """Modifica un beneficiario"""
    try:
        response = requests.delete(f"{BASE_URL}/Beneficiarios/{beneficiario_id}", verify=False)
        response.raise_for_status()
        return response.status_code
    except requests.exceptions.RequestException as e:
        st.error(f"Error al eliminar el beneficiario: {e}")
        return None

def eliminar_organizacion(organizacion_id:int):
    """Modifica una organización"""
    try:
        response = requests.delete(f"{BASE_URL}/Organizaciones/{organizacion_id}", verify=False)
        response.raise_for_status()
        return response.status_code
    except requests.exceptions.RequestException as e:
        st.error(f"Error al eliminar la organización: {e}")
        return None
    
def eliminar_proyecto(proyecto_id:int):
    """Modifica un proyecto"""
    try:
        response = requests.delete(f"{BASE_URL}/Proyectos/{proyecto_id}", verify=False)
        response.raise_for_status()
        return response.status_code
    except requests.exceptions.RequestException as e:
        st.error(f"Error al eliminar el proyecto: {e}")
        return None