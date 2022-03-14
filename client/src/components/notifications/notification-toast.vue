<template>
  <div class='notification'>
    <transition-group class="notification-list" name="transition-animate" tag="div">
      <div class="notification-content" v-for="message in messages" :key="message.id" :class="message.icon">
        <div class="notification-text">
          <span>{{ message.name }}</span>
          <i class="material-icons">{{ message.icon }}</i>
        </div>
        <div class="wrap">
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
      messages:    {type: Array,  default: () => []}, 
      rightButton: {type: String, default: ''}, 
      leftButton:  {type: String, default: ''}, 
      timeout:     {type: Number, default: 1000}
    }, 
    
    methods: {
      // ctx -- хранит ссылку на текущий контекст выполнения функции
      // используется для сохранения ссылки на исходный контекст выполнения функции,
      // чтобы можно было обращаться к его свойствам из вложенных функций без опасения потерять контекст выполнения.
      // так как внутри функции setTimeout контекст this уже не ссылается на Vue-компонент.
      hideNotification() {
        let ctx = this;
        if (this.messages.length) {
          setTimeout(() => ctx.messages.splice(ctx.messages.length - 1, 1), this.timeout)
        }
      }
    }, 
    
    watch: {
      messages: {
        handler() {this.hideNotification()}, 
        deep: true
      }
    },
  }
</script>

<style lang="scss" scoped>
  .notification {
    position: fixed;
    top: 100px;
    right: 10px;
    z-index: 10;
    opacity: 0.8;
  }

  .notification-list {
    display: flex;
    flex-direction: column-reverse;
  }  
  
  .notification-content {
    padding: 16px;
    border-radius: 4px;
    color: #ffffff;
    display: flex;
    justify-content: space-between;
    align-items: center;
    height: 50px;
    margin-bottom: 16px;
    background: #85a767;
  }  
  
  .notification-content.error {
    background: red;
  } 
  
  .notification-content.warning {
    background: orange;
  }  
  
  .notification-content.check_circle {
    background: green;
  }  
  
  .notification-content-text {
    display: flex;
    align-items: center;
    justify-content: space-between;
  }

  .transition-animate-enter-from {
    transform: translateX(120px);
    opacity: 0;
  }

  .transition-animate-enter-active {
    transition: all .6s ease;
  }

  .transition-animate-enter-to {
    opacity: 1;
  }

  .transition-animate-leave-from {
    height: 50px;
    opacity: 1;
  }

  .transition-animate-leave-active {
    transition: transform .6s ease, opacity .6s, height .6s .2s;
  }

  .transition-animate-leave-to {
    height: 0;
    transform: translateX(120px);
    opacity: 0;
  }

  .transition-animate-move {
    transition: transform .6s ease;
  }
</style>