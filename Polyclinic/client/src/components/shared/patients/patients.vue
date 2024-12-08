<template>
  <div class="content">
    <UITable 
        label="Пациенты" 
        :headers="getHeader()" 
        :rows="patients" 
        :grid="'1fr 2fr 2fr 2fr 3fr 1fr'"
        @update="clickUpdate"
        @delete="clickDelete"
        @add="clickAdd"
    />
  </div>
</template>

<script>
import UITable from '@/components/ui/table/table.vue'
export default {
    name: 'PatientsContent',

    components: {
        UITable,
    },

    props: {
        patients: {
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
                    label: "Имя",
                },
                {
                    label: "Паспорт",
                },
                {
                    label: "Дата рождения",
                },
                {
                    label: "Адрес",
                },
            ]
        },
    },
}
</script>

<style lang="scss">
.content {
    display: flex;
    flex-direction: column;
    justify-content: center;
    padding: 0 5%;
}
</style>