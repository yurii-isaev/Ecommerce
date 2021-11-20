<template>
   <div class="catalog-item">
      <img class="catalog-item__image"
           alt="img"
           v-if="product_data.image"
           v-bind:src="require('@/assets/images/' + product_data.image)">

      <p class="catalog-item__name">{{ product_data.name }}</p>
      <p class="catalog-item__price">{{ product_data.price }} &#8381;</p>

      <button class="catalog-item__add_to_cart_btn btn"
              @click="addToCart"> Add to cart
      </button>
   </div>
</template>

<script>
  export default {
     name: "catalog-item", 
     
     props: {
        product_data: {
           type: Object, 
           default() {
              return {}
           }
        }
     },
     
     methods: {
        addToCart() {
           const productToAdd = Object.assign({}, this.product_data);
           productToAdd.quantity = 1;
           
           // Send the product to the parent component via the addToCart event
           this.$emit('addToCart', productToAdd);
        }
     }
  }
</script>

<style lang="scss" scoped>
  .catalog-item {
     flex-basis: 25%;
     box-shadow: 0 0 8px 0 #e0e0e0;
     padding: $padding*2;
     margin-bottom: $margin*2;
     &__image {
        width: 100px;
     }
  }
</style>