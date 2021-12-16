<template>
   <header class="header">
      <router-link class="router-link" :to="{ name: 'mainPage' }">
         <div class="text-section">
            <img src="@/assets/logo/logo.png" alt="Logo" style="width:3.75rem"/>
            <div class="text-content">
               <h2 class="title">Vue cakes</h2>
               <p class="description">Online store for ordering cakes</p>
            </div>
         </div>
      </router-link>

      <div class="input-box">
         <i class="uil uil-search"></i>
         <input type="text" v-model="searchValue">
         <button class="search-btn" @click="search(searchValue)">
            <i class="material-icons">search</i>
         </button>
         <button class="search-btn_clear" @click="clearSearchField">
            <i class="material-icons">cancel</i>
         </button>
      </div>

      <ul class="header-menu">
         <li class="menu-item">
            <button @click="switchLanguage('en')">
               <img src="@/assets/graphics/vector/usa.svg" alt="United States Flag"/>
            </button>
         </li>
         <li class="menu-item">
            <router-link class="router-link" :to="{ name: 'cart' }">
               <img src="@/assets/graphics/vector/cart.svg" alt="cart"/>
               <span class="cart-quantity-icon"> {{ cart.length }} </span>
            </router-link>
         </li>
         <li class="menu-item">
            <router-link class="router-link" :to="{ name: 'favorits-list' }">
               <img src="@/assets/graphics/vector/heart.svg" alt="favorits"/>
               <span class="cart-quantity-icon"> {{ favorits.length }} </span>
            </router-link>
         </li>
         <li class="menu-item">
            <button type="button" class="btn" data-bs-toggle="modal" data-bs-target="#authModal">
               <img src="@/assets/graphics/vector/profile.svg" alt="profile"/>
            </button>
         </li>
      </ul>
      
      <!-- Login Form Modal -->
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
                     
                     <Form @submit="onSubmit" :validation-schema="schema" v-slot="{ errors }">
                        <div class="form-row">
                           <div class="form-group col">
                              <label>Email</label>
                              <Field name="email" type="text" class="form-control" :class="{ 'is-invalid': errors.email }" />
                              <div class="invalid-feedback">{{ errors.email }}</div>
                           </div>
                        </div>
                        <div class="form-row">
                           <div class="form-group col">
                              <label>Password</label>
                              <Field name="password" type="password" class="form-control" :class="{ 'is-invalid': errors.password }" />
                              <div class="invalid-feedback">{{ errors.password }}</div>
                           </div>
                        </div>
                        <div class="form-group">
                           <button type="submit" class="btn btn-primary" style="margin:10px">Register</button>
                           <button type="reset" class="btn btn-secondary">Reset</button>
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
                     <form action="#">
                        <input class="form-control"
                               v-model="usename"
                               type="text"
                               placeholder="usename"
                               autocomplete="usename"
                        >
                        <input class="form-control"
                               v-model="email"
                               type="email"
                               placeholder="email"
                               autocomplete="usename"
                        >
                        <input class="form-control"
                               v-model="current_password"
                               type="password"
                               autocomplete="current-password"
                               placeholder="password"
                        >
                        <input class="form-control"
                               v-model="confirm_password"
                               type="password"
                               autocomplete="current-password"
                               placeholder="confirm password"
                        >
                        <input type="button" class="button" value="Signup">
                     </form>
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
   </header>
</template>

