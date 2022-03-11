<template>
  <div class="main-wrapper">
    <layout-header/>
    <!-- Keeps the state when the URL changes-->
    <router-view v-slot="{ Component }">
      <keep-alive>
        <component :is="Component"/>
      </keep-alive>
    </router-view>
  </div>
  <layout-footer/>
</template>

<script>
  import LayoutHeader from '@/components/layouts/layout-header';
  import LayoutFooter from '@/components/layouts/layout-footer';

  export default {
    components: {LayoutHeader, LayoutFooter},
    data: () => ({title: "Main wrapper"})
  }
</script>

<style lang="scss" scoped>
  .main-wrapper {
    display: flex;
    flex-direction: column;
    justify-content: center;
    background-color: #fafafaf2;
    border-radius: 1.75rem 1.75rem 0 0;
    box-shadow: 0 25px 50px -12px rgba(0, 0, 0, 0.25);
  }
</style>