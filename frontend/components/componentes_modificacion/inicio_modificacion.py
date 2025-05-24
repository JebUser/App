import streamlit as st

from utils.utils import navigate_to

def inicio_pagina_mod():
    col1, col2 = st.columns(2)

    with col1:
        st.markdown("### Modificar Organización")
        if st.button("Modificar organización"):
            navigate_to('modificar', 'modificar_organizacion')

        st.markdown("### Modificar Actividad")
        if st.button("Modificar actividad"):
            navigate_to('modificar', 'modificar_actividad')

    with col2:
        st.markdown("### Modificar Proyecto")
        if st.button("Modificar proyecto"):
            navigate_to('modificar', 'modificar_evento')
        st.markdown("### Modificar Beneficiario")
        if st.button("Modificar beneficiario"):
            navigate_to('modificar', 'modificar_beneficiario')