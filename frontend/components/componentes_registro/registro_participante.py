import streamlit as st
from utils.utils import navigate_to
from assets.data import (
    obtener_lista_generos,
    obtener_lista_tipos_identificacion,
    obtener_lista_municipios,
    obtener_lista_tipos_beneficiario,
    obtener_lista_sectores,
    obtener_lista_organizaciones,
    obtener_lista_rango_edades,
    obtener_lista_grupos_etnicos
)

def pantalla_registro_participante():
    st.markdown("## Registro de Participante")
    
    if st.button("⬅️ Atrás"):
        navigate_to('registrar', 'registro_evento')
        st.rerun()

    with st.form("form_participante"):
        # Campos del formulario
        nombre_completo = st.text_input("Nombre completo*")
        genero = st.selectbox("Género*", options=[g['nombre'] for g in obtener_lista_generos()])
        rango_edad = st.selectbox("Rango de edad*", 
                                options = obtener_lista_rango_edades(),
                                format_func=lambda x: x['rango'],
                                key="tipoedad_select"
                                )
        grupo_etnico = st.selectbox("Grupo étnico*", 
                                options = obtener_lista_grupos_etnicos(),
                                format_func=lambda x: x['nombre'],
                                key="tipogrupo_select"
                                )
        tipo_id = st.selectbox("Tipo documento*", options=obtener_lista_tipos_identificacion(formato='select'))
        num_doc = st.text_input("Número de documento*")
        municipio = st.selectbox("Municipio*", options=obtener_lista_municipios(formato='select'))
        tipo_beneficiario = st.selectbox("Tipo beneficiario*", options=obtener_lista_tipos_beneficiario(formato='select'))
        
        # Obtener lista completa de organizaciones
        organizaciones_completas = obtener_lista_organizaciones(formato='completo')
        organizacion = st.selectbox("Organización", 
                                    options=[None] + organizaciones_completas,  # Agregar opción None al inicio
                                    format_func=lambda x: "Seleccionar..." if x is None else f"{x['nombre']} - ({x['municipio']['nombre']} - {x['municipio']['departamento']['nombre']})",
                                    key="organizacion_select"
                                    )
        
        sector = st.selectbox("Sector", options=obtener_lista_sectores(formato='select'))
        celular = st.text_input("Celular")

        if st.form_submit_button("💾 Agregar Participante"):
            if validar_participante(nombre_completo, rango_edad, genero, tipo_id, num_doc, municipio, tipo_beneficiario):
                agregar_participante(nombre_completo, rango_edad, genero, tipo_id, num_doc, municipio, tipo_beneficiario, organizacion, sector, celular, grupo_etnico)

def validar_participante(nombre, rango_edad, genero, tipo_id, num_doc, municipio, tipo_beneficiario):
    errores = []
    
    if not nombre.strip():
        errores.append("Nombre completo es obligatorio")
    if not genero:
        errores.append("Género es obligatorio")
    if not rango_edad:
        errores.append("Rango de edad es obligatorio")
    if not tipo_id:
        errores.append("Tipo de documento es obligatorio")
    if not num_doc.strip():
        errores.append("Número de documento es obligatorio")
    if not municipio:
        errores.append("Municipio es obligatorio")
    if not tipo_beneficiario:
        errores.append("Tipo beneficiario es obligatorio")
    
    for error in errores:
        st.error(error)
    return len(errores) == 0

def obtener_ids_por_nombre(nombre, lista_datos):
    """Función auxiliar para obtener ID basado en nombre"""
    for item in lista_datos:
        if item['nombre'] == nombre:
            return item['id']
    return 0

