import streamlit as st
from utils.utils import navigate_to
from assets.data import obtener_lista_municipios
from api.posts import crear_actividad_completa
import datetime

def pantalla_registro_evento():
    st.markdown("## Registro de Actividad/Evento")
    
    # Inicializar estado de sesión
    if 'actividad_temp' not in st.session_state:
        st.session_state.actividad_temp = {
            'nombre': '',
            'fecha_inicio': None,
            'fecha_fin': None,
            'lugar': '',
            'participantes': []
        }

    if st.button("⬅️ Atrás"):
        navigate_to("registrar")
        st.rerun()

    # Campos principales de la actividad
    nombre_actividad = st.text_input("Nombre Actividad/Taller*", 
                                   value=st.session_state.actividad_temp['nombre'])
    col1, col2 = st.columns(2)
    with col1:
        fecha_inicio = st.date_input("Fecha inicio*", 
                                   value=st.session_state.actividad_temp['fecha_inicio'] or datetime.date.today())
    with col2:
        fecha_fin = st.date_input("Fecha fin*", 
                                value=st.session_state.actividad_temp['fecha_fin'] or datetime.date.today())
    
    # Selección de lugar (asumiendo que existe endpoint para lugares)
    lugares = obtener_lista_municipios(formato='select')
    lugar_seleccionado = st.selectbox(
        "Lugar*", 
        options=lugares,
        index=lugares.index(st.session_state.actividad_temp['lugar']) if st.session_state.actividad_temp['lugar'] in lugares else 0
    )

    # Sección de participantes
    st.markdown("### Lista de participantes registrados")
    if st.session_state.actividad_temp['participantes']:
        st.table([{"Documento": p['identificacion'], "Nombre": p['nombre']} 
                for p in st.session_state.actividad_temp['participantes']])
    else:
        st.write("No hay participantes registrados aún")

    # Botones de acción
    col1, col2, col3 = st.columns(3)
    with col1:
        if st.button("➕ Registrar participante"):
            navigate_to('registrar', "registro_participante")
    with col2:
        uploaded_file = st.file_uploader("Subir lista (.csv)", type=["csv"])
    with col3:
        if st.button("💾 Guardar Actividad", type="primary"):
            if validar_actividad():
                guardar_actividad()

def validar_actividad():
    errores = []
    if not st.session_state.actividad_temp['nombre'].strip():
        errores.append("Nombre de la actividad es obligatorio")
    if not st.session_state.actividad_temp['fecha_inicio']:
        errores.append("Fecha de inicio es obligatoria")
    if not st.session_state.actividad_temp['lugar']:
        errores.append("Lugar es obligatorio")
    
    for error in errores:
        st.error(error)
    return len(errores) == 0

def guardar_actividad():
    actividad_data = {
        "nombre": st.session_state.actividad_temp['nombre'],
        "fechaInicio": st.session_state.actividad_temp['fecha_inicio'].isoformat(),
        "fechaFinal": st.session_state.actividad_temp['fecha_fin'].isoformat(),
        "lugar": {
            "id": 0,  # Asumiendo que se implementa lógica para lugares
            "nombre": st.session_state.actividad_temp['lugar']
        },
        "beneficiarios": st.session_state.actividad_temp['participantes']
    }
    
    resultado = crear_actividad_completa(actividad_data)
    if resultado:
        st.success("Actividad registrada exitosamente!")
        st.session_state.actividad_temp = {}  # Resetear estado
        st.balloons()