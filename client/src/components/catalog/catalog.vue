<template>
   <div class="catalog">

      <notification-toast :messages="messages" />
      
      <router-link :to="{ name: 'cart' }">
         <div class="catalog__link-to-cart">Cart: {{ CART.length }}</div>
      </router-link>

      <h1>Catalog</h1>

      <div class="catalog__filters">
         <catalog-item-select
            :selected="currentSelectedOption"
            :options="categories"
            @selectOption="sortByCategories"
         />
         <div class="catalog__filters-range-slider">
            <input
               type="range" min="0" max="100000"
               step="10"
               v-model.number="minPrice"
               @change="setRangeSlider"
            >
            <input
               type="range" min="0" max="100000"
               step="10"
               v-model.number="maxPrice" 
               @change="setRangeSlider"
            >
         </div>
         
         <div class="catalog__filters-range-values">
            <p>Min: {{ formatPriceWithSpaces(formatPrice(minPrice)) }}</p>
            <p>Max: {{ formatPriceWithSpaces(formatPrice(maxPrice)) }}</p>
         </div>
      </div>

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
  import CatalogItem from '../catalog/catalog-item';
  import CatalogItemSelect from '../catalog/catalog-item-select';
  import NotificationToast from '../notifications/notification-toast';
  import { formatPrice, formatPriceWithSpaces } from '@filters/price.filter';

  export default {
     name: "component-catalog",

     components: {
        NotificationToast,
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
           minPrice: 0,
           maxPrice: 100000,
           messages: []
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

        formatPrice,
        formatPriceWithSpaces,

        addToCart(data) {
           this.ADD_TO_CART(data).then(() => {
              let timeStamp = Date.now().toLocaleString();
              this.messages.unshift({
                 name: 'Product added to cart successfuly', icon: '', id: timeStamp
              })
           })
        },

        sortByCategories(selectedOption) {
           const { minPrice, maxPrice, PRODUCTS } = this;
           this.sortedProducts = PRODUCTS.filter(product => product.price >= minPrice && product.price <= maxPrice);

           if (selectedOption) {
              this.sortedProducts = this.sortedProducts.filter(product => {
                 this.selected = selectedOption.name;
                 return product.category === selectedOption.name;
              });
           }
        },
        
        setRangeSlider() {
           if (this.minPrice > this.maxPrice) {
              let temp = this.maxPrice;
              this.maxPrice = this.minPrice;
              this.minPrice = temp;
           }
           this.sortByCategories();
        }, 
        
        async loadDataFromApi() {
           try {
              await this.GET_PRODUCTS_FROM_API();
              this.sortByCategories();
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

  $padding: 10px;  
  
  .catalog { 
     &__list {
        display: flex;
        flex-wrap: wrap;
        justify-content: space-evenly;
        align-items: center;
     } 
     
     &__link-to-cart {
        position: fixed;
        top: 10px;
        right: 10px;
        padding: $padding * 2;
        border: 1px solid #aeaeae;
        background: #ffffff;
     } 
     
     &__filters { 
        &-range-slider { 
           input[type=range] {
              position: absolute;
              left: 65%;
              top: 180px;
           } 
        } 
        
        &-range-values { 
           p {
              margin: 5px 0;
           } 
        } 
     } 
  }  
  
  input[type=range] {
     -webkit-appearance: none; 
     
     &::-webkit-slider-runnable-track {
        width: 300px;
        height: 5px;
        background: #ddd;
        border: none;
        border-radius: 3px;
     } 
     
     &::-webkit-slider-thumb {
        -webkit-appearance: none;
        border: 1.5px solid #C1C1C1;
        height: 16px;
        width: 16px;
        border-radius: 50%;
        background: #EDEDED;
     } 
     
     &:focus {
        outline: none;
     } 
     
     &::-webkit-slider-runnable-track {
        width: 100%;
        height: 5px;
        box-shadow: 1px 1px 1px #C6C6C6, 0 0 1px #787878;
        border-radius: 2px;
        border: 0.2px solid #787878;
     } 
  }
</style>