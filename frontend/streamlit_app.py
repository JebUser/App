import streamlit as st

st.set_page_config(page_title="Portal de Eventos", page_icon="📅", layout="wide")

inicio = st.Page("pages/inicio.py", title="Inicio", icon=":material/house:")
registro = st.Page("pages/registrar.py", title = "Registrar")
consultar = st.Page("pages/consultar.py", title = "Consultar")
modificar = st.Page("pages/modificar.py", title = "Modificar")

st.sidebar.image("assets/logo.png")

pg = st.navigation([inicio, registro, consultar, modificar])
pg.run()