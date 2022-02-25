const OrderModule = {
  state: {
    orderDetails: {}
  },

  actions: {
    SAVE_ORDER_DETAILS({commit}, orderDetails) {
      commit('SET_ORDER_DETAILS', orderDetails);
    }
  },

  mutations: {
    SET_ORDER_DETAILS(state, orderDetails) {
      state.orderDetails = orderDetails;
    }
  },

  getters: {
    ORDER_DETAILS: state => state.orderDetails,
  },

  install: (app) => {
    app.provide('order', OrderModule.state);
  }
};

export default OrderModule;