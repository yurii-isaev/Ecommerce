import { createApp } from 'vue';
import App from './App.vue';
import store from './vuex/store';
import router from './router/router';
import FavoritsModule from '@/modules/favorits.module';

const app = createApp(App);

app.use(router).use(store).use(FavoritsModule); // Используем модуль FavoritsModule как плагин

app.mount('#app');
