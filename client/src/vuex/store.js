import { createStore } from 'vuex';
import ProductsModule from '@/modules/products.module';
import CartModule from '@/modules/cart.module';
import FavoritsModule from '@/modules/favorits.module';
import OrdersModule from '@/modules/orders.module';
import UserModule from '@/modules/user.module';

export default createStore({
   modules: {
      ProductsModule,
      CartModule,
      FavoritsModule,
      OrdersModule,
      UserModule
   }
});