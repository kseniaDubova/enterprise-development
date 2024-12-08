<template>
    <div class="content">
      <UITable 
          label="Посещения" 
          :headers="getHeader()" 
          :rows="visits" 
          :grid="'1fr 3fr 3fr 2fr 2fr 1fr'"
          @update="clickUpdate"
          @delete="clickDelete"
          @add="clickAdd"
      />
    </div>
</template>

<script>
import UITable from '@/components/ui/table/table.vue'
export default {
    name: 'VisitsContent',

    components: {
        UITable,
    },

    props: {
        visits: {
            type: Object,
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
                    label: "Пациент",
                },
                {
                    label: "Доктор",
                },
                {
                    label: "Дата",
                },
                {
                    label: "Заключение",
                },
            ]
        },
    },
}
</script>

<style lang="scss">
</style>