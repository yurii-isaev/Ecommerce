import { createApp } from 'vue';
import App from './App.vue';
import store from './vuex/store';
import router from './router/router';
import { formatPrice } from '@/filters/price.filter';
import { formatDate } from '@/filters/date.format';
import { checkDataType } from '@/types/check.type';

const app = createApp(App);

// Register a global function in main.js
app.config.globalProperties.$formatPrice = formatPrice;
app.config.globalProperties.$formatDate = formatDate;
app.config.globalProperties.$checkDataType = checkDataType;

app.use(router);
app.use(store);

app.mount('#app');
