import streamlit as st
from components import inicio  # Importamos la página de inicio

# Configuración de la página
st.set_page_config(page_title="Portal de Eventos", page_icon="📅", layout="wide")

# Sidebar de navegación (único)
with st.sidebar:
    st.image("assets/logo.png", use_container_width=True)  # Logo
    st.markdown("## Opciones")  # Nombre de la app
    opcion = st.radio("##", ["🏠 Inicio", "📝 Registrar información", "✏ Modificar información", "🔍 Consultar eventos"])

# Mostrar la página seleccionada
if opcion == "🏠 Inicio":
    inicio.mostrar()
else:
    st.write(f"🚧 La página '{opcion}' está en construcción...")

