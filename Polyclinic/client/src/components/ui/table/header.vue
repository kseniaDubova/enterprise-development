<template>
  <div class="ui-table-header" :style="headerStyle()">
    <div 
        class="ui-table-header__label"
        v-for="header, index in headers"
        :key="index">
        {{ header.label }}
    </div>
  </div>
</template>

<script>
export default {
    name: 'UITableHeader',

    props: {
        headers: {
            type: Array,
        },
        grid: {
            type: String,
            default: '1fr'
        },
    },

    methods: {
        headerStyle() {
            return {
                gridTemplateColumns: `${this.grid}`,
            }
        },
    },
}
</script>

<style lang="scss">
.ui-table-header {
    display: grid;
    background: rgba(125, 105, 112, 0.5);
    border-radius: 20px 20px 0 0;

    &__label {
        font-size: 20px;
        font-family: 'Montserrat-Medium';
        color: black;
        text-align: center;
        padding: 15px 0;
    }
}

</style>