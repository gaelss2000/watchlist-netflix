<script setup>
import { ref, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { API_URL } from '../api'
import { usuario } from '../auth'

const route = useRoute()
const router = useRouter()
const titulos = ref([])

async function cargar() {
  const respuesta = await fetch(`${API_URL}/listas/${route.params.id}/titulos`)
  titulos.value = await respuesta.json()
}

async function quitar(tituloId) {
  await fetch(`${API_URL}/listas/${route.params.id}/titulos/${tituloId}`, {
    method: 'DELETE',
  })
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
  <RouterLink to="/listas" class="btn btn-link px-0 mb-3">← Mis listas</RouterLink>
  <h1 class="h3 mb-4">Títulos de la lista</h1>

  <p v-if="titulos.length === 0" class="text-muted">Esta lista está vacía.</p>

  <div class="row g-3">
    <div v-for="t in titulos" :key="t.id" class="col-6 col-md-4 col-lg-3">
      <div class="card h-100">
        <img :src="t.imagenUrl" class="card-img-top" :alt="t.nombre" />
        <div class="card-body">
          <h5 class="card-title">
            <RouterLink :to="`/titulo/${t.id}`" class="text-decoration-none text-reset">
              {{ t.nombre }}
            </RouterLink>
          </h5>
          <p class="card-text text-muted">{{ t.tipo }} · {{ t.anioEstreno }}</p>
          <button class="btn btn-sm btn-outline-danger" @click="quitar(t.id)">Quitar</button>
        </div>
      </div>
    </div>
  </div>
</template>