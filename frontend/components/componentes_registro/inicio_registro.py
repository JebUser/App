import streamlit as st
from utils.utils import navigate_to

def pantalla_inicio():
    st.markdown("## Registrar información")

    col1, col2 = st.columns(2)

    with col1:
        st.markdown("### Registro eventos u organizaciones")
        st.write("Registrar un evento o una organización mediante el ingreso de datos manualmente.")
        if st.button("Crear evento"):
            navigate_to('registrar', 'registro_evento')
        if st.button("Registrar organización"):
            navigate_to('registrar', 'registro_organizacion')

    with col2:
        st.markdown("### Registro por archivo")
        st.write("Registrar organizaciones desde archivo EXCEL, PDF o imagen.")
        if st.button("Subir archivo"):
            navigate_to('registrar', 'registro_archivo')