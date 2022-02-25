<template>
  <section>
    <div class="container">
      <Form @submit="proceedToPayment" :validation-schema="schema" v-slot="{ errors, submitCount }">
        <div class="row">
          <div class="col">
            <h3 class="title">Order Address</h3>
            
            <div class="form-row">
              <div class="form-group col">
                <label>Full name:</label>
                <Field class="form-control" name="fullName" type="text" placeholder="Full name"
                       :class="{'is-invalid': errors.fullName}"
                       v-model="formData.fullName"/>
                <div class="invalid-feedback">{{ errors.fullName }}</div>
              </div>
            </div>
            
            <div class="form-row">
              <div class="form-group col">
                <label>Address:</label>
                <Field class="form-control" name="address" type="text" placeholder="street - house - apartment"
                       :class="{'is-invalid': errors.address}"
                       v-model="formData.address"/>
                <div class="invalid-feedback">{{ errors.address }}</div>
              </div>
            </div>
            
            <div class="form-row">
              <div class="form-group col">
                <label>City:</label>
                <Field class="form-control" name="city" type="text" placeholder="City"
                       :class="{'is-invalid': errors.city}"
                       v-model="formData.city"/>
                <div class="invalid-feedback">{{ errors.city }}</div>
              </div>
            </div>
            
            <div class="flex">
              <div class="form-row">
                <div class="form-group col">
                  <label>State:</label>
                  <Field class="form-control" name="state" type="text" placeholder="State"
                         :class="{'is-invalid': errors.state}"
                         v-model="formData.state"/>
                  <div class="invalid-feedback">{{ errors.state }}</div>
                </div>
              </div>
              
              <div class="form-group col">
                <label>Zip code:</label>
                <Field class="form-control" name="zipCode" type="text" placeholder="123 456"
                       :class="{'is-invalid': errors.zipCode}"
                       v-model="formData.zipCode"/>
                <div class="invalid-feedback">{{ errors.zipCode }}</div>
              </div>
            </div>
  
            <div class="form-group form-check">
              <div class="form-group col">
                <Field class="form-check-input" id="consentPrivateData"
                       name="consentPrivateData"
                       type="checkbox" value="true"
                       :class="{'is-invalid': errors.consentPrivateData && submitCount > 0}"
                />
                <label for="consentPrivateData" class="form-check-label" style="margin-left:10px">
                  Consent to the processing of private data
                </label>
                <div class="invalid-feedback">{{ errors.consentPrivateData }}</div>
              </div>
            </div>
          
          </div>
        </div>
        <input type="submit" value="Proceed to payment" class="btn btn-shadow_green">
      </Form>
    </div>
  </section>
</template>

<script>
  import DeliveryFormSchema from '@/validation/deliveryFormSchema';
  import { Field, Form } from 'vee-validate';

  export default {

    components: { Form, Field }, 
    
    data() {
      return {
        schema: DeliveryFormSchema,
        formData: {
          fullName: '',
          address: '',
          city: '',
          state: '',
          zipCode: '',
          consentPrivateData: false
        }
      }
    },
  
    methods: {
      async proceedToPayment() {
        this.formData.consentPrivateData = true;
        // console.warn("[ORDER-DELIVER] order: ",  this.formData);
        await this.$store.dispatch('SAVE_ORDER_DETAILS', this.formData);
        this.$router.push({name: 'order-payment'});
      }
    }
  };
</script>

<style lang="scss" scoped>

  .form-control {
    border: 1.5px solid #9B9B9B;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
    transition: border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out;
  }
  .form-control.is-invalid {
    border-color: #cb3b3b; 
    box-shadow: 0 2px 4px rgba(255, 0, 0, 0.1); 
  }  
  
  .form-group {
    flex-direction: column;
    align-items: flex-start;
    margin-top: 1rem;
    text-align: left;
  }  
  .form-group label {
    display: inline-block;
    margin-bottom: 0.5rem;
    font-weight: bold;
  }  
  .form-group span, .form-group .invalid-feedback {
    text-align: left;
  }

  span {
    font-weight: bold;
  }  
  
  .btn-shadow_green {
    background-color: #85a767;
    box-shadow: 0 .1rem .3rem rgba(0, 0, 0, 3);
    padding: 10px;
    margin-top: 10px;
    right: $padding*3;
    bottom: $padding*2;
    color: white;
  }  
  .btn:hover {
    background-color: #5f8141;
  }
  
  .container {
    display: flex;
    justify-content: center;
    align-items: center;
    padding: 20px; 
    //min-height: 100vh;
    background: white;
  }  
  .container form {
    padding: 20px;
    width: 400px;
    background: #fff; 
    //box-shadow: 0 5px 10px rgba(0, 0, 0, 1);
  }  
  .container form .row {
    display: flex;
    flex-wrap: wrap;
    gap: 15px;
  }
  .container form .row .col {
    flex: 1 1 250px;
  }  
  .container form .row .col .title {
    font-size: 20px;
    color: #333;
    padding-bottom: 5px;
    text-transform: uppercase;
  }  
  .container form .row .col .inputBox {
    margin: 15px 0;
  }  
  .container form .row .col .inputBox span {
    margin-bottom: 10px;
    display: block;
  }  
  .container form .row .col .inputBox input {
    width: 60%;
    border: 1px solid #796e6e;
    border-radius: 6px;
    padding: 10px 15px;
    font-size: 15px;
    text-transform: none;
    box-shadow: 0 0 7px rgba(0, 0, 0, 0.1);
  }  
  .container form .row .col .inputBox input:focus {
    border: 1px solid #000; 
    //box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);
  }
  .container form .row .col .flex {
    display: inline-flex;
    gap: 15px;
    margin: 10px;
  }  
  .container form .row .col .flex .inputBox {
    margin-top: 5px;
  }  
  .container form .row .col .inputBox img {
    height: 34px;
    margin-top: 5px;
    filter: drop-shadow(0 0 1px #000);
  }  
  .container form .submit-btn {
    width: 100%;
    padding: 12px;
    font-size: 17px;
    background: var(--green3);
    color: #fff;
    margin-top: 5px;
    cursor: pointer;
  }  
  .container form .submit-btn:hover {
    background: var(--green2);
  }
</style>