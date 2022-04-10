import axios from 'axios';

const OrderModule = {
  state: {
    orderList:    [],
    orderDetails: [],
    orderAddress: {},
  },

  actions: {
    SAVE_ORDER_DETAILS({commit}, orderAddress) {
      commit('SET_ORDER_ADDRESS_TO_STATE', orderAddress);
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
  },

  mutations: {
    SET_ORDERS_TO_STATE(state, inputOrderlist) {
      state.orderList = inputOrderlist;
    },

    SET_ORDER_DETAILS_TO_STATE(state, details) {
      state.orderDetails = details;
    },

    SET_ORDER_ADDRESS_TO_STATE(state, address) {
      state.orderAddress = address;
    },

    REMOVE_ORDER_FROM_STATE(state, orderId) {
      // Finding the order index in the list of orders by orderId
      const index = state.orderList.findIndex(order => order.id === orderId);
      if (index !== -1) {
        // Removing an order from the order list
        state.orderList.splice(index, 1);
      }
    }
  },

  getters: {
    ORDER_DETAILS: state => state.orderDetails,
    ORDER_LIST: state => state.orderList,
    ORDER_ADDRESS: state => state.orderAddress,
  },
};

export default OrderModule;