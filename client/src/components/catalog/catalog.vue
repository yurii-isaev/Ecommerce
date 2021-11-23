<template>
   <div class="catalog">
      <router-link :to="{ name: 'cart' }">
      <div class="catalog__link_to_cart">Cart: {{ CART.length }}</div>
      </router-link>
      
      <h1>Catalog</h1>
      
      <catalog-item-select 
         :selected="currentSelectedOption" 
         :options="categories"
         @selectOption="sortByCategories"
      />
      
      <div class="catalog__list">
         <catalog-item
            v-for="product in filtredProducts"
            :key="product.article"
            :product_data="product"
            @addToCart="addToCart"
         />
      </div>
      
   </div>
</template>

<script>
  import { mapActions, mapGetters } from 'vuex';
  import CatalogItem from '@/components/catalog/catalog-item';
  import CatalogItemSelect from '@/components/catalog/catalog-item-select';

  export default {
     name: "v-catalog",
     
     components: {
        CatalogItem,
        CatalogItemSelect
     },
     
     data() {
        return {
           categories: [
              {name: 'all', value: 'all'},
              {name: 'snacks', value: 'snacks'},
              {name: 'chips', value: 'chips'},
           ],
           currentSelectedOption: 'all',
           sortedProducts: [],
        }
     },
    
     computed: {
        ...mapGetters([
           'PRODUCTS',
           'CART'
        ]),

        filtredProducts() {
           return this.sortedProducts.length ? this.sortedProducts : this.PRODUCTS;
        },
     },
     
     methods: {
        ...mapActions([
           'GET_PRODUCTS_FROM_API',
           'ADD_TO_CART'
        ]),
        
        addToCart(data) {
           this.ADD_TO_CART(data);
        },
        
        sortByCategories(category) {
           this.sortedProducts = this.PRODUCTS.filter(product => product.category === category.name);
           this.currentSelectedOption = category.name;
        },

        async loadDataFromApi() {
           try {
              await this.GET_PRODUCTS_FROM_API();
           } catch (error) {
              console.error(error);
              return error;
           }
        }
     },

     created() {
        this.loadDataFromApi();
     }
  }
</script>

<style lang="scss" scoped>
  .catalog {
     &__list {
        display: flex;
        flex-wrap: wrap;
        justify-content: space-evenly;
        align-items: center;
     }
     &__link_to_cart {
        position: absolute;
        top: 10px;
        right: 10px;
        padding: $padding*2;
        border: solid 1px;
     }
  }
</style>