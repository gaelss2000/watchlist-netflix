<script setup>
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { API_URL } from '../api'
import { usuario } from '../auth'

const router = useRouter()
const listas = ref([])
const nombre = ref('')
const error = ref('')
const eliminandoId = ref(null)

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

async function eliminar(lista) {
  if (!window.confirm(`¿Seguro que quieres eliminar la lista "${lista.nombre}"?`)) return

  error.value = ''
  eliminandoId.value = lista.id

  try {
    const respuesta = await fetch(
      `${API_URL}/listas/${lista.id}?usuarioId=${usuario.value.id}`,
      { method: 'DELETE' },
    )

    if (!respuesta.ok) {
      error.value = (await respuesta.text()) || 'No se pudo eliminar la lista.'
      return
    }

    listas.value = listas.value.filter((item) => item.id !== lista.id)
  } catch {
    error.value = 'No se pudo conectar con el servidor. Inténtalo de nuevo.'
  } finally {
    eliminandoId.value = null
  }
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

  <p v-if="error" class="text-danger" role="alert">{{ error }}</p>
  <p v-if="listas.length === 0" class="text-muted">Todavía no tienes listas.</p>

  <div class="list-group">
    <div
      v-for="l in listas"
      :key="l.id"
      class="list-group-item d-flex align-items-center"
    >
      <RouterLink
        :to="`/listas/${l.id}`"
        class="flex-grow-1 d-flex justify-content-between align-items-center text-body text-decoration-none"
      >
        {{ l.nombre }}
        <span class="badge bg-secondary">{{ l.totalTitulos }}</span>
      </RouterLink>
      <button
        class="btn btn-sm btn-outline-danger ms-3"
        :disabled="eliminandoId === l.id"
        @click="eliminar(l)"
      >
        {{ eliminandoId === l.id ? 'Eliminando…' : 'Eliminar' }}
      </button>
    </div>
  </div>
</template>