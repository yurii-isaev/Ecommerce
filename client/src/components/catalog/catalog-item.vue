<template>
   <div class="product-card">

      <img class="favorite-icon" 
           alt="favorit"
           :src="!product_props.isFavorite
           ? require('@/assets/graphics/vector/like-1.svg') 
           : require('@/assets/graphics/vector/like-1.svg')"
           
           @click="addToFavorite"
      /> 
      
      <img class="product-image"
           alt="img"
           :src="require('@/assets/images/' + product_props.image)">

      <p class="product-title">{{ product_props.name }}</p>
      <p class="product-details">{{ formatPriceWithSpaces(formatPrice(product_props.price)) }}</p>

      <button class="catalog-item__add_to_cart_btn btn"
              @click="addToCart"> Add to cart
      </button>
   </div>
</template>

<script>
  import { formatPrice, formatPriceWithSpaces } from '@/filters/price.filter';
  
  export default {
     name: "component-catalog-item", 
     
     props: {
        product_props: {
           type:       Object,
           isFavorite: Boolean,
           isAdded:    Boolean,
           default:    () => {}
        }
     }, 
     
     methods: {
        addToCart() {
           const object = Object.assign({}, this.product_props);
           object.quantity = 1;
           this.$emit('addToCart', object);
        },

        addToFavorite() {
           const object = Object.assign({}, this.product_props);
           object.isFavorite = true;
           this.$emit('addToFavorite', object);
        },
        
        formatPrice, 
        formatPriceWithSpaces,
     },
  }
</script>

<style lang="scss" scoped>
  //.catalog-item {
  //   flex-basis: 25%;
  //   box-shadow: 0 0 8px 0 #e0e0e0;
  //   padding: $padding*2;
  //   margin-bottom: $margin*2;
  //   &__image {
  //      width: 100px;
  //   }
  //}


  .product-card {
     position: relative;
     background-color: #fff;
     border: 1px solid #ccc;
     border-radius: 20px;
     padding: 20px;
     cursor: pointer;
     transition: transform 0.3s, box-shadow 0.3s;
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

  //.product-image {
  //   max-width: 20%;
  //   height: 20%;
  //}

  .product-image {
     width: 200px; /* задаем ширину */
     height: 150px; /* задаем высоту */
  }

  //.card-image {
  //   width: 4rem;
  //   height: 4rem;
  //}

  .product-title {
     margin-top: 10px;
  }

  .product-details {
     display: flex;
     justify-content: space-between;
     margin-top: 15px;
  }

  .price-label {
     color: #666;
  }

  .add-icon {
     cursor: pointer;
  }
</style>