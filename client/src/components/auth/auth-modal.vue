<template>
  <!--	header-->
  <li class="menu-item">
    <button v-if="!loggedIn" data-bs-toggle="modal" data-bs-target="#authModal">
      <img src="@/assets/graphics/vector/profile.svg" alt="profile"/>
      <br> <br>
      <strong> Sign In </strong>
    </button>
    <div v-else>
      <a class="nav-link text-dark" data-bs-toggle="modal" data-bs-target="#logoutModal">
        <img src="@/assets/graphics/vector/profile.svg" alt="profile"/>
        <br> <br>
        Hello, <strong>{{ username }}</strong>
      </a>
    </div>
  </li>
  
  <!-- Login/Signup Form Modal -->
  <div class="modal fade" id="authModal" tabindex="-1" aria-labelledby="authModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="authModalLabel">Login/Signup</h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <!-- <div class="modal-body">-->
        <div class="container">
          <input type="checkbox" id="check">
          <div class="login form">
            <header>Login</header>
            <!-- ------------------------------------------>
            <!-- Login Form Component -->
            <LoginForm/>
            <!-- ------------------------------------------>
            <div class="signup">
              <span class="signup">Don't have an account?
                <label for="check">Signup</label>
              </span>
            </div>
          </div>
          <div class="registration form">
            <header>Signup</header>
            <!-- ------------------------------------------>
            <!-- Signup Form Component -->
            <SignupForm/>
            <!-- ------------------------------------------>
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
  import { mapActions, mapGetters } from 'vuex';
  import LoginForm from '@/components/auth/login-form';
  import SignupForm from '@/components/auth/signup-form';

  export default {
    components: { LoginForm, SignupForm },
    computed: { ...mapGetters(['USER_STATE', 'IS_AUTHENTICATED']) }, 
    
    methods: {
      ...mapActions(['LOGOUT', 'CHECK_AUTHENTICATION']), 
      
      async logout() {
        await this.LOGOUT();
        this.loggedIn = this.IS_AUTHENTICATED;
        this.username = '';
      }
    }, 
    
    async mounted() {
      await this.CHECK_AUTHENTICATION();
      this.loggedIn = this.IS_AUTHENTICATED;
      if (this.loggedIn && this.USER_STATE && this.USER_STATE.userName) {
        this.username = this.USER_STATE.userName;
      }
    }
  };
</script>

<style scoped>
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
    color: #009579;
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

  .signup {
    font-size: 17px;
    text-align: center;
  }

  .signup label {
    color: #2ea03c;
    cursor: pointer;
  }

  .signup label:hover {
    text-decoration: underline;
  }
</style>