import streamlit as st

# Utils
from utils.utils import init_session_state, navigate_to

# Pantallas
from components.componentes_registro.inicio_registro import pantalla_inicio
from components.componentes_registro.registro_evento import pantalla_registro_evento
from components.componentes_registro.registro_participante import pantalla_registro_participante
from components.componentes_registro.registro_organizacion import pantalla_registro_organizacion
from components.componentes_registro.registro_archivo import pantalla_registro_archivo
from components.componentes_registro.registro_proyecto import pantalla_registro_proyecto

# Inicializar el estado de la sesión
init_session_state()
if st.session_state.current_page != 'registrar':
    navigate_to('registrar')

# Solo mostrar contenido si estamos en la página principal 'registrar'
st.title("📝 Registrar información")

# Mostrar subpágina correspondiente
if st.session_state.subpage is None:
    # Pantalla de inicio de registrar
    pantalla_inicio()
elif st.session_state.subpage == 'registro_evento':
    pantalla_registro_evento()
elif st.session_state.subpage == 'registro_participante':
    pantalla_registro_participante()
elif st.session_state.subpage == 'registro_archivo':
    pantalla_registro_archivo()
elif st.session_state.subpage == 'registro_organizacion':
    pantalla_registro_organizacion()
elif st.session_state.subpage == 'registro_proyecto':
    pantalla_registro_proyecto()