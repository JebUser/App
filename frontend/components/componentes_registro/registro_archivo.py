import streamlit as st
from utils.utils import navigate_to

def pantalla_registro_archivo():
    st.markdown("## Registro por archivo")
    if st.button("⬅️ Atrás"):
        navigate_to("registrar")
        st.rerun()

    st.file_uploader("Subir archivo", type=["xlsx", "pdf", "png", "jpg", "svg"])
    st.button("Subir")
