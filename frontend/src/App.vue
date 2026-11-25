<script setup>
import { ref } from 'vue'
import { RouterView, RouterLink, useRouter } from 'vue-router'
import { usuario, logout } from './auth'

const router = useRouter()
const temaOscuro = ref(localStorage.getItem('watchlist-theme') === 'dark')

document.documentElement.setAttribute('data-bs-theme', temaOscuro.value ? 'dark' : 'light')

function alternarTema() {
  temaOscuro.value = !temaOscuro.value
  const tema = temaOscuro.value ? 'dark' : 'light'
  document.documentElement.setAttribute('data-bs-theme', tema)
  localStorage.setItem('watchlist-theme', tema)
}

function salir() {
  logout()
  router.push('/login')
}
</script>

<template>
  <nav class="navbar navbar-dark bg-dark px-3">
    <RouterLink to="/" class="navbar-brand">Watchlist</RouterLink>
    <div class="d-flex align-items-center gap-3 text-light">
      <template v-if="usuario">
        <RouterLink to="/listas" class="text-light text-decoration-none">Mis listas</RouterLink>
        <span>{{ usuario.username }}</span>
        <button class="btn btn-sm btn-outline-light" @click="salir">Salir</button>
      </template>
      <RouterLink v-else to="/login" class="btn btn-sm btn-outline-light">Entrar</RouterLink>
      <button
        type="button"
        class="btn btn-sm btn-outline-light d-inline-flex align-items-center justify-content-center p-2"
        :aria-label="temaOscuro ? 'Tema oscuro. Cambiar a modo claro' : 'Tema claro. Cambiar a modo oscuro'"
        :title="temaOscuro ? 'Cambiar a modo claro' : 'Cambiar a modo oscuro'"
        @click="alternarTema"
      >
        <svg
          v-if="temaOscuro"
          xmlns="http://www.w3.org/2000/svg"
          width="18"
          height="18"
          viewBox="0 0 24 24"
          fill="none"
          stroke="currentColor"
          stroke-width="1.8"
          stroke-linecap="round"
          stroke-linejoin="round"
          aria-hidden="true"
        >
          <circle cx="12" cy="12" r="4" />
          <path d="M12 2v2m0 16v2M4.93 4.93l1.42 1.42m11.3 11.3 1.42 1.42M2 12h2m16 0h2M4.93 19.07l1.42-1.42m11.3-11.3 1.42-1.42" />
        </svg>
        <svg
          v-else
          xmlns="http://www.w3.org/2000/svg"
          width="18"
          height="18"
          viewBox="0 0 24 24"
          fill="none"
          stroke="currentColor"
          stroke-width="1.8"
          stroke-linecap="round"
          stroke-linejoin="round"
          aria-hidden="true"
        >
          <path d="M20.9 13A9 9 0 0 1 11 3.1 9 9 0 1 0 20.9 13Z" />
        </svg>
      </button>
    </div>
  </nav>
  <main class="container py-4">
    <RouterView />
  </main>
</template>