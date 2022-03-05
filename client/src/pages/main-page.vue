<template>
  <div class='main-page'>
    <!-- <h1>Welcome to Online shop</h1> -->
    <catalog />
  </div>
</template>

<script>
  import Catalog from '@/components/catalog/catalog';

  export default {
    components: { catalog: Catalog }
  }
</script>