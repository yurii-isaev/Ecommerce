<template>
   <div class='header'>
      <div class="header__nav-container">
         <router-link :to="{ name: 'mainPage' }">
            <img src="@/assets/logo/logo.png" alt="">
         </router-link>
         
         <div class="header__nav-search">
            <input type="text" v-model="searchValue">
            <button class="header__nav-search-btn">
               <i class="material-icons" @click="search(searchValue)">search</i>
            </button>
            <button class="header__nav-search-btn_clear">
               <i class="material-icons" @click="clearSearchField">cancel</i>
            </button>
         </div>
            
         <div class="header__nav-cart">
            <li>
               <router-link :to="{ name: 'cart' }">
                  <svg width="1.8em" height="1.8em" viewBox="0 0 16 16" 
                       class="bi bi-cart-check" fill="currentColor" 
                       xmlns="http://www.w3.org/2000/svg">
                     <path fill-rule="evenodd" 
                           d="M0 1.5A.5.5 0 0 1 .5 1H2a.5.5 0 0 1 .485.379L2.89 3H14.5a.5.5 0 0 1 .491.592l-1.5 8A.5.5 0 0 1 13 12H4a.5.5 0 0 1-.491-.408L2.01 3.607 1.61 2H.5a.5.5 0 0 1-.5-.5zM3.102 4l1.313 7h8.17l1.313-7H3.102zM5 12a2 2 0 1 0 0 4 2 2 0 0 0 0-4zm7 0a2 2 0 1 0 0 4 2 2 0 0 0 0-4zm-7 1a1 1 0 1 0 0 2 1 1 0 0 0 0-2zm7 0a1 1 0 1 0 0 2 1 1 0 0 0 0-2z"/>
                     <path fill-rule="evenodd" 
                           d="M11.354 5.646a.5.5 0 0 1 0 .708l-3 3a.5.5 0 0 1-.708 0l-1.5-1.5a.5.5 0 1 1 .708-.708L8 8.293l2.646-2.647a.5.5 0 0 1 .708 0z"/>
                  </svg>
                  <span class="header__nav-cart-quantity">{{ cart.length }}</span>
               </router-link>
            </li>
         </div>
      </div>
   </div>
</template>

<script>
  import { mapActions, mapGetters } from 'vuex';

  export default {
     name: "layout-header",

     data: () => ({ searchValue: '' }),

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
     align-items: center;
     background: $background-black;
     padding: 16px;
     //position: fixed;
     width: 100%;
     top: 0;
     left: 0;
     height: 70px;
     background: #131921;
     
     img {
        width: 50px;
     }

     &__nav-container {
        display: flex;
        align-items: center;
        height: 70px;
        width: 100%;
        justify-content: space-between;
     }

     &__nav-search {
        padding: 16px;
        position: relative;
        right: 200px;
        display: flex;
        justify-content: center;
        align-items: center;
     }

     &__nav-search-btn {
        margin-left: 16px;
        background: #e1e1e1;
        border: none; 
        
        &_clear {
           margin-left: 16px;
           background: #e66c6c;
           border: none;
        }
     }

     &__nav-cart li {
        list-style: none;
     }

     &__nav-cart li a {
        text-decoration: none;
        color: #fff;
        position: relative;
     }

     &__nav-cart-quantity {
        position: absolute;
        top: -25px;
        right: -15px;
        width: 25px;
        height: 25px;
        border: none;
        background: #f08804;
        color: #fff;
        border-radius: 50%;
        display: flex;
        align-items: center;
        justify-content: center;
        font-size: 13px;
     }
  }
</style>