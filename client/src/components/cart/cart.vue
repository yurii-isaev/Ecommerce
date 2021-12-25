<template>
   
   <p v-if="cartIsEmpty" style="margin:35px">Cart is empty</p>
   
   <cart-item 
         v-for="(item, index) in cart" 
         :key="item.article" 
         :cart_item_data="item" 
         @removeFromCart="removeFromCart(index)" 
         @increment="incrementItem(index)" 
         @decrement="decrementItem(index)"
      />
      
      <div class="cart__total">
         <p class="cart__total_name">Total: </p>
         <p>{{ formattedTotal }}</p>
      </div>

</template>

<script>
  import { mapActions, mapGetters } from 'vuex';
  import CartItem from '../cart/cart-item';
  
  export default {
     name: "component-cart",
     
     components: { CartItem },
     
     props: {
        cart_data: {
           type: Array,
           default: () => []
        }
     },

     computed: {
        ...mapGetters(['CART_STATE']), // observeble

        cart() {
           return this.CART_STATE;
        },

        cartIsEmpty() {
           return this.cart.length === 0;
        },

        cartTotal() {
           return this.cart.reduce((total, item) => total + (item.price * item.quantity), 0);
        },

        formattedTotal() {
           return this.$formatPrice(this.cartTotal);
        },
     },
     
     methods: {
        ...mapActions(['DELETE_FROM_CART', 'INCREMENT_CART_ITEM', 'DECREMENT_CART_ITEM']),

        incrementItem(index) {
           this.INCREMENT_CART_ITEM(index);
        },

        decrementItem(index) {
           this.DECREMENT_CART_ITEM(index);
        },

        removeFromCart(index) {
           this.DELETE_FROM_CART(index);
        },
     },
  }
</script>

<style lang="scss" scoped>

  .cart {
     position: absolute;
     top: 10px;
     right: 10px;
     padding: $padding*2;
     //border: solid 1px;
  }  
  
  .cart__total {
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
  }

  .cart__total_name {
     margin-right: $margin*2;
  }
</style>
