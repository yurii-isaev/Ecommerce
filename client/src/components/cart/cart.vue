<template>
   <div class="cart"></div>

   <keep-alive>
      <router-link :to="{ name: 'catalog' }">
         <div class="cart__back">Back to Catalog</div>
      </router-link>
   </keep-alive>
   
   <h1>Cart</h1>
   <p v-if="cartIsEmpty">Cart is empty</p>
      
   <cart-item 
      v-for="(item, index) in cart"
      :key="item.article"
      :cart_item_data="item"
      @deleteCartItem="deleteCartItem(index)"
      @increment="incrementItem(index)"
      @decrement="decrementItem(index)"
   />
   
   <div class="cart__total">
      <p class="cart__total_name">Total: </p>
      <p>{{ formatPriceWithSpaces(formatPrice(cartTotal)) }}</p>
      
   </div>
</template>

<script>
  import { mapActions, mapGetters } from 'vuex';
  import CartItem from '../cart/cart-item';
  import { formatPrice, formatPriceWithSpaces } from '@/filters/price.filter';
  
  export default {
     name: "component-cart",
     
     components: {
        CartItem
     },
     
     props: {
        cart_data: {
           type: Array,
           default: () => []
        }
     },

     computed: {
        ...mapGetters([
           'CART_STATE_VALUE' // observeble
        ]),

        cart() {
           return this.CART_STATE_VALUE;
        },

        cartIsEmpty() {
           return this.cart.length === 0;
        },

        cartTotal() {
           return this.cart.reduce((total, item) => total + (item.price * item.quantity), 0);
        }
     },
     
     methods: {
        ...mapActions([
           'ACTION_DELETE_FROM_CART',
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
           this.ACTION_DELETE_FROM_CART(index);
        },

        formatPrice,
        formatPriceWithSpaces,
     },
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
        background: $background-black;
        color: #e0e0e0;
        font-size: 20px;
        &_name {
           margin-right: $margin*2;
        }
     }
  }
</style>
