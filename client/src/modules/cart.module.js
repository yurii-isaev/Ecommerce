const CartModule = {
   state: {
      cart: [],
      selectedItem: null
   },

   mutations: {
      SET_CART_VALUE: (state, product) => {
         if (state.cart.length) {
            let isProductExists = false;
            state.cart.map(function (item) {
               if (item.article === product.article) {
                  isProductExists = true;
                  item.quantity++;
               }
            });
            if (!isProductExists) {
               state.cart.push(product);
            }
         } else {
            state.cart.push(product);
         }
      },
      REMOVE_FROM_CART: (state, index) => {
         state.cart.splice(index, 1);
      },
      INCREMENT: (state, index) => {
         state.cart[index].quantity++;
      },
      DECREMENT: (state, index) => {
         if (state.cart[index].quantity > 1) {
            state.cart[index].quantity--;
         }
      }
   },
   actions: {
      ADD_TO_CART({commit}, product) {
         commit('SET_CART_VALUE', product);
      },

      DELETE_FROM_CART({commit}, index) {
         commit('REMOVE_FROM_CART', index);
      },

      INCREMENT_CART_ITEM({commit}, index) {
         commit('INCREMENT', index);
      },

      DECREMENT_CART_ITEM({commit}, index) {
         commit('DECREMENT', index);
      }
   },
   getters: {
      CART_STATE: state => state.cart,
      CART_ITEM_ID: (state) => (id) => {
         return state.cart.find(item => item.id === id);
      },
   },

   install: (app) => {
      app.provide('cart', CartModule.state);
   }
};

export default CartModule;