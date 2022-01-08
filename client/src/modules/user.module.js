import axios from 'axios';

const UserModule = {
  state: {
    user: {
      username: null // -- ???? 
    }
  },

  mutations: {
    SET_USER_DATA_TO_STATE: (state, {user, token}) => {
      state.user = user;
      state.token = token;
    }
  },

  actions: {

    async POST_USER_REGISTER_TO_API({commit}, formValues) {
      const userData = {
        UserName: formValues.username,
        Email: formValues.email,
        Password: formValues.password,
        AcceptTerms: !!formValues.acceptTerms,
      };

      axios.post('http://localhost:5000/api/auth/RegisterUser', userData).then(response => {
        console.log('Server response:', response.data);
        commit('SET_USER_DATA_TO_STATE', {
          user: response.data.user,
          token: response.data.token
        });
      }).catch(error => {
        console.error('Error executing a query:', error);
      });
    },
  },

  getters: {
    USER_STATE: state => state.user,
  },

  install: (app) => {
    app.provide('user', UserModule.state);
  }
};

export default UserModule;