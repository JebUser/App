import streamlit as st
import pandas as pd

from api.gets import obtener_actividades_recientes

from utils.utils import init_session_state, navigate_to

init_session_state()

# Resetear a la página de inicio si no lo está
if st.session_state.current_page != 'inicio':
    navigate_to('inicio')

st.title("📌 Inicio")
st.subheader("Actividades recientes")

# Obtener datos reales de la API
eventos = obtener_actividades_recientes()

if eventos:
    # Mostrar cada evento en una tarjeta visualmente atractiva
    for evento in eventos[::-1]:
        with st.container(border=True):
            col1, col2 = st.columns([3, 1])
            
            with col1:
                st.markdown(f"### {evento['nombre']}")
                st.caption(f"📍 {evento['lugar']}")
                
            with col2:
                st.markdown(f"**Inicio:** {evento['fecha_inicio']}")
                st.markdown(f"**Fin:** {evento['fecha_fin']}")
                
            # Separador visual entre eventos
        st.divider()
else:
    st.warning("No se encontraron eventos recientes")