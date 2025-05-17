from api.gets import obtener_generos, obtener_grupos_etnicos, obtener_lineas_producto, obtener_municipios, obtener_organizaciones, obtener_sectores,  obtener_tipos_beneficiario, obtener_tipos_identificacion, obtener_tipos_organizacion       # Importación actualizada
import streamlit as st

def obtener_lista_generos():
    """Obtiene y formatea los géneros para uso en la aplicación"""
    datos_api = obtener_generos()
    if datos_api is None:
        return []
    
    return [{
        "id": g.get("id", 0),
        "nombre": g.get("nombre", "Sin nombre")
    } for g in datos_api]


def obtener_lista_grupos_etnicos():
    datos_api = obtener_grupos_etnicos()
    if datos_api is None:
        return []
    
    return [{
        "id": g.get("id", 0),
        "nombre": g.get("nombre", "Sin nombre"),
        "descripcion": g.get("descripcion", "")  # Agrega más campos si es necesario
    } for g in datos_api]


def obtener_lista_lineas_producto():
    datos_api = obtener_lineas_producto()
    if datos_api is None:
        return []
    
    return [{
        "id": lp.get("id", 0),
        "codigo": lp.get("codigo", ""),
        "nombre": lp.get("nombre", "Sin nombre"),
        "descripcion": lp.get("descripcion", ""),
        "activo": lp.get("activo", False)
    } for lp in datos_api]

def obtener_lista_municipios(formato='completo'):
    """
    Obtiene y formatea los municipios para la aplicación
    Formatos disponibles:
    - 'completo': Retorna todos los datos estructurados
    - 'select': Retorna lista para dropdowns [nombre - departamento]
    - 'minimo': Retorna solo id y nombre
    """
    datos_api = obtener_municipios()
    if datos_api is None:
        return []

    if formato == 'select':
        return [f"{m['nombre']} - {m['departamento']['nombre']}" for m in datos_api]
    elif formato == 'minimo':
        return [{"id": m["id"], "nombre": m["nombre"]} for m in datos_api]
    else:  # formato completo por defecto
        return [{
            "id": m.get("id", 0),
            "nombre": m.get("nombre", "Sin nombre"),
            "departamento_id": m.get("departamento", {}).get("id", 0),
            "departamento_nombre": m.get("departamento", {}).get("nombre", "Sin departamento")
        } for m in datos_api]
    

def obtener_lista_organizaciones(formato='completo'):
    datos_api = obtener_organizaciones()
    if datos_api is None:
        return []

    if formato == 'select':
        return [f"{org['nombre']} - {org['municipio']['nombre']}" for org in datos_api]
    elif formato == 'minimo':
        return [{
            "id": org["id"],
            "nombre": org["nombre"],
            "municipio": org["municipio"]["nombre"]
        } for org in datos_api]
    elif formato == 'detallado':
        return [{
            "id": org.get("id"),
            "nombre": org.get("nombre"),
            "municipio": org.get("municipio", {}).get("nombre"),
            "departamento": org.get("municipio", {}).get("departamento", {}).get("nombre"),
            "nit": org.get("nit", "No registrado"),
            "integrantes": org.get("integrantes", 0),
            "mujeres": org.get("nummujeres", 0),
            "tipo_organizacion": org.get("tipoorg", "No especificado"),
            "tipo_actividad": org.get("tipoactividad", "No especificado"),
            "linea_producto": org.get("lineaprod", "No especificado"),
            "tipo_apoyo": org.get("tipoapoyo", "No especificado")
        } for org in datos_api]
    else:  # formato completo por defecto
        return datos_api  # Retorna los datos originales de la API
    

def obtener_lista_sectores(formato='normal'):
    datos_api = obtener_sectores()
    if datos_api is None:
        return [] if formato != 'diccionario' else {}

    if formato == 'select':
        return [sector['nombre'] for sector in datos_api]
    elif formato == 'diccionario':
        return {sector['id']: sector['nombre'] for sector in datos_api}
    else:  # formato normal por defecto
        return [{
            "id": sector.get("id", 0),
            "nombre": sector.get("nombre", "Sin nombre")
        } for sector in datos_api]
    
