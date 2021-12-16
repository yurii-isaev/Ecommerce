import { createRouter, createWebHashHistory } from 'vue-router';
import Catalog from '@/components/catalog/catalog';
import Cart from '@/components/cart/cart';
import Favorits from '@/components/favorits/favorits-list';
import MainPage from '@/pages/main-page';
import CartItemDetails from '@/components/cart/cart-item-details';

const routes = [
   // {
   //    component: () => import('@/pages/auth-page'),
   //    path: '/auth',
   //    name: 'auth-page',
   // },
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
   },
   {
      path: '/cart/item/:id',
      name: 'cart-item-details',
      component: CartItemDetails,
      props: true 
   },
   {
      path: '/favorits',
      name: 'favorits-list',
      component: Favorits,
   }
];

const router = createRouter({
   history: createWebHashHistory(),
   routes
});

export default router;