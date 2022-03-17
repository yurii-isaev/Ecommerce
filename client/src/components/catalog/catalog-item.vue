<template>
  <div class="product-card">
    <img class="favorite-icon" alt="favorit"
         :src="!product_props.isFavorite ? require('@/assets/vector/like-1.svg') : require('@/assets/vector/like-2.svg')"
         @click="addOrDeleteToFavoriteCallback"
    />
    <img class="product-image" alt="img" :src="require('@/assets/images/' + product_props.image)"/>
    <p class="product-title"><strong> {{ product_props.name }} </strong></p>
    <p class="product-details">{{ formattedPrice() }}</p>
    <button class="btn btn-green" @click="addToCartCallback"> Add to cart</button>
  </div>
</template>

<script>
  export default {
    props: {
      product_props: {
        type: Object,
        default: () => {}
      }
    }, 
    
    methods: {
      addToCartCallback() {
        const object = Object.assign({}, this.product_props);
        object.quantity = 1;
        this.$emit('addToCart', object);
      },
  
      addOrDeleteToFavoriteCallback() {
        this.$emit('addOrDeleteToFavorite', this.product_props);
      }, 
      
      formattedPrice() {
        return this.$formatPrice(this.product_props.price);
      },
    },
  }
</script>

<style lang="scss" scoped>
  .product-card {
    position: relative;
    background-color: #fff;
    border: 1px solid #ccc;
    border-radius: 20px;
    padding: 20px;
    cursor: pointer;
    transition: transform 0.3s, box-shadow 0.3s;
    margin: 1em;
  }  
  
  .product-card:hover {
    transform: translateY(-2px);
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  }  
  
  .favorite-icon {
    position: absolute;
    top: 10px;
    left: 10px;
  }  
  
  .product-image {
    width: 200px;
    height: 150px;
  }  
  
  .product-title {
    margin-top: 10px;
  }  
  
  .product-details {
    display: flex;
    justify-content: center;
    margin: 15px;
  }  
  
  .price-label {
    color: #666;
  }  
  
  .add-icon {
    cursor: pointer;
  }
</style>