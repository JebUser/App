import streamlit as st
from utils.utils import navigate_to
from assets.data import (
    obtener_lista_generos,
    obtener_lista_tipos_identificacion,
    obtener_lista_municipios,
    obtener_lista_tipos_beneficiario,
    obtener_lista_sectores,
    obtener_lista_organizaciones
)
from api.posts import crear_beneficiario

def pantalla_registro_participante():
    st.markdown("## Registro de Participante")
    
    if st.button("⬅️ Atrás"):
        navigate_to('registrar', 'registro_evento')
        st.rerun()

    with st.form("form_participante"):
        # Campos del formulario
        nombre = st.text_input("Nombre completo*")
        genero = st.selectbox("Género*", options=[g['nombre'] for g in obtener_lista_generos()])
        tipo_id = st.selectbox("Tipo documento*", options=obtener_lista_tipos_identificacion(formato='select'))
        num_doc = st.text_input("Número de documento*")
        municipio = st.selectbox("Municipio*", options=obtener_lista_municipios(formato='select'))
        tipo_beneficiario = st.selectbox("Tipo beneficiario*", options=obtener_lista_tipos_beneficiario(formato='select'))
        organizacion = st.selectbox("Organización", options=obtener_lista_organizaciones(formato='select'))
        sector = st.selectbox("Sector", options=obtener_lista_sectores(formato='select'))
        celular = st.text_input("Celular")

        if st.form_submit_button("💾 Guardar Participante"):
            if validar_participante(locals()):
                guardar_participante(locals())

def validar_participante(data):
    campos_obligatorios = {
        'nombre': data['nombre'],
        'genero': data['genero'],
        'tipo_id': data['tipo_id'],
        'num_doc': data['num_doc'],
        'municipio': data['municipio'],
        'tipo_beneficiario': data['tipo_beneficiario']
    }
    
    errores = [f"{k.replace('_', ' ').title()} es obligatorio" 
              for k, v in campos_obligatorios.items() if not v]
    
    for error in errores:
        st.error(error)
    return len(errores) == 0

def guardar_participante(data):
    beneficiario_data = {
        "identificacion": data['num_doc'],
        "nombre1": data['nombre'].split()[0],
        "apellido1": " ".join(data['nombre'].split()[1:]) if len(data['nombre'].split()) > 1 else "",
        "celular": data['celular'],
        "tipoiden": {"nombre": data['tipo_id']},
        "genero": {"nombre": data['genero']},
        "municipio": {"nombre": data['municipio'].split(' - ')[0]},
        "tipobene": {"nombre": data['tipo_beneficiario']},
        "sector": {"nombre": data['sector']} if data['sector'] else None,
        "organizaciones": [{"nombre": data['organizacion']}] if data['organizacion'] else []
    }
    
    if crear_beneficiario(beneficiario_data):
        st.session_state.actividad_temp['participantes'].append(beneficiario_data)
        st.success("Participante registrado!")
        navigate_to('registrar', 'registro_evento')