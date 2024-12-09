<template>
    <div class="content">
      <UITable 
          label="Доктора" 
          :headers="getHeader()" 
          :rows="doctors" 
          :grid="'1fr 2fr 2fr 2fr 2fr 3fr 1fr'"
          @update="clickUpdate"
          @delete="clickDelete"
          @add="clickAdd"
      />
    </div>
  </template>
  
  <script>
  import UITable from '@/components/ui/table/table.vue'
  export default {
      name: 'DoctorsContent',
  
      components: {
          UITable,
      },
  
      props: {
          doctors: {
              type: Array,
          },
      },
  
      methods: {
          clickUpdate(id) {
              this.$emit('update', id)
          },
  
          clickDelete(id) {
              this.$emit('delete', id)
          },
  
          clickAdd() {
              this.$emit('add')
          },
          getHeader() {
              return [
                  {
                      label: "Id",
                  },
                  {
                      label: "Паспорт",
                  },
                  {
                      label: "Имя",
                  },
                  {
                      label: "Дата рождения",
                  },
                  {
                      label: "Стаж работы",
                  },
                  {
                      label: "Специализация",
                  },
              ]
          },
      },
  }
  </script>
  
  <style lang="scss">
  </style>