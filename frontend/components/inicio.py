import streamlit as st
import pandas as pd

def mostrar():
    st.title("📌 Inicio")
    st.subheader("Eventos recientes")

    # Placeholder para la tabla
    eventos = pd.DataFrame({
        "Nombre del evento": ["Evento 1", "Evento 2", "Evento 3"],
        "Lugar": ["Cali", "Bogotá", "Medellín"],
        "Fecha": ["27/05/2024", "15/06/2024", "03/07/2024"]
    })
    # Futura opción de ordenamiento (Placeholder)
    st.write("Ordenar por:", st.selectbox("", ["Nombre", "Fecha"]))
    
    st.dataframe(eventos)  # Muestra la tabla

    