from api.gets import (
    # ... importaciones existentes
    obtener_tipos_apoyo  # Nueva importación
)


def obtener_lista_tipos_apoyo(formato='normal'):
    datos_api = obtener_tipos_apoyo()
    
    if formato == 'select':
        return [tipo['nombre'] for tipo in datos_api if isinstance(tipo, dict)]
    elif formato == 'diccionario':
        return {tipo['id']: tipo['nombre'] for tipo in datos_api if isinstance(tipo, dict)}
    else:  # formato normal
        return [{
            "id": tipo.get("id", 0),
            "nombre": tipo.get("nombre", "Sin nombre"),
            # Campos adicionales pueden agregarse aquí cuando la API los incluya
            "categoria": tipo.get("categoria", None),
            "activo": tipo.get("activo", False)
        } for tipo in datos_api if isinstance(tipo, dict)]
    

def obtener_lista_tipos_beneficiario(formato='normal'):
    datos_api = obtener_tipos_beneficiario()
    if datos_api is None:
        return [] if formato != 'diccionario' else {}

    if formato == 'select':
        return [tipo['nombre'] for tipo in datos_api]
    elif formato == 'diccionario':
        return {tipo['id']: tipo['nombre'] for tipo in datos_api}
    elif formato == 'extendido':
        return [{
            "id": tipo.get("id"),
            "nombre": tipo.get("nombre"),
            "codigo": tipo.get("codigo", ""),  # Por si agregas este campo después
            "descripcion": tipo.get("descripcion", "")  # Campo opcional
        } for tipo in datos_api]
    else:  # formato normal
        return [{
            "id": tipo.get("id", 0),
            "nombre": tipo.get("nombre", "Sin nombre")
        } for tipo in datos_api]
    

from api.gets import (
    # ... importaciones existentes
    obtener_tipos_identificacion  # Nueva importación
)

# ... (funciones existentes)

def obtener_lista_tipos_identificacion(formato='normal'):
    datos_api = obtener_tipos_identificacion()
    if datos_api is None:
        return [] if formato not in ['diccionario', 'validacion'] else {}

    if formato == 'select':
        return [tipo['nombre'] for tipo in datos_api]
    elif formato == 'diccionario':
        return {tipo['id']: tipo['nombre'] for tipo in datos_api}
    elif formato == 'validacion':
        return {tipo['nombre'].lower(): tipo['id'] for tipo in datos_api}
    else:  # formato normal
        return [{
            "id": tipo.get("id", 0),
            "nombre": tipo.get("nombre", "Sin nombre"),
            "abreviatura": tipo.get("abreviatura", "")  # Por si existe en el futuro
        } for tipo in datos_api]
    

def obtener_lista_tipos_organizacion(formato='normal'):
    datos_api = obtener_tipos_organizacion()
    
    # Manejo seguro para todos los formatos
    if not datos_api:
        return [] if formato != 'diccionario' else {}

    if formato == 'select':
        return [tipo['nombre'] for tipo in datos_api if isinstance(tipo, dict)]
    elif formato == 'diccionario':
        return {tipo['id']: tipo['nombre'] for tipo in datos_api if isinstance(tipo, dict)}
    elif formato == 'extendido':
        return [{
            "id": tipo.get("id"),
            "nombre": tipo.get("nombre"),
            "descripcion": tipo.get("descripcion", ""),
            "activo": tipo.get("activo", True),  # Por defecto True si no existe
            "codigo": tipo.get("codigo", "")  # Campo opcional
        } for tipo in datos_api if isinstance(tipo, dict)]
    else:  # formato normal
        return [{
            "id": tipo.get("id", 0),
            "nombre": tipo.get("nombre", "Sin nombre")
        } for tipo in datos_api if isinstance(tipo, dict)]