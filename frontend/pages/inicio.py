import streamlit as st
import pandas as pd

st.title("📌 Inicio")
st.subheader("Eventos recientes")

# Contenedor con fondo verde personalizado
with st.container():

    # Placeholder de datos
    eventos = pd.DataFrame({
        "Nombre del evento": ["Evento 1", "Evento 2", "Evento 3"],
        "Lugar": ["Cali", "Bogotá", "Medellín"],
        "Fecha": ["27/05/2024", "15/06/2024", "03/07/2024"]
    })

    # Elementos dentro del contenedor visual
    st.dataframe(eventos)