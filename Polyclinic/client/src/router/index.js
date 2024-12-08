import { createRouter, createWebHistory } from 'vue-router'

import Patients from '@/views/patients.vue'
import Request from '@/views/request.vue'
import Doctors from '@/views/doctors.vue'
import Visits from '@/views/visits.vue'

const BACKGROUNDS = {
    default: '#FFFAFC',
  }

const routes = [
    {
        path: '/',
        name: 'Patients',
        component: Patients,
        meta: {
          layout: 'default',
          background: BACKGROUNDS.default,
        },
      },
      {
        path: '/request',
        name: 'Request',
        component: Request,
        meta: {
          layout: 'default',
          background: BACKGROUNDS.default,
        },
      },
      {
        path: '/doctors',
        name: 'Doctors',
        component: Doctors,
        meta: {
          layout: 'default',
          background: BACKGROUNDS.default,
        },
      },
      {
        path: '/visits',
        name: 'Home',
        component: Visits,
        meta: {
          layout: 'default',
          background: BACKGROUNDS.default,
        },
      },
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
