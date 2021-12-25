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
      DELETE_FAVORITS_VALUE: (state, favoritId) => {
         state.favorits = state.favorits.filter(i => i.id !== favoritId);
      }
   },
   actions: {
      ADD_TO_FAVORITS({commit}, product) {
         commit('SET_FAVORITS_VALUE', product);
      },
      DELETE_FROM_FAVORITS({commit}, favoritId) {
         commit('DELETE_FAVORITS_VALUE', favoritId);
      },
   },
   getters: {
      FAVORITS_STATE: state => state.favorits,
   },
   
   // Provide the status of your favorite products for inject
   install: (app) => {
      app.provide('favorits', FavoritsModule.state);
   }
};

export default FavoritsModule;