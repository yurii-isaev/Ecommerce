import axios from 'axios';

const OrderModule = {
  state: {
    orderList: [],
    orderDetails: [],
  },

  actions: {
    SAVE_ORDER_DETAILS({commit}, orderDetails) {
      commit('SET_ORDER_DETAILS', orderDetails);
    },

    async GET_ORDERS_FROM_API({commit}, userId) {
      try {
        const response = await axios.get(`http://localhost:5000/api/order/GetOrderList/${userId}`, {
          withCredentials: true // Pass the withCredentials flag to send cookies.
        });
        
        if (response.data.code == 200) {
          commit('SET_ORDERS_TO_STATE', response.data.dataSet)
          // Извлекаем детали заказа из каждого заказа в dataSet и объединяем их в единый массив
          const allOrderDetails = response.data.dataSet.reduce((acc, order) => {
            return acc.concat(order.orderDetails);
          }, []);
          // Записываем объединенные детали заказа в хранилище
          commit('SET_ORDER_DETAILS_TO_STATE', allOrderDetails);
          
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
      console.info('SET_ORDERS_TO_STATE:', inputOrderlist);
    },

    SET_ORDER_DETAILS_TO_STATE(state, details) {
      state.orderDetails = details;
      console.info('SET_ORDER_DETAILS_TO_STATE:', details);
    },
  },

  getters: {
    ORDER_DETAILS: state => state.orderDetails,
    ORDER_LIST: state => state.orderList,
  },
};

export default OrderModule;