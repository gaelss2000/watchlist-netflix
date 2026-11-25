<script setup>
import { ref, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { API_URL } from '../api'
import { usuario } from '../auth'

const route = useRoute()
const titulo = ref(null)
const noExiste = ref(false)

const listas = ref([])
const listaElegida = ref('')
const mensaje = ref('')
const mensajeOk = ref(true)

async function añadir() {
  if (!listaElegida.value) return
  const respuesta = await fetch(
    `${API_URL}/listas/${listaElegida.value}/titulos/${titulo.value.id}`,
    { method: 'POST' }
  )
  if (respuesta.ok) {
    mensaje.value = 'Añadido a la lista.'
    mensajeOk.value = true
  } else if (respuesta.status === 409) {
    mensaje.value = 'Este título ya está en esa lista.'
    mensajeOk.value = false
  } else {
    mensaje.value = 'No se ha podido añadir.'
    mensajeOk.value = false
  }
}

onMounted(async () => {
  const respuesta = await fetch(`${API_URL}/titulos/${route.params.id}`)
  if (respuesta.status === 404) {
    noExiste.value = true
    return
  }
  titulo.value = await respuesta.json()

  if (usuario.value) {
    const r = await fetch(`${API_URL}/listas?usuarioId=${usuario.value.id}`)
    listas.value = await r.json()
  }
})
</script>

<template>
  <RouterLink to="/" class="btn btn-link px-0 mb-3">← Volver al catálogo</RouterLink>

  <p v-if="noExiste">Este título no existe.</p>

  <div v-else-if="titulo" class="row g-4">
    <div class="col-12 col-md-4">
      <img :src="titulo.imagenUrl" class="img-fluid rounded" :alt="titulo.nombre" />
    </div>
    <div class="col-12 col-md-8">
      <h1 class="h3">{{ titulo.nombre }}</h1>
      <p class="text-muted">{{ titulo.tipo }} · {{ titulo.anioEstreno }}</p>
      <div class="mb-3">
        <span v-for="g in titulo.generos" :key="g" class="badge bg-secondary me-1">{{ g }}</span>
      </div>
      <p>{{ titulo.sinopsis }}</p>

      <div v-if="usuario" class="mt-4">
        <div v-if="listas.length" class="input-group" style="max-width: 400px">
          <select v-model="listaElegida" class="form-select">
            <option value="">Elige una lista</option>
            <option v-for="l in listas" :key="l.id" :value="l.id">{{ l.nombre }}</option>
          </select>
          <button class="btn btn-danger" @click="añadir">Añadir</button>
        </div>
        <p v-else class="text-muted">
          Aún no tienes listas. <RouterLink to="/listas">Crea una</RouterLink>.
        </p>
        <p v-if="mensaje" :class="mensajeOk ? 'text-success' : 'text-danger'" class="mt-2">
          {{ mensaje }}
        </p>
      </div>
      <p v-else class="text-muted mt-4">
        <RouterLink to="/login">Inicia sesión</RouterLink> para añadir este título a una lista.
      </p>
    </div>
  </div>
</template>