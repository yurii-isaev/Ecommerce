<template>
	 <div class="v-main-wrapper"></div>
	 <v-catalog></v-catalog>
</template>

<script>
import VCatalog from '@/components/v-catalog';

export default {
	 name: "v-main-wrapper",

	 components: {
			VCatalog
	 },

	 data() {
			return {
				 title: "Main wrapper"
			}
	 }
}
</script>

<style lang="scss" scoped>

  .v-main-wrapper {
	   display: flex;
	   flex-direction: column;
	   justify-content: center;
	   align-items: center;
	   max-width: 900px;
	   margin: 0 auto;
  }
</style>