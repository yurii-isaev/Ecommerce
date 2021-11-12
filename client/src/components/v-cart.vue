<template>
   <div class="v-cart"></div>
   <h1>Cart</h1>
   <v-cart-item 
      v-for="(item, index) in cart_data"
      :key="item.article"
      :cart_item_data="item"
      @deleteCartItem="deleteCartItem(index)"
   />
</template>

<script>
  import VCartItem from './v-cart-item';
  import { mapActions } from 'vuex';

  export default {
     name: "v-cart",
     components: {
        VCartItem
     },
     props: {
        cart_data: {
           type: Array,
           default() {
              return []
           }
        }
     },
     methods: {
        ...mapActions([
           'DELETE_FROM_CART'
        ]),
        deleteCartItem(index) {
           this.DELETE_FROM_CART(index);
        }
     }
  }
</script>
