<template>
   <div class="v-cart-item">

      <img class="v-cart-item__image" alt="img" v-if="cart_item_data.image"
           :src="require('../assets/images/' + cart_item_data.image)">

      <div class="v-cart-item__info">
         <p>{{ cart_item_data.name }}</p>
         <p>{{ cart_item_data.price }}</p>
         <p>{{ cart_item_data.article }}</p>
      </div>

      <div class="v-cart-item__quantity">
         <p>Quantity:</p>
         <span>
             <span class="quantity__btn" @click="increment">+</span>
            {{ cart_item_data.quantity }}
            <span class="quantity__btn" @click="decrement">-</span>
         </span>
      </div>

      <button @click="deleteCartItem">Delete</button>
   </div>
</template>

<script>
  export default {
     name: "v-cart-item", 
     
     props: {
        cart_item_data: {
           type: Object,
           default() {
              return {}
           }
        }
     },

     methods: {
        increment() {
           this.$emit('increment')
        },
        
        decrement() {
           this.$emit('decrement')
        },
        
        deleteCartItem() {
           this.$emit('deleteCartItem')
        }
     }
  }
</script>

<style lang="scss" scoped>
  .v-cart-item {
     display: flex;
     flex-wrap: nowrap;
     justify-content: space-between;
     align-items: center;
     box-shadow: 0 0 8px 0 #e0e0e0;
     padding: $padding*2;
     margin-bottom: $margin*2;

     &__image {
        max-width: 100px;
     }
  }
     
  .quantity__btn {
     cursor: pointer;
  }
</style>