import streamlit as st
from utils.utils import init_session_state, navigate_to

# Pantallas
from components.componentes_modificacion.inicio_modificacion import inicio_pagina_mod
from components.componentes_modificacion.ver_modificar_eventos import pantalla_modificar_organizacion
from components.componentes_modificacion.actualizar_organizacion import pantalla_actualizar_organizacion

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
elif st.session_state.subpage == 'modificar_evento':
    # Aquí iría tu función para modificar eventos
    st.write("Pantalla de modificación de eventos")
    # pantalla_modificar_evento()  # Descomenta cuando tengas esta función
elif st.session_state.subpage == 'actualizar_organizacion':
    pantalla_actualizar_organizacion(st.session_state.organizacion_editar)