<template>
  <div class="app">
    <component :is="getLayout" />
  </div>
</template>

<script>
import DefaultLayout from '@/layouts/default.vue'

const LAYOUTS = {
  default: DefaultLayout,
}

export default {
  name: 'App',
  components: {
    DefaultLayout,
  },

  watch: {
    '$route.meta.background'(background) {
      document.body.style.background = background
    },
  },

  computed: {
    getLayout() {
      const { layout } = this.$route.meta

      return LAYOUTS[layout]
    },
  },
}
</script>

<style>
.app {
  height: 100%;
}
</style>
