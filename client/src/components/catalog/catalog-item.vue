<template>
   <div class="product-card">
      <img class="favorite-icon" 
           alt="favorit"
           :src="!product_props.isFavorite
           ? require('@/assets/graphics/vector/like-1.svg') 
           : require('@/assets/graphics/vector/like-2.svg')"
           @click="addOrDeleteToFavorite"
      />
      <img class="product-image"
           alt="img"
           :src="require('@/assets/images/' + product_props.image)"
      />

      <p class="product-title"><strong> {{ product_props.name }} </strong></p>

      <p class="product-details">{{ formattedPrice() }} </p>

      <button class="btn btn-success" @click="addToCart"> Add to cart </button>
   </div>
</template>

<script>
  
  export default {
     name: "component-catalog-item", 
     
     props: {
        product_props: {
           type: Object,
           isFavorite: Boolean,
           default: () => {}
        }
     }, 
     
     methods: {
        addToCart() {
           const object = Object.assign({}, this.product_props);
           object.quantity = 1;
           this.$emit('addToCart', object);
        },

        addOrDeleteToFavorite() {
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
     margin: 15px;
  }

  .price-label {
     color: #666;
  }

  .add-icon {
     cursor: pointer;
  }
</style>