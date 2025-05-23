import streamlit as st
import pandas as pd
from streamlit_modal import Modal
from assets.data.organizaciones import organizaciones_ejemplo
from utils.utils import navigate_to
from datetime import datetime
from api.puts import modificar_organizacion
from api.gets import obtener_organizaciones

def pantalla_modificar_organizacion():
    # Botón para volver atrás
    if st.button("⬅️ Volver al menú principal"):
        navigate_to("modificar")
    
    # Obtener las organizaciones.
    organizaciones = obtener_organizaciones()
    # Convertir datos a DataFrame.
    df = pd.DataFrame(organizaciones)
    
    # Configurar el modal para confirmar eliminación
    modal_eliminar = Modal(
        "Confirmar eliminación",
        key="modal_eliminar",
        padding=10,
        max_width=600
    )

    # Sección de búsqueda/filtrado
    with st.expander("🔍 Buscar/Filtrar organizaciones", expanded=True):
        filtro_nombre = st.text_input("Buscar por nombre")
        
        # Aplicar filtros
        if filtro_nombre:
            df = df[df['nombre'].str.contains(filtro_nombre, case=False)]

    # Mostrar tabla de organizaciones
    st.subheader("Listado de organizaciones")
    
    # Encabezado de columnas
    col1, col2 = st.columns([4, 1])
    with col1:
        st.markdown("**Nombre de la organización**")
    with col2:
        st.markdown("**Acciones**")

    # Mostrar cada organización con indicador de antigüedad
    for _, org in df.iterrows():
        with st.container():
            cols = st.columns([4, 1])
            
            # Columna 1: Nombre y detalles
            with cols[0]:
                st.markdown(f"**{org['nombre']}**")
            
            # Columna 2: Botones de acción
            with cols[1]:
                if st.button("✏️ Editar", key=f"editar_{org['id']}", help=f"Editar {org['nombre']}"):
                    st.session_state.organizacion_editar = org.to_dict()
                    navigate_to('modificar', 'actualizar_organizacion')
                
                if st.button("🗑️", key=f"eliminar_{org['id']}", help=f"Eliminar {org['nombre']}"):
                    st.session_state.organizacion_a_eliminar = org['nombre']
                    modal_eliminar.open()
            
            st.divider()

    # Modal de confirmación de eliminación
    if modal_eliminar.is_open():
        with modal_eliminar.container():
            org_name = st.session_state.get('organizacion_a_eliminar', '')
            st.markdown(f"### ¿Confirmar eliminación?")
            st.markdown(f"**Organización:** {org_name}")
            st.markdown("**Consecuencias:**")
            st.markdown("- Se marcará como eliminada en el sistema")
            st.markdown("- Las referencias se cambiarán por 'borrado'")
            st.warning("Esta acción no puede deshacerse")
            
            col1, col2 = st.columns(2)
            with col1:
                if st.button("✅ Confirmar", type="primary", use_container_width=True):
                    st.success(f"Organización '{org_name}' marcada para eliminación")
                    modal_eliminar.close()
                    st.rerun()
            with col2:
                if st.button("❌ Cancelar", use_container_width=True):
                    modal_eliminar.close()
                    st.rerun()