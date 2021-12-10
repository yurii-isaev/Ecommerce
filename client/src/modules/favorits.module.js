const FavoritsModule = {
   state: {
      favorits: [],
   },
   mutations: {
      SET_FAVORITS_VALUE: (state, product) => {
         if (state.favorits.length) {
            let isProductExists = false;
            state.favorits.map(function (item) {
               if (item.id === product.id) {
                  isProductExists = true;
                  item.quantity++;
               }
            });
            if (!isProductExists) {
               state.favorits.push(product);
            }
         } else {
            state.favorits.push(product);
         }
      },
   },
   actions: {
      ACTION_ADD_TO_FAVORITS({commit}, product) {
         commit('SET_FAVORITS_VALUE', product);
      },
   },
   getters: {
      FAVORITS_STATE_VALUE: state => state.favorits,
   },
   
   // Provide the status of your favorite products
   install: (app) => {
      app.provide('favorits', FavoritsModule.state);
   }
};

export default FavoritsModule;