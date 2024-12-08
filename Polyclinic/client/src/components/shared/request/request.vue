<template>
  <div class="request-content">
    <div 
      class="request-content__item"
      v-for="item in requests"
      :key="item.id">
      <UIRequest 
        :text="item.text" 
        :result="item.request" 
        :id="item.id"
        :choose_id="item.search_id"
        :options="doctors"
        @getRes="getResult"
        @doctorId="setId"/>
    </div>
  </div>
</template>

<script>
import UIRequest from "@/components/ui/request/request.vue"

export default {
    name: "RequestContent",
    components: {
      UIRequest,
    }, 

    props: {
      requests: {
        type: Array,
        required: true,
      }, 
      doctors: {
        type: Array,
        required: true,
      }
    },

    methods: {
      getResult(id) {
        this.$emit("update", id);
      },

      setId(id) {
        this.$emit('doctorId', id);
      },
    }
}
</script>

<style lang="scss">
.request-content {
    display: grid;
    grid-template-columns: 1fr 1fr;
    gap: 20px;
    flex-direction: column;
    justify-content: center;
    padding: 0 5%;
}
</style>