@echo off
echo 🔧 Activando entorno virtual y ejecutando la aplicación...

:: Verifica si el entorno virtual existe
if not exist env (
    echo ❌ El entorno virtual no existe. Creando uno...
    python -m venv env
)

:: Activar el entorno virtual
call env\Scripts\activate

:: Instalar dependencias (si es necesario)
echo 📦 Instalando dependencias...
pip install -r requirements.txt

:: Ejecutar la aplicación en Streamlit
echo 🚀 Iniciando la aplicación...
streamlit run app.py

:: Mantener la ventana abierta si hay un error
echo ❌ Si la aplicación no se ejecutó, revisa los mensajes de error.
pause
