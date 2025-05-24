import streamlit as st
from utils.utils import navigate_to
from api.gets import obtener_tipos_identificacion, obtener_generos, obtener_rango_edades, obtener_grupos_etnicos, obtener_tipos_beneficiario, obtener_municipios, obtener_sectores, obtener_organizaciones
from api.puts import modificar_beneficiario

def pantalla_actualizar_beneficiario(beneficiario_data=None):
    """
    Muestra el formulario para actualizar un beneficiario.

    Args:
        beneficiario_data (dict): Diccionario con los datos actuales del beneficiario.
            Ejemplo: {
                "id": 1,
                "identificacion": "6440986",
                "nombre1": "Alvaro",
                "nombre2": null,
                "apellido1": "González",
                "apellido2": "Téllez",
                "celular": "3122864350",
                "firma": null,
                "tipoiden": {
                    "id": 1,
                    "nombre": "Cédula"
                },
                "genero": {
                    "id": 1,
                    "nombre": "Masculino"
                },
                "rangoedad": {
                    "id": 4,
                    "rango": ">59"
                },
                "grupoetnico": null,
                "tipobene": {
                    "id": 1,
                    "nombre": "Campesino(a)"
                },
                "municipio": {
                    "id": 1116,
                    "nombre": "Roldanillo",
                    "departamento": {
                    "id": 30,
                    "nombre": "Valle del Cauca"
                    }
                },
                "sector": {
                    "id": 1,
                    "nombre": "Productor/a AFCC"
                },
                "organizaciones": [
                    {
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
                ]
                }
    """

    # Datos completos.
    Tiposiden = obtener_tipos_identificacion()
    Generos = obtener_generos()
    Rangoedades = obtener_rango_edades()
    Gruposetnicos = obtener_grupos_etnicos()
    Tiposbene = obtener_tipos_beneficiario()
    Municipios = obtener_municipios()
    Sectores = obtener_sectores()
    Organizaciones = obtener_organizaciones()

    st.markdown("## Actualizar beneficiario")

    # Botón para volver atrás
    if st.button("⬅️ Volver al listado"):
        navigate_to('modificar', 'modificar_beneficiario')
        st.rerun()

    # Si no se proporcionan datos, mostrar mensaje
    if beneficiario_data is None:
        st.warning("No se ha seleccionado ningún beneficiario para editar")
        return
    
    col1, col2 = st.columns(2)

    with col1:
        tipo_identificacion = st.selectbox(
            "Tipo de Identificación",
            options=Tiposiden,
            format_func=lambda x: x['nombre'],
            index=beneficiario_data.get('tipoiden').get('id')-1 if beneficiario_data["tipoiden"] != None else None,
            key='tipo_identificacion_select',
            help="Seleccione el tipo de identificación al que se encuentra asociado el número de identificación"
        )
        identificacion = st.text_input(
            "Número de Identificación",
            value=beneficiario_data.get('identificacion','')
        )
        primer_nombre = st.text_input(
            "Primer Nombre*",
            value=beneficiario_data.get('nombre1','')
        )
        segundo_nombre = st.text_input(
            "Segundo Nombre",
            value=beneficiario_data.get('nombre2','')
        )
        primer_apellido = st.text_input(
            "Primer Apellido*",
            value=beneficiario_data.get('apellido1','')
        )
        segundo_apellido = st.text_input(
            "Segundo Apellido",
            value=beneficiario_data.get('apellido2','')
        )
        celular = st.text_input(
            "Celular",
            value=beneficiario_data.get('celular','')
        )
    with col2:
        genero = st.selectbox(
            "Género*",
            options=Generos,
            format_func=lambda x: x['nombre'],
            index=beneficiario_data.get('genero').get('id')-1 if beneficiario_data["genero"] != None else None,
            key='genero_select',
            help="Seleccione el género del beneficiario"
        )
        rango_edad = st.selectbox(
            "Rango de Edad",
            options=Rangoedades,
            format_func=lambda x: x['rango'],
            index=beneficiario_data.get('rangoedad').get('id')-1 if beneficiario_data["rangoedad"] != None else None,
            key='rango_edad_select',
            help="Seleccione el rango de edad del beneficiario"
        )
        grupo_etnico = st.selectbox(
            "Grupo étnico al que pertenece (si aplica)",
            options=Gruposetnicos,
            format_func=lambda x: x['nombre'],
            index=beneficiario_data.get('grupoetnico').get('id')-1 if beneficiario_data["grupoetnico"] != None else None,
            key='grupo_etnico_select',
            help="Seleccione el grupo étnico al que pertenece el beneficiario"
        )
        tipo_beneficiario = st.selectbox(
            "El beneficiario es",
            options=Tiposbene,
            format_func=lambda x: x['nombre'],
            index=beneficiario_data.get('tipobene').get('id')-1 if beneficiario_data["tipobene"] != None else None,
            key='tipo_beneficiario_select',
            help="Seleccione el tipo de beneficiario"
        )
        municipio = st.selectbox(
            "Municipio",
            options=Municipios,
            format_func=lambda x: f"{x['nombre']}-{x['departamento']['nombre']}",
            index=beneficiario_data.get('municipio').get('id')-1 if beneficiario_data["municipio"] != None else None,  # Selecciona el municipio de la organización por defecto. Se resta uno por cómo funcionan las listas en Python.
            key="municipio_select",
            help="Seleccione el municipio de la lista"
        )
        sector = st.selectbox(
            "Sector al que pertenece",
            options=Sectores,
            format_func=lambda x: x['nombre'],
            index=beneficiario_data.get('sector').get('id')-1 if beneficiario_data["sector"] != None else None,
            key='sector_select',
            help="Seleccione el sector al que pertenece el beneficiario"
        )
        organizaciones = st.multiselect(
            "Organizaciones*",
            options=Organizaciones,
            format_func=lambda x: x["nombre"],
            default=beneficiario_data.get('organizaciones',[])
        )
    
    # Validación de campos obligatorios
    campos_obligatorios = {
        'Nombre1': primer_nombre,
        'Apellido1': primer_apellido,
        'Genero': genero,
        'Organizaciones': organizaciones
    }

    campos_faltantes = [campo for campo, valor in campos_obligatorios.items() if not valor]
    # Botón de actualización con validación
    if st.button("💾 Guardar cambios", type="primary"):
        if campos_faltantes:
            st.error(f"Por favor complete los campos obligatorios: {', '.join(campos_faltantes)}")
        else:

            # Aquí iría la lógica para guardar los cambios en la base de datos
            datos_actualizados = {
                "id": beneficiario_data["id"],
                "identificacion": identificacion,
                "nombre1": primer_nombre,
                "nombre2": segundo_nombre,
                "apellido1": primer_apellido,
                "apellido2": segundo_apellido,
                "celular": celular,
                "firma": None,
                "tipoiden": tipo_identificacion,
                "genero": genero,
                "rangoedad": rango_edad,
                "grupoetnico": grupo_etnico,
                "tipobene": tipo_beneficiario,
                "municipio": municipio,
                "sector": sector,
                "organizaciones": organizaciones
            }

            modificar_beneficiario(datos_actualizados)
            
            # Lógica para actualizar en BD iría aquí
            st.success("Beneficiario actualizado correctamente!")
            st.balloons()
            
            # Opcional: Volver al listado después de guardar
            navigate_to('modificar', 'modificar_beneficiario')
            st.rerun()

    # Mostrar datos originales (para referencia)
    with st.expander("🔍 Ver datos originales"):
        st.json(beneficiario_data)
