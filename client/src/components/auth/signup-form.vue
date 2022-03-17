<template>
  <Form @submit="onSignupSubmit" :validation-schema="schema" v-slot="{ errors }">
    <div class="form-row">
      <div class="form-group col">
        <label>Usename</label>
        <Field class="form-control"
               name="username" type="text"
               :class="{ 'is-invalid': errors.username }"
        />
        <div class="invalid-feedback">{{ errors.username }}</div>
      </div>
    </div>
    <div class="form-row">
      <div class="form-group col">
        <label>Email</label>
        <Field class="form-control"
               name="email" type="email"
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
    <div class="form-row">
      <div class="form-group col">
        <label>Confirm Password</label>
        <Field class="form-control"
               autocomplete="on"
               name="confirmPassword" type="password"
               :class="{ 'is-invalid': errors.confirmPassword }"
        />
        <div class="invalid-feedback">{{ errors.confirmPassword }}</div>
      </div>
    </div>
    <div class="form-group form-check">
      <Field class="form-check-input" id="acceptTerms"
             name="acceptTerms" type="checkbox"
             value="true"
             :class="{ 'is-invalid': errors.acceptTerms }"
      />
      <label for="acceptTerms" class="form-check-label">Accept Terms & Conditions</label>
      <div class="invalid-feedback">{{ errors.acceptTerms }}</div>
    </div>
    <div class="form-group">
      <button type="submit" class="btn btn-primary">Register</button>
    </div>
  </Form>
  <div class="signup">
    <span class="signup">Already have an account?
      <a href="#" @click="emitLogin">Login</a>
    </span>
  </div>
</template>

<script>
  import { Field, Form } from 'vee-validate';
  import { mapGetters } from 'vuex';
  import RegistrationSchema from '@/validation/registrationSchema';

  export default {
    components: { Form, Field },
    emits: ['auth', 'login'],
  
    data() {
      return {
        loggedIn: false,
        username: '',
        schema: RegistrationSchema,
      };
    }, 
    
    computed: { ...mapGetters(['USER_STATE', 'IS_AUTHENTICATED']) }, 
    
    methods: {
      emitLogin(event) {
        event.preventDefault();
        this.$emit('login');
      },
      
      async onSignupSubmit(formData) {
        try {
  
          // const formData = {
          //   email: this.email,
          //   password: this.password
          // };
          
          const credentialData = {
            userName: formData.username,
            email: formData.email,
            password: formData.password,
            acceptTerms: !!formData.acceptTerms, // converting a variable value to a boolean data type
          };
  
          await this.$store.dispatch('POST_USER_REGISTER_TO_API', credentialData);
          
          
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
