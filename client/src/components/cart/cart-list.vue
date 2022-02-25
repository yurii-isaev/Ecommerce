<template>
  <section>
    <p v-if="cartIsEmpty" style="margin:35px">Your cart is empty.</p>
    
    <!--  Cart Item Component -->
    <CartItem
        v-for="(item, index) in cartArr"
        :key="item.article"
        :cart_item="item"
        @removeFromCart="removeFromCart(index)"
        @increment="incrementItem(index)"
        @decrement="decrementItem(index)"
    />
    
    <!--  Total Cart Obj-->
    <div class="total" v-if="cartArr">
      <div class="row">
        <div class="col-6">
          <p>Subtotal: </p>
          <p>Tax: &nbsp;</p>
          <p>Total: &nbsp;</p>
          <p>Discount: &nbsp;</p>
          <p>Quantity: &nbsp;</p>
        </div>
        <div class="col-6">
          <p>{{ formattedTotal }}</p>
          <p>0</p>
          <p>{{ formattedTotal }}</p>
          <p>0</p>
          <p>{{ cartArr.length }}</p>
        </div>
      </div>
      
      <div class="wrap">
        <button class="btn btn-shadow_green" @click="payOrder"> Pay order </button>
        <button class="btn btn-shadow_yellow" @click="addDelivery"> Add delivery </button>
      </div>
    </div>
  </section>
</template>

<script>
  import { mapGetters } from 'vuex';
  import CartItem from '@/components/cart/cart-item';

  export default {
  
    components: { CartItem },
  
    data() {
      return {
        totalCartObj: {
          subtotal: 0,
          tax:      0,
          total:    0,
          discount: 0,
          quantity: 0
        }
      };
    }, 
    
    mounted() {
      this.updateTotalCartObj();
    }, 
    
    computed: {
      ...mapGetters(['CART_STATE']), 
      
      cartArr() { return this.CART_STATE }, 
      
      cartIsEmpty() {
        return this.cartArr.length === 0
      }, 
      
      cartTotal() {
        return this.cartArr.reduce((total, item) => {
          if (typeof item.price !== 'number' || typeof item.quantity !== 'number') {
            console.error("Invalid item in cart:", item);
            return total;
          }
          return total + (item.price * item.quantity);
        }, 0);
      }, 
      
      formattedTotal() {
        return this.$formatPrice(this.cartTotal);
      },
    }, 
    
    methods: {
      async payOrder() {
        await this.$store.dispatch('UPDATE_CART', this.cartArr);
        await this.$store.dispatch('ADD_CART_TOTAL', this.totalCartObj);
        this.$router.push({ name: 'order-payment' });
      },
  
      async addDelivery() {
        await this.$store.dispatch('UPDATE_CART', this.cartArr);
        await this.$store.dispatch('ADD_CART_TOTAL', this.totalCartObj);
        this.$router.push({ name: 'order-delivery' });
      },
  
      updateTotalCartObj() {
        this.totalCartObj = {
          subtotal: this.cartTotal, 
          tax:      0, 
          total:    this.cartTotal, 
          discount: 0, 
          quantity: this.cartArr.length
        };
        console.log("[CART-LIST]::totalObj:", this.totalCartObj);
      }, 
      
      incrementItem(index) {
        this.$store.dispatch('INCREMENT_CART_ITEM', index);
        this.updateTotalCartObj();
      }, 
      
      decrementItem(index) {
        this.$store.dispatch('DECREMENT_CART_ITEM', index);
        this.updateTotalCartObj();
      }, 
      
      removeFromCart(index) {
        this.$store.dispatch('DELETE_FROM_CART', index);
        },
      },
  }
</script>

<style lang="scss" scoped>

  .col-6 p {
    font-size: 17px;
    margin: 10px;
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
    padding: 2px 52px;
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
    margin-top: 15px;
    right: $padding*3;
    bottom: $padding*2;
  }  
  
  .btn-shadow_green:hover {
    background-color: #5f8141;
  }
</style>
