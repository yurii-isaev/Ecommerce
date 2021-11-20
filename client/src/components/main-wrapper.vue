<template>
   <div class="main-wrapper"></div>
   <!-- Keeps the state when the URL changes-->
   <router-view v-slot="{ Component }">
      <keep-alive>
         <component :is="Component" />
      </keep-alive>
   </router-view>
   
</template>

<script>
  export default {
     name: "main-wrapper",
     
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
     align-items: center;
     max-width: 900px;
     margin: 0 auto;
  }
</style>