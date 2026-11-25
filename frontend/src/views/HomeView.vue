<script setup>
import { ref, onMounted } from 'vue'
import { API_URL } from '../api'

const titulos = ref([])
const generos = ref([])
const cargando = ref(true)
const error = ref('')
const errorGeneros = ref('')

const filtros = ref({ nombre: '', tipo: '', genero: '', anio: '' })
let busquedaActual = 0

async function buscar() {
  const numeroBusqueda = ++busquedaActual
  cargando.value = true
  error.value = ''

  const params = new URLSearchParams()
  for (const [clave, valor] of Object.entries(filtros.value)) {
    if (valor) params.append(clave, valor)
  }

  try {
    const respuesta = await fetch(`${API_URL}/titulos?${params}`)
    if (!respuesta.ok) throw new Error('La búsqueda no se pudo completar.')

    const resultados = await respuesta.json()
    if (numeroBusqueda === busquedaActual) titulos.value = resultados
  } catch {
    if (numeroBusqueda === busquedaActual) {
      titulos.value = []
      error.value = 'No se pudieron cargar los títulos. Inténtalo de nuevo.'
    }
  } finally {
    if (numeroBusqueda === busquedaActual) cargando.value = false
  }
}

async function cargarGeneros() {
  try {
    const respuesta = await fetch(`${API_URL}/generos`)
    if (!respuesta.ok) throw new Error('No se pudieron cargar los géneros.')
    generos.value = await respuesta.json()
  } catch {
    errorGeneros.value = 'No se pudieron cargar los géneros.'
  }
}

function limpiar() {
  filtros.value = { nombre: '', tipo: '', genero: '', anio: '' }
  buscar()
}

onMounted(() => {
  cargarGeneros()
  buscar()
})
</script>

<template>
  <h1 class="h3 mb-4">Catálogo</h1>

  <form class="row g-2 mb-4" @submit.prevent="buscar">
    <div class="col-12 col-md-3">
      <input v-model="filtros.nombre" class="form-control" placeholder="Nombre" />
    </div>
    <div class="col-6 col-md-2">
      <select v-model="filtros.tipo" class="form-select">
        <option value="">Tipo</option>
        <option>Película</option>
        <option>Serie</option>
      </select>
    </div>
    <div class="col-6 col-md-3">
      <select v-model="filtros.genero" class="form-select">
        <option value="">Género</option>
        <option v-for="g in generos" :key="g">{{ g }}</option>
      </select>
    </div>
    <div class="col-6 col-md-2">
      <input v-model="filtros.anio" type="number" class="form-control" placeholder="Año" />
    </div>
    <div class="col-6 col-md-2 d-flex gap-2">
      <button class="btn btn-danger flex-fill">Buscar</button>
      <button type="button" class="btn btn-outline-secondary" @click="limpiar">✕</button>
    </div>
  </form>

  <p v-if="errorGeneros" class="text-warning" role="status">{{ errorGeneros }}</p>
  <div v-if="cargando" class="text-muted py-4 text-center" role="status">
    <span class="spinner-border spinner-border-sm me-2" aria-hidden="true"></span>
    Cargando títulos…
  </div>
  <p v-else-if="error" class="text-danger" role="alert">{{ error }}</p>
  <p v-else-if="titulos.length === 0" class="text-muted">No hay resultados.</p>

  <div v-else class="row g-3">
    <div v-for="t in titulos" :key="t.id" class="col-6 col-md-4 col-lg-3">
      <div class="card h-100">
        <img :src="t.imagenUrl" class="card-img-top" :alt="t.nombre" />
        <div class="card-body">
          <h5 class="card-title">
            <RouterLink :to="`/titulo/${t.id}`" class="stretched-link text-decoration-none text-reset">
              {{ t.nombre }}
            </RouterLink>
          </h5>
          <p class="card-text text-muted">{{ t.tipo }} · {{ t.anioEstreno }}</p>
        </div>
      </div>
    </div>
  </div>
</template>