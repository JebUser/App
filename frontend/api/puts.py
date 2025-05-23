import requests
import urllib3
import streamlit as st

urllib3.disable_warnings(urllib3.exceptions.InsecureRequestWarning)

BASE_URL = "https://localhost:7032/api"

def modificar_actividad(actividad:dict):
    """Modifica una actividad"""
    try:
        response = requests.put(f"{BASE_URL}/Actividades/{actividad["id"]}", json=actividad, verify=False)
        response.raise_for_status()
        return response.status_code
    except requests.exceptions.RequestException as e:
        st.error(f"Error al modificar la actividad: {e}")
        return None

def modificar_beneficiario(beneficiario:dict):
    """Modifica un beneficiario"""
    try:
        response = requests.put(f"{BASE_URL}/Beneficiarios/{beneficiario["id"]}", json=beneficiario, verify=False)
        response.raise_for_status()
        return response.status_code
    except requests.exceptions.RequestException as e:
        st.error(f"Error al modificar el beneficiario: {e}")
        return None

def modificar_organizacion(organizacion:dict):
    """Modifica una organización"""
    try:
        response = requests.put(f"{BASE_URL}/Organizaciones/{organizacion["id"]}", json=organizacion, verify=False)
        response.raise_for_status()
        return response.status_code
    except requests.exceptions.RequestException as e:
        st.error(f"Error al modificar la organización: {e}")
        return None
    
def modificar_proyecto(proyecto:dict):
    """Modifica un proyecto"""
    try:
        response = requests.put(f"{BASE_URL}/Proyectos/{proyecto["id"]}", json=proyecto, verify=False)
        response.raise_for_status()
        return response.status_code
    except requests.exceptions.RequestException as e:
        st.error(f"Error al modificar el proyecto: {e}")
        return None