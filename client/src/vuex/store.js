import Vuex from 'vuex';
import axios from 'axios';

let store = new Vuex.Store({
   state: {
      products: []
   },
   mutations: {
      SET_PRODUCTS_TO_STATE: (state, products) => {
         state.products = products
      }
   }, // sync actions

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
      }
   }, // async actions
   getters: {
      PRODUCTS(state) {
         return state.products;
      }
   } // state observer
});

export default store;
