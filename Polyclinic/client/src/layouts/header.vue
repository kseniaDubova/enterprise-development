<template>
  <div class="header-layouts">
    <UIMenu :links="getMenu()"/>
  </div>
</template>

<script>
import UIMenu from "@/components/ui/menu/menu.vue"
export default {
 name: "HeaderLayouts",

 components: {
    UIMenu,
 },

 methods: {
    getMenu() {
      return [
        {
          id: 0,
          label: 'Пациенты',
          link: '/',
        },
        {
          id: 1,
          label: 'Доктора',
          link: '/doctors',
        },
        {
          id: 2,
          label: 'Посещения',
          link: '/visits',
        },
        {
          id: 1,
          label: 'Запросы',
          link: '/request',
        },
      ]
    },
 },
}
</script>

<style>
.header-layouts {
    display: flex;
    padding: 40px 10%;
    flex-direction: row;
    justify-content: flex-end;
}
</style>