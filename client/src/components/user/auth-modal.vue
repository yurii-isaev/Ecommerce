<template>
  <!--	header-->
  <li class="menu-item">
    <button v-if="!loggedIn" data-bs-toggle="modal" data-bs-target="#authModal">
      <img src="@/assets/graphics/vector/profile.svg" alt="profile"/>
    </button>
    <div v-else>
      <a class="nav-link text-dark" @click="logout">
        Hello, <strong>{{ username }}</strong>
      </a>
    </div>
  </li>
  
  <!-- Login Form Modal -->
  <!--	<div class="modal fade" id="authModal" tabindex="-1" aria-labelledby="authModalLabel" aria-hidden="true">-->
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
            <Form @submit="onSubmit" :validation-schema="schemaLogin" v-slot="{ errors }">
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
            
            <div class="signup">
							<span class="signup">Don't have an account?
								<label for="check">Signup</label>
							</span>
            </div>
          </div>
          <div class="registration form">
            <header>Signup</header>
            <Form @submit="onSubmit" :validation-schema="schemaRegistration" v-slot="{ errors }">
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
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  import { Field, Form } from 'vee-validate';
  import { mapActions, mapGetters } from 'vuex';
  import validationLoginSchema from '@/schemas/validationLoginSchema';
  import validationRegistrationSchema from '@/schemas/validationRegistrationSchema';

  export default {
  
    components: { Form, Field },
  
    data() {
      return {
        loggedIn: false,
        username: '',
        schemaLogin: validationLoginSchema,
        schemaRegistration: validationRegistrationSchema
      };
    },
  
    computed: {
      ...mapGetters(['USER_STATE']), 
      
      currentUser() {
        return this.USER_STATE;
      },
    }, 
    
    methods: {
      ...mapActions(['POST_USER_REGISTER_TO_API']), 
      
      async onSubmit(formValues) {
        try {
          await this.POST_USER_REGISTER_TO_API(formValues);
        } catch (error) {
          console.error('An error occurred while retrieving data from the API:', error);
        }
      }, 
      
      async checkAuthentication() {
        try {
          if (this.currentUser.username != null) {
            this.loggedIn = true;
            this.username = this.currentUser.username;
          }
        } catch (error) {
          console.error('Error when verifying user authorization:', error);
        }
      }
    }, 
    
    mounted() {
      this.checkAuthentication();
      console.log(this.currentUser.username)
      console.log(this.loggedIn)
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

.error-message {
  color: red;
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