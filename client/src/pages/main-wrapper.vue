<template>
   <div class="main-wrapper shadow-grey-200">
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

     components: { LayoutHeader },

     data: () => ({title: "Main wrapper"})
  }
</script>

<style lang="scss" scoped>
  .main-wrapper {
     display: flex;
     flex-direction: column;
     justify-content: center;
     margin: 0 auto;
     background-color: #ffffff;
     width: 95%; /* 3/5 = 60% */
     border-radius: 0.75rem;
     box-shadow: 0 25px 50px -12px rgba(0, 0, 0, 0.25);
     margin-top: 3rem;
  }
  
  .shadow-grey-200 {
     box-shadow: 0 4px 6px -1px rgba(0, 0, 0, 0.1), 0 2px 4px -1px rgba(0, 0, 0, 0.06); /* grey-200 в Tailwind */
  }
</style>