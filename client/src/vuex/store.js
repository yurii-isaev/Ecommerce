import Vuex from 'vuex';
import axios from 'axios';

let store = new Vuex.Store({
   state: {
      products: [],
      cart: []
   },

   // sync function working with state
   mutations: {
      SET_PRODUCTS_TO_STATE: (state, products) => {
         state.products = products;
      },
      SET_CART: (state, product) => {
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
         console.log(index)
         state.cart[index].quantity++
      },

      DECREMENT: (state, index) => {
         console.log(index)
         if (state.cart[index].quantity > 1) {
            state.cart[index].quantity--
         }
      },
   },

   // async main function working with mutations
   actions: {
      GET_PRODUCTS_FROM_API({commit}) {
         return axios('http://localhost:3000/products', {
            method: "GET"
         }).then((products) => {
            commit('SET_PRODUCTS_TO_STATE', products.data);
            return products;
         }).catch((error) => {
            console.error(error);
            return error;
         })
      },

      ADD_TO_CART({commit}, product) {
         commit('SET_CART', product);
      },

      DELETE_FROM_CART({commit}, index) {
         commit('REMOVE_FROM_CART', index)
      },

      INCREMENT_CART_ITEM({commit}, index) {
         commit('INCREMENT', index)
      },

      DECREMENT_CART_ITEM({commit}, index) {
         commit('DECREMENT', index)
      },
   },
   getters: {
      PRODUCTS(state) {
         return state.products;
      },
      CART(state) {
         return state.cart;
      },
   } // state observer
});

export default store;
