import streamlit as st
from utils.utils import navigate_to
from assets.data import obtener_lista_municipios
from assets.data import obtener_lista_tipos_organizacion
from assets.data import obtener_lista_tipos_apoyo
#from assets.data import obtener_lista_lineas_producto
from assets.data import obtener_lista_tipos_identificacion

def pantalla_actualizar_organizacion(organizacion_data=None):
    """
    Muestra el formulario para actualizar una organización
    
    Args:
        organizacion_data (dict): Diccionario con los datos actuales de la organización
            Ejemplo: {
                'nombre': 'Org Ejemplo',
                'nit': '123456789',
                'tipo': 'ONG',
                'linea_productiva': 'Agricultura',
                'num_integrantes': 15,
                'departamento': 'Cundinamarca',
                'municipio': 'Bogotá',
                'num_mujeres': 8,
                'tipo_apoyo': 'Capacitación',
                'otro_apoyo': 'Semillas',
                'org_mujeres': 'Sí'
            }
    """
    municipios = obtener_lista_municipios(formato='select')
    Orgnizaciones = obtener_lista_tipos_organizacion(formato='select')
    Tipoapoyos = obtener_lista_tipos_apoyo(formato='select')
 #   Lineaprods = obtener_lista_lineas_producto(formato='select')
    Tipoidens = obtener_lista_tipos_identificacion(formato='select')
    

    st.markdown("## Actualizar organización")
    
    # Botón para volver atrás
    if st.button("⬅️ Volver al listado"):
        navigate_to('modificar', 'modificar_organizacion')
        st.rerun()

    # Si no se proporcionan datos, mostrar mensaje
    if organizacion_data is None:
        st.warning("No se ha seleccionado ninguna organización para editar")
        return

    # Dividir el formulario en dos columnas
    col1, col2 = st.columns(2)
    
    with col1:
        nombre = st.text_input(
            "Nombre de la organización*", 
            value=organizacion_data.get('nombre', ''),
            help="Nombre completo de la organización"
        )
        
        nit = st.text_input(
            "NIT*", 
            value=organizacion_data.get('nit', ''),
            help="Número de Identificación Tributaria"
        )
        
        tipo = st.selectbox(
            "Tipo de organización*", 
            options=["ONG", "Fundación", "Cooperativa", "Asociación", "Otra"],
            index=["ONG", "Fundación", "Cooperativa", "Asociación", "Otra"].index(
                organizacion_data.get('tipo', 'ONG')
            )
        )
        
        linea_productiva = st.text_input(
            "Línea productiva (si aplica)", 
            value=organizacion_data.get('linea_productiva', '')
        )
        
        num_integrantes = st.number_input(
            "Número de integrantes*", 
            min_value=1, 
            value=organizacion_data.get('num_integrantes', 1)
        )
        
        departamento = st.text_input(
            "Departamento*", 
            value=organizacion_data.get('departamento', '')
        )

    with col2:
        municipio = st.selectbox(
        "Municipio*",
        options=municipios,
        index=0,  # Selecciona el primer elemento por defecto
        key="municipio_select",
        help="Seleccione el municipio de la lista"
    )
        
        num_mujeres = st.number_input(
            "Número de mujeres*", 
            min_value=0, 
            max_value=num_integrantes,
            value=organizacion_data.get('num_mujeres', 0),
            help=f"Máximo {num_integrantes} (total de integrantes)"
        )
        
        tipo_apoyo = st.text_input(
            "Tipo de apoyo brindado", 
            value=organizacion_data.get('tipo_apoyo', '')
        )
        
        otro_apoyo = st.text_input(
            "Otro apoyo recibido", 
            value=organizacion_data.get('otro_apoyo', '')
        )
        
        org_mujeres = st.selectbox(
            "¿Es una organización de mujeres?*", 
            options=["Sí", "No"],
            index=0 if organizacion_data.get('org_mujeres', 'No') == "Sí" else 1
        )

    # Validación de campos obligatorios
    campos_obligatorios = {
        'Nombre': nombre,
        'NIT': nit,
        'Departamento': departamento,
        'Municipio': municipio
    }
    
    campos_faltantes = [campo for campo, valor in campos_obligatorios.items() if not valor]
    
    # Botón de actualización con validación
    if st.button("💾 Guardar cambios", type="primary"):
        if campos_faltantes:
            st.error(f"Por favor complete los campos obligatorios: {', '.join(campos_faltantes)}")
        else:
            # Aquí iría la lógica para guardar los cambios en la base de datos
            datos_actualizados = {
                'nombre': nombre,
                'nit': nit,
                'tipo': tipo,
                'linea_productiva': linea_productiva,
                'num_integrantes': num_integrantes,
                'departamento': departamento,
                'municipio': municipio,
                'num_mujeres': num_mujeres,
                'tipo_apoyo': tipo_apoyo,
                'otro_apoyo': otro_apoyo,
                'org_mujeres': org_mujeres
            }
            
            # Lógica para actualizar en BD iría aquí
            st.success("¡Organización actualizada correctamente!")
            st.balloons()
            
            # Opcional: Volver al listado después de guardar
            navigate_to('modificar', 'modificar_organizacion')
            st.rerun()

    # Mostrar datos originales (para referencia)
    with st.expander("🔍 Ver datos originales"):
        st.json(organizacion_data)