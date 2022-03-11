<template>
  <section class="section">
    <NotificationToast :messages="messages"/>
    <div class="container">
      <p v-if="favoritsListIsEmpty" class="empty-list-message">Favorits List is empty.</p>
      
      <!-- Catalog Item component-->
      <CatalogItem
          v-for="favorit in favorits"
          :key="favorit.id"
          :product_props="favorit"
          @addToCart="addToCart"
          @addOrDeleteToFavorite="addOrDeleteToFavorite"/>
    </div>
  </section>
</template>

<script>
  import { mapGetters } from 'vuex';
  import CatalogItem from '../catalog/catalog-item';
  import NotificationToast from '../notifications/notification-toast';

  export default {
    
    components: { CatalogItem, NotificationToast },
  
    data() {
      return { messages: [] }
    },
  
    computed: {
      ...mapGetters(['FAVORITS_STATE']),
    
      favorits() {
        return this.FAVORITS_STATE;
      },
    
      favoritsListIsEmpty() {
        return this.favorits.length === 0;
      },
    },
  
    methods: {
      async addToCart(favorit) {
        await this.$store.dispatch('ADD_TO_CART', favorit);
        let timeStamp = Date.now().toLocaleString();
        this.messages.unshift({name: 'Product added to cart', icon: '', id: timeStamp})
      },
  
      async addOrDeleteToFavorite(favorit) {
        if (!favorit.isFavorite) {
          favorit.isFavorite = true;
          try {
            await this.$store.dispatch('ADD_TO_FAVORITS', favorit);
          } catch (error) {
            favorit.isFavorite = false;
          }
        } else {
          favorit.isFavorite = false;
          try {
            await this.$store.dispatch('DELETE_FROM_FAVORITS', favorit.id);
          } catch (error) {
            favorit.isFavorite = true;
          }
        }
      },
    }
  }
</script>

<style scoped>
  .empty-list-message {
    margin: 35px;
    font-size: 17px;
  }
</style>