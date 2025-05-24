import streamlit as st
import pandas as pd
from streamlit_modal import Modal
from assets.data.organizaciones import organizaciones_ejemplo
from utils.utils import navigate_to
from api.gets import obtener_proyectos

def pantalla_modificar_proyecto():
    # Botón para volver atrás
    if st.button("⬅️ Volver al menú principal"):
        navigate_to("modificar")

    # Obtener los proyectos.
    proyectos = obtener_proyectos()
    
    # Convertir datos a DataFrame
    df = pd.DataFrame(proyectos)
    # Conversión de fechas.
    df["fechainicio"] = pd.to_datetime(df["fechainicio"]).dt.date
    df["fechafinal"] = pd.to_datetime(df["fechafinal"]).dt.date

    # Configurar el modal para confirmar eliminación
    modal_eliminar = Modal(
        "Confirmar eliminación",
        key="modal_eliminar",
        padding=10,
        max_width=600
    )

    # Sección de búsqueda/filtrado
    with st.expander("🔍 Buscar/Filtrar proyectos", expanded=True):
        col1, col2, col3 = st.columns(3)
        with col1:
            filtro_nombre = st.text_input("Buscar por nombre")
        with col2:
            filtro_fecha_inicio = st.date_input("Filtrar por fecha de inicio", None)
        with col3:
            filtro_fecha_final = st.date_input("Filtrar por fecha de finalización", None)
        
        # Aplicar filtros
        if filtro_nombre:
            df = df[df['nombre'].str.contains(filtro_nombre, case=False)]
        if filtro_fecha_inicio:
            df = df[df['fechainicio'] == filtro_fecha_inicio]
        if filtro_fecha_final:
            df = df[df['fechafinal'] == filtro_fecha_final]

    # Mostrar tabla de proyectos
    st.subheader("Listado de proyectos")
    
    # Encabezado de columnas
    col1, col2, col3, col4 = st.columns([6, 1, 1, 1])
    with col1:
        st.markdown("**Nombre del proyecto**")
    with col2:
        st.markdown("**Fecha de Inicio**")
    with col3:
        st.markdown("**Fecha de Finalización**")
    with col4:
        st.markdown("**Acciones**")

    # Mostrar cada proyecto
    for _, proy in df.iterrows():
        with st.container():
            cols = st.columns([6, 1, 1, 1])
            
            # Columna 1: Nombre de la organización
            with cols[0]:
                st.markdown(f"**{proy['nombre']}**")
            # Columan 2; Fecha de inicio.
            with cols[1]:
                st.markdown(f"**{proy['fechainicio']}**")
            # Columan 2; Fecha de finalización.
            with cols[2]:
                st.markdown(f"**{proy['fechafinal']}**")
            # Columna 4: Botones de acción
            with cols[3]:
                if st.button("✏️ Editar", key=f"editar_{proy['id']}", help=f"Editar {proy['nombre']}"):
                    # Guardar la organización seleccionada en session_state
                    st.session_state.proyecto_editar = proy.to_dict()
                    # Redirigir a pantalla de actualización
                    navigate_to('modificar', 'actualizar_proyecto')
                
                if st.button("🗑️", key=f"eliminar_{proy['id']}", help=f"Eliminar {proy['nombre']}"):
                    st.session_state.organizacion_a_eliminar = proy['nombre']
                    modal_eliminar.open()
            
            st.divider()

    # Modal de confirmación de eliminación
    if modal_eliminar.is_open():
        with modal_eliminar.container():
            org_name = st.session_state.get('organizacion_a_eliminar', '')
            st.markdown(f"### ¿Confirmar eliminación?")
            st.markdown(f"**Proyecto:** {org_name}")
            st.markdown("**Consecuencias:**")
            st.markdown("- Se marcará como eliminada en el sistema")
            st.markdown("- Las referencias se cambiarán por 'borrado'")
            st.warning("Esta acción no puede deshacerse")
            
            col1, col2 = st.columns(2)
            with col1:
                if st.button("✅ Confirmar", type="primary", use_container_width=True):
                    # Lógica para eliminar (aquí iría tu conexión a BD)
                    st.success(f"Organización '{org_name}' marcada para eliminación")
                    modal_eliminar.close()
                    st.rerun()
            with col2:
                if st.button("❌ Cancelar", use_container_width=True):
                    modal_eliminar.close()
                    st.rerun()