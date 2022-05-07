<template>
  <div class="header">
    <router-link class="router-link" :to="{ name: 'mainPage' }">
      <div class="text-section">
        <!-- <img src="@/assets/logo/logo.png" alt="Logo" style="width:3.75rem"/> -->
        <div class="text-content">
          <h2 class="title">Vue cakes</h2>
          <p class="description">Online store for ordering cakes</p>
        </div>
      </div>
    </router-link>
  
    <div class="input-box">
      <i class="uil uil-search search-icon"></i>
      <input type="text" v-model="searchValue" :placeholder="placeholderText"
             @input="search(searchValue)" @focus="hidePlaceholder" @blur="showPlaceholder">
      <button class="search-btn_clear" @click="clearSearchField">
        <i class="material-icons">cancel</i>
      </button>
    </div>
    
    <ul class="header-menu">
      <li class="menu-item">
        <router-link class="router-link" :to="{ name: 'catalog' }">
          <img class="icon" :class="{ 'active-icon': isActiveRoute('catalog') }" src="@/assets/vector/cake.svg"  alt="icon"/>
        </router-link>
      </li>
    
      <li class="menu-item">
        <div v-if="!isLoggedIn" data-bs-toggle="modal" data-bs-target="#authModal">
          <img src="@/assets/vector/order-list.svg"  alt="icon"/>
        </div>
        <div v-else>
          <router-link class="router-link" :to="{ name: 'order-list' }">
            <img class="icon" :class="{ 'active-icon': isActiveRoute('order-list') }" src="@/assets/vector/order-list.svg" alt="icon"/>
          </router-link>
        </div>
      </li>
    
      <li class="menu-item">
        <div v-if="!isLoggedIn" data-bs-toggle="modal" data-bs-target="#authModal">
          <img class="icon" :class="{ 'active-icon': isActiveRoute('cart-list') }" src="@/assets/vector/cart.svg" alt="icon"/>
          <span class="cart-quantity-icon" v-if="cartLength > 0"> {{ cartLength }} </span>
        </div>
        <div v-else>
          <router-link class="router-link" :to="{ name: 'cart-list' }">
            <img class="icon" :class="{ 'active-icon': isActiveRoute('cart-list') }" src="@/assets/vector/cart.svg" alt="icon"/>
            <span class="cart-quantity-icon" v-if="cartLength > 0"> {{ cartLength }} </span>
          </router-link>
        </div>
      </li>
    
      <li class="menu-item">
        <div v-if="!isLoggedIn" data-bs-toggle="modal" data-bs-target="#authModal">
          <img class="icon" :class="{ 'active-icon': isActiveRoute('favorits-list') }" src="@/assets/vector/favorits-heart.svg" alt="icon"/>
          <span class="cart-quantity-icon" v-if="favoritsLength > 0"> {{ favoritsLength }} </span>
        </div>
        <div v-else>
          <router-link class="router-link" :to="{ name: 'favorits-list' }">
            <img class="icon" :class="{ 'active-icon': isActiveRoute('favorits-list') }" src="@/assets/vector/favorits-heart.svg" alt="icon"/>
            <span class="cart-quantity-icon" v-if="favoritsLength > 0"> {{ favoritsLength }} </span>
          </router-link>
        </div>
      </li>
    
      <li class="menu-item">
        <!-- Component -->
        <AuthModal/>
      </li>
    </ul>
  </div>
</template>

<script>
  import { mapGetters } from 'vuex';
  import AuthModal from '@/components/auth/auth-modal';
  
  export default {
    components: { AuthModal }, 
    
    data() {
      return {
        isLoggedIn: false,
        searchValue: '',
        placeholderText: 'Catalog search',
        isPlaceholderVisible: true
      }
    },
  
    async mounted() {
      await this.$store.dispatch('CHECK_AUTHENTICATION');
      this.isLoggedIn = this.IS_AUTHENTICATED;
    },
  
    watch: {
      IS_AUTHENTICATED(value) {
        this.isLoggedIn = value;
      }
    },
    
    computed: {
      ...mapGetters(['CART_STATE', 'FAVORITS_STATE', 'IS_AUTHENTICATED', 'SEARCH_VALUE']),
  
      cartLength() {
        return this.CART_STATE.length;
      },
      
      favoritsLength() {
        return this.FAVORITS_STATE.length;
      },
  
      currentRouteName() {
        return this.$route.name;
      },
    }, 
    
    methods: {
      search(value) {
        this.$store.dispatch('SEARCH_VALUE_TO_STORE', value);
        if (this.$route.path !== '/catalog') {
          this.$router.push('/catalog');
        }
      },
      
      clearSearchField() {
        this.searchValue = '';
        this.isPlaceholderVisible = true;
        this.$store.dispatch('SEARCH_VALUE_TO_STORE');
        if (this.$route.path !== '/catalog') {
          this.$router.push('/catalog');
        }
        this.updatePlaceholder();
      },
      
      hidePlaceholder() {
        this.isPlaceholderVisible = false;
        this.updatePlaceholder();
      },
      
      showPlaceholder() {
        if (!this.searchValue) {
          this.isPlaceholderVisible = true;
          this.updatePlaceholder();
        }
      },
      
      updatePlaceholder() {
        this.placeholderText = this.isPlaceholderVisible ? 'Catalog search' : '';
      },
  
      isActiveRoute(routeName) {
        return this.currentRouteName === routeName;
      },
    }
  }
</script>

<style scoped>
  /*.icon {*/
  /*  filter: drop-shadow(0 0 #9B9B9B) drop-shadow(0px 0px 0 #9B9B9B);*/
  /*}*/

  .active-icon {
    filter: drop-shadow(0 0 #9B9B9B) drop-shadow(0 0 0 #232222);
  }

  .icon {
    width: 30px;
    height: 30px;
  }

  .active-icon {
    width: 45px;
    height: 45px;
  }
  
  .header {
    display: flex;
    justify-content: center;
    align-items: center;
    padding: 1rem;
  }

  .text-section {
    display: flex;
    align-items: center;
  }

  .logo {
    width: 3.75rem; 
    height: auto;
  }

  .text-content {
    margin-left: 1rem;
  }

  .title {
    font-size: 1.5rem; /* rem для относительных размеров шрифтов */
    margin: 0;
  }

  .description {
    font-size: 1rem; /* rem для относительных размеров шрифтов */
    margin: 0;
  }


  @media (max-width: 600px) {
    .text-section {
      flex-direction: column;
      align-items: center;
    }
  
    .logo {
      width: 2.5rem; /* Уменьшите размер логотипа на маленьких экранах */
    }
  
    .title {
      font-size: 1.2rem;
    }
  
    .description {
      font-size: 0.9rem;
    }
  }

  ol, ul {
    padding: inherit
  }
  
  .form-group label {
    display: inline-block;
    margin-bottom: 0.5rem;
    font-weight: bold;
  }  
  
  .header {
    display: flex;
    justify-content: space-between;
    padding: 1rem;
    border-bottom: 1px solid #CBD5E0;
  }  
  
  @media (max-width: 768px) {  
    .header {
      flex-direction: column;
      align-items: center;
    }  
    
    .header-menu {
      display: none; /* Скрыть меню на узких экранах */
    }  
    
    .input-box {
      margin: 0.5rem 0;
      width: 100%; /* Ширина на всю страницу */
    }  
    
    .text-section {
      justify-content: center; /* Центрировать содержимое */
    }
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
    gap: 1.7rem;
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

  .menu-item {
    position: relative;
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
    z-index: 10; /* Ensure it's above other elements */
  }
</style>