import { ref } from 'vue'
import { API_URL } from './api'

const guardado = localStorage.getItem('usuario')
export const usuario = ref(guardado ? JSON.parse(guardado) : null)

export async function login(username, password) {
  const respuesta = await fetch(`${API_URL}/auth/login`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ username, password }),
  })
  if (!respuesta.ok) return false

  usuario.value = await respuesta.json()
  localStorage.setItem('usuario', JSON.stringify(usuario.value))
  return true
}

export async function register(username, password) {
  const respuesta = await fetch(`${API_URL}/auth/register`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ username, password }),
  })

  if (!respuesta.ok) {
    return {
      ok: false,
      error: (await respuesta.text()) || 'No se pudo crear la cuenta.',
    }
  }

  usuario.value = await respuesta.json()
  localStorage.setItem('usuario', JSON.stringify(usuario.value))
  return { ok: true }
}

export function logout() {
  usuario.value = null
  localStorage.removeItem('usuario')
}