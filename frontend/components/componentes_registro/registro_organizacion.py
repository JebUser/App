import streamlit as st
from utils.utils import navigate_to
from assets.data import obtener_lista_municipios
from assets.data import obtener_lista_tipos_organizacion
from assets.data import obtener_lista_tipos_apoyo
#from assets.data import obtener_lista_lineas_producto

def pantalla_registro_organizacion():

    municipios = obtener_lista_municipios(formato='select')
    Organizaciones = obtener_lista_tipos_organizacion(formato='select')
    Tipoapoyos = obtener_lista_tipos_apoyo(formato='select')
 #   Lineaprods = obtener_lista_lineas_producto(formato='select')
    
    st.markdown("## Registro organización")
    if st.button("⬅️ Atrás"):
        navigate_to("registrar")
        st.rerun()

    col1, col2 = st.columns(2)
    with col1:
        st.text_input("Nombre de la organización")
        st.text_input("NIT")
        Organizacion =  st.selectbox(
        "Organizaciones*",
        options=Organizaciones,
        index=0,  # Selecciona el primer elemento por defecto
        key="Organizacion_select",
        help="Seleccione la Organizacion de la lista"
    )
        

  #      st.text_input("Línea productiva (si aplica)") #este
  #      Lineaprod = st.selectbox(
  #      "Lineaprods*",
  #      options=Lineaprods,
  #      index=0,  # Selecciona el primer elemento por defecto
  #      key="Lineaprod_select",
  #      help="Seleccione el Lineaprods de la lista"
  #  )


        st.number_input("Número de integrantes", min_value=1)

    with col2:
        municipio = st.selectbox(
        "Municipio*",
        options=municipios,
        index=0,  # Selecciona el primer elemento por defecto
        key="municipio_select",
        help="Seleccione el municipio de la lista"
    )
        
        st.number_input("Número de mujeres", min_value=0)

        
        Tipoapoyo = st.selectbox(
        "Tipoapoyos*",
        options=Tipoapoyos,
        index=0,  # Selecciona el primer elemento por defecto
        key="Tipoapoyos_select",
        help="Seleccione el Tipoapoyos de la lista"
    )    
        st.text_input("Otro apoyo")
        st.selectbox("¿Es una organización de mujeres?", ["Sí", "No"])

    st.button("Registrar")
