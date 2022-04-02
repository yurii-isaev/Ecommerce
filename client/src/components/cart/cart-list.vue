<template>
  <section class="section">
    <div v-if="cartIsEmpty">
      <p class="empty-list-message">Your cart is empty.</p>
    </div>
    <div v-else class="cart-items">
      <!-- Cart Item Component -->
      <CartItem
          v-for="(item, index) in cartArr"
          :key="item.article"
          :cart_item="item"
          @removeFromCart="removeFromCart(index)"
          @increment="incrementItem(index)"
          @decrement="decrementItem(index)">
      </CartItem>
    </div>
    <div class="total-wrapper">
      <div class="total" v-if="cartArr.length > 0">
        <div class="row">
          <div class="col-6">
            <p>Subtotal:</p>
            <p>Tax:</p>
            <p>Total:</p>
            <p>Discount:</p>
            <p>Quantity:</p>
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
          <button class="btn btn-green" @click="payOrder"> Pay order </button>
          <button class="btn btn-yellow" @click="addDelivery"> Add delivery </button>
        </div>
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
          tax: 0, 
          total: 0, 
          discount: 0, 
          quantity: 0
        }
      }
    }, 
    
    mounted() {
      this.updateTotalCartObj();
    }, 
    
    computed: {
      ...mapGetters(['CART_STATE']), 
      
      cartArr() {
        return this.CART_STATE
      }, 
      
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
      }
    }, 
    
    methods: {
      async payOrder() {
        await this.$store.dispatch('UPDATE_CART', this.cartArr);
        await this.$store.dispatch('ADD_CART_TOTAL', this.totalCartObj);
        this.$router.push({name: 'order-payment'});
      }, 
      
      async addDelivery() {
        await this.$store.dispatch('UPDATE_CART', this.cartArr);
        await this.$store.dispatch('ADD_CART_TOTAL', this.totalCartObj);
        this.$router.push({name: 'order-delivery'});
      }, 
      
      updateTotalCartObj() {
        this.totalCartObj = {
          subtotal: this.cartTotal, 
          tax: 0, 
          total: this.cartTotal, 
          discount: 0, 
          quantity: this.cartArr.length
        };
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
      }
    }
  }
</script>

<style lang="scss" scoped>
  .col-6 p {
    font-size: 17px;
    margin: 10px;
    white-space: nowrap; /* Prevents text wrapping */
  }

  .wrap {
    display: grid;
  }

  .cart-section {
    display: flex;
    flex-direction: column;
    min-height: 70vh; // The minimum height of the container is equal to the height of the viewport
  }

  .cart-items {
    flex-grow: 1;     // Causes cart items to take up all available space
    overflow-y: auto; // Allows you to scroll through recycle bin items if they don't fit
    display: flex;
    flex-direction: column;
    justify-content: flex-start; // Changed to flex-start for normal arrangement of elements
    min-height: 0;  // Make sure cart-items have a minimum height
  }

  .total-wrapper {
    margin-top: auto; // This will cause the total-wrapper to move down if there are few elements
    width: 100%;      // Spans the entire width
    background: rgba($background-black, 0.9); // Adding Transparency to an Existing Variable
  }

  .total {
    text-align: start;
    display: flex;
    justify-content: space-evenly;
    color: #e0e0e0;
    font-size: 20px;
    //padding: 2px 52px;
  }

  .btn-yellow {
    background-color: #db9a31;
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
    margin-bottom: 25px;
    right: $padding*3;
    bottom: $padding*2;
  }
  .btn-yellow:hover {
    background-color: #aa7a2c;
  }  
  
  .btn-green {
    background-color: #85a767;
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
    margin-bottom: 25px;
    margin-top: 15px;
    right: $padding*3;
    bottom: $padding*2;
  }
  .btn-green:hover {
    background-color: #5f8141;
  }
</style>
