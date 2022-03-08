import { createRouter, createWebHashHistory } from 'vue-router';

const router = createRouter({
  history: createWebHashHistory(),
  routes: [
    {
      component: () => import('@/components/auth/change-password-form'),
      path: '/change-password',
      name: 'change-password-form',
    },
    {
      component: () => import('@/components/order/order-payment'),
      path: '/order-payment',
      name: 'order-payment',
    },
    {
      component: () => import('@/components/order/order-delivery'),
      path: '/order-delivery',
      name: 'order-delivery',
    },
    {
      component: () => import('@/components/order/order-payment-response'),
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