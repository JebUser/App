import streamlit as st
from utils.utils import cambiar_pagina

def pantalla_registro_archivo():
    st.markdown("## Registro por archivo")
    if st.button("⬅️ Atrás"):
        cambiar_pagina("registrar")

    st.file_uploader("Subir archivo", type=["xlsx", "pdf", "png", "jpg", "svg"])
    st.button("Subir")
