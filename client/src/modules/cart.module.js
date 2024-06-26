import axios from 'axios';

const CartModule = {
  state: {
    cart: [],
    selectedItem: null,
    message: null,
    isPaid: false,
    cartTotal: {}
  },

  mutations: {
    SET_CART(state, cart) {
      state.cart = cart;
    },
    SET_TOTAL_CART(state, cartTotal) {
      state.cartTotal = cartTotal;
    },
    
    SET_CART_VALUE: (state, product) => {
      console.log("Adding product to cart:", product);
      
      if (!('price' in product) || typeof product.price !== 'number') {
        console.error("Invalid product price:", product);
        return; // Don't add the product if the price is invalid
      }
      
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
    },
    
    UPDATE_CART: (state, updatedOrder) => {
      const index = state.cart.findIndex(order => order.id === updatedOrder.id);
      if (index !== -1) {
        state.cart[index] = { ...state.cart[index], ...updatedOrder };
        // Vue.set(state.cart, index, { ...state.cart[index], ...updatedOrder });
      } else {
        state.cart.push(updatedOrder);
      }
    },

    SET_ORDER_RESPONSE_MESSAGE(state, success) {
      state.isPaid = success;
    }
  },

  actions: {
    UPDATE_CART({ commit }, cart) {
      commit('SET_CART', cart);
    },

    ADD_CART_TOTAL({ commit }, totalCartObj) {
      commit('SET_TOTAL_CART', totalCartObj);
    },
    
    ADD_TO_CART({commit}, product) {
      commit('SET_CART_VALUE', product);
    },

    CLEAR_CART({ commit }) {
      commit('SET_CART', []);
    },

    DELETE_FROM_CART({commit}, index) {
      commit('REMOVE_FROM_CART', index);
    },

    INCREMENT_CART_ITEM({commit}, index) {
      commit('INCREMENT', index);
    },

    DECREMENT_CART_ITEM({commit}, index) {
      commit('DECREMENT', index);
    },

    UPDATE_CART_STATE({commit}, itemSnapshot) {
      commit('UPDATE_CART', itemSnapshot);
    },

    async POST_USER_ORDER_TO_API({commit}, order) {
      try {
        const response = await axios.post('http://localhost:5000/api/order/CreateOrder', order, {
          withCredentials: true
        });
        if (response.data.code == 200) {
          await commit('SET_ORDER_RESPONSE_MESSAGE', response.data.success);
        }
        if (response.data.code == 400) {
          //
        }
      } catch (error) { // if response.status == 400
        console.error('POST_USER_ORDER_TO_API_ERROR:', error);
        // throw error;
      }
    },
  },

  getters: {
    CART_STATE: state => state.cart,
    CART_TOTAL: state => state.cartTotal,
    CART_ITEM_ID: (state) => (id) => {
      return state.cart.find(item => item.id === id);
    },
    IS_ORDER_PAID: state => state.isPaid,
  }
};

export default CartModule;