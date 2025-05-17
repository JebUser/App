import streamlit as st
from utils.utils import navigate_to
from assets.data import obtener_lista_generos
from assets.data import obtener_lista_tipos_identificacion
from assets.data import obtener_lista_municipios
#from assets.data import obtener_lista_grupos_etnicos
from assets.data import obtener_lista_tipos_beneficiario
from assets.data import obtener_lista_sectores
from assets.data import obtener_lista_organizaciones

def pantalla_registro_participante():

    generos = obtener_lista_generos()
    Tipoidens = obtener_lista_tipos_identificacion(formato='select')
    municipios = obtener_lista_municipios(formato='select')
    #Grupoetnicos = obtener_lista_grupos_etnicos(formato='select')
    Tipobenes = obtener_lista_tipos_beneficiario(formato='select')
    Sectores = obtener_lista_sectores(formato='select')
    Organizaciones = obtener_lista_organizaciones(formato='select')

    st.markdown("## Registro participante")
    if st.button("⬅️ Atrás"):
        navigate_to("registro_evento")
        st.rerun()

    col1, col2 = st.columns(2)
    with col1:
        st.text_input("Nombre y apellido")

        #st.selectbox("Sexo", ["Masculino", "Femenino", "Otro"]) #este

        Genero = st.selectbox(
        "Generos*",
        options=generos,
        index=0,  # Selecciona el primer elemento por defecto
        key="Genero_select",
        help="Seleccione el Generos de la lista"
        
    )

        Tipoiden = st.selectbox(
        "Tipoidens*",
        options=Tipoidens,
        index=0,  # Selecciona el primer elemento por defecto
        key="Tipoidens_select",
        help="Seleccione el Tipoidens de la lista"
    )
        st.text_input("Número de documento")
        st.number_input("Edad", min_value=0, max_value=120)

    with col2:

        municipio = st.selectbox(
        "Municipio*",
        options=municipios,
        index=0,  # Selecciona el primer elemento por defecto
        key="municipio_select",
        help="Seleccione el municipio de la lista"
    )

        st.selectbox("¿Pertenece a un grupo étnico?", ["Sí", "No"]) # este

#        Grupoetnico = st.selectbox(
#        "Grupoetnico*",
#        options=Grupoetnicos,
#        index=0,  # Selecciona el primer elemento por defecto
#        key="Grupoetnico_select",
#        help="Seleccione el Grupoetnico de la lista"
#    )

        Tipobene = st.selectbox(
        "Tipobene*",
        options=Tipobenes,
        index=0,  # Selecciona el primer elemento por defecto
        key="Tipobene_select",
        help="Seleccione el Tipobene de la lista"
    )        
        
        Organizacion = st.selectbox(
        "Organizacion*",
        options=Organizaciones,
        index=0,  # Selecciona el primer elemento por defecto
        key="Organizacion_select",
        help="Seleccione el Organizacion de la lista"
    )

        Sector = st.selectbox(
        "Sector*",
        options=Sectores,
        index=0,  # Selecciona el primer elemento por defecto
        key="Sectores_select",
        help="Seleccione el Sector de la lista"
    )  
        
        st.text_input("Celular")

    st.button("Registrar participante")
