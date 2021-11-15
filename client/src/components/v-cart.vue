<template>
   <div class="v-cart"></div>

   <keep-alive>
      <router-link :to="{name: 'catalog'}">
         <div class="back_to_cart">Back to Catalog</div>
      </router-link>
   </keep-alive>
   
   <h1>Cart</h1>
   <p v-if="!CART.length">Cart is empty</p>
      
   <v-cart-item 
      v-for="(item, index) in CART"
      :key="item.article"
      :cart_item_data="item"
      @deleteCartItem="deleteCartItem(index)"
   />
</template>

<script>
  import VCartItem from './v-cart-item'
  import { mapActions, mapGetters } from 'vuex'

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
     },
     computed: { // observer
        ...mapGetters([
           'CART' // observeble
        ])
     }

     // mounted() {
     //    if (this.cart_data) {
     //       const cartData = JSON.parse(this.cart_data);
     //       console.log(cartData)
     //    }
     // },
  }
</script>


<style lang="scss" scoped>
  .back_to_cart {
     position: absolute;
     top: 10px;
     right: 10px;
     padding: $padding*2;
     border: solid 1px;
  }
</style>
