import axios from 'axios';

const ProductModule = {
  state: {
    products: [],
    searchValue: '',
    currentPage: 1,
    totalPages: 0,
  },

  mutations: {
    SET_SEARCH_VALUE: (state, value) => {
      state.searchValue = value;
    },

    SET_PRODUCTS_TO_STATE: (state, {items, totalPages}) => {
      state.products = items;
      state.totalPages = totalPages;
    },

    SET_CURRENT_PAGE: (state, pageNumber) => {
      state.currentPage = pageNumber;
    }
  },

  actions: {
    SEARCH_VALUE_TO_STORE({commit}, value) {
      commit('SET_SEARCH_VALUE', value)
    },

    async GET_PRODUCTS_FROM_API({commit, state}, {pageNumber = state.currentPage, pageSize = 4}) {
      try {
        const response = await axios.get(
            `http://localhost:5000/api/store/GetAllProducts?pageNumber=${pageNumber}&pageSize=${pageSize}`
        );
        console.log('response', response);
        if (response.status === 200 && response.data.code === 200) {
          const { items, totalPages } = response.data.set;
          commit('SET_PRODUCTS_TO_STATE', { items, totalPages });
          commit('SET_CURRENT_PAGE', pageNumber);
        } else {
          console.warn('GET_PRODUCTS_FROM_API response:', response.data.message);
        }
      } catch (error) {
        console.error('Error during GET_PRODUCTS_FROM_API:', error);
      }
    }
  },

  getters: {
    SEARCH_VALUE: state => state.searchValue,
    PRODUCTS_STATE: state => state.products,
    CURRENT_PAGE: state => state.currentPage,
    TOTAL_PAGES: state => state.totalPages,
  },
};

export default ProductModule;
