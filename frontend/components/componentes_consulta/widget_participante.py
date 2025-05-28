import streamlit as st
import pandas as pd

def mostrar_info_participante(participante):
    """Muestra la información detallada en un popover"""
    with st.popover(f"👤 {participante.get('Nombre', 'Participante')}", use_container_width=True):
        # Mostrar información en dos columnas
        col1, col2 = st.columns(2)
        
        with col1:
            st.markdown(f"**Documento:**  \n{participante.get('Documento', 'N/A')}")
            st.markdown(f"**Teléfono:**  \n{participante.get('Celular', 'N/A')}")
            st.markdown(f"**Edad:**  \n{participante.get('Edad', 'N/A')}")
            
        with col2:
            st.markdown(f"**Municipio:**  \n{participante.get('Municipio', 'N/A')}")
            st.markdown(f"**Organización:**  \n{participante.get('Organizacion', 'N/A')}")
            st.markdown(f"**Sector:**  \n{participante.get('Sector', 'N/A')}")

def mostrar_participantes(actividad):
    """Muestra la lista de participantes ordenada solo por nombre"""
    st.markdown(f"## {actividad['Nombre de la actividad']}")
    
    # Información básica del evento
    col1, col2 = st.columns(2)
    with col1:
        st.markdown(f"**Lugar:** {actividad['Lugar']}")
    with col2:
        st.markdown(f"**Fecha:** {actividad['Fecha']}")
    
    st.divider()
    st.markdown("### Lista de Participantes")
    
    # Crear DataFrame y ordenar SOLO por nombre
    df = pd.DataFrame(actividad["Participantes"])
    df = df.sort_values("Nombre")
    
    # Mostrar cada participante con su botón
    for idx, row in df.iterrows():
        participante = row.to_dict()
        
        # Tarjeta de participante
        with st.container(border=True):
            cols = st.columns([4, 1])
            with cols[0]:
                st.markdown(f"""
                **Nombre:** {participante.get('Nombre', '')}  
                **Documento:** {participante.get('Documento', '')}  
                **Teléfono:** {participante.get('Celular', '')}
                """)
            with cols[1]:
                if st.button("Detalles", 
                           key=f"btn_{idx}",  # Clave única basada en el índice
                           use_container_width=True):
                    mostrar_info_participante(participante)