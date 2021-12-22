import { createApp } from 'vue';
import App from './App.vue';
import store from './vuex/store';
import router from './router/router';
import FavoritsModule from '@/modules/favorits.module';
import CartModule from '@/modules/cart.module';
import OrdersModule from '@/modules/orders.module';
import { formatPrice } from '@/filters/price.filter';

const app = createApp(App);

app.config.globalProperties.$formatPrice = formatPrice;

app.use(router)
    .use(store)
    .use(CartModule)
    .use(FavoritsModule)
    .use(OrdersModule);

app.mount('#app');
