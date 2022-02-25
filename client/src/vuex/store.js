import { createStore } from 'vuex';
import ProductsModule from '@/modules/products.module';
import CartModule from '@/modules/cart.module';
import FavoritsModule from '@/modules/favorits.module';
import UserModule from '@/modules/user.module';
import OrderModule from '@/modules/order.module';

export default createStore({
   modules: {
      ProductsModule,
      CartModule,
      FavoritsModule,
      UserModule,
      OrderModule
   }
});