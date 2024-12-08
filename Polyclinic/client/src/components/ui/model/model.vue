<template>
  <div class="model-add" @click="clickClose">
    <UICard 
        padding="5" 
        :border-radius="20"
        class="model-add__card"
    >
        <slot></slot>
    </UICard>
  </div>
</template>

<script>
import UICard from "@/components/ui/card/card.vue"
export default {
    name: "UIModel",

    components: {
        UICard,
    },

    methods: {
        clickClose(event) {
            if (event.target === event.currentTarget) {
                this.$emit('close')                
            }
        }
    }
}
</script>

<style lang="scss">
.model-add {
    width: 100%;
    height: 100%;
    background: rgba(125, 105, 112, 0.5);
    display: flex;
    align-items: center;
    justify-content: center;
}

</style>