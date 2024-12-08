<template>
    <div class="ui-table-row" :style="rowStyle()">
      <div 
          class="ui-table-row__label"
          v-for="cell, index in cells"
          :key="index">
          {{ cell }}
      </div>
      <div class="ui-table-row__button">
        <button class="ui-table-row__button-update">
            <img 
                class="ui-table-row__button-img" 
                src="@/assets/img/pencil.svg"
                @click="clickUpdate"
            >
        </button>
        <button class="ui-table-row__button-delete">
            <img 
                class="ui-table-row__button-img" 
                src="@/assets/img/trash.svg"
                @click="clickDelete"
            >
        </button>
      </div>
    </div>
  </template>
  
  <script>
  export default {
      name: 'UITableRow',
  
      props: {
          cells: {
              type: Object,
          },
          grid: {
              type: String,
              default: '1fr'
          },
      },
  
      methods: {
          rowStyle() {
              return {
                  gridTemplateColumns: `${this.grid}`,
              }
          },

          clickUpdate() {
            this.$emit('update', this.cells.id)
          },

          clickDelete() {
            this.$emit('delete', this.cells.id)
          },
      },
  }
  </script>
  
<style lang="scss">
.ui-table-row {
    display: grid;

    &__label {
        font-size: 20px;
        font-family: 'Montserrat-Light';
        color: black;
        text-align: center;
        padding: 15px 0;
    }

    &__button {
        display: flex;
        flex-direction: row;
        gap: 10px;
        padding: 10px;
        justify-content: flex-end;

        &-img {
            width: 20px;
        }

        &-update, 
        &-delete {
            border: none;
            background: none;
        }
    }
}
</style>