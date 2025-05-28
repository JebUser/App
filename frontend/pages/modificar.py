import streamlit as st
from utils.utils import init_session_state, navigate_to

# Pantallas
from components.componentes_modificacion.inicio_modificacion import inicio_pagina_mod
from components.componentes_modificacion.ver_modificar_organizacion import pantalla_modificar_organizacion
from components.componentes_modificacion.actualizar_organizacion import pantalla_actualizar_organizacion
from components.componentes_modificacion.ver_modificar_proyecto import pantalla_modificar_proyecto
from components.componentes_modificacion.actualizar_proyecto import pantalla_actualizar_proyecto
from components.componentes_modificacion.ver_modificar_actividad import pantalla_modificar_actividad
from components.componentes_modificacion.actualizar_actividad import pantalla_actualizar_actividad
from components.componentes_modificacion.ver_modificar_beneficiario import pantalla_modificar_beneficiario
from components.componentes_modificacion.actualizar_beneficiario import pantalla_actualizar_beneficiario

# Inicializar el estado de la sesión
init_session_state()

if st.session_state.current_page != 'modificar':
    navigate_to('modificar')

# Solo mostrar contenido si estamos en la página principal 'modificar'
st.title("✏ Modificar información")

# Mostrar subpágina correspondiente
if st.session_state.subpage is None:
    # Pantalla de inicio de modificar
    inicio_pagina_mod()
elif st.session_state.subpage == 'modificar_organizacion':
    pantalla_modificar_organizacion()
elif st.session_state.subpage == 'modificar_proyecto':
    pantalla_modificar_proyecto()
elif st.session_state.subpage == 'modificar_actividad':
    pantalla_modificar_actividad()
elif st.session_state.subpage == 'modificar_beneficiario':
    pantalla_modificar_beneficiario()
elif st.session_state.subpage == 'actualizar_organizacion':
    pantalla_actualizar_organizacion(st.session_state.organizacion_editar)
elif st.session_state.subpage == 'actualizar_proyecto':
    pantalla_actualizar_proyecto(st.session_state.proyecto_editar)
elif st.session_state.subpage == 'actualizar_actividad':
    pantalla_actualizar_actividad(st.session_state.actividad_editar)
elif st.session_state.subpage == 'actualizar_beneficiario':
    pantalla_actualizar_beneficiario(st.session_state.beneficiario_editar)