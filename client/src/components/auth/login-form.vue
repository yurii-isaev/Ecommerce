<template>
  <div>
    <!-- Displaying a Spinner or Modal -->
    <div v-if="isLoading">
      <p class="authentication-message">authentication process ...</p>
      <Spinner :is-loading="isLoading"/>
    </div>
    <div v-else-if="isShowResponse">
      <p class="success-response">{{ response }}</p>
    </div>
    <!-- Login Form -->
    <div v-else>
      <header>Login</header>
      <Form @submit="onLoginSubmit" :validation-schema="schema" v-slot="{ errors }">
        <div class="form-row">
          <div class="form-group col">
            <label>Email</label>
            <Field class="form-control" name="email" type="text" v-model="email"
                   :class="{ 'is-invalid': errors.email }"/>
            <div class="invalid-feedback">{{ errors.email }}</div>
          </div>
        </div>
        <div class="form-row">
          <div class="form-group col">
            <label>Password</label>
            <Field class="form-control" name="password" type="password" v-model="password" autocomplete="on"
                   :class="{ 'is-invalid': errors.password }"/>
            <div class="invalid-feedback">{{ errors.password }}</div>
          </div>
        </div>
        <div class="form-group">
          <button type="submit" class="btn btn-primary" :class="{ 'loading': isLoading }">Login</button>
        </div>
        <br>
        <div class="nav-links">
          <div class="signup">
            <span class="signup">Don't have an account?
              <a href="#" @click="emitSignup">Signup</a>
            </span>
          </div>
          <br>
          <div class="forgot-password">
            <span class="forgot-password">Forgot password?
              <a href="#" @click="emitForgotPassword">Reset Password</a>
            </span>
          </div>
        </div>
      </Form>
    </div>
  </div>
</template>

<script>
  import { Field, Form } from 'vee-validate';
  import  {mapGetters } from 'vuex';
  import LoginSchema from '@/validation/loginSchema';
  import Spinner from '@/components/auth/auth-spinner';

  export default {
    components: {Form, Field, Spinner}, 
    emits: ['auth', 'forgot-password', 'signup'], 
    
    data() {
      return {
        response: '', 
        isLoading: false, 
        loggedIn: false, 
        username: '', 
        email: '', 
        password: '', 
        schema: LoginSchema, 
        isShowResponse: false, // Add a variable to control the display of the modal
      };
    }, 
    
    computed: {
      ...mapGetters(['USER_STATE', 'IS_AUTHENTICATED', 'GET_AUTH_RESPONSE'])
    },
    
    methods: {
      emitForgotPassword() {
        this.$emit('forgot-password');
      }, 
      
      emitSignup() {
        this.$emit('signup');
      }, 
      
      async onLoginSubmit(event) {
        try {
          if (event.prevent) {
            event.prevent();
          }
          this.isLoading = true;
          const formData = {
            email: this.email, 
            password: this.password
          };
          await this.$store.dispatch('POST_USER_LOGIN_TO_API', formData);
          this.loggedIn = this.IS_AUTHENTICATED;
          if (this.loggedIn) {
            this.username = this.USER_STATE.userName;
            const authData = {
              username: this.username, 
              loggedIn: this.loggedIn
            };
            this.$emit('auth', authData);
          }
          this.response = this.GET_AUTH_RESPONSE;
          await new Promise(resolve => setTimeout(resolve, 2000));
          this.isShowResponse = true;
        } catch (error) {
          console.error('An error occurred:', error);
        } finally {
          this.isLoading = false;
          await setTimeout(() => {
            this.isShowResponse = false;
            const backdrop = document.querySelector('.modal-backdrop');
            if (backdrop) {
              backdrop.remove();
            }
            const authModal = document.getElementById('authModal');
            if (authModal) {
              authModal.style.display = 'none';
              authModal.setAttribute('aria-hidden', 'true');
            }
            }, 3000);
        }
      }
    }
  }
</script>

<style scoped src='./auth-modal.vue'/>
