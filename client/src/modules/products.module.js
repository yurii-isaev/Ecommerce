import axios from 'axios';

const ProductModule = {
   state: {
      products: [],
      searchValue: ''
   },

   mutations: {
      SET_SEARCH_VALUE: (state, value) => {
         state.searchValue = value;
      },
      
      SET_PRODUCTS_TO_STATE: (state, products) => {
         state.products = products;
      }
   },

   actions: {
      SEARCH_VALUE_TO_STORE({commit}, value) {
         commit('SET_SEARCH_VALUE', value)
      },

      async GET_PRODUCTS_FROM_API({commit}) {
         try {
            const response = await axios.get('http://localhost:5000/api/store/GetAllProducts');
            commit('SET_PRODUCTS_TO_STATE', response.data);
            // return response;
         } catch (error) {
            console.error(error);
            //return error;
         }
      }
   },

   getters: {
      SEARCH_VALUE: state => state.searchValue,
      PRODUCTS_STATE: state => state.products,
   },

   install: (app) => {
      app.provide('products', ProductModule.state);
   }
};

export default ProductModule;
