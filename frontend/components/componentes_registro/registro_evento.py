import streamlit as st
from utils.utils import navigate_to

def pantalla_registro_evento():
    st.markdown("## Registro proyecto")
    if st.button("⬅️ Atrás"):
        navigate_to("registrar")
        st.rerun()

    st.text_input("Nombre Actividad/Taller")
    st.date_input("Fecha")
    st.text_input("Lugar")

    st.markdown("### Lista participantes (beneficiario)")
    st.write("Aquí iría la tabla de participantes...")

    col1, col2, col3 = st.columns(3)
    with col1:
        if st.button("Registrar participante"):
            navigate_to('registrar',"registro_participante")
    with col2:
        st.file_uploader("Subir archivo de participantes (beneficiario)", type=["xlsx", "csv"])
    with col3:
        st.button("Terminar")
