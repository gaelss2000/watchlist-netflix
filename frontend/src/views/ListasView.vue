<script setup>
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { API_URL } from '../api'
import { usuario } from '../auth'

const router = useRouter()
const listas = ref([])
const nombre = ref('')

async function cargar() {
  const respuesta = await fetch(`${API_URL}/listas?usuarioId=${usuario.value.id}`)
  listas.value = await respuesta.json()
}

async function crear() {
  if (!nombre.value.trim()) return
  await fetch(`${API_URL}/listas`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ usuarioId: usuario.value.id, nombre: nombre.value }),
  })
  nombre.value = ''
  cargar()
}

onMounted(() => {
  if (!usuario.value) {
    router.push('/login')
    return
  }
  cargar()
})
</script>

<template>
  <h1 class="h3 mb-4">Mis listas</h1>

  <form class="input-group mb-4" @submit.prevent="crear">
    <input v-model="nombre" class="form-control" placeholder="Nombre de la nueva lista" />
    <button class="btn btn-danger">Crear lista</button>
  </form>

  <p v-if="listas.length === 0" class="text-muted">Todavía no tienes listas.</p>

  <div class="list-group">
    <RouterLink
      v-for="l in listas"
      :key="l.id"
      :to="`/listas/${l.id}`"
      class="list-group-item list-group-item-action d-flex justify-content-between"
    >
      {{ l.nombre }}
      <span class="badge bg-secondary">{{ l.totalTitulos }}</span>
    </RouterLink>
  </div>
</template>