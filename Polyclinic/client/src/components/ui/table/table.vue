<template>
  <div class="ui-table">
    <div class="ui-table__label">
        <h3 class="ui-table__label-text">
            {{ label }}
        </h3>
        <img v-if="button" class="ui-table__label-img" src="@/assets/img/add.svg" @click="clickAdd()">
    </div>
    <div class="ui-table__table">
        <UITableHeader :headers="headers" :grid="grid"/>
        <div class="ui-table__table-body">
            <div 
                class="ui-table__table-body__row"
                v-for="row, index in rows"
                :key="index"
                >
                <UITableRow :button="button" :cells="row" :grid="grid" @update="clickUpdate" @delete="clickDelete"/>
            </div>
        </div>
    </div>
  </div>
</template>

<script>
import UITableHeader from "@/components/ui/table/header.vue"
import UITableRow from "@/components/ui/table/row.vue"
export default {
    name: "UITable",

    components: {
        UITableHeader,
        UITableRow,
    },

    props: {
        label: {
            type: String,
            default: "",
        },
        headers: {
            type: Array,
        },
        rows: {
            type: Array,
        },
        grid: {
            type: String,
            default: '1fr',
        },
        button: {
            type: Boolean,
            default: true,
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
    }
}
</script>

<style lang="scss">
.ui-table {
    display: flex;
    flex-direction: column;

    &__label {
        display: flex;
        flex-direction: row;
        gap: 10px;

        &-text {
            color: black;
            font-size: 40px;
            font-family: 'Montserrat-Light';            
        }

        &-img {
            width: 30px;
        }
    }

    &__table {
        &-body {
            &__row {
                border-left: 2px solid rgba(125, 105, 112, 0.5);
                border-right: 2px solid rgba(125, 105, 112, 0.5);
                &:nth-child(even) {
                    background: rgba(125, 105, 112, 0.1);
                }
                &:last-child {
                    border-radius: 0 0 20px 20px;
                    border-bottom: 2px solid rgba(125, 105, 112, 0.5);
                }
                background: white;
            }
        }
    }
}

</style>