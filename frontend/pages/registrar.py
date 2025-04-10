import streamlit as st

#Utils
from utils.utils import cambiar_pagina

#Páginas
from components.componentes_registro.registro_evento import pantalla_registro_evento
from components.componentes_registro.registro_participante import pantalla_registro_participante
from components.componentes_registro.registro_organizacion import pantalla_registro_organizacion
from components.componentes_registro.registro_archivo import pantalla_registro_archivo



st.title("📝 Registrar informacion")
# Manejador del "estado" de la vista actual
if 'pagina' not in st.session_state:
    st.session_state.pagina = 'registrar'

# Navegación entre pantallas
def pantalla_inicio():
    st.markdown("## Registrar información")

    col1, col2 = st.columns(2)

    with col1:
        st.markdown("### Registro eventos u organizaciones")
        st.write("Registrar un evento o una organización mediante el ingreso de datos manualmente.")
        if st.button("Crear evento"):
            cambiar_pagina("registro_evento")
        if st.button("Registrar organización"):
            cambiar_pagina("registro_organizacion")

    with col2:
        st.markdown("### Registro por archivo")
        st.write("Registrar organizaciones desde archivo EXCEL, PDF o imagen.")
        if st.button("Subir archivo"):
            cambiar_pagina("registro_archivo")

# Enrutador de páginas
if st.session_state.pagina == "registrar":
    pantalla_inicio()
elif st.session_state.pagina == "registro_evento":
    pantalla_registro_evento()
elif st.session_state.pagina == "registro_participante":
    pantalla_registro_participante()
elif st.session_state.pagina == "registro_archivo":
    pantalla_registro_archivo()
elif st.session_state.pagina == "registro_organizacion":
    pantalla_registro_organizacion()