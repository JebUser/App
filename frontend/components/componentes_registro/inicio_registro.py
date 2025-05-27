import streamlit as st
from utils.utils import navigate_to

def pantalla_inicio():
    st.markdown("## Registrar información")
    st.markdown("### Registro proyectos u organizaciones")
    st.write("Registrar un proyectos, actividades u organizaciones")
    col1, col2, col3= st.columns(3, border=True)


    with col1:
        if st.button("Registrar proyecto"):
            navigate_to('registrar', 'registro_proyecto')
        
    with col2:
        if st.button("Registrar actividad"):
            navigate_to('registrar', 'registro_evento')
    
    with col3:
        if st.button("Registrar organización"):
            navigate_to('registrar', 'registro_organizacion')
        

