<template>
   <div class='v-main-page'>
      <h1>Welcome to Online shop</h1>
      <p>
         <router-link :to="{ name: 'catalog' }">
            to Catalog
         </router-link>
      </p>
   </div>
</template>

<script>
  export default {
     name: "main-page",
  }
</script>
