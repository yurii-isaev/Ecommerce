<template>
   <div class='notification'>
      <transition-group class="notification__list" name="transition-animate" tag="div">
         <div class="notification__content" 
              v-for="message in messages" 
              :key="message.id" 
              :class="message.icon"
         >
            <div class="notification__text">
               <span>{{ message.name }}</span>
               <i class="material-icons">{{ message.icon }}</i>
            </div>
            
            <div class="notification_buttons">
               <button v-if="rightButton.length">{{ rightButton }}</button>
               <button v-if="leftButton.length">{{ leftButton }}</button>
            </div>
         </div>
      </transition-group>
   </div>
</template>

<script>
  export default {
     props: {
        messages: {
           type: Array, 
           default: () => []
        }, 
        rightButton: {
           type: String, 
           default: ''
        }, 
        leftButton: {
           type: String, 
           default: ''
        }, 
        timeout: {
           type: Number, 
           default: 2000
        }
     }, 
     
     methods: {
        // внутри функции обратного вызова setTimeout используется vm.messages.length,
        // чтобы обратиться к массиву messages,
        // так как внутри функции setTimeout контекст this уже не ссылается на Vue-компонент.
        hideNotification() {
           let vm = this;
           if (this.messages.length) {
              setTimeout(() => vm.messages.splice(vm.messages.length - 1, 1), this.timeout)
           }
        }
     }, 
     
     watch: {
        messages: {
           handler() {
              this.hideNotification()
           }, 
           deep: true
        }
     },
  }
</script>

<style lang="scss" scoped>
  .notification {
     position: fixed;
     top: 80px;
     right: 16px;
     z-index: 10;
     opacity: 0.8;
     
     &__list {
        display: flex;
        flex-direction: column-reverse;
     } 
     
     &__content {
        padding: 16px;
        border-radius: 4px;
        color: #ffffff;
        display: flex;
        justify-content: space-between;
        align-items: center;
        height: 50px;
        margin-bottom: 16px;
        background: green; 
        
        &.error {
           background: red;
        } 
        
        &.warning {
           background: orange;
        } 
        
        &.check_circle {
           background: green;
        } 
     
        &__text {
           display: flex;
           align-items: center;
           justify-content: space-between;
        } 
     } 
     
    
  }  
  
  .transition-animate { 
     &-enter-from {
        transform: translateX(120px);
        opacity: 0;
     } 
     
     &-enter-active {
        transition: all .6s ease;
     } 
     
     &-enter-to {
        opacity: 1;
     } 
     
     &-leave-from {
        height: 50px;
        opacity: 1;
     } 
     
     &-leave-active {
        transition: transform .6s ease, opacity .6s, height .6s .2s;
     } 
     
     &-leave-to {
        height: 0;
        transform: translateX(120px);
        opacity: 0;
     } 
     
     &-move {
        transition: transform .6s ease;
     } 
  }
</style>