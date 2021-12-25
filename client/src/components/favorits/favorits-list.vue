<template>
   <div class="container">

      <p v-if="favoritsListIsEmpty" style="margin:15px">Favorits List is empty</p>
      
      <NotificationToast :messages="messages"/>

      <CatalogItem
         v-for="favorit in favorits" 
         :key="favorit.id" 
         :product_props="favorit" 
         :isFavorite="favorit.isFavorite" 
         @addToCart="addToCart" 
         @addOrDeleteToFavorite="addOrDeleteToFavorite"
      />
   </div>
</template>

<script>
  import { mapActions, mapGetters } from 'vuex';
  import CatalogItem from '../catalog/catalog-item';
  import NotificationToast from '../notifications/notification-toast';

  export default {
     name: "favorits-list", 
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
        ...mapActions(['ADD_TO_CART', 'ADD_TO_FAVORITS', 'DELETE_FROM_FAVORITS']), 
        
        addToCart(favorit) {
           this.ADD_TO_CART(favorit);
           let timeStamp = Date.now().toLocaleString();
           this.messages.unshift({
              name: 'Product added to cart', icon: '', id: timeStamp
           })
        }, 
        
        addOrDeleteToFavorite: async function (favorit) {
           if (!favorit.isFavorite) {
              favorit.isFavorite = true;
              try {
                 await this.ADD_TO_FAVORITS(favorit);
                 // console.log("Item added to favorites:", favorit);
              } catch (error) {
                 // console.error("Error adding item to favorites:", error);
                 favorit.isFavorite = false;
              }
           } else {
              favorit.isFavorite = false;
              try {
                 await this.DELETE_FROM_FAVORITS(favorit.id);
                 // console.log("Item removed from favorites:", favorit);
              } catch (error) {
                 // console.error("Error removing item from favorites:", error);
                 favorit.isFavorite = true;
              }
           }
        },
     }
  }
</script>