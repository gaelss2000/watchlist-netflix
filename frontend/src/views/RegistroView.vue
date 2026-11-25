<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { register } from '../auth'

const router = useRouter()
const username = ref('')
const password = ref('')
const error = ref('')
const enviando = ref(false)

async function registrarse() {
  error.value = ''
  enviando.value = true

  try {
    const resultado = await register(username.value, password.value)
    if (resultado.ok) {
      router.push('/')
    } else {
      error.value = resultado.error
    }
  } catch {
    error.value = 'No se pudo conectar con el servidor. Inténtalo de nuevo.'
  } finally {
    enviando.value = false
  }
}
</script>

<template>
  <div class="mx-auto" style="max-width: 360px">
    <h1 class="h3 mb-4">Crear cuenta</h1>
    <form @submit.prevent="registrarse">
      <label for="username" class="form-label">Usuario</label>
      <input
        id="username"
        v-model.trim="username"
        class="form-control mb-3"
        placeholder="Usuario"
        autocomplete="username"
        maxlength="50"
        required
      />
      <label for="password" class="form-label">Contraseña</label>
      <input
        id="password"
        v-model="password"
        type="password"
        class="form-control mb-3"
        placeholder="Contraseña"
        autocomplete="new-password"
        required
      />
      <p v-if="error" class="text-danger" role="alert">{{ error }}</p>
      <button class="btn btn-danger w-100" :disabled="enviando">
        {{ enviando ? 'Creando cuenta…' : 'Registrarse' }}
      </button>
    </form>
    <p class="mt-3 mb-0 text-center">
      ¿Ya tienes cuenta? <RouterLink to="/login">Inicia sesión</RouterLink>
    </p>
  </div>
</template>
