import { createRouter, createWebHashHistory } from 'vue-router';
import Catalog from '@/components/catalog/catalog';
import Cart from '@/components/cart/cart';
import MainPage from '@/pages/main-page';

const routes = [
   {
      path: '/',
      name: 'mainPage',
      component: MainPage
   },
   {
      path: '/catalog',
      name: 'catalog',
      component: Catalog
   },
   {
      path: '/cart',
      name: 'cart',
      component: Cart,
      props: true
   }
];

const router = createRouter({
   history: createWebHashHistory(),
   routes
});

export default router;