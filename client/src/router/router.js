import { createRouter, createWebHashHistory } from 'vue-router';
import Catalog from '@/components/catalog/catalog';
import Favorits from '@/components/favorits/favorits-list';
import MainPage from '@/pages/main-page';
import CartItemDetails from '@/components/cart/cart-item-details';

const routes = [
  {
    component: () => import('@/components/auth/change-password-form'),
    path: '/change-password',
    name: 'change-password-form',
  },
    
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
    component: () => import('@/components/cart/cart-list'),
    path: '/cart-list',
    name: 'cart-list',
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