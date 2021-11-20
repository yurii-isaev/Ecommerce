import { createStore } from 'vuex';
import productsModule from '@/modules/products.module';
import cartModule from '@/modules/cart.module';

export default createStore({
   modules: {
      productsModule,
      cartModule
   }
});