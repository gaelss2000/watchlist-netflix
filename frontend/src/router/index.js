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
      path: '/about',
      name: 'about',
      // route level code-splitting
      // this generates a separate chunk (About.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import('../views/AboutView.vue'),
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
