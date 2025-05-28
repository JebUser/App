import streamlit as st
import pandas as pd
from streamlit_modal import Modal
from utils.utils import navigate_to
from api.gets import obtener_beneficiarios, obtener_generos, obtener_rango_edades
from api.deletes import eliminar_beneficiario

def pantalla_modificar_beneficiario():
    # Botón para volver atrás
    if st.button("⬅️ Volver al menú principal"):
        navigate_to("modificar")
    
    # Obtener los beneficiarios.
    beneficiarios = obtener_beneficiarios()
    # Obtener los generos.
    generos = obtener_generos()
    # Obtener los rangos de edades.
    rango_edades = obtener_rango_edades()
    # Convertir datos a DataFrame.
    df = pd.DataFrame(beneficiarios)

    # Configurar el modal para confirmar eliminación
    modal_eliminar = Modal(
        "Confirmar eliminación",
        key="modal_eliminar",
        padding=10,
        max_width=600
    )

    # Sección de búsqueda/filtrado
    with st.expander("🔍 Buscar/Filtrar beneficiarios", expanded=True):
        col1, col2, col3, col4, col5 = st.columns(5)
        with col1:
            filtro_nombre = st.text_input("Buscar por nombre(s)")
        with col2:
            filtro_apellido = st.text_input("Buscar por apellido(s)")
        with col3:
            filtro_identificacion = st.text_input("Buscar por identificación")
        with col4:
            filtro_genero = st.selectbox(
                "Buscar por género",
                options=generos,
                format_func=lambda x: x["nombre"],
                index=None
            )
        with col5:
            filtro_rango_edad = st.selectbox(
                "Buscar por rango de edad",
                options=rango_edades,
                format_func=lambda x: x["rango"],
                index=None
            )
        
        # Aplicar filtros
        if filtro_nombre:
            nombres = filtro_nombre.split()
            patron = '|'.join([nombre.strip() for nombre in nombres])
            df = df[
                df['nombre1'].str.contains(patron, case=False, na=False) |
                df['nombre2'].str.contains(patron, case=False, na=False)
            ]
        if filtro_apellido:
            apellidos = filtro_apellido.split()
            patron = '|'.join([apellido.strip() for apellido in apellidos])
            df = df[
                df['apellido1'].str.contains(patron, case=False, na=False) |
                df['apellido2'].str.contains(patron, case=False, na=False)
            ]
        if filtro_identificacion:
            df = df[df['identificacion'] == filtro_identificacion]
        if filtro_genero:
            df = df[df['genero'] == filtro_genero]
        if filtro_rango_edad:
            df = df[df['rangoedad'] == filtro_rango_edad]

        # Mostrar tabla de beneficiarios
    st.subheader("Listado de beneficiarios")
    
    # Encabezado de columnas
    col1, col2, col3, col4, col5 = st.columns([2, 1, 1, 1, 1])
    with col1:
        st.markdown("**Nombre(s) y Apellido(s)**")
    with col2:
        st.markdown("**Identificación**")
    with col3:
        st.markdown("**Género**")
    with col4:
        st.markdown("**Rango de Edad**")
    with col5:
        st.markdown("**Acciones**")

    # Mostrar cada proyecto
    for _, bene in df.iterrows():
        with st.container():
            cols = st.columns([2, 1, 1, 1, 1])
            
            # Columna 1: Nombre(s) y Apellido(s) de los Beneficiarios.
            with cols[0]:
                st.markdown(f"**{bene['nombre1']}{f' {bene['nombre2']}' if bene['nombre2'] != None else ''} {bene['apellido1']}{f' {bene['apellido2']}' if bene['apellido2'] != None else ''}**")
            # Columna 2: Número de Identificación
            with cols[1]:
                st.markdown(f"**{bene['identificacion'] if bene['identificacion'] != None else '(no informa)'}**")
            # Columan 3: Género.
            with cols[2]:
                st.markdown(f"**{bene['genero']['nombre']}**")
            # Columan 4: Rango de edad.
            with cols[3]:
                st.markdown(f"**{bene['rangoedad']['rango'] if bene['rangoedad'] != None else '(no informa)'}**")
            # Columna 5: Botones de acción
            with cols[4]:
                if st.button("✏️ Editar", key=f"editar_{bene['id']}", help=f"Editar {bene['nombre1']} {bene['nombre2'] if bene['nombre2'] != None else ''} {bene['apellido1']} {bene['apellido2'] if bene['apellido2'] != None else ''}"):
                    # Guardar el beneficiario seleccionado en session_state
                    st.session_state.beneficiario_editar = bene.to_dict()
                    # Redirigir a pantalla de actualización
                    navigate_to('modificar', 'actualizar_beneficiario')
                
                if st.button("🗑️", key=f"eliminar_{bene['id']}", help=f"Eliminar {bene['nombre1']} {bene['nombre2'] if bene['nombre2'] != None else ''} {bene['apellido1']} {bene['apellido2'] if bene['apellido2'] != None else ''}"):
                    st.session_state.beneficiario_a_eliminar = (bene['id'], f'{bene['nombre1']} {bene['nombre2'] if bene['nombre2'] != None else ''} {bene['apellido1']} {bene['apellido2'] if bene['apellido2'] != None else ''}')
                    modal_eliminar.open()
            
            st.divider()

    # Modal de confirmación de eliminación
    if modal_eliminar.is_open():
        with modal_eliminar.container():
            bene_id, bene_name = st.session_state.get('beneficiario_a_eliminar', '')
            st.markdown(f"### ¿Confirmar eliminación?")
            st.markdown(f"**Beneficiario:** {bene_name}")
            st.markdown("**Consecuencias:**")
            st.markdown("- Se marcará como eliminado en el sistema")
            st.markdown("- Las referencias se cambiarán por 'borrado'")
            st.warning("Esta acción no puede deshacerse")
            
            col1, col2 = st.columns(2)
            with col1:
                if st.button("✅ Confirmar", type="primary", use_container_width=True):
                    # Lógica para eliminar (aquí iría tu conexión a BD)
                    eliminar_beneficiario(bene_id)
                    st.success(f"Beneficiario '{bene_name}' marcado para eliminación")
                    modal_eliminar.close()
                    st.rerun()
            with col2:
                if st.button("❌ Cancelar", use_container_width=True):
                    modal_eliminar.close()
                    st.rerun()