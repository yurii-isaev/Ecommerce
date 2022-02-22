import axios from "axios";

const CartModule = {
  state: {
    cart: [],
    selectedItem: null,
    message: null,
    isPaid: false,
  },

  mutations: {
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

    SET_CART(state, cart) {
      state.cart = cart;
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
        // Или используя Vue.set
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
        const response = await axios.post('http://localhost:5000/api/order/CreateUserOrder', order);
        if (response.data.success) {
          await commit('SET_ORDER_RESPONSE_MESSAGE', response.data.message);
        } else {
          await commit('SET_ORDER_RESPONSE_MESSAGE', response.data.message);
        }
      } catch (error) {
        console.error('Error during post user order:', error);
        throw error;
      }
    },
  },

  getters: {
    CART_STATE: state => state.cart,
    CART_ITEM_ID: (state) => (id) => {
      return state.cart.find(item => item.id === id);
    },
    IS_ORDER_PAID: state => state.isPaid,
    IS_ORDER_MESSAGE: state => state.message,
  },

  install: (app) => {
    app.provide('cart', CartModule.state);
  }
};

export default CartModule;