<script>
  import { Form, Field } from 'vee-validate';
  import { mapActions, mapGetters } from 'vuex';
  import * as Yup from 'yup';
  
  export default {
     name: "layout-header", 
     
     components: {
        Form, 
        Field,
     }, 
     
     data() {
        const schema = Yup.object().shape({
           title: Yup.string().required('Title is required'), 
           firstName: Yup.string().required('First Name is required'), 
           lastName: Yup.string().required('Last name is required'),
           
           dob: Yup.string()
              .required('Date of Birth is required')
              .matches(/^\d{4}-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[01])$/, 'Date of Birth must be a valid date in the format YYYY-MM-DD'),
           
           email: Yup.string()
              .required('Email is required')
              .email('Email is invalid'),
           
           password: Yup.string()
              .min(6, 'Password must be at least 6 characters')
              .required('Password is required'),
           
           confirmPassword: Yup.string()
              .oneOf([Yup.ref('password'), null], 'Passwords must match')
              .required('Confirm Password is required'),
           
           acceptTerms: Yup.string().required('Accept Ts & Cs is required')
        });
        return {
           schema, 
           searchValue: '', 
           login_password: '', 
           usename: '', 
           email: '', 
           current_password: '', 
           confirm_password: '', 
           attemptSubmit: false,
        }
     }, 
     
     computed: {
        ...mapGetters(['CART_STATE_VALUE', 'FAVORITS_STATE_VALUE']), 
        
        cart() {
           return this.CART_STATE_VALUE;
        }, 
        
        favorits() {
           return this.FAVORITS_STATE_VALUE;
        },
     }, 
     
     methods: {
        ...mapActions(['ACTION_SEARCH_VALUE_TO_STORE']), 
        
        search(value) {
           this.ACTION_SEARCH_VALUE_TO_STORE(value);
           if (this.$route.path !== '/catalog') {
              this.$router.push('/catalog');
           }
        }, 
        
        clearSearchField() {
           this.searchValue = '';
           this.ACTION_SEARCH_VALUE_TO_STORE();
           if (this.$route.path !== '/catalog') {
              this.$router.push('/catalog');
           }
        }, 
        
        onSubmit(values) {
           alert('SUCCESS!! :-)\n\n' + JSON.stringify(values, null, 4));
        }
     }
  }
</script>

<style lang="scss" scoped>

  .form-group {
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
     box-shadow: 0 5px 10px rgba(0,0,0,0.3);
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
  
  .form input {
     height: 45px;
     width: 100%;
     padding: 0 15px;
     font-size: 17px;
     //margin-bottom: 1.3rem;
     border: 1px solid #ddd;
     border-radius: 6px;
     outline: none;
  }  
  
  .form input:focus{
     box-shadow: 0 1px 0 rgba(0,0,0,0.2);
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
     color: #009579;
     cursor: pointer;
  }  
  
  .signup label:hover {
     text-decoration: underline;
  }

  .header {
     display: flex;
     justify-content: space-between;
     border-bottom: 1px solid #CBD5E0;
     padding: 0 1.25rem;
     padding-top: 0.5rem;
     padding-bottom: 0.625rem;
  }  
  
  .text-section {
     display: flex;
     align-items: center;
  }  
  
  .title {
     font-family: 'Tangerine', serif;
     font-size: 48px;
     text-shadow: 4px 4px 4px #aaa;
  }

  .description {
     color: #718096;
  }

  .text-content {
     margin-left: 1rem;
  }

  .header-menu {
    display: flex;
     align-items: center;
     gap: 2.5rem;
  }

  .menu-item {
     display: flex;
     align-items: center;
     gap: 0.75rem;
     cursor: pointer;
  }

  .header-menu li {
     list-style: none;
  }

  .header-menu li a {
     text-decoration: none;
     color: #92533a;
     position: relative;
  }

  .input-box {
     position: relative;
     display: flex;
     align-items: center;
     height: 30px;
     max-width: 500px; 
     //width: 70%;
     background: #fff;
     margin: 20px;
     border-radius: 6px;
     box-shadow: 0 3px 6px rgba(0, 0, 0, 0.1);
     border: 2px solid #9B9B9B;
  }  
  
  .input-box i {
     margin-right: 8px;
     background: none;
     color: black;
  }  
  
  .input-box input {
     flex: 1;
     border: none;
     outline: none;
     height: 100%;
     padding: 0 8px;
     font-size: 14px;
  }  
  
  .search-btn, .search-btn_clear {
     display: flex;
     align-items: center;
     justify-content: center;
     width: 36px;
     height: 90%;
     cursor: pointer;
     background: none;
  }  
  
  .search-btn i, .search-btn_clear i {
     font-size: 20px;
     background: none;
     color: #9B9B9B;
  }  
  
  .cart-quantity-icon {
     display: flex;
     position: absolute;
     top: -25px;
     right: -15px;
     width: 25px;
     height: 25px;
     border: none;
     background: #f08804;
     color: #fff;
     border-radius: 50%;
     align-items: center;
     justify-content: center;
     font-size: 13px;
  }
</style>