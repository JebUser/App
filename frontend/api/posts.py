import requests
import json
import urllib3
from datetime import datetime
import streamlit as st
import time

urllib3.disable_warnings(urllib3.exceptions.InsecureRequestWarning)

BASE_URL = "https://localhost:57740/api"

def post_to_api(endpoint: str, data: dict):
    """Función genérica para POST a cualquier endpoint"""
    url = f"{BASE_URL}/{endpoint}"
    
    try:
        # Debug: mostrar la URL y los datos que se enviarán
        st.write(f"🔍 **Debug - URL:** {url}")
        st.write(f"🔍 **Debug - Datos enviados:**")
        st.json(data)
        
        response = requests.post(
            url,
            headers={"Content-Type": "application/json"},
            data=json.dumps(data, ensure_ascii=False),
            verify=False,
            timeout=30
        )
        
        # Debug: mostrar la respuesta
        st.write(f"🔍 **Debug - Status Code:** {response.status_code}")
        st.write(f"🔍 **Debug - Response Headers:** {dict(response.headers)}")
        
        if response.status_code in [200, 201]:
            try:
                # Intentar parsear JSON si hay contenido
                if response.text.strip():
                    response_data = response.json()
                    st.write(f"🔍 **Debug - Response Data:**")
                    st.json(response_data)
                    return response_data
                else:
                    # Si no hay contenido, la API devolvió 201 sin body
                    # Extraer ID del header Location si existe
                    location = response.headers.get('Location', '')
                    st.write(f"🔍 **Debug - Response vacío, Location:** {location}")
                    
                    if location and endpoint in ["TipoProyectos", "Tipoorgs", "Tipoapoyos", "Lineaproductivas"]:
                        # Extraer ID del path: /api/TipoProyectos/123 -> 123
                        try:
                            tipo_id = int(location.split('/')[-1])
                            return {
                                "id": tipo_id,
                                "nombre": data.get("nombre", "")
                            }
                        except (ValueError, IndexError):
                            st.error(f"No se pudo extraer ID de Location: {location}")
                            return None
                    
                    return {"success": True, "location": location}
                    
            except json.JSONDecodeError:
                st.write(f"🔍 **Debug - Response Text:** {response.text}")
                # Para tipos, intentar extraer ID del Location
                if endpoint in ["TipoProyectos", "Tipoorgs", "Tipoapoyos", "Lineaproductivas"] and response.status_code == 201:
                    location = response.headers.get('Location', '')
                    if location:
                        try:
                            tipo_id = int(location.split('/')[-1])
                            return {
                                "id": tipo_id,
                                "nombre": data.get("nombre", "")
                            }
                        except:
                            pass
                return {"success": True, "text": response.text}
        else:
            st.error(f"❌ Error en API ({endpoint})")
            st.error(f"Status: {response.status_code}")
            st.error(f"Response: {response.text}")
            
            # Intentar parsear el error como JSON
            try:
                error_data = response.json()
                st.json(error_data)
            except:
                pass
                
            return None
            
    except requests.exceptions.Timeout:
        st.error(f"⏰ Timeout en API ({endpoint})")
        return None
    except requests.exceptions.ConnectionError:
        st.error(f"🔌 Error de conexión en API ({endpoint})")
        return None
    except Exception as e:
        st.error(f"💥 Error inesperado: {str(e)} en {endpoint}")
        return None

def get_from_api(endpoint: str):
    """Función genérica para GET de cualquier endpoint"""
    url = f"{BASE_URL}/{endpoint}"
    try:
        response = requests.get(url, verify=False, timeout=10)
        if response.status_code == 200:
            return response.json()
        else:
            st.error(f"Error al obtener {endpoint}: {response.status_code}")
            return []
    except Exception as e:
        st.error(f"Error de conexión al obtener {endpoint}: {e}")
        return []

# Funciones específicas para proyectos
def crear_tipo_proyecto(nombre: str):
    """Crea un nuevo tipo de proyecto"""
    data = {"nombre": nombre.strip()}
    resultado = post_to_api("TipoProyectos", data)
    
    # Si el resultado tiene ID 0, intentar obtener el tipo recién creado
    if resultado and resultado.get("id") == 0:
        st.warning("⚠️ API devolvió ID 0, intentando obtener el tipo recién creado...")
        # Hacer una petición GET para obtener todos los tipos y encontrar el nuestro
        try:
            response = requests.get(
                f"{BASE_URL}/TipoProyectos",
                verify=False,
                timeout=10
            )
            if response.status_code == 200:
                tipos = response.json()
                # Buscar el tipo con el nombre que acabamos de crear
                tipo_encontrado = next(
                    (t for t in tipos if t.get("nombre") == nombre.strip()), 
                    None
                )
                if tipo_encontrado:
                    st.success(f"✅ Tipo encontrado con ID: {tipo_encontrado['id']}")
                    return tipo_encontrado
        except Exception as e:
            st.error(f"Error al obtener tipos: {e}")
    
    return resultado

