import streamlit as st
import streamlit.components.v1 as components

def mostrar_info_participante(participante):
    """Muestra la información en una mini-pestaña emergente"""
    
    # CSS para el pop-up
    popup_html = f"""
    <style>
    .popup {{
        position: fixed;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
        width: 60%;
        max-width: 600px;
        background: white;
        padding: 20px;
        border-radius: 10px;
        box-shadow: 0 5px 15px rgba(0,0,0,0.3);
        z-index: 1000;
        border: 1px solid #ddd;
    }}
    .popup-header {{
        background: #4b8df8;
        color: white;
        padding: 10px 15px;
        border-radius: 8px 8px 0 0;
        margin: -20px -20px 15px -20px;
        display: flex;
        justify-content: space-between;
    }}
    .popup-close {{
        background: transparent;
        border: none;
        color: white;
        font-size: 20px;
        cursor: pointer;
    }}
    </style>

    <div class="popup">
        <div class="popup-header">
            <h4>{participante.get('Nombre', 'Participante')}</h4>
            <button class="popup-close" onclick="parent.document.querySelector('.popup').style.display='none'">×</button>
        </div>
        <div style="display: grid; grid-template-columns: 1fr 1fr; gap: 15px;">
            <div>
                <p><strong>Municipio:</strong><br>{participante.get('Municipio', 'N/A')}</p>
                <p><strong>Sexo:</strong><br>{participante.get('Sexo', 'N/A')}</p>
                <p><strong>Tipo documento:</strong><br>{participante.get('Tipo Documento', 'N/A')}</p>
            </div>
            <div>
                <p><strong>N° Documento:</strong><br>{participante.get('Documento', 'N/A')}</p>
                <p><strong>Edad:</strong><br>{participante.get('Edad', 'N/A')}</p>
                <p><strong>Celular:</strong><br>{participante.get('Celular', 'N/A')}</p>
            </div>
        </div>
        <div style="margin-top: 20px; display: flex; gap: 10px;">
            <button style="padding: 8px 15px; background: #4b8df8; color: white; border: none; border-radius: 5px;">Editar</button>
            <button style="padding: 8px 15px; background: #2ecc71; color: white; border: none; border-radius: 5px;">Exportar</button>
        </div>
    </div>
    """
    
    # Mostrar el pop-up
    components.html(popup_html, height=300)