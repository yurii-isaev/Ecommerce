import axios from 'axios';

const UserModule = {
  state: {
    user: null,
    isAuthenticated: false,
    message: null,
  },

  mutations: {
    SET_USER_DATA(state, profile) {
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
    },
    UPDATE_USER_DATA(state) {
      state.user = null;
      state.isAuthenticated = false;
    },
    SET_AUTH_MESSAGE(state, serverMessage) {
      state.message = serverMessage;
    }
  },

  actions: {
    async POST_USER_REGISTER_TO_API({commit}, credentialData) {
      try {
        const response = await axios.post('http://localhost:5000/api/auth/SignUp', credentialData, {
          // Pass the withCredentials flag to send cookies.
          withCredentials: true 
        });

        if (response.status == 200) {
          console.log('Registration successful');
          commit('SET_USER_DATA', response.data.set);
        } else {
          // console.error('Registration failed:', response.data.message);
          commit('UPDATE_USER_DATA');
        }
      } catch (error) {
        // console.error('Error during registration:', error);
        commit('UPDATE_USER_DATA');
      }
    },

    async POST_USER_LOGIN_TO_API({commit}, formData) {
      try {
        const response = await axios.post('http://localhost:5000/api/auth/SignIn', formData, {
          // Pass the withCredentials flag to send cookies.
          withCredentials: true
        });

        if (response.status == 200) {
          // console.log('Sign In successful');
          commit('SET_AUTH_MESSAGE', 'Sign In successful');
          commit('SET_USER_DATA', response.data.set);
        } else {
          // console.error('Registration failed:', response.data.message);
          commit('UPDATE_USER_DATA');
        }
      } catch (error) {
        // console.error('Error during registration:', error);
        commit('UPDATE_USER_DATA');
      }
    },

    async LOGOUT({commit}) {
      try {
        const response = await axios.post('http://localhost:5000/api/auth/Logout', null, {
          // Pass the withCredentials flag to send cookies.
          withCredentials: true
        });

        if (response.status == 200) {
          // Successfully log out, delete cookies on the client side.
          document.cookie = "jwt-cookies=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
          await commit('UPDATE_USER_DATA');
          await console.log('The user logged out');
        } else {
          // console.error('Error during logout:', response.data.message);
        }
      } catch (error) {
        // console.error('Error during logout:', error);
      }
    },
    
    async CHECK_AUTHENTICATION({commit}) {
      try {
        const response = await axios.get('http://localhost:5000/api/auth/GetAuthProfile', {
          withCredentials: true
        });

        if (response.status == 200) {
          commit('SET_USER_DATA', response.data.set.profile);
        } else {
          console.error('Authentication check failed:', response.data.message);
          commit('UPDATE_USER_DATA');
        }
      } catch (error) {
        // console.error('Error during authentication check:', error);
        // commit('UPDATE_USER_DATA');
      }
    },

    async SEND_FORGOT_PASSWORD_EMAIL({commit}, credential) {
      try {
        const response = await axios.post('http://localhost:5000/api/auth/ForgotPassword', credential);

        if (response.status == 200) {
          await commit('SET_AUTH_MESSAGE', response.data.message);
        } else {
          // console.error('Registration failed:', response.data.message);
        }
      } catch (error) {
        // console.error('Error during authentication check:', error);
      }
    },
    
    async SEND_PASSWORD_CHANGED({commit}, credential) {
      try {
        const response = await axios.post('http://localhost:5000/api/auth/ChangePassword', credential);

        if (response.status == 200) {
          await commit('SET_AUTH_MESSAGE', response.data.message);
        } else {
          // console.error('Registration failed:', response.data.message);
        }
      } catch (error) {
        // console.error('Error during authentication check:', error);
      }
    }
  },

  getters: {
    USER_STATE: state => state.user,
    IS_AUTHENTICATED: state => state.isAuthenticated,
    GET_AUTH_RESPONSE: state => state.message,
  }
}

export default UserModule;