import streamlit as st
from utils.utils import cambiar_pagina

def pantalla_registro_evento():
    st.markdown("## Registro evento")
    if st.button("⬅️ Atrás"):
        cambiar_pagina("registrar")

    st.text_input("Nombre Actividad/Taller")
    st.date_input("Fecha")
    st.text_input("Lugar")

    st.markdown("### Lista participantes")
    st.write("Aquí iría la tabla de participantes...")

    col1, col2, col3 = st.columns(3)
    with col1:
        if st.button("Registrar participante"):
            cambiar_pagina("registro_participante")
    with col2:
        st.file_uploader("Subir archivo de participantes", type=["xlsx", "csv"])
    with col3:
        st.button("Terminar")
