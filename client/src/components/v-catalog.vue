<template>
	 <div class="v-catalog">
			<h1>Catalog</h1>
			<div class="v-catalog__list">
				 <v-catalog-item
						 v-for="product in PRODUCTS"
						 :key="product.article"
						 :product_data="product"
						 @sendArticleToParent="getArticleFromChild"
				 />
			</div>
	 </div>
</template>

<script>
// import products from '../../db.json';
import VCatalogItem from '@/components/v-catalog-item';
import { mapActions, mapGetters } from 'vuex';

export default {
	 name: "v-catalog",
	 components: {
			VCatalogItem
	 },
	 props:{},
	 data() {
			return {
				 // products: products.products
			}
	 },
	 computed: {
			...mapGetters([
				 'PRODUCTS'
			])
	 },
	 methods: {
			getArticleFromChild(data) {
				 console.log(data)
			},
			...mapActions([
					'GET_PRODUCTS_FROM_API'
			])
	 },
	 mounted() {
			this.GET_PRODUCTS_FROM_API().then((res) => {
				 if (res.data) {
						console.log("Load data successuly")
				 }
			}).catch((error) => {
				 console.log(error);
				 return error;
			})
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