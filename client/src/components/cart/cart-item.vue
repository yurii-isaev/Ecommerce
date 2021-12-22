<template>
   <div class="cart-item">
      <img class="cart-item-image"
           alt="image"
           v-if="cart_item_data.image"
           :src="require('@/assets/images/' + cart_item_data.image)"
      >

      <div>
         <p><strong>{{ cart_item_data.name }}</strong></p>
         <div v-if="orderItem && cart_item_data">
            <p>{{ formattedCustomPrice }}</p>
         </div>
         <div v-else>
            <p>{{ formattedPrice }}</p>
         </div>
      </div>
      
      <div>
         <p><strong>Quantity:</strong></p>
         <span>
            <span class="quantity__btn" @click="increment">+</span>
            <strong>{{ cart_item_data.quantity }}</strong>
            <span class="quantity__btn" @click="decrement">-</span>
         </span>
      </div>

      <router-link class="router-link" 
                   :to="   { name: 'cart-item-details',
                   params: { id: cart_item_data.id },
                   props:  { id: cart_item_data.id } }">
         <button class="btn btn-shadow_yellow"> Details</button>
      </router-link>
      
      <button class="btn btn-shadow_red"
              @click="removeFromCart"> Delete
      </button>
   </div>
</template>

<script>
  import { mapGetters } from 'vuex';

  export default {
     name: "component-cart-item",

     props: {
        cart_item_data: {
           type: Object,
           default: () => {}
        },
     },
     
     computed: {
        ...mapGetters(['ORDER_ITEM_ID']),
        
        orderItem() {
           return this.ORDER_ITEM_ID(this.cart_item_data.id);
        },
        
        formattedPrice() {
           return this.$formatPrice(this.cart_item_data.price);
        },

        formattedCustomPrice() {
           return this.$formatPrice(this.orderItem.price);
        }
     },

     methods: {
        increment() {
           this.$emit('increment')
        },
        
        decrement() {
           this.$emit('decrement')
        },

        removeFromCart() {
           this.$emit('removeFromCart')
        },
        
     }
  }
</script>

<style lang="scss" scoped>

  p {
     font-size: large;
  }

  .cart-item {
     display: flex;
     flex-wrap: nowrap;
     justify-content: space-between;
     align-items: center;
     box-shadow: 0 0 8px 0 #e0e0e0;
     padding: $padding*2;
     margin-bottom: $margin*2;
  }

  .cart-item-image {
     max-width: 100px;
  }

  .quantity__btn {
     display: inline-block;
     padding: 8px 12px;
     background-color: #f0f0f0;
     color: #333;
     font-size: 1rem;
     cursor: pointer;
     transition: background-color 0.3s, color 0.3s;
  }

  .quantity__btn:hover {
     background-color: #333;
     color: #fff;
  }

  .quantity__btn:active {
     transform: scale(0.95);
  }

  .btn-shadow {
     box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  }

  .btn:hover {
     background-color: #b0c39f;
  }

  .btn-shadow_red {
     background-color: #db5f4b;
  }

  .btn-shadow_yellow {
     background-color: #db9a31;
  }
</style>