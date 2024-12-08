<template>
  <div class="ui-request">
    <UICard :background="'white'" :flexDirection="'column'" :borderRadius="20" :padding="2">
      <p class="ui-request__text">{{ text }}</p>
      <div v-if="choose_id" class="ui-request__id">
        <UIDropdown 
          :options="options" 
          :select="'Выберите врача'" 
          @changed="setId"
        />
      </div>
      <p class="ui-request__result">{{ result }}</p>
      <button class="ui-request__button" @click="getRes">Получить результат</button>
    </UICard>
  </div>
</template>

<script>
import UIDropdown from "@/components/ui/dropdown/dropdown.vue"
import UICard from "@/components/ui/card/card.vue"

export default {
    name: "UIRequest",

    components: {
      UICard,
      UIDropdown,
    },

    props: {
      text: {
        type: String,
      },
      result: {
        type: String,
      },
      id: {
        type: Number,
      },
      choose_id: {
        type: Boolean,
        default: true,
      }, 
      options: {
        type: Array,
        default: null,
      },
    },

    methods: {
      getRes() {
        this.$emit('getRes', this.id);
      },
      setId(id) {
        this.$emit('doctorId', id);
      },
    }
}
</script>

<style lang="scss">
.ui-request {
  display: flex;
  flex-direction: column;

  font-size: 20px;
  font-family: 'Montserrat-Light';
  text-align: center;

  &__text {
    color: #7D6970;
  }

  &__result {
    min-height: 50px;
    color: black;
    font-size: 18px;
    padding: 10px 15px;
    border: 1px solid #7D6970;
    border-radius: 20px;
  }

  &__button {
    width: 30%;
    padding: 10px;
    background-color: white;
    color: #7D6970;
    font-size: 16px;
    border-radius: 10px;
    border: 1px solid #7D6970;
    margin-top: 40px;

    &:hover {
        background-color: #7D6970;
        color: white;
    }
  }
}

</style>