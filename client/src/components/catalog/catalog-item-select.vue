<template>
   <div class="catalog-item-select">

      <p class="title" @click="isOptionsVisible = ! isOptionsVisible">
         {{ selected }}
      </p>

      <div class="options" v-if="isOptionsVisible">
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
     name: "catalog-item-select",
     
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

<style scoped>
  .catalog-item-select {
     position: relative;
     width: 200px;
     cursor: pointer;
     text-align: left;
  }
  .title {
     border: solid 1px #2c3e50;
     padding: 8px;
  }

  .catalog-item-select p {
     margin: 0;
  }
  
  .options {
     border: solid 1px #2c3e50;
     background: #e0e0e0;
     position: absolute;
     top: 30px;
     left: 0;
     width: 100%;
     padding: 8px;
  }

  .options p:hover {
     background: #e0e0e0;
  }
  
</style>