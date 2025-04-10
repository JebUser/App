import streamlit as st
from utils.utils import cambiar_pagina

def pantalla_registro_organizacion():
    st.markdown("## Registro organización")
    if st.button("⬅️ Atrás"):
        cambiar_pagina("registrar")

    col1, col2 = st.columns(2)
    with col1:
        st.text_input("Nombre de la organización")
        st.text_input("NIT")
        st.selectbox("Tipo de organización", ["ONG", "Fundación", "Otra"])
        st.text_input("Línea productiva (si aplica)")
        st.number_input("Número de integrantes", min_value=1)
        st.text_input("Departamento")
    with col2:
        st.text_input("Municipio")
        st.number_input("Número de mujeres", min_value=0)
        st.text_input("Tipo de apoyo brindado")
        st.text_input("Otro apoyo")
        st.selectbox("¿Es una organización de mujeres?", ["Sí", "No"])

    st.button("Registrar")
