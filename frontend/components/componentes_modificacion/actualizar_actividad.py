import streamlit as st
import pandas as pd
from datetime import datetime
from utils.utils import navigate_to
from api.gets import obtener_beneficiarios, obtener_lugares
from api.puts import modificar_actividad

def pantalla_actualizar_actividad(actividad_data=None):
    """
    Muestra el formulario para actualizar una actividad.

    Args:
        actividad_data (dict): Diccionario con los datos actuales de la actividad.
            Ejemplo: {
                "id": 1,
                "nombre": "Taller Actores de la Agroecología Microregión Roldanillo",
                "fechaInicio": "2023-07-24T00:00:00",
                "fechaFinal": null,
                "lugar": {
                "id": 1,
                "nombre": "INTEP Cedeagro-Roldanillo"
                },
                "beneficiarios": [
                {
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
                },
                {
                    "id": 2,
                    "identificacion": "16401172",
                    "nombre1": "Hermin",
                    "nombre2": null,
                    "apellido1": "Vichada",
                    "apellido2": null,
                    "celular": "3156329293",
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
                    "id": 3,
                    "rango": "35-59"
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
                        "id": 2,
                        "nombre": "CORPORACIÓN CD-TORO",
                        "municipio": {
                        "id": 1119,
                        "nombre": "Toro",
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
                },...]
            }
            
    """

    # Datos completos
    Beneficiarios = obtener_beneficiarios()
    Lugares = obtener_lugares()

    st.markdown("## Actualizar actividad")

    # Botón para volver atrás
    if st.button("⬅️ Volver al listado"):
        navigate_to('modificar', 'modificar_actividad')
        st.rerun()

    # Si no se proporcionan datos, mostrar mensaje
    if actividad_data is None:
        st.warning("No se ha seleccionado ninguna actividad para editar")
        return
    
    col1, col2 = st.columns(2)

    with col1:
        nombre = st.text_input(
            "Nombre de la actividad*",
            value=actividad_data.get('nombre',''),
            help="Nombre completo de la actividad"
        )
        fecha_inicio = st.date_input(
            "Fecha de inicio*",
            value=actividad_data.get('fechaInicio')
        )
        fecha_final = st.date_input(
            "Fecha de finalización",
            value=actividad_data.get('fechaFinal') if pd.notna(actividad_data["fechaFinal"]) else None,
            help="Dejar el campo como 0001/01/01 para eliminar la fecha registrada"
        )

    with col2:
        lugar = st.selectbox(
            "Lugar*",
            options=Lugares,
            format_func=lambda x: x['nombre'],
            index=actividad_data.get('lugar').get('id')-1,
            key="lugar_select",
            help="Selecciona el lugar en el que se realizó la actividad"
        )
        # TODO: Aplicar POST para permitir crear nuevos Lugares.
        beneficiarios = st.multiselect(
            "Beneficiarios*",
            options=Beneficiarios,
            format_func=lambda x: f"{x['nombre1']} {x['nombre2'] if x['nombre2'] != None else ''} {x['apellido1']} {x['apellido2'] if x['apellido2'] != None else ''}",
            default=actividad_data.get('beneficiarios', [])
        )

    # Validación de campos obligatorios
    campos_obligatorios = {
        'Nombre': nombre,
        'FechaInicio': fecha_inicio,
        'Lugar': lugar,
        'Beneficiarios': beneficiarios
    }

    campos_faltantes = [campo for campo, valor in campos_obligatorios.items() if not valor]
    # Botón de actualización con validación
    if st.button("💾 Guardar cambios", type="primary"):
        if campos_faltantes:
            st.error(f"Por favor complete los campos obligatorios: {', '.join(campos_faltantes)}")
        else:
            # Conversión de las fechas para permitir serialización a JSON.
            fecha_inicio = datetime.combine(fecha_inicio, datetime.min.time())
            if fecha_final != None:
                if fecha_final == datetime.min.date():
                    fecha_final = None
                else:
                    fecha_final = datetime.combine(fecha_final, datetime.min.time())

            # Aquí iría la lógica para guardar los cambios en la base de datos
            datos_actualizados = {
                "id": actividad_data["id"],
                "nombre": nombre,
                "fechaInicio": fecha_inicio.isoformat(),
                "fechaFinal": fecha_final.isoformat() if fecha_final != None else None,
                "lugar": lugar,
                "beneficiarios": beneficiarios
            }

            modificar_actividad(datos_actualizados)
            
            # Lógica para actualizar en BD iría aquí
            st.success("¡Actividad actualizada correctamente!")
            st.balloons()
            
            # Opcional: Volver al listado después de guardar
            # navigate_to('modificar', 'modificar_proyecto')
            # st.rerun()

    # Mostrar datos originales (para referencia)
    with st.expander("🔍 Ver datos originales"):
        st.json(actividad_data)