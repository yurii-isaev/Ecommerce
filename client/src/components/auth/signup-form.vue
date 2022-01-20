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
      <label for="check">Login</label>
    </span>
  </div>
</template>

<script>
  import { Field, Form } from 'vee-validate';
  import { mapActions, mapGetters } from 'vuex';
  import validationRegistrationSchema from '@/schemas/validationRegistrationSchema';

  export default {
    components: { Form, Field },
  
    data() {
      return {
        loggedIn: false,
        username: '',
        schema: validationRegistrationSchema,
      };
    }, 
    
    computed: { ...mapGetters(['USER_STATE', 'IS_AUTHENTICATED']) }, 
    
    methods: {
      ...mapActions(['POST_USER_REGISTER_TO_API', 'LOGOUT', 'CHECK_AUTHENTICATION']),
      
      async onSignupSubmit(formValues) {
        try {
          await this.POST_USER_REGISTER_TO_API(formValues);
          await this.CHECK_AUTHENTICATION();
          this.loggedIn = this.IS_AUTHENTICATED;
          if (this.loggedIn) {
            this.username = this.USER_STATE.userName;
          }
        } catch (error) {
          console.error('An error occurred:', error);
        }
      }, 
      
      async logout() {
        await this.LOGOUT();
        // await this.CHECK_AUTHENTICATION();
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
  }
</script>

<style scoped src="./auth-modal.vue"></style>
