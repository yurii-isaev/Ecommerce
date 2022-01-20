import axios from 'axios';

const UserModule = {
  state: {
    user: null,
    isAuthenticated: false,
  },

  mutations: {
    SET_USER_DATA(state, profile) {
      // console.warn(state);
      // console.warn(profile);
      if (profile) {
        state.user = {
          id: profile.id,
          email: profile.email,
          userName: profile.userName,
          dateCreated: profile.dateCreated,
          role: profile.role
        };
        state.isAuthenticated = true;
      } else {
        state.user = null;
        state.isAuthenticated = false;
      }
      // if (state.user) {
      //   console.warn(state.user.userName);
      // } else {
      //   console.warn("User is null");
      // }
    },
    SET_USER_LOGOUT(state) {
      state.user = null;
      state.isAuthenticated = false;
    }
  },

  actions: {
    async POST_USER_REGISTER_TO_API({commit}, formValues) {
      try {
        const userData = {
          UserName: formValues.username,
          Email: formValues.email,
          Password: formValues.password,
          AcceptTerms: !!formValues.acceptTerms, // converting a variable value to a boolean data type
        };

        const response = await axios.post('http://localhost:5000/api/auth/RegisterUser', userData, {
          // Pass the withCredentials flag to send cookies.
          withCredentials: true 
        });

        if (response.data.success) {
          // await console.log('Registration successful:', response.data);
          commit('SET_USER_DATA', response.data.dataSet);
        } else {
          // console.error('Registration failed:', response.data.message);
          commit('SET_USER_LOGOUT');
        }
      } catch (error) {
        // console.error('Error during registration:', error);
        commit('SET_USER_LOGOUT');
      }
    },

    async LOGOUT({ commit }) {
      try {
        const response = await axios.post('http://localhost:5000/api/auth/Logout', null, {
          // Pass the withCredentials flag to send cookies.
          withCredentials: true
        });

        if (response.data.success) {
          // Successfully log out, delete cookies on the client side.
          document.cookie = "user-cookies=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
          await commit('SET_USER_LOGOUT');
          await console.log('The user has successfully logged out');
        } else {
          // console.error('Error during logout:', response.data.message);
        }
      } catch (error) {
        // console.error('Error during logout:', error);
      }
    },
    

    async CHECK_AUTHENTICATION({commit}) {
      try {
        const response = await axios.get('http://localhost:5000/api/auth/CheckAuthentication', {
          withCredentials: true
        });

        if (response.data.success) {
          commit('SET_USER_DATA', response.data.dataSet.profile);
        } else {
          console.error('Authentication check failed:', response.data.message);
          commit('SET_USER_LOGOUT');
        }
      } catch (error) {
        // console.error('Error during authentication check:', error);
        // commit('LOGOUT');
      }
    }
  },

  getters: {
    USER_STATE: state => state.user,
    IS_AUTHENTICATED: state => state.isAuthenticated,
  }
}

export default UserModule;