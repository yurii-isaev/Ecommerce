const OrdersModule = {
   state: {
      totalOrders: [],
      orders: []
   },

   mutations: {
      SET_ORDER_VALUE: (state, product) => {
         state.orders.push(product);
      },
   },
   actions: {
      ACTION_ADD_TO_ORDERS({commit}, product) {
         commit('SET_ORDER_VALUE', product);
      },
   },
   getters: {
      ORDER_STATE_VALUE: state => state.orders,
      ORDER_ITEM_ID: (state) => (id) => {
         return state.orders.find(item => item.id === id);
      },
   },

   install: (app) => {
      app.provide('order', OrdersModule.state);
   }
};

export default OrdersModule;