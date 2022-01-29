import { createApp } from 'vue';
import App from './App.vue';
import store from './vuex/store';
import router from './router/router';
import { formatPrice } from '@/filters/price.filter';

const app = createApp(App);

app.config.globalProperties.$formatPrice = formatPrice;

app.use(router);
app.use(store);

app.mount('#app');
