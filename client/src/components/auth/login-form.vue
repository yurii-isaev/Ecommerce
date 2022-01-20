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
  </Form>
</template>

<script>
  import { Field, Form } from 'vee-validate';
  import validationLoginSchema from '@/schemas/validationLoginSchema';

  export default {
    components: { Form, Field },
  
    data() {
      return {
        schema: validationLoginSchema,
      };
    },
  };
</script>

<style scoped src="./auth-modal.vue"></style>
