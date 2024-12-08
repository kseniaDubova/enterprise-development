<template>
  <div class="defauil-layouts">
    <HeaderLayouts />
    <router-view />
  </div>
</template>

<script>
import HeaderLayouts from "@/layouts/header.vue"

export default {
 name: "DefaultLayout",

 components: {
    HeaderLayouts,
 }
}
</script>

<style>

</style>