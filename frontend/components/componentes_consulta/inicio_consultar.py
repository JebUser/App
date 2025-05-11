import streamlit as st
import pandas as pd
import requests
import urllib3  
from components.componentes_consulta.widget_participante import mostrar_participantes

urllib3.disable_warnings(urllib3.exceptions.InsecureRequestWarning) # ignoren esto me estaba molestando un warning

def consultar_actividades():
    st.markdown("# Consultar actividades")

    # Llamada a la API real
    try:
        response = requests.get("https://localhost:50604/api/Actividades", verify=False)
        response.raise_for_status()
        datos_api = response.json()
    except requests.exceptions.RequestException as e:
        st.error(f"Error al obtener datos: {e}")
        return

    # Procesar datos
    datos_actividades = []
    for act in datos_api:
        participantes = []
        for bene in act.get("beneficiarios", []):
            municipio = bene.get("municipio")
            sector = bene.get("sector")
            tipo_doc = bene.get("tipoiden")
            genero = bene.get("genero")
            rango_edad = bene.get("rangoedad")
            organizaciones = bene.get("organizaciones", [])

            organizacion_nombre = organizaciones[0]["nombre"] if organizaciones else "N/A"

            participante = {
                "Nombre": f"{bene.get('nombre1', '')} {bene.get('apellido1', '')} {bene.get('apellido2', '')}",
                "Documento": bene.get("identificacion", "N/A"),
                "Celular": bene.get("celular", "N/A"),
                "Edad": rango_edad["rango"] if rango_edad else "N/A",
                "Sexo": genero["nombre"] if genero else "N/A",
                "Municipio": municipio["nombre"] if municipio else "N/A",
                "Organizacion": organizacion_nombre,
                "Sector": sector["nombre"] if sector else "N/A",
                "Tipo Documento": tipo_doc["nombre"] if tipo_doc else "N/A"
            }
            participantes.append(participante)

        datos_actividades.append({
            "Nombre de la actividad": act.get("nombre", "Sin nombre"),
            "Lugar": act.get("lugar", {}).get("nombre", "Sin lugar"),
            "Fecha": act.get("fechaInicio", "Sin fecha"),
            "Participantes": participantes
        })

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
    actividad_seleccionada = st.selectbox("Seleccione una actividad para ver participantes:", actividades, key="selector_actividad")

    if actividad_seleccionada:
        actividad = next(act for act in datos_actividades if act["Nombre de la actividad"] == actividad_seleccionada)
        mostrar_participantes(actividad)
