<template>
  <!-- Header -->
  <li class="menu-item">
    <div v-if="!loggedIn" data-bs-toggle="modal" data-bs-target="#authModal">
      <img src="@/assets/graphics/vector/profile.svg" alt="profile"/>
    </div>
    <div v-else>
      <a class="nav-link text-dark" data-bs-toggle="modal" data-bs-target="#logoutModal">
        <div class="profile-info">
          <img src="@/assets/graphics/vector/profile.svg" alt="profile"/>
          <span>Hello, <br> <strong>{{ username }}</strong></span>
        </div>
      </a>
    </div>
  </li>
  
  <!-- Login/Signup Form Modal -->
  <div class="modal fade" id="authModal" tabindex="-1" aria-labelledby="authModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="authModalLabel">Log in to the system</h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <!-- <div class="modal-body">-->
        <div class="container">
          <!-- <input type="checkbox" id="check">-->
          
          <!-- Login Form Component -->
          <div v-if="currentForm === 'login'" class="login form">
            <div class="login form">
              <header>Login</header>
              <LoginForm
                  @auth="handleAuthentication"
                  @forgot-password="switchToForgotPasswordForm"
                  @signup="switchToSignupForm"/>
            </div>
          </div>
          <!-- Signup Form Component -->
          <div v-if="currentForm === 'signup'" class="signup form">
            <div class="signup form">
              <header>Signup</header>
              <SignupForm
                  @auth="handleAuthentication"
                  @login="switchToLoginForm"/>
              </div>
          </div>
          <!-- Signup Form Component -->
          <div v-if="currentForm === 'forgotPassword'" class="forgot-password form">
            <div class="forgotPassword form">
              <ForgotPasswordForm @login="switchToLoginForm"/>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  
  <!-- Exit Confirmation Modal -->
  <div class="modal fade" id="logoutModal" tabindex="-1" aria-labelledby="logoutModalLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="logoutModalLabel">Confirm Logout</h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">Are you sure you want to log out?</div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
          <button type="button" class="btn btn-primary" data-bs-dismiss="modal" @click="logout">Logout</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  import { mapGetters } from 'vuex';
  import LoginForm from '@/components/auth/login-form';
  import SignupForm from '@/components/auth/signup-form';
  import ForgotPasswordForm from '@/components/auth/forgot-password-form';

  export default {
    components: { LoginForm, SignupForm, ForgotPasswordForm },
  
    data() {
      return {
        loggedIn: false,
        username: '',
        currentForm: 'login',
      };
    },
    
    computed: {
      ...mapGetters(['USER_STATE', 'IS_AUTHENTICATED'])
    },
    
    methods: {
      switchToForgotPasswordForm() {
        this.currentForm = 'forgotPassword';
      },
  
      switchToSignupForm() {
        this.currentForm = 'signup';
      },
  
      switchToLoginForm() {
        this.currentForm = 'login';
      },
  
      handleAuthentication(authData) {
        this.username = authData.username;
        this.loggedIn = authData.loggedIn;
      },
      
      async logout() {
        await this.$store.dispatch('LOGOUT');
        this.loggedIn = this.IS_AUTHENTICATED;
        this.username = '';
      }
    }, 
    
    async mounted() {
      await this.$store.dispatch('CHECK_AUTHENTICATION');
      this.loggedIn = this.IS_AUTHENTICATED;
      if (this.loggedIn && this.USER_STATE && this.USER_STATE.userName) {
        this.username = this.USER_STATE.userName;
      }
    }
  };
</script>

<style scoped>
  .profile-info {
    display: flex;
    align-items: center;
  }

  .profile-info img {
    margin-right: 8px; /* Отступ между иконкой и текстом */
  }

  .form-group {
    margin-top: 1rem;
    text-align: left;
  }

  .form-group label {
    display: inline-block;
    margin-bottom: 0.5rem;
    font-weight: bold;
  }  
  
  .btn {
    margin-bottom: 10px;
  }

  .container {
    top: 50%;
    left: 50%;
    max-width: 600px;
    width: 100%;
    background: #fff;
    border-radius: 7px;
    box-shadow: 0 5px 10px rgba(0, 0, 0, 0.3);
    padding-right: 0;
    padding-left: 0;
    /*position: absolute;*/
    /*transform: translate(-50%,-50%);*/
  }

  .container .registration {
    display: none;
  }

  #check:checked ~ .registration {
    display: block;
  }

  #check:checked ~ .login {
    display: none;
  }

  #check {
    display: none;
  }

  .container .form {
    padding: 1rem;
  }

  .form header {
    font-size: 2rem;
    font-weight: 500;
    text-align: center;
    margin-bottom: 1.5rem;
  }

  .form input:focus {
    box-shadow: 0 1px 0 rgba(0, 0, 0, 0.2);
  }

  .form a {
    font-size: 16px;
    color: #3173cb;
    text-decoration: none;
  }

  .form a:hover {
    text-decoration: underline;
  }

  .form input.button {
    color: #fff;
    background: #009579;
    font-size: 1.2rem;
    font-weight: 500;
    letter-spacing: 1px;
    margin-top: 1.7rem;
    cursor: pointer;
    transition: 0.4s;
  }

  .form input.button:hover {
    background: #006653;
  }

  span {
    font-size: 16px;
    text-align: center;
  }

  span label {
    color: #3173cb;
    cursor: pointer;
  }

  span label:hover {
    text-decoration: underline;
  }
</style>