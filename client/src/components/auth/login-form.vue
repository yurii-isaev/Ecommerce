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
  import { mapActions, mapGetters } from 'vuex';
  import validationLoginSchema from '@/schemas/validationLoginSchema';

  export default {
    components: { Form, Field },
    emits: ['auth'],
  
    data() {
      return {
        loggedIn: false,
        username: '',
        schema: validationLoginSchema,
      };
    },
  
    computed: { ...mapGetters(['USER_STATE', 'IS_AUTHENTICATED']) },
  
    methods: {
      ...mapActions(['POST_USER_LOGIN_TO_API' ]),
    
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

<style scoped src="./auth-modal.vue"></style>
