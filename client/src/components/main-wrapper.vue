<template>
   <div class="main-wrapper">
      <layout-header />
      <!-- Keeps the state when the URL changes-->
      <router-view v-slot="{ Component }">
         <keep-alive>
            <component :is="Component" />
         </keep-alive>
      </router-view>
   </div>
</template>

<script>
  import LayoutHeader from '@/components/layouts/layout-header';
  
  export default {
     name: "main-wrapper",

     components: {
        LayoutHeader
     },
     
     data() {
        return {
           title: "Main wrapper"
        }
     }
  }
</script>

<style lang="scss" scoped>
  .main-wrapper {
     display: flex;
     flex-direction: column;
     justify-content: center;
     //align-items: center;
     //max-width: 900px;
     margin: 0 auto;
  }
</style>