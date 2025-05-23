import streamlit as st
import pandas as pd
from api.gets import consultar_actividades
from components.componentes_consulta.widget_participante import mostrar_participantes

def mostrar_consultar_actividades():
    st.markdown("# Consultar actividades")

    # Obtener datos de la API
    datos_actividades = consultar_actividades()
    if not datos_actividades:
        return

    # Convertir a DataFrame sin participantes
    df = pd.DataFrame([{k: v for k, v in item.items() if k != 'Participantes'} for item in datos_actividades])

    # Filtros y orden
    st.markdown("## Actividades")
    opcion_orden = st.selectbox("Ordenar por:", ["Nombre", "Fecha", "Lugar"], key="orden_actividades")

    if opcion_orden == "Nombre":
        df = df.sort_values("Nombre de la actividad")
    elif opcion_orden == "Fecha":
        df = df.sort_values("Fecha")
    elif opcion_orden == "Lugar":
        df = df.sort_values("Lugar")

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

    # Mostrar participantes por actividad
    actividades = [act["Nombre de la actividad"] for act in datos_actividades]
    actividad_seleccionada = st.selectbox("Seleccione una actividad para ver participantes (beneficiario):", actividades, key="selector_actividad")

    if actividad_seleccionada:
        actividad = next(act for act in datos_actividades if act["Nombre de la actividad"] == actividad_seleccionada)
        mostrar_participantes(actividad)