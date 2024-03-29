import axios from 'axios';

const ProductModule = {
   state: {
      products: [],
      searchValue: ''
   },

   mutations: {
      SET_SEARCH_VALUE_TO_VUEX: (state, value) => {
         state.searchValue = value;
      },
      SET_PRODUCTS_TO_STATE: (state, products) => {
         state.products = products;
      }
   },

   actions: {
      SEARCH_VALUE_TO_STORE({commit}, value) {
         commit('SET_SEARCH_VALUE_TO_VUEX', value)
      },

      GET_PRODUCTS_FROM_API({commit}) {
         return axios('http://localhost:3000/products', {
            method: "GET"
         }).then((products) => {
            commit('SET_PRODUCTS_TO_STATE', products.data)
            return products;
         }).catch((error) => {
            console.error(error);
            return error;
         })
      }
   },

   getters: {
      SEARCH_STATE: state => state.searchValue,
      PRODUCTS_STATE: state => state.products,
   },

   install: (app) => {
      app.provide('products', ProductModule.state);
   }
};

export default ProductModule;
