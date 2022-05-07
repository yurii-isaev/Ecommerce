import axios from 'axios';

const OrderModule = {
  state: {
    orderList: [],
    orderDetails: [],
    allOrderAddress: [],
    currentOrderAddress: {}
  },

  actions: {
    SAVE_ORDER_DETAILS({commit}, orderAddress) {
      commit('SET_CURRENT_ORDER_ADDRESS_TO_STATE', orderAddress);
    },

    // actions
    DELETE_ORDER({commit}, orderId) {
      axios.delete(`http://localhost:5000/api/order/DeleteOrder/${orderId}`, {
        withCredentials: true
      }).then(response => {
        if (response.data.code === 200) {
          commit('REMOVE_ORDER_FROM_STATE', orderId);
        } else {
          console.warn('DELETE_ORDER API response:', response.data.message);
        }
      }).catch(error => {
        console.error('Error during DELETE_ORDER:', error);
      });
    },

    async GET_ORDERS_FROM_API({commit}, userId) {
      try {
        const response = await axios.get(`http://localhost:5000/api/order/GetOrderList/${userId}`, {
          withCredentials: true
        });
        if (response.data.code == 200) {
          // SET_ORDERS
          commit('SET_ORDERS_TO_STATE', response.data.set)
          const allOrderDetails = response.data.set.reduce((acc, order) => {
            return acc.concat(order.orderDetails);
          }, []);
          // SET_ORDER_DETAILS
          commit('SET_ORDER_DETAILS_TO_STATE', allOrderDetails);
          // SET_ORDER_ADDRESS
          commit('SET_ORDER_ADDRESS_TO_STATE', response.data.set.map(order => order.orderAddress));
        } else {
          console.warn('GET_ORDERS_FROM_API:', response.data.message);
        }
      } catch (error) {
        console.error('Error during GET_ORDERS_FROM_API:', error);
      }
    },

    CLEAR_ORDER_DETAILS({ commit }) {
      commit('CLEAR_ORDER_DETAILS');
    },

    CLEAR_IS_ORDER_PAID({ commit }) {
      commit('CLEAR_IS_ORDER_PAID');
    },
  },

  mutations: {
    SET_ORDERS_TO_STATE(state, inputOrderlist) {
      state.orderList = inputOrderlist;
    },

    SET_ORDER_DETAILS_TO_STATE(state, details) {
      state.orderDetails = details;
    },

    SET_ORDER_ADDRESS_TO_STATE(state, address) {
      state.allOrderAddress = address;
    },

    SET_CURRENT_ORDER_ADDRESS_TO_STATE(state, address) {
      state.currentOrderAddress = address;
    },

    REMOVE_ORDER_FROM_STATE(state, orderId) {
      // Finding the order index in the list of orders by orderId
      const index = state.orderList.findIndex(order => order.id === orderId);
      if (index !== -1) {
        // Removing an order from the order list
        state.orderList.splice(index, 1);
      }
    },

    CLEAR_ORDER_DETAILS(state) {
      state.orderDetails = [];
    },

    CLEAR_IS_ORDER_PAID(state) {
      state.isOrderPaid = false;
    },
  },

  getters: {
    ORDER_DETAILS: state => state.orderDetails,
    ORDER_LIST: state => state.orderList,
    ALL_ORDER_ADDRESS: state => state.allOrderAddress,
    CURRENT_ORDER_ADDRESS: state => state.currentOrderAddress,
  },
};

export default OrderModule;