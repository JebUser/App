# frontend/assets/data.py
from .data import obtener_lista_municipios
from .data import obtener_lista_organizaciones
from .data import obtener_lista_tipos_organizacion
from .data import obtener_lista_tipos_apoyo
from .data import obtener_lista_generos
from .data import obtener_lista_lineas_produccion
from .data import obtener_lista_tipos_identificacion
#from .data import obtener_lista_grupos_etnicos
from .data import obtener_lista_tipos_beneficiario
from .data import obtener_lista_sectores
from assets.data import obtener_lista_organizaciones

# ... (todas tus funciones existentes)

# Lista de todas las funciones exportables
__all__ = [
    'obtener_lista_generos',
    'obtener_lista_grupos_etnicos',
    'obtener_lista_lineas_producto',
    'obtener_lista_municipios',
    'obtener_lista_organizaciones',
    'obtener_lista_sectores',
    'obtener_lista_tipos_actividad',
    'obtener_lista_tipos_apoyo',
    'obtener_lista_tipos_beneficiario',
    'obtener_lista_tipos_identificacion',
    'obtener_lista_tipos_organizacion',
    'obtener_lista_lineas_produccion'
]