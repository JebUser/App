import streamlit as st
from utils.utils import navigate_to
from assets.data import obtener_lista_municipios
from assets.data import obtener_lista_tipos_organizacion
from assets.data import obtener_lista_tipos_apoyo
from api.posts import (
    crear_organizacion_completa, 
    crear_tipo_organizacion, 
    crear_tipo_apoyo,
    crear_linea_productiva,
    obtener_tipos_organizaciones,
    obtener_tipos_apoyos,
    obtener_lineas_productivas,
    crear_tipo_actividad,
    obtener_tipos_actividades
)
import time

def obtener_municipios_para_select():
    """Obtiene municipios en formato para selectbox conservando el ID"""
    municipios = obtener_lista_municipios(formato='completo')
    return [
        (m['id'], m['departamento_id'], f"{m['nombre']} - {m['departamento_nombre']}") 
        for m in municipios
    ]

def pantalla_registro_organizacion():
    st.markdown("## Registro organización")
    
    if st.button("⬅️ Atrás"):
        navigate_to("registrar")
        st.rerun()

    # Toggle para debug
    debug_mode = st.checkbox("🔧 Modo Debug", help="Muestra información detallada del proceso")

    # Obtener listas de datos
    municipios = obtener_lista_municipios(formato='completo')
    tipos_organizacion_lista = obtener_lista_tipos_organizacion(formato='select')
    tipos_apoyo_lista = obtener_lista_tipos_apoyo(formato='select')
    
    # Obtener datos actuales de la API para los tipos que se pueden crear
    tipos_org_api = obtener_tipos_organizaciones() or []
    tipos_apoyo_api = obtener_tipos_apoyos() or []
    lineas_productivas_api = obtener_lineas_productivas() or []
    tipos_actividad_api = obtener_tipos_actividades() or []
    
    if debug_mode:
        st.write("🔍 **Tipos de organización disponibles:**")
        st.json(tipos_org_api)
        st.write("🔍 **Tipos de apoyo disponibles:**")
        st.json(tipos_apoyo_api)
        st.write("🔍 **Líneas productivas disponibles:**")
        st.json(lineas_productivas_api)

    col1, col2 = st.columns(2)
    
    with col1:
        # Campos obligatorios
        nombre_organizacion = st.text_input("Nombre de la organización*", placeholder="Ej: Cooperativa San José")
        nit = st.text_input("NIT")
        
        # Tipo de organización con opción de crear nuevo
        crear_nuevo_tipo_org = st.checkbox("Crear nuevo tipo de organización")
        
        if crear_nuevo_tipo_org:
            nuevo_tipo_org_nombre = st.text_input("Nombre del nuevo tipo de organización*")
            tipo_org_seleccionado = None
        else:
            nuevo_tipo_org_nombre = None
            if tipos_org_api:
                seleccion_org = st.selectbox(
                    "Tipo de organización*",
                    options=range(len(tipos_org_api)),
                    format_func=lambda x: tipos_org_api[x]["nombre"],
                    help="Seleccione el tipo de organización"
                )
                
                if seleccion_org is not None:
                    tipo_org_seleccionado = tipos_org_api[seleccion_org]
                else:
                    tipo_org_seleccionado = None
            else:
                tipo_org_seleccionado = None
                st.warning("No hay tipos de organización disponibles. Crea uno nuevo.")

        # Línea productiva con opción de crear nueva
        crear_nueva_linea_prod = st.checkbox("Crear nueva línea productiva")
        
        if crear_nueva_linea_prod:
            nueva_linea_prod_nombre = st.text_input("Nombre de la nueva línea productiva")
            linea_prod_seleccionada = None
        else:
            nueva_linea_prod_nombre = None
            if lineas_productivas_api:
                seleccion_linea = st.selectbox(
                    "Línea productiva (opcional)",
                    options=range(len(lineas_productivas_api)),
                    format_func=lambda x: lineas_productivas_api[x]["nombre"],
                    help="Opcional: Seleccione una línea productiva"
                )
                
                if seleccion_linea is not None:
                    linea_prod_seleccionada = lineas_productivas_api[seleccion_linea]
                else:
                    linea_prod_seleccionada = None
            else:
                linea_prod_seleccionada = None
                if not crear_nueva_linea_prod:
                    st.info("No hay líneas productivas disponibles. Puede crear una nueva si lo desea.")

        # Número de integrantes
        num_integrantes = st.number_input("Número de integrantes", min_value=1, value=1)

    with col2:
        municipios_con_id = obtener_municipios_para_select()
        # Municipio - mantener estructura original
        municipio_seleccionado = st.selectbox(
            "Municipio*",
            options=municipios_con_id,
            format_func=lambda x: x[2],  # Mostrar solo el nombre-description
            index=0,
            key="municipio_select"
        )
        municipio_id, departamento_id, municipio_texto = municipio_seleccionado
        municipio_nombre = municipio_texto.split(' - ')[0]
        departamento_nombre = municipio_texto.split(' - ')[1]

        # Número de mujeres
        num_mujeres = st.number_input("Número de mujeres", min_value=0, value=0)
        
        # Tipo de apoyo con opción de crear nuevo
        crear_nuevo_tipo_apoyo = st.checkbox("Crear nuevo tipo de apoyo")
        
        if crear_nuevo_tipo_apoyo:
            nuevo_tipo_apoyo_nombre = st.text_input("Nombre del nuevo tipo de apoyo*")
            tipo_apoyo_seleccionado = None
        else:
            nuevo_tipo_apoyo_nombre = None
            if tipos_apoyo_api:
                seleccion_apoyo = st.selectbox(
                    "Tipo de apoyo*",
                    options=range(len(tipos_apoyo_api)),
                    format_func=lambda x: tipos_apoyo_api[x]["nombre"],
                    help="Seleccione el tipo de apoyo"
                )
                
                if seleccion_apoyo is not None:
                    tipo_apoyo_seleccionado = tipos_apoyo_api[seleccion_apoyo]
                else:
                    tipo_apoyo_seleccionado = None
            else:
                tipo_apoyo_seleccionado = None
                st.warning("No hay tipos de apoyo disponibles. Crea uno nuevo.")

        # Otros campos opcionales
        es_org_mujeres = st.selectbox("¿Es una organización de mujeres?", ["No", "Sí"])

        # Campo para tipo de actividad
        crear_nuevo_tipo_actividad = st.checkbox("Crear nuevo tipo de actividad")

        if crear_nuevo_tipo_actividad:
            nuevo_tipo_actividad_nombre = st.text_input("Nombre del nuevo tipo de actividad*")
            tipo_actividad_seleccionado = None
        else:
            nuevo_tipo_actividad_nombre = None
            if tipos_actividad_api:
                # CORRECCIÓN: Agregar opción "Sin seleccionar" y manejar índice None
                seleccion_actividad = st.selectbox(
                    "Tipo de actividad (opcional)",
                    options=tipos_actividad_api,
                    format_func=lambda x: x['nombre'],
                    key = 'tipo_actividad_select',
                    help="Seleccione un tipo de actividad"
                )
                tipo_actividad_seleccionado = seleccion_actividad
            else:
                tipo_actividad_seleccionado = None
                st.info("No hay tipos de actividad disponibles. Puede crear uno nuevo si lo desea.")

    # Botón de registro
    if st.button("Registrar", type="primary"):
        # Validaciones básicas
        errores = []
        
        if not nombre_organizacion.strip():
            errores.append("Nombre de la organización es obligatorio")
            
        if crear_nuevo_tipo_org and not nuevo_tipo_org_nombre.strip():
            errores.append("Nombre del nuevo tipo de organización es obligatorio")
            
        if not crear_nuevo_tipo_org and not tipo_org_seleccionado:
            errores.append("Debe seleccionar un tipo de organización existente")
            
        if crear_nuevo_tipo_apoyo and not nuevo_tipo_apoyo_nombre.strip():
            errores.append("Nombre del nuevo tipo de apoyo es obligatorio")
            
        if not crear_nuevo_tipo_apoyo and not tipo_apoyo_seleccionado:
            errores.append("Debe seleccionar un tipo de apoyo existente")
            
        if not municipio_seleccionado:
            errores.append("Debe seleccionar un municipio")
        
        if errores:
            for error in errores:
                st.error(f"❌ {error}")
            return
            
        # Procesamiento
        progress_bar = st.progress(0)
        status_text = st.empty()
        
        try:
            tipo_organizacion = None
            tipo_apoyo = None
            linea_productiva = None
            
            # PASO 1: Manejar el tipo de organización
            if crear_nuevo_tipo_org:
                status_text.text("Paso 1/4: Creando nuevo tipo de organización...")
                progress_bar.progress(15)
                
                if debug_mode:
                    st.write(f"🔍 **Creando tipo de organización:** {nuevo_tipo_org_nombre}")
                
                tipo_org_creado = crear_tipo_organizacion(nuevo_tipo_org_nombre.strip())
                
                if tipo_org_creado:
                    if debug_mode:
                        st.write("✅ **Respuesta del tipo de organización creado:**")
                        st.json(tipo_org_creado)
                    
                    if isinstance(tipo_org_creado, dict) and "id" in tipo_org_creado and "nombre" in tipo_org_creado:
                        tipo_organizacion = {
                            "id": tipo_org_creado["id"],
                            "nombre": tipo_org_creado["nombre"]
                        }
                        st.success(f"✅ Tipo de organización '{nuevo_tipo_org_nombre}' creado con ID: {tipo_org_creado['id']}")
                    else:
                        st.error("❌ El tipo de organización se creó pero la respuesta no tiene la estructura esperada")
                        return
                else:
                    st.error("❌ Error al crear el nuevo tipo de organización")
                    return
            else:
                # Usar tipo existente
                progress_bar.progress(15)
                tipo_organizacion = {
                    "id": tipo_org_seleccionado["id"],
                    "nombre": tipo_org_seleccionado["nombre"]
                }
                if debug_mode:
                    st.write("✅ **Usando tipo de organización existente:**")
                    st.json(tipo_organizacion)
            
            time.sleep(1)  # Pausa para dar tiempo a la API
            
            # PASO 2: Manejar la línea productiva (opcional)
            if crear_nueva_linea_prod and nueva_linea_prod_nombre:
                status_text.text("Paso 2/4: Creando nueva línea productiva...")
                progress_bar.progress(30)
                
                if debug_mode:
                    st.write(f"🔍 **Creando línea productiva:** {nueva_linea_prod_nombre}")
                
                linea_prod_creada = crear_linea_productiva(nueva_linea_prod_nombre.strip())
                
                if linea_prod_creada:
                    if debug_mode:
                        st.write("✅ **Respuesta de la línea productiva creada:**")
                        st.json(linea_prod_creada)
                    
                    if isinstance(linea_prod_creada, dict) and "id" in linea_prod_creada and "nombre" in linea_prod_creada:
                        linea_productiva = {
                            "id": linea_prod_creada["id"],
                            "nombre": linea_prod_creada["nombre"]
                        }
                        st.success(f"✅ Línea productiva '{nueva_linea_prod_nombre}' creada con ID: {linea_prod_creada['id']}")
                    else:
                        st.error("❌ La línea productiva se creó pero la respuesta no tiene la estructura esperada")
                        return
                else:
                    st.error("❌ Error al crear la nueva línea productiva")
                    return
            elif linea_prod_seleccionada:
                # Usar línea existente
                progress_bar.progress(30)
                linea_productiva = {
                    "id": linea_prod_seleccionada["id"],
                    "nombre": linea_prod_seleccionada["nombre"]
                }
                if debug_mode:
                    st.write("✅ **Usando línea productiva existente:**")
                    st.json(linea_productiva)
            else:
                # Sin línea productiva
                progress_bar.progress(30)
                linea_productiva = {
                    "id": 0,
                    "nombre": ""
                }
            
            time.sleep(1)  # Pausa para dar tiempo a la API
            
            # PASO 3: Manejar el tipo de apoyo
            if crear_nuevo_tipo_apoyo:
                status_text.text("Paso 3/4: Creando nuevo tipo de apoyo...")
                progress_bar.progress(50)
                
                if debug_mode:
                    st.write(f"🔍 **Creando tipo de apoyo:** {nuevo_tipo_apoyo_nombre}")
                
                tipo_apoyo_creado = crear_tipo_apoyo(nuevo_tipo_apoyo_nombre.strip())
                
                if tipo_apoyo_creado:
                    if debug_mode:
                        st.write("✅ **Respuesta del tipo de apoyo creado:**")
                        st.json(tipo_apoyo_creado)
                    
                    if isinstance(tipo_apoyo_creado, dict) and "id" in tipo_apoyo_creado and "nombre" in tipo_apoyo_creado:
                        tipo_apoyo = {
                            "id": tipo_apoyo_creado["id"],
                            "nombre": tipo_apoyo_creado["nombre"]
                        }
                        st.success(f"✅ Tipo de apoyo '{nuevo_tipo_apoyo_nombre}' creado con ID: {tipo_apoyo_creado['id']}")
                    else:
                        st.error("❌ El tipo de apoyo se creó pero la respuesta no tiene la estructura esperada")
                        return
                else:
                    st.error("❌ Error al crear el nuevo tipo de apoyo")
                    return
            else:
                # Usar tipo existente
                progress_bar.progress(50)
                tipo_apoyo = {
                    "id": tipo_apoyo_seleccionado["id"],
                    "nombre": tipo_apoyo_seleccionado["nombre"]
                }
                if debug_mode:
                    st.write("✅ **Usando tipo de apoyo existente:**")
                    st.json(tipo_apoyo)
            
            time.sleep(1)  # Pausa para dar tiempo a la API
            # PASO 3.5: Manejar el tipo de actividad (añadir esto junto a los otros pasos)
            status_text.text("Paso 3.5/5: Procesando tipo de actividad...")
            progress_bar.progress(60)

            tipo_actividad = None
            if crear_nuevo_tipo_actividad and nuevo_tipo_actividad_nombre:
                if debug_mode:
                    st.write(f"🔍 **Creando tipo de actividad:** {nuevo_tipo_actividad_nombre}")
                
                tipo_actividad_creado = crear_tipo_actividad(nuevo_tipo_actividad_nombre.strip())
                
                if tipo_actividad_creado:
                    if debug_mode:
                        st.write("✅ **Respuesta del tipo de actividad creado:**")
                        st.json(tipo_actividad_creado)
                    
                    if isinstance(tipo_actividad_creado, dict) and "id" in tipo_actividad_creado and "nombre" in tipo_actividad_creado:
                        tipo_actividad = {
                            "id": tipo_actividad_creado["id"],
                            "nombre": tipo_actividad_creado["nombre"]
                        }
                        st.success(f"✅ Tipo de actividad '{nuevo_tipo_actividad_nombre}' creado con ID: {tipo_actividad_creado['id']}")
                    else:
                        st.error("❌ El tipo de actividad se creó pero la respuesta no tiene la estructura esperada")
                        if debug_mode:
                            st.write("Estructura recibida:", tipo_actividad_creado)
                        return
                else:
                    st.error("❌ Error al crear el nuevo tipo de actividad")
                    return
            elif tipo_actividad_seleccionado:
                # Usar tipo existente
                tipo_actividad = {
                    "id": tipo_actividad_seleccionado["id"],
                    "nombre": tipo_actividad_seleccionado["nombre"]
                }
                if debug_mode:
                    st.write("✅ **Usando tipo de actividad existente:**")
                    st.json(tipo_actividad)
            else:
                # Sin tipo de actividad (campo opcional)
                tipo_actividad = {
                    "id": 0,
                    "nombre": ""
                }
                if debug_mode:
                    st.write("ℹ️ **Sin tipo de actividad seleccionado**")

            time.sleep(0.5)
            # PASO 4: Crear organización completa
            status_text.text("Paso 4/4: Creando organización...")
            progress_bar.progress(70)
            
            # Preparar datos de la organización
            organizacion_data = {
                "id": 0,
                "nombre": nombre_organizacion.strip(),
                "municipio": {
                    "id": municipio_id,  # Ajustar según tu estructura de municipios
                    "nombre": str(municipio_nombre),
                    "departamento": {
                        "id": departamento_id,  # Puede necesitar ajuste según tu estructura
                        "nombre": str(departamento_nombre)
                    }
                },
                "nit": nit.strip() if nit else "",
                "integrantes": int(num_integrantes),
                "nummujeres": int(num_mujeres),
                "orgmujeres": True if es_org_mujeres == "Sí" else False,
                "tipoorg": tipo_organizacion,
                "tipoactividad": {
                    "id": tipo_actividad["id"] if tipo_actividad else 0,
                    "nombre": tipo_actividad["nombre"] if tipo_actividad else ""
                },
                "lineaprod": linea_productiva,
                "tipoapoyo": tipo_apoyo
            }
            
            if debug_mode:
                st.write("🔍 **Datos de la organización a enviar:**")
                st.json(organizacion_data)
            
            progress_bar.progress(90)
            resultado = crear_organizacion_completa(organizacion_data)
            
            if resultado:
                progress_bar.progress(100)
                status_text.text("✅ ¡Organización creada exitosamente!")
                
                organizacion_id = resultado.get('id', 'N/A')
                st.success(f"🎉 Organización '{nombre_organizacion}' creada exitosamente!")
                st.info(f"📝 ID de la organización: {organizacion_id}")
                
                # Mostrar resumen
                with st.expander("📋 Resumen de la organización creada", expanded=True):
                    col1, col2 = st.columns(2)
                    with col1:
                        st.write(f"**Nombre:** {nombre_organizacion}")
                        st.write(f"**NIT:** {nit or 'No especificado'}")
                        st.write(f"**Tipo:** {tipo_organizacion['nombre']} (ID: {tipo_organizacion['id']})")
                        st.write(f"**Municipio:** {municipio_seleccionado}")
                    with col2:
                        st.write(f"**Integrantes:** {num_integrantes}")
                        st.write(f"**Mujeres:** {num_mujeres}")
                        st.write(f"**Org. de mujeres:** {es_org_mujeres}")
                        st.write(f"**Tipo de apoyo:** {tipo_apoyo['nombre']} (ID: {tipo_apoyo['id']})")
                        st.write(f"**ID organización:** {organizacion_id}")
                
                st.balloons()
                
                # Opcional: botón para crear otra organización
                if st.button("➕ Crear otra organización"):
                    st.rerun()
                    
            else:
                progress_bar.progress(0)
                status_text.text("❌ Error al crear la organización")
                st.error("❌ No se pudo crear la organización. Revisa los logs de debug.")
                
        except Exception as e:
            progress_bar.progress(0)
            status_text.text("❌ Error inesperado")
            st.error(f"💥 Error inesperado: {str(e)}")
            if debug_mode:
                st.exception(e)