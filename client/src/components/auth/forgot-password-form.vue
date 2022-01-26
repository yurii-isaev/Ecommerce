<template>
  <Form v-if="!emailSent" @submit="onEmailSubmit" :validation-schema="schema" v-slot="{ errors }">
    <header>Forgot Password</header>
    <div class="form-row">
      <div class="form-group col">
        <label>Email</label>
        <Field class="form-control" name="email" type="email" :class="{ 'is-invalid': errors.email }"/>
        <div class="invalid-feedback">{{ errors.email }}</div>
      </div>
    </div>
    <div class="form-group">
      <button type="submit" class="btn btn-primary">Submit</button>
    </div>
  </Form>
  <div v-else>
    <p style="color:green;font-weight:bold;font-size:150%">{{ message }}</p>
  </div>
  <br> <br>
  <div>
    <span>Return to login
      <a href="#" @click="emitLogin">Login</a>
    </span>
  </div>
</template>

<script>
  import { Field, Form } from 'vee-validate';
  import { mapActions, mapGetters } from 'vuex';
  import forgotPasswordSchema from "@/validation/forgotPasswordSchema";

  export default {
    components: { Form, Field },
    emits: ['login', 'forgot-password'], // Explicitly declaring a custom events
    
    data() {
      return {
        emailSent: false,
        message: '',
         schema: forgotPasswordSchema,
      };
    },
  
    computed: {...mapGetters(['GET_AUTH_RESPONSE'])},
  
    methods: {
      ...mapActions(['SEND_FORGOT_PASSWORD_EMAIL']),
    
      emitLogin(event) {
        event.preventDefault();
        this.$emit('login');
      },
    
      async onEmailSubmit(values) {
        try {
          await this.SEND_FORGOT_PASSWORD_EMAIL(values);
          this.emailSent = true;
          this.message = this.GET_AUTH_RESPONSE;
        } catch (error) {
          console.error('An error occurred:', error);
        }
      }
    }
  }
</script>

<style scoped src='./auth-modal.vue'/>

