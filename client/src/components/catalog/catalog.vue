<template>
   <div class="catalog">
      <router-link :to="{ name: 'cart' }">
      <div class="catalog__link_to_cart">Cart: {{ CART.length }}</div>
      </router-link>
      
      <h1>Catalog</h1>
      
      <div class="catalog__list">
         <catalog-item
            v-for="product in PRODUCTS"
            :key="product.article"
            :product_data="product"
            @addToCart="addToCart"
         />
      </div>
   </div>
</template>

<script>
  import CatalogItem from '@/components/catalog/catalog-item'
  import { mapActions, mapGetters } from 'vuex'

  export default {
     name: "v-catalog",
     
     components: {
        CatalogItem
     },
    
     computed: {
        ...mapGetters([
           'PRODUCTS',
           'CART'
        ])
     },
     
     methods: {
        ...mapActions([
           'GET_PRODUCTS_FROM_API',
           'ADD_TO_CART'
        ]),
        addToCart(data) {
           this.ADD_TO_CART(data)
        },
     },
     
     // Load data check
     mounted() {
        this.GET_PRODUCTS_FROM_API().then((res) => {
           if (res.data) {
              console.log("Load data successuly")
           }
        }).catch((error) => {
           console.log(error)
           return error
        })
     }
  }
</script>

<style lang="scss" scoped>
  .catalog {
     &__list {
        display: flex;
        flex-wrap: wrap;
        justify-content: space-between;
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