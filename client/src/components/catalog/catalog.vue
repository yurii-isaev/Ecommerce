<template>
   <div class="catalog">
      <notification-toast :messages="messages" />
      
      <div class="catalog-filters">
         <catalog-item-select
            :selected="currentSelectedOption" 
            :options="categories" 
            @selectOption="sortByCategories"
         />
         
         <div class="catalog-filters-range-slider">
            <input
               type="range" min="0" max="10000"
               step="10"
               v-model.number="minPrice"
               @change="setRangeSlider"
            >
            <input
               type="range" min="0" max="10000"
               step="10"
               v-model.number="maxPrice" 
               @change="setRangeSlider"
            >
         </div>
         
         <div class="catalog-filters-range-values">
            <p>Min: {{ formattedMinPrice }}</p>
            <p>Max: {{ formattedMaxPrice }}</p>
         </div>
      </div>

      <div class="catalog-list">
         <catalog-item
            v-for="product in filtredProducts" 
            :key="product.id" 
            :product_props="product" 
            @addToCart="addToCart" 
            @addToFavorite="addToFavorite" 
            :isFavorite="product.isFavorite"
         />
       
      </div>
   </div>
</template>

<script>
  import { mapActions, mapGetters } from 'vuex';
  import CatalogItem from '../catalog/catalog-item';
  import CatalogItemSelect from '../catalog/catalog-item-select';
  import NotificationToast from '../notifications/notification-toast';

  export default {
     name: "component-catalog",

     components: { NotificationToast, CatalogItem, CatalogItemSelect },

     data() {
        return {
           categories: [
              {name: 'all', value: 'all'},
              {name: 'snacks', value: 'snacks'},
              {name: 'chips', value: 'chips'},
           ],
           currentSelectedOption: 'all',
           sortedProducts: [],
           messages: [],
           
           minPrice: 0,
           maxPrice: 10000,
        }
     },

     props: {
        catalog_props: {
           type: Array,
           default: () => []
        }
     },

     computed: {
        ...mapGetters(['PRODUCTS_STATE', 'CART_STATE', 'SEARCH_STATE', 'FAVORITS_STATE']),

        products() {
           return this.PRODUCTS_STATE;
        },
        
        cart() {
           return this.CART_STATE;
        },

        filtredProducts() {
           return this.sortedProducts.length ? this.sortedProducts : this.products;
        },

        formattedMinPrice() {
           return this.$formatPrice(this.minPrice);
        },
        
        formattedMaxPrice() {
           return this.$formatPrice(this.maxPrice);
        },
     },

     methods: {
        ...mapActions(['GET_PRODUCTS_FROM_API', 'ADD_TO_CART', 'ADD_TO_FAVORITS']),

        addToCart(data) {
           this.ADD_TO_CART(data);
           let timeStamp = Date.now().toLocaleString();
           this.messages.unshift({
              name: 'Product added to cart successfuly', icon: '', id: timeStamp
           })
        },

        addToFavorite(object) {
           this.ADD_TO_FAVORITS(object);
        },

        sortByCategories(selectedOption) {
           const { minPrice, maxPrice, products } = this;
           this.sortedProducts = products.filter(p => p.price >= minPrice && p.price <= maxPrice);

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

        sortProductsBySearchValue(value) {
           this.sortedProducts = [...this.products];
           this.sortedProducts = value
              ? this.products.filter(i => i.name.toLowerCase().includes(value.toLowerCase()))
              : this.products;
        },
     },

     watch: {
        SEARCH_STATE_VALUE() {
           this.sortProductsBySearchValue(this.SEARCH_STATE_VALUE);
        }
     },
     
     created() {
        this.GET_PRODUCTS_FROM_API().then((response) => {
           if (response.data) {
              this.sortByCategories();
              this.sortProductsBySearchValue(this.SEARCH_STATE_VALUE);
           }
        }).catch((error) => {
           console.error('An error occurred while retrieving data from the API:', error);
        });
     }
  }
</script>

<style scoped>
  .catalog-list {
     display: flex;
     flex-wrap: wrap;
     justify-content: center;
     gap: 40px;
  }
  
  @media screen and (max-width: 1200px) {  
     .catalog-item {
        flex: 0 0 calc(50% - 80px);
     }  
  }
  
  .catalog-filters {
     display: flex;
     flex-wrap: nowrap;
     justify-content: space-around;
     align-items: center;
     box-shadow: 0 0 8px 0 #e0e0e0;
     padding: 20px;
     margin-bottom: 20px;
 
  }

/*.catalog-link-to-cart {*/
/*   position: fixed;*/
/*   top: 10px;*/
/*   right: 10px;*/
/*   padding: var(--padding) * 2;*/
/*   border: 1px solid #aeaeae;*/
/*   background: #ffffff;*/
/*}*/

  .catalog-filters-range-slider input[type=range] {
     /*position: absolute;*/
     left: 65%;
     top: 180px;
  }  
  
  .catalog-filters-range-values p {
     margin: 5px 0;
  }  
  
  input[type=range] {
     -webkit-appearance: none;
  }  
  
  input[type=range]::-webkit-slider-runnable-track {
     width: 300px;
     height: 5px;
     background: #ddd;
     border: none;
     border-radius: 3px;
  }  
  
  input[type=range]::-webkit-slider-thumb {
     -webkit-appearance: none;
     border: 1.5px solid #C1C1C1;
     height: 16px;
     width: 16px;
     border-radius: 50%;
     background: #EDEDED;
  }  
  
  input[type=range]:focus {
     outline: none;
  }  
  
  input[type=range]::-webkit-slider-runnable-track {
     width: 100%;
     height: 5px;
     box-shadow: 1px 1px 1px #C6C6C6, 0 0 1px #787878;
     border-radius: 2px;
     border: 0.2px solid #787878;
  }
</style>