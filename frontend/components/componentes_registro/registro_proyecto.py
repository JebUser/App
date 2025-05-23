import streamlit as st
from utils.utils import navigate_to
import datetime
import pandas as pd
import time

from assets.data import obtener_lista_tipos_proyectos
from api.posts import crear_proyecto_completo, crear_tipo_proyecto, obtener_tipos_proyectos

def pantalla_registro_proyecto():

    st.markdown("## Registro de Proyecto")
    if st.button("⬅️ Atrás"):
        navigate_to("registro_evento")
        st.rerun()

    # Toggle para debug
    debug_mode = st.checkbox("🔧 Modo Debug", help="Muestra información detallada del proceso")

    # 1. Obtener tipos existentes
    tipos_proyectos = obtener_lista_tipos_proyectos() or []
    
    if debug_mode and tipos_proyectos:
        st.write("🔍 **Tipos de proyecto disponibles:**")
        st.json(tipos_proyectos)
    
    # 2. Campos del formulario
    nombre_proyecto = st.text_input("Nombre del proyecto*", placeholder="Ej: Plataforma Web")
    
    # Opciones para tipo de proyecto
    crear_nuevo_tipo = st.checkbox("Crear nuevo tipo de proyecto")
    
    if crear_nuevo_tipo:
        nuevo_tipo_nombre = st.text_input("Nombre del nuevo tipo*")
        tipo_seleccionado = None
    else:
        nuevo_tipo_nombre = None
        if tipos_proyectos:
            # Crear opciones con índices para evitar problemas de referencia
            opciones = [(i, tp["id"], tp["nombre"]) for i, tp in enumerate(tipos_proyectos)]
            
            seleccion = st.selectbox(
                "Tipo de proyecto existente",
                options=range(len(tipos_proyectos)),
                format_func=lambda x: tipos_proyectos[x]["nombre"]
            )
            
            if seleccion is not None:
                tipo_seleccionado = tipos_proyectos[seleccion]
            else:
                tipo_seleccionado = None
        else:
            tipo_seleccionado = None
            st.warning("No hay tipos disponibles. Crea uno nuevo.")

    # Fechas
    col1, col2 = st.columns(2)
    with col1:
        fecha_inicio = st.date_input("Fecha inicio*", value=datetime.date.today())
    with col2:
        fecha_final = st.date_input("Fecha final*", value=datetime.date.today())

    # 3. Registro
    if st.button("Guardar proyecto", type="primary"):
        # Validaciones básicas
        errores = []
        
        if not nombre_proyecto.strip():
            errores.append("Nombre del proyecto es obligatorio")
            
        if crear_nuevo_tipo and not nuevo_tipo_nombre.strip():
            errores.append("Nombre del nuevo tipo es obligatorio")
            
        if not crear_nuevo_tipo and not tipo_seleccionado:
            errores.append("Debe seleccionar un tipo de proyecto existente")
            
        if fecha_final < fecha_inicio:
            errores.append("La fecha final no puede ser anterior a la fecha de inicio")
        
        if errores:
            for error in errores:
                st.error(f"❌ {error}")
            return
            
        # Procesamiento
        progress_bar = st.progress(0)
        status_text = st.empty()
        
        try:
            tipo_proyecto = None
            
            # PASO 1: Manejar el tipo de proyecto
            if crear_nuevo_tipo:
                status_text.text("Paso 1/2: Creando nuevo tipo de proyecto...")
                progress_bar.progress(25)
                
                if debug_mode:
                    st.write(f"🔍 **Creando tipo:** {nuevo_tipo_nombre}")
                
                tipo_creado = crear_tipo_proyecto(nuevo_tipo_nombre.strip())
                
                if tipo_creado:
                    if debug_mode:
                        st.write("✅ **Respuesta del tipo creado:**")
                        st.json(tipo_creado)
                    
                    # Verificar estructura de la respuesta
                    if isinstance(tipo_creado, dict):
                        if "id" in tipo_creado and "nombre" in tipo_creado:
                            tipo_proyecto = {
                                "id": tipo_creado["id"],
                                "nombre": tipo_creado["nombre"]
                            }
                            st.success(f"✅ Tipo '{nuevo_tipo_nombre}' creado con ID: {tipo_creado['id']}")
                        else:
                            st.error("❌ El tipo se creó pero la respuesta no tiene la estructura esperada")
                            if debug_mode:
                                st.write("Estructura esperada: {'id': int, 'nombre': str}")
                            return
                    else:
                        st.error("❌ La respuesta del tipo de proyecto no es un diccionario")
                        return
                else:
                    st.error("❌ Error al crear el nuevo tipo de proyecto")
                    return
                    
            else:
                # Usar tipo existente
                progress_bar.progress(25)
                tipo_proyecto = {
                    "id": tipo_seleccionado["id"],
                    "nombre": tipo_seleccionado["nombre"]
                }
                if debug_mode:
                    st.write("✅ **Usando tipo existente:**")
                    st.json(tipo_proyecto)
            
            # Pequeña pausa para simular procesamiento y dar tiempo a la API
            time.sleep(2)  # Aumenté el tiempo de espera
            
            # Verificación adicional: refrescar la lista de tipos
            if crear_nuevo_tipo and tipo_proyecto:
                st.info("🔄 Verificando que el tipo esté disponible...")
                tipos_actualizados = obtener_tipos_proyectos()
                tipo_verificado = next(
                    (t for t in tipos_actualizados if t["id"] == tipo_proyecto["id"]), 
                    None
                )
                if not tipo_verificado:
                    st.error("❌ El tipo creado no se encuentra en la base de datos")
                    return
                else:
                    st.success("✅ Tipo verificado en la base de datos")
            
            # PASO 2: Crear proyecto completo
            status_text.text("Paso 2/2: Creando proyecto...")
            progress_bar.progress(50)
            
            proyecto_data = {
                "id": 0,
                "nombre": nombre_proyecto.strip(),
                "fechainicio": f"{fecha_inicio.isoformat()}T00:00:00.000Z",
                "fechafinal": f"{fecha_final.isoformat()}T00:00:00.000Z",
                "tipoproyecto": tipo_proyecto,
                "actividades": []
            }
            
            if debug_mode:
                st.write("🔍 **Datos del proyecto a enviar:**")
                st.json(proyecto_data)
            
            progress_bar.progress(75)
            resultado = crear_proyecto_completo(proyecto_data)
            
            if resultado:
                progress_bar.progress(100)
                status_text.text("✅ ¡Proyecto creado exitosamente!")
                
                proyecto_id = resultado.get('id', 'N/A')
                st.success(f"🎉 Proyecto '{nombre_proyecto}' creado exitosamente!")
                st.info(f"📝 ID del proyecto: {proyecto_id}")
                
                # Mostrar resumen
                with st.expander("📋 Resumen del proyecto creado", expanded=True):
                    col1, col2 = st.columns(2)
                    with col1:
                        st.write(f"**Nombre:** {nombre_proyecto}")
                        st.write(f"**Tipo:** {tipo_proyecto['nombre']} (ID: {tipo_proyecto['id']})")
                    with col2:
                        st.write(f"**Fecha inicio:** {fecha_inicio}")
                        st.write(f"**Fecha final:** {fecha_final}")
                        st.write(f"**ID proyecto:** {proyecto_id}")
                
                st.balloons()
                
                # Opcional: botón para crear otro proyecto
                if st.button("➕ Crear otro proyecto"):
                    st.rerun()
                    
            else:
                progress_bar.progress(0)
                status_text.text("❌ Error al crear el proyecto")
                st.error("❌ No se pudo crear el proyecto. Revisa los logs de debug.")
                
        except Exception as e:
            progress_bar.progress(0)
            status_text.text("❌ Error inesperado")
            st.error(f"💥 Error inesperado: {str(e)}")
            if debug_mode:
                st.exception(e)