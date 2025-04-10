import streamlit as st
from utils.utils import cambiar_pagina

def pantalla_registro_participante():
    st.markdown("## Registro participante")
    if st.button("⬅️ Atrás"):
        cambiar_pagina("registro_evento")

    col1, col2 = st.columns(2)
    with col1:
        st.text_input("Nombre y apellido")
        st.selectbox("Sexo", ["Masculino", "Femenino", "Otro"])
        st.selectbox("Tipo de documento", ["CC", "TI", "CE"])
        st.text_input("Número de documento")
        st.number_input("Edad", min_value=0, max_value=120)
        st.text_input("Departamento")

    with col2:
        st.text_input("Municipio")
        st.selectbox("¿Pertenece a un grupo étnico?", ["Sí", "No"])
        st.selectbox("Tipo de participante", ["Asistente", "Ponente", "Otro"])
        st.text_input("Entidad / Organización")
        st.text_input("Sector participante")
        st.text_input("Celular")

    st.button("Registrar participante")
