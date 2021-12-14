import { createApp } from 'vue';
import App from './App.vue';
import store from './vuex/store';
import router from './router/router';
import FavoritsModule from '@/modules/favorits.module';
import CartModule from '@/modules/cart.module';
import OrdersModule from '@/modules/orders.module';

const app = createApp(App);

app.use(router)
    .use(store)
    .use(CartModule)
    .use(FavoritsModule)
    .use(OrdersModule);

app.mount('#app');
