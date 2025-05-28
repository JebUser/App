import streamlit as st
from utils.utils import navigate_to
from api.gets import obtener_municipios, obtener_tipos_organizacion, obtener_tipos_apoyo, obtener_lineas_produccion, obtener_tipos_actividad
from api.puts import modificar_organizacion

def encontrar_elemento(id:int, data:list):
    default_index = None
    for i in range(len(data)):
        if data[i]["id"] == id:
            default_index = i
    return default_index

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
    # Datos completos
    municipios = obtener_municipios()
    Tipoorgs = obtener_tipos_organizacion()
    Tipoapoyos = obtener_tipos_apoyo()
    Lineaprods = obtener_lineas_produccion()
    Tipoactividades = obtener_tipos_actividad()

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

        default_index = None
        if organizacion_data["tipoorg"] != None:
            default_index = encontrar_elemento(organizacion_data["tipoorg"]["id"], Tipoorgs)
        
        tipo = st.selectbox(
            "Tipo de organización", 
            options=Tipoorgs,
            format_func=lambda x: x['nombre'],
            index=default_index,
            key="tipoorg_select",
            help="Selecciona el tipo de organización de la lista"
        )

        default_index = None
        if organizacion_data["lineaprod"] != None:
            default_index = encontrar_elemento(organizacion_data["lineaprod"]["id"], Lineaprods)
        
        linea_productiva = st.selectbox(
            "Línea productiva",
            options = Lineaprods,
            format_func=lambda x: x['nombre'],
            index=default_index,
            key="lineaprod_select",
            help="Selecciona la línea de producción de la lista"
        )
        
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
        format_func=lambda x: f"{x['nombre']}-{x['departamento']['nombre']}",
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

        default_index = None
        if organizacion_data["tipoapoyo"] != None:
            default_index = encontrar_elemento(organizacion_data["tipoapoyo"]["id"], Tipoapoyos)
        
        tipo_apoyo = st.selectbox(
            "Tipo de apoyo brindado",
            options=Tipoapoyos,
            format_func=lambda x: x['nombre'],
            index=default_index,
            key="tipoapoyo_select",
            help="Selecciona el tipo de apoyo de la lista"
        )

        default_index = None
        if organizacion_data["tipoactividad"] != None:
            default_index = encontrar_elemento(organizacion_data["tipoactividad"]["id"], Tipoactividades)

        tipo_actividad = st.selectbox(
            "Tipo de actividad",
            options=Tipoactividades,
            format_func=lambda x: x['nombre'],
            index=default_index,
            key="tipoactividad_select",
            help="Selecciona el tipo de actividad de la lista"
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
            # Convertir a Booleano la organización de mujeres
            bool_org_mujeres = None
            if org_mujeres == "Sí":
                bool_org_mujeres = True
            elif org_mujeres == "No":
                bool_org_mujeres = False

            # Aquí iría la lógica para guardar los cambios en la base de datos
            datos_actualizados = {
                "id": organizacion_data["id"],
                "nombre": nombre,
                "municipio": municipio,
                "nit": nit,
                "integrantes": None if num_integrantes == -1 else num_integrantes,
                "nummujeres": None if num_mujeres == -1 else num_mujeres,
                "orgmujeres": bool_org_mujeres,
                "tipoorg": tipo,
                "tipoactividad": tipo_actividad,
                "lineaprod": linea_productiva,
                "tipoapoyo": tipo_apoyo
            }
            st.write(datos_actualizados)

            modificar_organizacion(datos_actualizados)
            
            # Lógica para actualizar en BD iría aquí
            st.success("¡Organización actualizada correctamente!")
            st.balloons()
            
            # Opcional: Volver al listado después de guardar
            navigate_to('modificar', 'modificar_organizacion')
            st.rerun()

    # Mostrar datos originales (para referencia)
    with st.expander("🔍 Ver datos originales"):
        st.json(organizacion_data)