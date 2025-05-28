@echo off
echo Activando entorno virtual y ejecutando la aplicacion...

:: Verifica si el entorno virtual existe
if not exist env (
    echo El entorno virtual no existe. Creando uno...
    python -m venv env
)

:: Activar el entorno virtual
call env\Scripts\activate

:: Instalar dependencias (si es necesario)
echo Instalando dependencias...
pip install -r requirements.txt

:: Cambiar a la carpeta frontend
cd frontend

:: Ejecutar la aplicacion en Streamlit
echo Iniciando la aplicacion...
start "" "streamlit" "run" "streamlit_app.py"

:: Volver a la carpeta raíz (App)
cd ..

:: Mensaje final
echo Aplicación iniciada. Puedes cerrar esta ventana.
pause

