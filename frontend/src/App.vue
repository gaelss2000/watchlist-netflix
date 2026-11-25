<script setup>
import { RouterView, RouterLink, useRouter } from 'vue-router'
import { usuario, logout } from './auth'

const router = useRouter()

function salir() {
  logout()
  router.push('/login')
}
</script>

<template>
  <nav class="navbar navbar-dark bg-dark px-3">
    <RouterLink to="/" class="navbar-brand">Watchlist</RouterLink>
    <div v-if="usuario" class="d-flex align-items-center gap-3 text-light">
      <RouterLink to="/listas" class="text-light text-decoration-none">Mis listas</RouterLink>
      <span>{{ usuario.username }}</span>
      <button class="btn btn-sm btn-outline-light" @click="salir">Salir</button>
    </div>
    <RouterLink v-else to="/login" class="btn btn-sm btn-outline-light">Entrar</RouterLink>
  </nav>
  <main class="container py-4">
    <RouterView />
  </main>
</template>