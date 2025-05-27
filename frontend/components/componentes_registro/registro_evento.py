import streamlit as st
from utils.utils import navigate_to
from assets.data import obtener_lista_lugares
from api.posts import crear_actividad_completa, crear_lugar
import datetime

def pantalla_registro_evento():
    st.markdown("## Registro de Actividad")
    
    # Inicializar estado de sesión
    if 'actividad_temp' not in st.session_state:
        st.session_state.actividad_temp = {
            'nombre': '',
            'fecha_inicio': None,
            'fecha_fin': None,
            'lugar': '',
            'lugar_obj': None,
            'participantes': []
        }

    if st.button("⬅️ Atrás"):
        navigate_to("registrar")
        st.rerun()

    # Campos principales de la actividad
    nombre_actividad = st.text_input("Nombre Actividad/Taller*", 
                                   value=st.session_state.actividad_temp['nombre'])
    
    # Actualizar el estado cuando el usuario cambie el nombre
    if nombre_actividad != st.session_state.actividad_temp['nombre']:
        st.session_state.actividad_temp['nombre'] = nombre_actividad
    
    col1, col2 = st.columns(2)
    with col1:
        fecha_inicio = st.date_input("Fecha inicio*", 
                                   value=st.session_state.actividad_temp['fecha_inicio'] or datetime.date.today())
        # Actualizar estado
        if fecha_inicio != st.session_state.actividad_temp['fecha_inicio']:
            st.session_state.actividad_temp['fecha_inicio'] = fecha_inicio
            
    with col2:
        fecha_fin = st.date_input("Fecha fin*", 
                                value=st.session_state.actividad_temp['fecha_fin'] or datetime.date.today())
        # Actualizar estado
        if fecha_fin != st.session_state.actividad_temp['fecha_fin']:
            st.session_state.actividad_temp['fecha_fin'] = fecha_fin
    
    # Sección de lugar con opción de agregar nuevo
    st.markdown("### Lugar del evento")
    
    # Obtener lugares de la API
    lugares_api = obtener_lista_lugares()
    lugares_nombres = [lugar['nombre'] for lugar in lugares_api]
    lugares_nombres.append("➕ Agregar nuevo lugar")
    
    # Encontrar índice del lugar seleccionado
    lugar_index = 0
    if (st.session_state.actividad_temp['lugar'] and 
        st.session_state.actividad_temp['lugar'] in lugares_nombres[:-1]):  # Excluir "Agregar nuevo"
        lugar_index = lugares_nombres.index(st.session_state.actividad_temp['lugar'])
    
    lugar_seleccionado = st.selectbox("Lugar*", options=lugares_nombres, index=lugar_index)
    
    # Manejar selección de lugar
    if lugar_seleccionado == "➕ Agregar nuevo lugar":
        # Mostrar campo para nuevo lugar
        nuevo_lugar = st.text_input("Nombre del nuevo lugar*", 
                                  placeholder="Ingrese el nombre del lugar")
        
        col_crear, col_cancelar = st.columns(2)
        with col_crear:
            if st.button("✅ Crear lugar") and nuevo_lugar.strip():
                with st.spinner("Creando lugar..."):
                    resultado = crear_lugar(nuevo_lugar.strip())
                    if resultado:
                        st.success(f"✅ Lugar '{nuevo_lugar}' creado exitosamente")
                        # Actualizar estado con el nuevo lugar
                        st.session_state.actividad_temp['lugar'] = nuevo_lugar.strip()
                        st.session_state.actividad_temp['lugar_obj'] = resultado
                        st.rerun()
                    else:
                        st.error("❌ Error al crear el lugar")
        
        with col_cancelar:
            if st.button("❌ Cancelar"):
                st.rerun()
                
    else:
        # Lugar existente seleccionado
        if lugar_seleccionado != st.session_state.actividad_temp['lugar']:
            st.session_state.actividad_temp['lugar'] = lugar_seleccionado
            # Encontrar el objeto lugar correspondiente
            lugar_obj = next((lugar for lugar in lugares_api if lugar['nombre'] == lugar_seleccionado), None)
            st.session_state.actividad_temp['lugar_obj'] = lugar_obj

    # Mostrar lugar seleccionado
    if st.session_state.actividad_temp['lugar'] and st.session_state.actividad_temp['lugar'] != "➕ Agregar nuevo lugar":
        st.info(f"📍 Lugar seleccionado: **{st.session_state.actividad_temp['lugar']}**")

    # Sección de participantes
    st.markdown("### Lista de participantes registrados")
    if st.session_state.actividad_temp['participantes']:
        # Mostrar participantes con opción de eliminar
        for i, p in enumerate(st.session_state.actividad_temp['participantes']):
            nombre_completo = f"{p['nombre1']} {p['nombre2']} {p['apellido1']} {p['apellido2']}".strip()
            nombre_completo = ' '.join(nombre_completo.split())  # Eliminar espacios extra
            
            col1, col2 = st.columns([5, 1])
            with col1:
                st.write(f"**{i+1}.** {nombre_completo} - {p['tipoiden']['nombre']}: {p['identificacion']} - {p['municipio']['nombre']}")
            with col2:
                # Usar un key único y estable para cada botón
                btn_key = f"btn_eliminar_{p['identificacion']}_{i}"
                if st.button("🗑️", key=btn_key, help="Eliminar participante"):
                    quitar_participante(i)
                    st.rerun()
        
        st.info(f"Total participantes: {len(st.session_state.actividad_temp['participantes'])}")
    else:
        st.write("No hay participantes registrados aún")

    # Botones de acción
    col1, col2 = st.columns(2)
    with col1:
        if st.button("➕ Registrar participante"):
            navigate_to('registrar', "registro_participante")
            st.rerun()
            
    with col2:
        if st.button("💾 Guardar Actividad", type="primary"):
            if validar_actividad():
                guardar_actividad()

