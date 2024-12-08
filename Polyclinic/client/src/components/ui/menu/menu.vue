<template>
    <div class="ui-menu">
      <div class="ui-menu__teams" v-for="link in links" :key="link.id">
        <router-link class="ui-menu__label" :to="link.link">{{
          link.label
        }}</router-link>
      </div>
    </div>
  </template>
  
<script>
  export default {
    name: 'UIMenu',
  
    props: {
      links: {
        type: Object,
        default: null,
  
        // {
        //     id: 0,
        //     label: "",
        //     link: "",
        // }
      },
    },
  }
</script>
  
<style lang="scss">
  .ui-menu {
    display: flex;
    gap: 50px;
  
    &__teams {
      transition: transform 0.5s ease;
      &:hover {
        transform: scale(1.1);
      }
    }
  
    &__label {
      text-decoration: none;
      color: #7D6970;
      font-size: 22px;
      text-transform: uppercase;
      font-family: 'Montserrat-Light';
    }
  }
  
  @media screen and (max-width: 1600px) {
    .ui-nav {
      gap: 20px;
  
      &__label {
        font-size: 16px;
      }
    }
  }
  
  @media screen and (max-width: 840px) {
    .ui-nav__label {
      font-size: 14px;
    }
  }
  
  @media screen and (max-width: 760px) {
    .ui-nav__teams {
      display: none;
    }
  }
</style>
  