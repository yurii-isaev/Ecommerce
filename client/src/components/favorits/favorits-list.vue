<template>
   <div class="container">
      <catalog-item
         v-for="product in favorits"
         :key="product.id"
         :product_props="product"
      />
   </div>
</template>

<script>
  import { inject } from 'vue';
  import CatalogItem from '../catalog/catalog-item';

  export default {

     name: "favorits-list",
     
     components: {
        CatalogItem
     }, 
     
     setup() {
        const { favorits } = inject('favorits');
        return {
           favorits
        }
     }
  }
</script>

<style>
  .container {
     display: flex;
     flex-wrap: wrap;
     justify-content: space-evenly;
     align-items: center;
 }
</style>