<template>
   <header class="header">
      <router-link class="router-link" :to="{ name: 'mainPage' }">
         <div class="text-section">
            <img src="@/assets/logo/logo.png" alt="Logo" style="width:3.75rem"/>
            <div class="text-content">
               <h2 class="title">Vue cakes</h2>
               <p class="description">Online store for ordering cakes</p>
            </div>
         </div>
      </router-link>

      <div class="input-box">
         <i class="uil uil-search"></i>
         <input type="text" v-model="searchValue">
         <button class="search-btn" @click="search(searchValue)">
            <i class="material-icons">search</i>
         </button>
         <button class="search-btn_clear" @click="clearSearchField">
            <i class="material-icons">cancel</i>
         </button>
      </div>

      <ul class="header-menu">
         <li class="menu-item">
            <button @click="switchLanguage('en')">
               <img src="@/assets/graphics/vector/usa.svg" alt="United States Flag"/>
            </button>
         </li>
         <li class="menu-item">
            <router-link class="router-link" :to="{ name: 'cart' }">
               <img src="@/assets/graphics/vector/cart.svg" alt="Cart"/>
               <span class="cart-quantity-icon">{{ cart.length }}</span>
            </router-link>
         </li>
         <li class="menu-item">
            <router-link class="router-link" :to="{ name: 'cart' }">
               <img src="@/assets/graphics/vector/heart.svg" alt="Favorite"/>
               <span class="cart-quantity-icon"> 5 </span>
            </router-link>
         </li>
         <li class="menu-item">
            <router-link class="router-link" :to="{ name: 'cart' }">
               <img src="@/assets/graphics/vector/profile.svg" alt="Favorite"/>
            </router-link>
         </li>
      </ul>
   </header>
</template>

<script>
  import { mapActions, mapGetters } from 'vuex';

  export default {
     name: "layout-header", 
     data: () => ({searchValue: ''}), 
     
     computed: {
        ...mapGetters([
           'CART_STATE_VALUE',
        ]), 
        
        cart() {
           return this.CART_STATE_VALUE;
        },
     },
     
     methods: {
        ...mapActions([
           'ACTION_SEARCH_VALUE_TO_STORE'
        ]), 
        
        search(value) {
           this.ACTION_SEARCH_VALUE_TO_STORE(value);
           if (this.$route.path !== '/catalog') {
              this.$router.push('/catalog');
           }
        }, 
        
        clearSearchField() {
           this.searchValue = '';
           this.ACTION_SEARCH_VALUE_TO_STORE();
           if (this.$route.path !== '/catalog') {
              this.$router.push('/catalog');
           }
        }
     }
  }
</script>

<style lang="scss" scoped>

  .header {
     display: flex;
     justify-content: space-between;
     border-bottom: 1px solid #CBD5E0;
     padding: 0 1.25rem;
     padding-top: 0.5rem;
     padding-bottom: 0.625rem;
  }  
  
  .text-section {
     display: flex;
     align-items: center;
  }  
  
  .title {
     font-family: 'Tangerine', serif;
     font-size: 48px;
     text-shadow: 4px 4px 4px #aaa;
  }

  .description {
     color: #718096;
  }

  .text-content {
     margin-left: 1rem;
  }

  .header-menu {
    display: flex;
     align-items: center;
     gap: 2.5rem;
  }

  .menu-item {
     display: flex;
     align-items: center;
     gap: 0.75rem;
     cursor: pointer;
  }

  .header-menu li {
     list-style: none;
  }

  .header-menu li a {
     text-decoration: none;
     color: #92533a;
     position: relative;
  }

  .input-box {
     position: relative;
     display: flex;
     align-items: center;
     height: 30px;
     max-width: 500px; 
     //width: 70%;
     background: #fff;
     margin: 20px;
     border-radius: 6px;
     box-shadow: 0 3px 6px rgba(0, 0, 0, 0.1);
     border: 2px solid #9B9B9B;
  }  
  
  .input-box i {
     margin-right: 8px;
     background: none;
     color: black;
  }  
  
  .input-box input {
     flex: 1;
     border: none;
     outline: none;
     height: 100%;
     padding: 0 8px;
     font-size: 14px;
  }  
  
  .search-btn, .search-btn_clear {
     display: flex;
     align-items: center;
     justify-content: center;
     width: 36px;
     height: 90%;
     cursor: pointer;
     background: none;
  }  
  
  .search-btn i, .search-btn_clear i {
     font-size: 20px;
     background: none;
     color: #9B9B9B;
  }  
  
  .cart-quantity-icon {
     display: flex;
     position: absolute;
     top: -25px;
     right: -15px;
     width: 25px;
     height: 25px;
     border: none;
     background: #f08804;
     color: #fff;
     border-radius: 50%;
     align-items: center;
     justify-content: center;
     font-size: 13px;
  }
</style>