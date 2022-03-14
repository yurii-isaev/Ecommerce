<template>
  <Form v-if="!passwordChanged" @submit="onSubmit" :validation-schema="schema" v-slot="{ errors }">
    <div class="container">
      <header>Change Password Form</header>
      <div class="form-content"> 
        <div class="form-row">
          <div class="form-group col">
            <label> New Password</label>
            <Field class="form-control" name="password" type="password" autocomplete="on"
                   :class="{ 'is-invalid': errors.password }"/>
            <div class="invalid-feedback">{{ errors.password }}</div>
          </div>
        </div>
        <div class="form-row">
          <div class="form-group col">
            <label>Confirm Password</label>
            <Field class="form-control" name="confirmPassword" type="password" autocomplete="on"
                   :class="{ 'is-invalid': errors.confirmPassword }"/>
            <div class="invalid-feedback">{{ errors.confirmPassword }}</div>
          </div>
        </div>
      </div>
      <div class="form-group">
        <button type="submit" class="btn btn-primary">Save сhanges</button>
      </div>
    </div>
  </Form>
  <div v-else>
    <p class="success-message">{{ message }}</p>
  </div>
  <br> <br>
</template>

<script>
  import { Field, Form } from 'vee-validate';
  import { mapActions, mapGetters } from 'vuex';
  import changePasswordSchema from '@/validation/changePasswordSchema';

  export default {
    components: { Form, Field }, 
    
    data() {
      return {
        passwordChanged: false, 
        message: '', 
        schema: changePasswordSchema,
      }
    }, 
    
    computed: {...mapGetters(['GET_AUTH_RESPONSE'])}, 
    
    methods: {
      ...mapActions(['SEND_PASSWORD_CHANGED']), 
      
      async onSubmit(values) {
        try {
          await this.SEND_PASSWORD_CHANGED(values);
          this.passwordChanged = true;
          this.message = this.GET_AUTH_RESPONSE;
        } catch (error) {
          console.error('An error occurred:', error);
        }
      }
    }
  }
</script>

<style scoped>
  .success-message {
    color: green;
    font-weight: bold;
    font-size: 150%;
  }
  
  Form {
    margin: 20px;
  }
  
  .container {
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    max-width: 400px;
    margin: 0 auto;
    padding: 20px;
    background-color: #f9f9f9;
    border-radius: 8px;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
    height: 100%;
  }  
  
  header {
    font-size: 24px;
    margin-bottom: 20px;
    text-align: center;
  }  
  
  .form-row {
    margin-bottom: 20px;
  }  
  
  .form-group {
    display: flex;
    flex-direction: column;
  }  
  
  .form-control {
    padding: 10px;
    border: 1px solid #ccc;
    border-radius: 4px;
    margin-top: 5px;
  }  
  
  .form-control:focus {
    border-color: #007bff;
    box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075), 0 0 8px rgba(0, 123, 255, 0.6);
  }  
  
  .invalid-feedback {
    color: #d9534f;
    font-size: 14px;
    margin-top: 5px;
  }  
  
  .btn-primary {
    padding: 10px 15px;
    background-color: #007bff;
    border: none;
    border-radius: 4px;
    color: white;
    cursor: pointer;
    transition: background-color 0.2s;
  }  
  
  .btn-primary:hover {
    background-color: #0056b3;
  }  
  
  header {
    font-size: 24px;
    margin-bottom: 20px;
    text-align: center;
  }  
  
  label {
    font-weight: bold;
  }  
  
  label, .form-control, .invalid-feedback {
    margin-top: 10px;
  }  
  
  .btn-primary {
    margin-top: 20px;
  }
</style>