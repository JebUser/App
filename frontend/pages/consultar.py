import streamlit as st

# Utils
from utils.utils import init_session_state, navigate_to

# Pantallas
from components.componentes_consulta.inicio_consultar import mostrar_consultar_actividades

# Inicializar el estado de la sesión
init_session_state()
if st.session_state.current_page != 'consultar':
    navigate_to('consultar')

# Solo mostrar contenido si estamos en la página principal 'registrar'
st.title("📝 Consultar información")

# Mostrar subpágina correspondiente
if st.session_state.subpage is None:
    # Pantalla de inicio de registrar
    mostrar_consultar_actividades()