def agregar_participante(nombre_completo, rango_edad, genero, tipo_id, num_doc, municipio, tipo_beneficiario, organizacion, sector, celular, grupo_etnico):
    # Separar nombre y apellidos
    partes_nombre = nombre_completo.strip().split()
    nombre1 = partes_nombre[0] if len(partes_nombre) > 0 else ""
    nombre2 = partes_nombre[1] if len(partes_nombre) > 2 else ""
    apellido1 = partes_nombre[1] if len(partes_nombre) == 2 else (partes_nombre[2] if len(partes_nombre) > 2 else "")
    apellido2 = partes_nombre[3] if len(partes_nombre) > 3 else ""
    
    # Obtener listas para mapear nombres a IDs
    generos = obtener_lista_generos()
    tipos_id = obtener_lista_tipos_identificacion()
    municipios = obtener_lista_municipios()
    tipos_beneficiario = obtener_lista_tipos_beneficiario()
    sectores = obtener_lista_sectores()
    
    # Extraer nombre del municipio (formato: "Nombre - Departamento")
    nombre_municipio = municipio.split(' - ')[0] if ' - ' in municipio else municipio
    
    # Buscar IDs correspondientes
    genero_id = obtener_ids_por_nombre(genero, generos)
    tipo_id_id = obtener_ids_por_nombre(tipo_id, tipos_id)
    municipio_obj = next((m for m in municipios if m['nombre'] == nombre_municipio), None)
    tipo_bene_id = obtener_ids_por_nombre(tipo_beneficiario, tipos_beneficiario)

    # Manejar grupo étnico
    grupo_etnico_data = {
        "id": grupo_etnico.get('id', 0),
        "nombre": grupo_etnico.get('nombre', '') if grupo_etnico.get('nombre') != 'Ninguno' else ''
    }
    
    # Construir estructura del beneficiario según el JSON requerido
    beneficiario_data = {
        "id": 0,
        "identificacion": num_doc.strip(),
        "nombre1": nombre1,
        "nombre2": nombre2,
        "apellido1": apellido1,
        "apellido2": apellido2,
        "celular": celular.strip() if celular else "",
        "firma": None,
        "tipoiden": {
            "id": tipo_id_id,
            "nombre": tipo_id
        },
        "genero": {
            "id": genero_id,
            "nombre": genero
        },
        "rangoedad": {
            "id": rango_edad['id'],
            "rango": rango_edad['rango']
        },
        "grupoetnico": grupo_etnico_data,
        "tipobene": {
            "id": tipo_bene_id,
            "nombre": tipo_beneficiario
        },
        "municipio": {
            "id": municipio_obj['id'] if municipio_obj else 0,
            "nombre": nombre_municipio,
            "departamento": {
                "id": municipio_obj['departamento_id'] if municipio_obj else 0,
                "nombre": municipio_obj['departamento_nombre'] if municipio_obj else ""
            }
        },
        "sector": None,
        "organizaciones": []
    }
    
    # Agregar sector si está seleccionado
    if sector and sector.strip():
        sector_id = obtener_ids_por_nombre(sector, sectores)
        beneficiario_data["sector"] = {
            "id": sector_id,
            "nombre": sector
        }
    
    # Agregar organización si está seleccionada - AHORA USANDO EL OBJETO COMPLETO
    if organizacion and organizacion is not None:
        # Como ya tenemos el objeto completo de la organización, solo necesitamos estructurarlo
        org_completa = {
            "id": organizacion.get('id', 0),
            "nombre": organizacion.get('nombre', ''),
            "municipio": {
                "id": organizacion.get('municipio', {}).get('id', 0),
                "nombre": organizacion.get('municipio', {}).get('nombre', ''),
                "departamento": {
                    "id": organizacion.get('municipio', {}).get('departamento', {}).get('id', 0),
                    "nombre": organizacion.get('municipio', {}).get('departamento', {}).get('nombre', '')
                }
            },
            "nit": organizacion.get('nit'),  # Puede ser null
            "integrantes": organizacion.get('integrantes'),  # Puede ser null
            "nummujeres": organizacion.get('nummujeres'),  # Puede ser null
            "orgmujeres": organizacion.get('orgmujeres'),  # Puede ser null
            "tipoorg": organizacion.get('tipoorg'),  # Puede ser null
            "tipoactividad": organizacion.get('tipoactividad'),  # Puede ser null
            "lineaprod": organizacion.get('lineaprod'),  # Puede ser null
            "tipoapoyo": organizacion.get('tipoapoyo')  # Puede ser null
        }
        beneficiario_data["organizaciones"] = [org_completa]
    
    # Agregar participante a la lista temporal (no guardar individualmente)
    if 'actividad_temp' not in st.session_state:
        st.session_state.actividad_temp = {
            'nombre': '',
            'fecha_inicio': None,
            'fecha_fin': None,
            'lugar': '',
            'participantes': []
        }
    
    # Verificar si el participante ya existe (por documento)
    participante_existente = any(
        p['identificacion'] == num_doc.strip() 
        for p in st.session_state.actividad_temp['participantes']
    )
    
    if participante_existente:
        st.warning(f"Ya existe un participante con documento {num_doc}")
        return
    
    st.session_state.actividad_temp['participantes'].append(beneficiario_data)
    st.success(f"Participante {nombre_completo} agregado correctamente!")
    
    # Regresar a la pantalla de registro de evento
    navigate_to('registrar', 'registro_evento')
    st.rerun()