import streamlit as st
from utils.utils import init_session_state

init_session_state()  # Inicializa el estado al principio

st.set_page_config(page_title="Portal de Eventos", page_icon="📅", layout="wide")

# Configuración de páginas (como ya la tienes)
inicio = st.Page("pages/inicio.py", title="Inicio", icon=":material/home:")
registro = st.Page("pages/registrar.py", title="Registrar", icon=":material/person_add:")
consultar = st.Page("pages/consultar.py", title = "Consultar", icon=":material/search:")
modificar = st.Page("pages/modificar.py", title = "Modificar", icon=":material/checkbook:")

st.sidebar.image("assets/logo.png")
pg = st.navigation([inicio, registro, consultar, modificar])
pg.run()