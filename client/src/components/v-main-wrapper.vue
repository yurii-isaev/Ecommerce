<template>
   <div class="v-main-wrapper"></div>
   <v-catalog/>
   <v-cart
      v-if="CART.length"
      :cart_data="CART"
   />
</template>

<script>
  import VCatalog from './v-catalog';
  import VCart from './v-cart';
  import { mapGetters } from 'vuex';

  export default {
     name: "v-main-wrapper",
  
     components: {
        VCart,
        VCatalog
     },

     data() {
        return {
           title: "Main wrapper"
        }
     },
  
     computed: {
        ...mapGetters([
           'CART'
        ])
     }
  }
</script>

<style lang="scss" scoped>
  .v-main-wrapper {
     display: flex;
     flex-direction: column;
     justify-content: center;
     align-items: center;
     max-width: 900px;
     margin: 0 auto;
  }
</style>