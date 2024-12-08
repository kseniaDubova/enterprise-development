<template>
  <div class="ui-card" :style="cardStyle">
    <slot></slot>
  </div>
</template>

<script>
export default {
  name: 'UICard',

  props: {
    padding: {
      type: String,
      default: '0',
    },
    background: {
      type: String,
      default: 'white',
    },
    borderSize: {
      type: Number,
      default: 0,
    },
    borderType: {
      type: String,
      default: 'solid',
    },
    borderColor: {
      type: String,
      default: 'white',
    },
    borderRadius: {
      type: Number,
      default: 0,
    },
    shadow: {
      type: String,
      default: '0',
    },
    shadowColor: {
      type: String,
      default: 'white',
    },
    flexDirection: {
      type: String,
      default: 'row',
    },
  },

  computed: {
    cardStyle() {
      return {
        flexDirection: this.flexDirection,
        background: this.background,
        padding: `${this.padding}%`,
        border: `${this.borderSize}px ${this.borderType} ${this.borderColor}`,
        borderRadius: `${this.borderRadius}px`,
        boxShadow: `${this.shadow} ${this.shadowColor}`,
      }
    },
  },
}
</script>

<style lang="scss">
.ui-card {
  display: flex;
}
</style>
