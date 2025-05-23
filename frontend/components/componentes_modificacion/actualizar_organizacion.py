import streamlit as st
from utils.utils import navigate_to
from assets.data import obtener_lista_municipios, obtener_lista_tipos_organizacion, obtener_lista_tipos_apoyo, obtener_lista_lineas_produccion
from api.gets import obtener_municipios, obtener_tipos_organizacion, obtener_tipos_apoyo, obtener_lineas_produccion

def pantalla_actualizar_organizacion(organizacion_data=None):
    """
    Muestra el formulario para actualizar una organización
    
    Args:
        organizacion_data (dict): Diccionario con los datos actuales de la organización
            Ejemplo: {
                "id": 1,
                "nombre": "COOAGRODOVIO",
                "municipio": {
                    "id": 1103,
                    "nombre": "El Dovio",
                    "departamento": {
                        "id": 30,
                        "nombre": "Valle del Cauca"
                    }
                },
                "nit": null,
                "integrantes": null,
                "nummujeres": null,
                "orgmujeres": null,
                "tipoorg": null,
                "tipoactividad": null,
                "lineaprod": null,
                "tipoapoyo": null
            }
    """
    # Datos para mostrar
    municipios = obtener_lista_municipios(formato='select')
    Tipoorgs = obtener_lista_tipos_organizacion(formato='select')
    Tipoapoyos = obtener_lista_tipos_apoyo(formato='select')
    Lineaprods = obtener_lista_lineas_produccion(formato='select')
    
    # Datos completos
    municipiosC = obtener_municipios()
    TipoorgsC = obtener_tipos_organizacion()
    TipoapoyosC = obtener_tipos_apoyo()
    LineaprodsC = obtener_lineas_produccion()

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
            "NIT", 
            value=organizacion_data.get('nit', ''),
            help="Número de Identificación Tributaria"
        )
        
        tipo = st.selectbox(
            "Tipo de organización", 
            options=Tipoorgs,
            index=organizacion_data.get('tipoorg').get('id')-1 if organizacion_data["tipoorg"] != None else 0,
            key="tipoorg_select",
            help="Selecciona el tipo de organización de la lista"
        )
        # TODO: Aplicar POST para permitir crear nuevos Tipos de Organización.
        
        linea_productiva = st.selectbox(
            "Línea productiva",
            options = Lineaprods,
            index=organizacion_data.get('lineaprod').get('id')-1 if organizacion_data["lineaprod"] != None else 0,
            key="lineaprod_select",
            help="Selecciona la línea de producción de la lista"
        )
        # TODO: Aplicar POST para permitir crear nuevas Líneas de Producción.
        
        num_integrantes = st.number_input(
            "Número de integrantes", 
            min_value=-1, 
            value=organizacion_data.get('num_integrantes', -1),
            help="Escribir valor de -1 para indicar que no se tiene información de este campo"
        )

    with col2:
        municipio = st.selectbox(
        "Municipio*",
        options=municipios,
        index=organizacion_data["municipio"]["id"]-1,  # Selecciona el municipio de la organización por defecto. Se resta uno por cómo funcionan las listas en Python.
        key="municipio_select",
        help="Seleccione el municipio de la lista"
    )
        
        num_mujeres = st.number_input(
            "Número de mujeres", 
            min_value=-1, 
            max_value=num_integrantes,
            value=organizacion_data.get('num_mujeres', -1),
            help=f"Máximo {num_integrantes} (total de integrantes). Escribir valor de -1 para indicar que no se tiene información de este campo"
        )
        
        tipo_apoyo = st.selectbox(
            "Tipo de apoyo brindado",
            options=Tipoapoyos,
            index=organizacion_data.get('tipoapoyo').get('id')-1 if organizacion_data["tipoapoyo"] != None else 0,
            key="tipoapoyo_select",
            help="Selecciona el tipo de apoyo de la lista"
        )
        
        org_mujeres = st.selectbox(
            "¿Es una organización de mujeres?*", 
            options=["Sí", "No", "No Informa"],
            index=organizacion_data.get('orgmujeres') if organizacion_data["orgmujeres"] != None else 2,
            help="Selecciona si es una organización de mujeres o no o si no se tiene conocimiento"
        )

    # Validación de campos obligatorios
    campos_obligatorios = {
        'Nombre': nombre,
        'Municipio': municipio,
        'Orgmujeres': org_mujeres
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