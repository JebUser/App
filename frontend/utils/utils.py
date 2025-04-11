# utils.py
import streamlit as st

def init_session_state():
    if 'current_page' not in st.session_state:
        st.session_state.current_page = 'inicio'
    if 'subpage' not in st.session_state:
        st.session_state.subpage = None

def navigate_to(page, subpage=None):
    st.session_state.current_page = page
    st.session_state.subpage = subpage
    st.rerun()
    # No necesitas st.rerun() aquí porque Streamlit manejará la navegación