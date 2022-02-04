<template>
  <p v-if="cartIsEmpty" style="margin:35px">Your cart is empty.</p>
  
  <!-- Cart Item Component -->
  <CartItem
      v-for="(item, index) in cart" 
      :key="item.article" 
      :cart_item_data="item" 
      @removeFromCart="removeFromCart(index)" 
      @increment="incrementItem(index)" 
      @decrement="decrementItem(index)"
  />
    
  <div class="total">
    <div class="row">
      <div class="col-6">
        <p>Subtotal: </p>
        <p>Tax: &nbsp;</p>
        <p>Total: &nbsp;</p>
        <p>Quantity: &nbsp;</p>
      </div>
      <div class="col-6">
        <p>{{ formattedTotal }}</p>
        <p>0</p>
        <p>{{ formattedTotal }}</p>
        <p>{{ cart.length }}</p>
      </div>
    </div>
    <div class="wrap">
      <router-link class="router-link" :to="{ name: 'order-payment'}">
        <button class="btn btn-shadow_green"> Pay order </button>
      </router-link>
      <router-link class="router-link" :to="{ name: 'order-delivery'}">
        <button class="btn btn-shadow_yellow"> Add delivery </button>
      </router-link>
    </div>
  </div>
</template>

<script>
  import { mapActions, mapGetters } from 'vuex';
  import CartItem from '@/components/cart/cart-item';
  
  export default {

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

  .col-6 p {
    white-space: nowrap; /* Предотвращает перенос текста */
  }
  
  .wrap {
    display: grid;
  }
  
  .cart {
    position: absolute;
    top: 10px;
    right: 10px;
    padding: $padding*2;
    //border: solid 1px;
  }

  .total {
    text-align: start;
    display: flex;
    position: fixed;
    bottom: 0;
    right: 0;
    left: 0;
    padding: $padding*2 $padding*3;
    justify-content: space-evenly;
    background: $background-black;
    color: #e0e0e0;
    font-size: 20px;
  }

  .btn-shadow_yellow {
    background-color: #db9a31;
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
    margin-bottom: 25px;
    right: $padding*3;
    bottom: $padding*2;
  }
  .btn-shadow_yellow:hover {
    background-color: #aa7a2c;
  }

  .btn-shadow_green {
    background-color: #85a767;
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
    margin-bottom: 25px;
    right: $padding*3;
    bottom: $padding*2;
  }  
  .btn-shadow_green:hover {
    background-color: #5f8141;
  }
</style>
