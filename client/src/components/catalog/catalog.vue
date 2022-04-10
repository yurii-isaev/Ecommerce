<template>
  <div class="catalog">
    <!-- Component -->
    <NotificationToast :messages="messages"/>
    
    <div class="catalog-filters">
      <select class="form-sort" v-model="selectedOption" @change="sortByCategories(selectedOption)">
        <option v-for="category in categories" :value="category.value" :key="category.value">
          {{ category.name }}
        </option>
      </select>
      <div class="range-slider">
        <div class="range-slider-container">
          <p> Min: {{ formattedMinPrice }} </p>
          <input type="range" min="0" max="5000" step="5" v-model.number="minPrice" @change="setRangeSlider">
        </div>
        <div class="range-slider-container">
          <p> Max: {{ formattedMaxPrice }} </p>
          <input type="range" min="5000" max="10000" step="5" v-model.number="maxPrice" @change="setRangeSlider">
        </div>
      </div>
    </div>
    
    <div class="catalog-list" v-if="hasSortedProducts">
      <!-- Component -->
      <CatalogItem 
          v-for="product in sortedProducts"
          :key="product.id"
          :product_props="product"
          :isFavorite="product.isFavorite"
          @addToCart="addToCart"
          @addOrDeleteToFavorite="addOrDeleteToFavorite">
      </CatalogItem>
    </div>
    
    <div v-else class="no-matching-products">
      <p>There are no products that match the selected filters.</p>
    </div>
    
    <!-- Component -->
    <Pagination :currentPage="currentPage" :totalPages="totalPages" @page-changed="changePage"/>
  </div>
</template>

<script>
  import { mapGetters } from 'vuex';
  import CatalogItem from '../catalog/catalog-item';
  import NotificationToast from '../notifications/notification-toast';
  import Pagination from './catalog-pagination';

  export default {
    name: 'v-catalog',
  
    components: { NotificationToast, CatalogItem, Pagination },
  
    props: {
      catalog_props: {
        type: Array,
        default: () => []
      }
    },
  
    data() {
      return {
        categories: [
          {name: 'All',   value: 'all' },
          {name: 'Cakes', value: 'cake'},
          {name: 'Pies',  value: 'pie' },
          {name: 'Rolls', value: 'roll'},
        ],
      
        selectedOption: 'all',
        sortedProducts: [],
        messages: [],
        minPrice: 0,
        maxPrice: 10000,
  
        totalPages: 0,
        currentPage: 1,
        pageSize: 4,
        totalItems: 0,
      }
    }, 
    
    computed: {
      ...mapGetters(['PRODUCTS_STATE', 'CART_STATE', 'SEARCH_VALUE', 'FAVORITS_STATE', 'TOTAL_PAGES']),
    
      products() {
        return this.PRODUCTS_STATE;
      },
    
      cart() {
        return this.CART_STATE;
      },
    
      hasSortedProducts() {
        return this.sortedProducts.length > 0;
      },
    
      formattedMinPrice() {
        return this.$formatPrice(this.minPrice);
      },
    
      formattedMaxPrice() {
        return this.$formatPrice(this.maxPrice);
      }
    },
  
    methods: {
      addToCart(product) {
        this.$store.dispatch('ADD_TO_CART', product);
        let timeStamp = Date.now().toLocaleString();
        this.messages.unshift({
          name: 'Product added to cart',
          icon: '',
          id: timeStamp
        })
      },
    
      async addOrDeleteToFavorite(product) {
        if (!product.isFavorite) {
          product.isFavorite = true;
          try {
            await this.$store.dispatch('ADD_TO_FAVORITS', product);
          } catch (error) {
            product.isFavorite = false;
          }
        } else {
          product.isFavorite = false;
          try {
            await this.$store.dispatch('DELETE_FROM_FAVORITS', product.id);
          } catch (error) {
            product.isFavorite = true;
          }
        }
      },
    
      sortByCategories(category) {
        // Creating a copy of an array of products
        this.sortedProducts = [...this.products];
      
        // Filter products first by category, if it is specified and not equal to 'all'
        if (category && category !== 'all') {
          this.sortedProducts = this.sortedProducts.filter(item => item.category === category);
        }
      
        // Then filter the filtered array by price range
        this.sortedProducts = this.sortedProducts.filter(item => item.price >= this.minPrice && item.price <= this.maxPrice);
      },
    
      setRangeSlider() {
        // First, adjust the price range
        if (this.minPrice > this.maxPrice) {
          let temp = this.maxPrice;
          this.maxPrice = this.minPrice;
          this.minPrice = temp;
        }
        this.sortByCategories(this.selectedOption);
      },
    
      sortProductsBySearchValue(value) {
        this.sortedProducts = [...this.products];
        this.sortedProducts = value
            ? this.products.filter(i => i.name.toLowerCase().includes(value.toLowerCase()))
            : this.products;
      },
  
      changePage(page) {
        this.currentPage = page;
        this.fetchProducts();
      },
  
      async fetchProducts() {
        try {
          await this.$store.dispatch('GET_PRODUCTS_FROM_API', {
            pageNumber: this.currentPage,
            pageSize: this.pageSize
          });
          this.sortedProducts = this.products;
          this.totalPages = this.TOTAL_PAGES;
        } catch (error) {
          console.error('An error occurred while retrieving data from the API:', error);
        }
      },
    },
  
    watch: {
      SEARCH_VALUE() {
        this.sortProductsBySearchValue(this.SEARCH_VALUE);
      }
    },
  
    async created() {
      this.fetchProducts();
    }
  }
</script>

<style scoped>
  .no-matching-products {
    margin: 115px;
    font-size: larger;
  }

  .range-slider {
    display: flex;
    justify-content: space-between;
  }

  .range-slider-container {
    display: flex;
    flex-direction: column;
    align-items: center;
  }

  input[type="range"] {
    width: 100%; /* The slider occupies the entire available width of the container */
  }

  .form-sort {
    position: relative;
    width: 200px;
    cursor: pointer;
    text-align: left;
    padding: 5px;
    border-radius: 5px;
    box-shadow: 0 3px 6px rgba(0, 0, 0, 0.1);
    border: 2px solid #9B9B9B;
  }

  .catalog-list {
    display: flex;
    flex-wrap: wrap;
    justify-content: center;
    gap: 40px;
    margin-bottom: 30px;
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