def obtener_tipos_proyectos():
    """Obtiene la lista actual de tipos de proyecto"""
    return get_from_api("TipoProyectos")

def crear_proyecto_completo(proyecto_data: dict):
    """Crea un proyecto con la estructura completa"""
    # Asegurar que la estructura sea correcta según el API
    proyecto_limpio = {
        "id": 0,  # La API espera un ID, normalmente 0 para nuevos registros
        "nombre": proyecto_data.get("nombre", ""),
        "fechainicio": proyecto_data.get("fechainicio", ""),
        "fechafinal": proyecto_data.get("fechafinal", ""),
        "tipoproyecto": proyecto_data.get("tipoproyecto", {}),
        "actividades": proyecto_data.get("actividades", [])
    }
    
    return post_to_api("Proyectos", proyecto_limpio)

# Funciones específicas para organizaciones
def crear_tipo_organizacion(nombre: str):
    """Crea un nuevo tipo de organización"""
    data = {"nombre": nombre.strip()}
    resultado = post_to_api("Tipoorgs", data)
    
    # Si el resultado tiene ID 0, intentar obtener el tipo recién creado
    if resultado and resultado.get("id") == 0:
        st.warning("⚠️ API devolvió ID 0, intentando obtener el tipo de organización recién creado...")
        try:
            response = requests.get(
                f"{BASE_URL}/Tipoorgs",
                verify=False,
                timeout=10
            )
            if response.status_code == 200:
                tipos = response.json()
                # Buscar el tipo con el nombre que acabamos de crear
                tipo_encontrado = next(
                    (t for t in tipos if t.get("nombre") == nombre.strip()), 
                    None
                )
                if tipo_encontrado:
                    st.success(f"✅ Tipo de organización encontrado con ID: {tipo_encontrado['id']}")
                    return tipo_encontrado
        except Exception as e:
            st.error(f"Error al obtener tipos de organización: {e}")
    
    return resultado

def crear_tipo_apoyo(nombre: str):
    """Crea un nuevo tipo de apoyo"""
    data = {"nombre": nombre.strip()}
    resultado = post_to_api("Tipoapoyos", data)
    
    # Si el resultado tiene ID 0, intentar obtener el tipo recién creado
    if resultado and resultado.get("id") == 0:
        st.warning("⚠️ API devolvió ID 0, intentando obtener el tipo de apoyo recién creado...")
        try:
            response = requests.get(
                f"{BASE_URL}/Tipoapoyos",
                verify=False,
                timeout=10
            )
            if response.status_code == 200:
                tipos = response.json()
                # Buscar el tipo con el nombre que acabamos de crear
                tipo_encontrado = next(
                    (t for t in tipos if t.get("nombre") == nombre.strip()), 
                    None
                )
                if tipo_encontrado:
                    st.success(f"✅ Tipo de apoyo encontrado con ID: {tipo_encontrado['id']}")
                    return tipo_encontrado
        except Exception as e:
            st.error(f"Error al obtener tipos de apoyo: {e}")
    
    return resultado

def crear_linea_productiva(nombre: str):
    """Crea una nueva línea productiva"""
    data = {"nombre": nombre.strip()}
    resultado = post_to_api("Lineaprods", data)
    
    # Si el resultado tiene ID 0 o no tiene la estructura esperada, intentar obtener la línea recién creada
    if (resultado is None or 
        resultado.get("id") == 0 or 
        not isinstance(resultado, dict) or 
        "id" not in resultado or 
        "nombre" not in resultado):
        
        st.warning("⚠️ Respuesta inesperada de la API, intentando obtener la línea recién creada...")
        
        try:
            # Esperar un momento para asegurar que la creación se complete
            time.sleep(1)
            
            # Obtener todas las líneas y buscar la recién creada
            lineas = obtener_lineas_productivas()
            if lineas:
                linea_encontrada = next(
                    (l for l in lineas if l.get("nombre") == nombre.strip()), 
                    None
                )
                if linea_encontrada:
                    st.success(f"✅ Línea productiva encontrada con ID: {linea_encontrada['id']}")
                    return linea_encontrada
        
        except Exception as e:
            st.error(f"Error al obtener líneas productivas: {e}")
            return None
    
    return resultado

def obtener_tipos_organizaciones():
    """Obtiene la lista actual de tipos de organización"""
    return get_from_api("Tipoorgs")

def obtener_tipos_apoyos():
    """Obtiene la lista actual de tipos de apoyo"""
    return get_from_api("Tipoapoyos")

def obtener_lineas_productivas():
    """Obtiene la lista actual de líneas productivas"""
    return get_from_api("Lineaprods")

def crear_organizacion_completa(organizacion_data: dict):
    """Crea una organización con la estructura completa"""
    return post_to_api("Organizaciones", organizacion_data)

def crear_tipo_actividad(nombre: str):
    """Crea un nuevo tipo de actividad"""
    data = {"nombre": nombre.strip()}
    return post_to_api("Tipoactividades", data)

def obtener_tipos_actividades():
    """Obtiene la lista actual de tipos de actividad"""
    return get_from_api("Tipoactividades")