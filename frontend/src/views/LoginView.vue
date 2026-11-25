<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { login } from '../auth'

const router = useRouter()
const username = ref('')
const password = ref('')
const error = ref('')

async function entrar() {
  const ok = await login(username.value, password.value)
  if (ok) {
    router.push('/')
  } else {
    error.value = 'Usuario o contraseña incorrectos.'
  }
}
</script>

<template>
  <div class="mx-auto" style="max-width: 360px">
    <h1 class="h3 mb-4">Iniciar sesión</h1>
    <form @submit.prevent="entrar">
      <input v-model="username" class="form-control mb-3" placeholder="Usuario" />
      <input v-model="password" type="password" class="form-control mb-3" placeholder="Contraseña" />
      <p v-if="error" class="text-danger">{{ error }}</p>
      <button class="btn btn-danger w-100">Entrar</button>
    </form>
  </div>
</template>