def quitar_participante(indice):
    """Elimina un participante de la lista temporal por su índice"""
    if 'actividad_temp' in st.session_state and indice < len(st.session_state.actividad_temp['participantes']):
        participante_eliminado = st.session_state.actividad_temp['participantes'].pop(indice)
        nombre_completo = f"{participante_eliminado['nombre1']} {participante_eliminado['nombre2']} {participante_eliminado['apellido1']} {participante_eliminado['apellido2']}".strip()
        nombre_completo = ' '.join(nombre_completo.split())
        st.success(f"Participante {nombre_completo} eliminado de la lista")

def validar_actividad():
    errores = []
    
    if not st.session_state.actividad_temp['nombre'].strip():
        errores.append("Nombre de la actividad es obligatorio")
    if not st.session_state.actividad_temp['fecha_inicio']:
        errores.append("Fecha de inicio es obligatoria")
    if not st.session_state.actividad_temp['lugar'] or st.session_state.actividad_temp['lugar'] == "➕ Agregar nuevo lugar":
        errores.append("Debe seleccionar o crear un lugar")
    if not st.session_state.actividad_temp['participantes']:
        errores.append("Debe registrar al menos un participante")
    
    for error in errores:
        st.error(error)
    return len(errores) == 0

def guardar_actividad():
    # Obtener información del lugar
    lugar_obj = st.session_state.actividad_temp['lugar_obj']
    
    # Si no tenemos el objeto lugar, crearlo con ID 0 (nuevo)
    if not lugar_obj:
        lugar_obj = {
            "id": 0,
            "nombre": st.session_state.actividad_temp['lugar']
        }
    
    # Construir estructura de la actividad según el JSON requerido
    actividad_data = {
        "id": 0,
        "nombre": st.session_state.actividad_temp['nombre'].strip(),
        "fechaInicio": st.session_state.actividad_temp['fecha_inicio'].isoformat() + "T00:00:00.000Z",
        "fechaFinal": st.session_state.actividad_temp['fecha_fin'].isoformat() + "T23:59:59.999Z",
        "lugar": {
            "id": lugar_obj['id'],
            "nombre": lugar_obj['nombre']
        },
        "beneficiarios": st.session_state.actividad_temp['participantes']
    }
    
    # Mostrar datos que se van a enviar (para debug)
    with st.expander("🔍 Ver datos que se enviarán a la API"):
        st.json(actividad_data)
    
    resultado = crear_actividad_completa(actividad_data)
    
    if resultado:
        st.success("✅ Actividad registrada exitosamente!")
        st.balloons()
        
        # Mostrar resumen
        st.markdown("### Resumen de la actividad creada:")
        st.write(f"**Nombre:** {actividad_data['nombre']}")
        st.write(f"**Fecha inicio:** {st.session_state.actividad_temp['fecha_inicio']}")
        st.write(f"**Fecha fin:** {st.session_state.actividad_temp['fecha_fin']}")
        st.write(f"**Lugar:** {lugar_obj['nombre']}")
        st.write(f"**Participantes:** {len(actividad_data['beneficiarios'])}")
        
        # Resetear estado
        st.session_state.actividad_temp = {
            'nombre': '',
            'fecha_inicio': None,
            'fecha_fin': None,
            'lugar': '',
            'lugar_obj': None,
            'participantes': []
        }
        
        # Opción para regresar o crear otra actividad
        col1, col2 = st.columns(2)
        with col1:
            if st.button("🏠 Ir al menú principal"):
                navigate_to("registrar")
                st.rerun()
        with col2:
            if st.button("➕ Crear nueva actividad"):
                st.rerun()
    else:
        st.error("❌ Error al guardar la actividad. Revisa los logs de debug arriba.")

def limpiar_actividad_temporal():
    """Función para limpiar la actividad temporal si es necesario"""
    if 'actividad_temp' in st.session_state:
        st.session_state.actividad_temp = {
            'nombre': '',
            'fecha_inicio': None,
            'fecha_fin': None,
            'lugar': '',
            'lugar_obj': None,
            'participantes': []
        }
        st.success("Actividad temporal limpiada")