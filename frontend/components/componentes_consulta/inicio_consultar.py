import streamlit as st
import pandas as pd
from components.componentes_consulta.widget_participante import mostrar_participantes

# El resto del código permanece igual

def consultar_actividades():
    st.markdown("# Consultar actividades")
    
    # Datos de ejemplo con participantes incluidos
    datos_actividades = [
        {
            "Nombre de la actividad": "Taller de programación", 
            "Lugar": "Aula 101", 
            "Fecha": "15/06/2024",
            "Participantes": [
                {"Nombre": "Ana López", "ID": "ID1001", "Teléfono": "3001234567"},
                {"Nombre": "Carlos Ruiz", "ID": "ID1002", "Teléfono": "3102345678"}
            ]
        },
        {
            "Nombre de la actividad": "Charla sobre IA", 
            "Lugar": "Auditorio principal", 
            "Fecha": "18/06/2024",
            "Participantes": [
                {"Nombre": "María Gómez", "ID": "ID1003", "Teléfono": "3203456789"},
                {"Nombre": "Pedro Sánchez", "ID": "ID1004", "Teléfono": "3154567890"}
            ]
        }
    ]
    
    # Convertir a DataFrame (sin participantes)
    df = pd.DataFrame([{k: v for k, v in item.items() if k != 'Participantes'} for item in datos_actividades])
    
    # Opciones de ordenamiento
    st.markdown("## Actividades")
    opcion_orden = st.selectbox(
        "Ordenar por:",
        ["Nombre", "Fecha", "Lugar"],
        key="orden_actividades"
    )
    
    # Ordenar el DataFrame según la selección
    if opcion_orden == "Nombre":
        df = df.sort_values("Nombre de la actividad")
    elif opcion_orden == "Fecha":
        df = df.sort_values("Fecha")
    elif opcion_orden == "Lugar":
        df = df.sort_values("Lugar")
    
    # Mostrar la tabla con los datos
    st.dataframe(
        df,
        column_config={
            "Nombre de la actividad": "Nombre del evento",
            "Lugar": "Lugar",
            "Fecha": "Fecha"
        },
        hide_index=True,
        use_container_width=True
    )
    
    # Selector de actividad para ver participantes
    actividades = [act["Nombre de la actividad"] for act in datos_actividades]
    actividad_seleccionada = st.selectbox(
        "Seleccione una actividad para ver participantes:",
        actividades,
        key="selector_actividad"
    )
    
    # Mostrar participantes cuando se selecciona una actividad
    if actividad_seleccionada:
        actividad = next(act for act in datos_actividades if act["Nombre de la actividad"] == actividad_seleccionada)
        mostrar_participantes(actividad)