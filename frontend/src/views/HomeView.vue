<script setup>
import { ref, onMounted } from 'vue'
import { API_URL } from '../api'

const titulos = ref([])
const generos = ref([])

const filtros = ref({ nombre: '', tipo: '', genero: '', anio: '' })

async function buscar() {
  const params = new URLSearchParams()
  for (const [clave, valor] of Object.entries(filtros.value)) {
    if (valor) params.append(clave, valor)
  }
  const respuesta = await fetch(`${API_URL}/titulos?${params}`)
  titulos.value = await respuesta.json()
}

function limpiar() {
  filtros.value = { nombre: '', tipo: '', genero: '', anio: '' }
  buscar()
}

onMounted(async () => {
  const respuesta = await fetch(`${API_URL}/generos`)
  generos.value = await respuesta.json()
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

  <p v-if="titulos.length === 0" class="text-muted">No hay resultados.</p>

  <div class="row g-3">
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