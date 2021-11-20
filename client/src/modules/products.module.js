import axios from 'axios';

const state = {
   products: []
};

const mutations = {
   SET_PRODUCTS_TO_STATE: (state, products) => {
      state.products = products;
   }
};

const actions = {
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
};

const getters = {
   PRODUCTS(state) {
      return state.products;
   }
};

export default {
   state,
   mutations,
   actions,
   getters
};
