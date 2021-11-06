<template>
	 <div class="v-catalog">
			<h1>Catalog</h1>
			<div class="v-catalog__list">
				 <v-catalog-item
						 v-for="product in products"
						 :key="product.article"
						 :product_data="product"
						 @sendArticleToParent="getArticleFromChild"
				 />
			</div>
	 </div>
</template>

<script>
import VCatalogItem from '@/components/v-catalog-item';
import products from '../../db.json';

export default {
	 name: "v-catalog",
	 
	 components: {
			VCatalogItem
	 },
	 
	 methods: {
			getArticleFromChild(data) {
				 console.log(data)
			}
	 },
	 
	 data() {
			return {
				 products: products.products
			}
	 }
}
</script>

<style lang="scss" scoped>
@import 'src/assets/styles/styles';

  .v-catalog {
	   &__list {
			  display: flex;
			  flex-wrap: wrap;
			  justify-content: space-between;
			  align-items: center;
	   }
  }
</style>