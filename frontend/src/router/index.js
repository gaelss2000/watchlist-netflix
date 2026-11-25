import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
    },
    {
      path: '/titulo/:id',
      name: 'titulo',
      component: () => import('../views/TituloView.vue'),
    },
    {
      path: '/login',
      name: 'login',
      component: () => import('../views/LoginView.vue'),
    },
    {
      path: '/registro',
      name: 'registro',
      component: () => import('../views/RegistroView.vue'),
    },
    {
      path: '/listas',
      name: 'listas',
      component: () => import('../views/ListasView.vue'),
    },
    {
      path: '/listas/:id',
      name: 'lista-detalle',
      component: () => import('../views/ListaDetalleView.vue'),
    },
  ],
})

export default router
