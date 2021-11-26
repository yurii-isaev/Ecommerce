<template>
   <div class="catalog-item-select">
      <p class="catalog-item-select__title" @click="isOptionsVisible = !isOptionsVisible">
         {{ selected }}
      </p>
      <div class="catalog-item-select__options" v-if="isOptionsVisible">
         <p v-for="option in options"
            :key="option.value"
            @click="selectOption(option)">
            {{ option.name }}
         </p>
      </div>
   </div>
</template>

<script>
  export default {
     name: "component-catalog-item-select",
     
     props: {
        options: {
           type: Array, 
           default() {
              return []
           }
        }, 
        selected: {
           type: String, 
           default: ''
        }
     }, 
     
     data() {
        return {
           isOptionsVisible: false
        }
     }, 
     
     methods: {
        selectOption(option) {
           this.$emit('selectOption', option);
           this.isOptionsVisible = false;
        },
     }
  }
</script>

<style lang="scss" scoped>
  .catalog-item-select {
     position: relative;
     width: 200px;
     cursor: pointer;
     text-align: left; 
     
     &__title {
        border: 1px solid #aeaeae;
        padding: 8px;
     } 
     
     & p {
        margin: 0;
     } 
     
     &__options {
        border: 1px solid #aeaeae;
        background: #ffffff;
        position: absolute;
        top: 30px;
        left: 0;
        width: 100%;
        padding: 8px; 
        
        p:hover {
           background: #e7e7e7;
        } 
     } 
  }
</style>