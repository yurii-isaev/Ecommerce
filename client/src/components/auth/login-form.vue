<template>
  <Form @submit="onLoginSubmit" :validation-schema="schema" v-slot="{ errors }">
    <div class="form-row">
      <div class="form-group col">
        <label>Email</label>
        <Field class="form-control"
               name="email" type="text"
               :class="{ 'is-invalid': errors.email }"
        />
        <div class="invalid-feedback">{{ errors.email }}</div>
      </div>
    </div>
    <div class="form-row">
      <div class="form-group col">
        <label>Password</label>
        <Field class="form-control"
               name="password" type="password"
               autocomplete="on"
               :class="{ 'is-invalid': errors.password }"
        />
        <div class="invalid-feedback">{{ errors.password }}</div>
      </div>
    </div>
    <div class="form-group">
      <button type="submit" class="btn btn-primary">Login</button>
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
</template>

<script>
  import { Field, Form } from 'vee-validate';
  import { mapActions, mapGetters } from 'vuex';
  import LoginSchema from "@/validation/loginSchema";

  export default {
    components: { Form, Field},
    emits: ['auth', 'forgot-password', 'signup'],
  
    data() {
      return {
        loggedIn: false,
        username: '',
        schema: LoginSchema,
      };
    },
  
    computed: { ...mapGetters(['USER_STATE', 'IS_AUTHENTICATED']) },
  
    methods: {
      ...mapActions(['POST_USER_LOGIN_TO_API' ]),
  
      emitForgotPassword(event) {
        event.preventDefault();
        this.$emit('forgot-password'); // Излучаем событие для родительского компонента
      },
  
      emitSignup(event) {
        event.preventDefault();
        this.$emit('signup'); 
      },
    
      async onLoginSubmit(formValues) {
        try {
          await this.POST_USER_LOGIN_TO_API(formValues);
          this.loggedIn = this.IS_AUTHENTICATED;
          if (this.loggedIn) {
            this.username = this.USER_STATE.userName;
            const authData = {
              username: this.username,
              loggedIn: this.loggedIn
            };
            this.$emit('auth', authData);
          }
        } catch (error) {
          console.error('An error occurred:', error);
        }
      }
    }
  }
</script>

<style scoped src='./auth-modal.vue'/>
