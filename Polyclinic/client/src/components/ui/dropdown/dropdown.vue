<template>
  <div class="ui-dropdown">
    <div class="ui-dropdown__selected" @click="onToggle">
        {{ mainOption }}
    </div>
    <div v-if="toggle" class="ui-dropdown__wrapper">
        <div 
            class="ui-dropdown__wrapper-option"
            v-for="option, index in options"
            :key="index"
            @click="selectedOption(option)">
            {{ option.fullName }}
        </div>
    </div>
  </div>
</template>

<script>
export default {
    name: "UIDropdown",

    data() {
        return {
            mainOption: this.select,
            toggle: false,
        }
    },

    props: {
        options: {
            type: Array,
            default: null,
        },
        select: {
            type: String,
            default: "",
        }
    },

    computed: {
    },

    methods: {
        selectedOption(option) {
            this.mainOption = option.fullName;
            this.toggle = false;

            this.$emit("changed", option.id);
        },

        onToggle() {
            this.toggle = !this.toggle;
        }
    },

}
</script>

<style lang="scss">
.ui-dropdown {
    font-family: 'Montserrat-Light';
    font-size: 14px;
    color: black;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    position: relative;
    border: 1px solid #7D6970;
    border-radius: 20px;
    padding: 10px;

    &__wrapper {
        z-index: 10;
        background: white;
        border: 1px solid rgba(45, 66, 80, 0.1);
        border-radius: 10px;
        position: absolute;
        top: 20px;

        &-option {
            padding: 10px 15px;
            border-bottom: 1px solid rgba(45, 66, 80, 0.1);

            &:last-child {
                border: none;
            }
        }
    }
}
</style>