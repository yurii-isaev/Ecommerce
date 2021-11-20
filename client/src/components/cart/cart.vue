<template>
   <div class="cart"></div>

   <keep-alive>
      <router-link :to="{name: 'catalog'}">
         <div class="cart__back">Back to Catalog</div>
      </router-link>
   </keep-alive>
   
   <h1>Cart</h1>
   <p v-if="!CART.length">Cart is empty</p>
      
   <cart-item 
      v-for="(item, index) in CART"
      :key="item.article"
      :cart_item_data="item"
      @deleteCartItem="deleteCartItem(index)"
      @increment="incrementItem(index)"
      @decrement="decrementItem(index)"
   />
   
   <div class="cart__total">
      <p class="cart__total_name">Total: </p>
      <p>{{cartTotalCart}} &#8381;</p>
   </div>
</template>

<script>
  import CartItem from '@/components/cart/cart-item';
  import { mapActions, mapGetters } from 'vuex';

  export default {
     name: "v-cart",
     
     components: {
        CartItem
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
           'DELETE_FROM_CART',
           'INCREMENT_CART_ITEM',
           'DECREMENT_CART_ITEM'
        ]),

        incrementItem(index) {
           this.INCREMENT_CART_ITEM(index);
        },

        decrementItem(index) {
           this.DECREMENT_CART_ITEM(index);
        },
        
        deleteCartItem(index) {
           this.DELETE_FROM_CART(index);
        }
     }, 
     
     computed: { // observer
        ...mapGetters([
           'CART' // observeble
        ]),

        cartTotalCart() {
           return this.CART.reduce((total, item) => total + (item.price * item.quantity), 0);
        }
     }
  }
</script>


<style lang="scss" scoped>
  .cart {
     margin-bottom: 100px;
     &__back {
        position: absolute;
        top: 10px;
        right: 10px;
        padding: $padding*2;
        border: solid 1px;
     }
     &__total {
        display: flex;
        position: fixed;
        bottom: 0;
        right: 0;
        left: 0;
        padding: $padding*2 $padding*3;
        justify-content: center;
        background: $background-yellow;
        color: #e0e0e0;
        font-size: 20px;
        &_name {
           margin-right: $margin*2;
        }
     }
  }
</style>
