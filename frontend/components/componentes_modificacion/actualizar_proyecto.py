import streamlit as st
from datetime import datetime
from utils.utils import navigate_to
from api.gets import obtener_actividades, obtener_tipos_proyectos
from api.puts import modificar_proyecto

def pantalla_actualizar_proyecto(proyecto_data=None):
    """
    Muestra el formulario para actualizar un proyecto.

    Args:
        proyecto_data (dict): Diccionario con los datos actuales del proyecto.
            Ejemplo: {
                "id": 1,
                "nombre": "AGROECOLOGÍA PARA LA VIDA: Aportes para la incidencia y puesta en práctica desde una articulación multiactor en el Valle del Cauca",
                "fechainicio": "2023-07-24T00:00:00",
                "fechafinal": "2023-11-16T00:00:00",
                "tipoproyecto": null,
                "actividades": [
                {
                    "id": 1,
                    "nombre": "Taller Actores de la Agroecología Microregión Roldanillo",
                    "fechaInicio": "2023-07-24T00:00:00",
                    "fechaFinal": null,
                    "lugar": {
                    "id": 1,
                    "nombre": "INTEP Cedeagro-Roldanillo"
                    },
                    "beneficiarios": null
                }
                ]
            }
    """

    # Datos completos
    Actividades = obtener_actividades()
    # Convertir los beneficiarios de las actividades del proyecto con las actividades obtenidas.
    for actividad in Actividades:
        actividad["beneficiarios"] = None
    Tipoproyectos = obtener_tipos_proyectos()

    st.markdown("## Actualizar proyecto")

    # Botón para volver atrás
    if st.button("⬅️ Volver al listado"):
        navigate_to('modificar', 'modificar_proyecto')
        st.rerun()

    # Si no se proporcionan datos, mostrar mensaje
    if proyecto_data is None:
        st.warning("No se ha seleccionado ningún proyecto para editar")
        return
    
    col1, col2 = st.columns(2)

    with col1:
        nombre = st.text_input(
            "Nombre del proyecto*",
            value=proyecto_data.get('nombre',''),
            help="Nombre completo del proyecto"
        )

        fecha_inicio = st.date_input(
            "Fecha de inicio*",
            value=proyecto_data.get('fechainicio')
        )

        fecha_final = st.date_input(
            "Fecha de finalización*",
            value=proyecto_data.get('fechafinal')
        )
    with col2:
        default_index = None
        if proyecto_data["tipoproyecto"] != None:
            for i in range(len(Tipoproyectos)):
                if Tipoproyectos[i]["id"] == proyecto_data["tipoproyecto"]["id"]:
                    default_index = i
        
        tipo_proyecto = st.selectbox(
            "Tipo de proyecto",
            options=Tipoproyectos,
            format_func=lambda x: x['nombre'],
            index=default_index,
            key = "tipoproyecto_select",
            help = "Selecciona el tipo de proyecto de la lista"
        )
        actividades = st.multiselect(
            "Actividades*",
            options=Actividades,
            format_func=lambda x: x["nombre"],
            default=proyecto_data.get('actividades',[])
        )

    # Validación de campos obligatorios
    campos_obligatorios = {
        'Nombre': nombre,
        'FechaInicio': fecha_inicio,
        'FechaFinal': fecha_final,
        'Actividades': actividades
    }

    campos_faltantes = [campo for campo, valor in campos_obligatorios.items() if not valor]

    # Botón de actualización con validación
    if st.button("💾 Guardar cambios", type="primary"):
        if campos_faltantes:
            st.error(f"Por favor complete los campos obligatorios: {', '.join(campos_faltantes)}")
        else:
            # Conversión de las fechas para permitir serialización a JSON.
            fecha_inicio = datetime.combine(fecha_inicio, datetime.min.time())
            fecha_final = datetime.combine(fecha_final, datetime.min.time())

            # Aquí iría la lógica para guardar los cambios en la base de datos
            datos_actualizados = {
                "id": proyecto_data["id"],
                "nombre": nombre,
                "fechainicio": fecha_inicio.isoformat(),
                "fechafinal": fecha_final.isoformat(),
                "tipoproyecto": tipo_proyecto,
                "actividades": actividades
            }

            modificar_proyecto(datos_actualizados)
            
            # Lógica para actualizar en BD iría aquí
            st.success("¡Proyecto actualizado correctamente!")
            st.balloons()

            navigate_to('modificar', 'modificar_proyecto')
            st.rerun()

    # Mostrar datos originales (para referencia)
    with st.expander("🔍 Ver datos originales"):
        st.json(proyecto_data)