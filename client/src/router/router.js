import { createRouter, createWebHashHistory } from 'vue-router';

const router = createRouter({
  history: createWebHashHistory(),
  routes: [
    {
      component: () => import('@/components/auth/change-password'),
      path: '/change-password',
      name: 'change-password',
    },
    {
      component: () => import('@/components/order/order-list'),
      path: '/order-list',
      name: 'order-list',
    },
    {
      component: () => import('@/components/order/order-list-item-details'),
      path: '/order/:id',
      name: 'order-list-item-details',
      props: true 
    },
    {
      component: () => import('@/components/payment/order-payment'),
      path: '/order-payment',
      name: 'order-payment',
    },
    {
      component: () => import('@/components/payment/order-delivery'),
      path: '/order-delivery',
      name: 'order-delivery',
    },
    {
      component: () => import('@/components/payment/order-payment-response'),
      path: '/order-payment-response',
      name: 'order-payment-response',
    },
    {
      component: () => import('@/pages/main-page'),
      path: '/',
      name: 'mainPage'
    },
    {
      component: () => import('@/components/catalog/catalog'),
      path: '/catalog',
      name: 'catalog'
    },
    {
      component: () => import('@/components/cart/cart-list'),
      path: '/cart-list',
      name: 'cart-list',
      props: true
    },
    {
      component: () => import('@/components/cart/cart-item-details'),
      path: '/cart/item/:id',
      name: 'cart-item-details',
      props: true
    },
    {
      component: () => import('@/components/favorits/favorits-list'),
      path: '/favorits',
      name: 'favorits-list'
    }
  ]
});

// router.beforeEach((to, from, next) => {
//   const requiresAuth = to.matched.some(record => record.meta.requiresAuth);
//   const isAuthenticated = store.getters['IS_AUTHENTICATED'];
//
//   if (requiresAuth && !isAuthenticated) {
//     next('/login');
//     next(false);
//   } else {
//     next();
//   }
// });

export default router;