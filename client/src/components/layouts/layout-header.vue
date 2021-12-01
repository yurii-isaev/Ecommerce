<template>
   <div class='header'>
      <router-link :to="{ name: 'mainPage' }">
         <img src="../../assets/logo.png" alt="">
      </router-link>
      <div class="header__search">
         <input type="text" v-model="searchValue">
         <button class="header__search-btn">
            <i class="material-icons" @click="search(searchValue)">search</i>
         </button>
         <button class="header__search-btn_clear">
            <i class="material-icons" @click="clearSearchField">cancel</i>
         </button>
      </div>
   </div>
</template>

<script>
  import { mapActions } from 'vuex';

  export default {
     name: "layout-header",

     data: () => ({ searchValue: '' }),
     
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
     position: fixed;
     width: 100%;
     top: 0;
     left: 0; 
     
     img {
        width: 50px;
     }

     &__search {
        padding: 16px;
        position: relative;
        right: 200px;
        display: flex;
        justify-content: center;
        align-items: center;
     }

     &__search-btn {
        margin-left: 16px;
        background: #e1e1e1;
        border: none; 
        
        &_clear {
           margin-left: 16px;
           background: #e66c6c;
           border: none;
        }
     }
  }
</style>