<template>
  <div class="cart-item">
    
    <img class="cart-item-image"
         alt="image"
         v-if="cart_item_data.image"
         :src="require('@/assets/images/' + cart_item_data.image)"
    />
  
    <div class="cart-item-name">
      <p><strong>{{ cart_item_data.name }}</strong></p>
      <br>
      <div v-if="orderItem && cart_item_data">
        <p>{{ formattedCustomPrice }}</p>
      </div>
      <div v-else>
        <p>{{ formattedPrice }}</p>
      </div>
    </div>
    
    <div class="cart-item-quantity">
      <p><strong>Quantity:</strong></p>
      <span>
        <button class="quantity-btn" @click="increment">
          <strong>+</strong>
        </button>
        <strong>{{ cart_item_data.quantity }}</strong>
        <button class="quantity-btn" @click="decrement">
          <strong>-</strong>
        </button>
      </span>
    </div>
    
    <div class="cart-item-btn">
      <router-link class="router-link" :to="{
            name: 'cart-item-details',
            params: { id: cart_item_data.id },
            props:  { id: cart_item_data.id } }">
        <button class="btn btn-yellow"> Details</button>
      </router-link>
      <button class="btn btn-red" @click="removeFromCart"> Delete</button>
    </div>
  </div>
</template>

<script>
  import { mapGetters } from 'vuex';

  export default {
    
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

  .quantity-btn {
    margin: 0 6px;
    display: inline-block;
    width: 30px;
    height: 30px;
    border-radius: 50%;
    background-color: #f0f0f0;
    color: #333;
    font-size: 1rem;
    cursor: pointer;
    transition: background-color 0.3s, color 0.3s;
    border: none;
  }
  .quantity-btn:hover {
    background-color: #e0e7b5;
    color: #fff;
  }
  .quantity-btn:active {
    transform: scale(0.95);
  } 
  
  .cart-item-btn {
    display: flex;
    justify-content: space-evenly;
  }

  .cart-item-image {
    max-width: 100px;
    margin-right: 20px;
  }
  
  .cart-item {
    display: flex;
    align-items: center;
    flex-wrap: nowrap;
    justify-content: space-between;
    box-shadow: 0 0 8px 0 #e0e0e0;
    padding: $padding*2;
    border-radius: 20px;
    margin: $margin*2;
  }  
  
  .cart-item > div {
    flex: 1; /* Занимает доступное пространство внутри родительского контейнера */
  }

  .cart-item p {
    margin: 0; 
  }

  /* Детали */
  .btn-red {
    background-color: #db5f4b;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
  }

  .btn-red:hover {
    background-color: #b84633;
  }
  
  .btn-yellow {
    background-color: #db9a31;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
  }
  .btn-yellow:hover {
    background-color: #a07023;
  }
  
  @media (max-width: 768px) { 
    .cart-item {
      flex-direction: column; 
    }
  
    .cart-item > div {
      margin-bottom: 10px; /* Добавляет отступ между блоками в столбце */
    }
  
    .cart-item-image {
      max-width: 100%; /* Изображение занимает всю доступную ширину */
      margin-bottom: 10px; /* Добавляет отступ снизу */
    }
  
    .cart-item-btn {
      margin-top: 10px; /* Добавляет отступ сверху для блока с кнопками */
    }
  }
</style>