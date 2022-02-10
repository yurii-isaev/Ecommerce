<template>
  <section>
    <div class="container">
      <Form @submit="proceedToCheckout" :validation-schema="schema" v-slot="{ errors }">
        <div class="row">
          <div class="col">
            <h3 class="title">Payment</h3>
            
            <div class="inputBox">
              <span>Cards accepted:</span>
              <img src="@/assets/logo/payment-cards.png" alt="">
            </div>
  
            <div class="form-row">
              <div class="form-group col">
                <label>Card Holder:</label>
                <Field class="form-control" name="nameOnCard" type="text" placeholder="Name On Card"
                       :class="{'is-invalid': errors.nameOnCard}"/>
                <div class="invalid-feedback">{{ errors.nameOnCard }}</div>
              </div>
            </div>
  
            <div class="form-row">
              <div class="form-group col">
                <label>Card Number:</label>
                <Field class="form-control" name="numberOnCard" type="text" placeholder="1111-2222-3333-4444"
                       :class="{'is-invalid': errors.numberOnCard}"/>
                <div class="invalid-feedback">{{ errors.numberOnCard }}</div>
              </div>
            </div>
  
            <div class="form-row">
              <div class="form-group col">
                <label>Exp month:</label>
                <Field class="form-control" name="expMonth" type="text" placeholder="month"
                       :class="{'is-invalid': errors.numberOnCard}"/>
                <div class="invalid-feedback">{{ errors.numberOnCard }}</div>
              </div>
            </div>
  
            <div class="flex">
              <div class="form-row">
                <div class="form-group col">
                  <label>Exp year:</label>
                  <Field class="form-control" name="expYear" type="number" placeholder="year"
                         :class="{'is-invalid': errors.expYear}"/>
                  <div class="invalid-feedback">{{ errors.expYear }}</div>
                </div>
              </div>
    
              <div class="form-row">
                <div class="form-group col">
                  <label>CVV:</label>
                  <Field class="form-control" name="cvv" type="text" placeholder="cvv"
                         :class="{'is-invalid': errors.cvv}"/>
                  <div class="invalid-feedback">{{ errors.cvv }}</div>
                </div>
              </div>
            </div>
            
          </div>
        </div>
        <input type="submit" value="Finish order" class="btn btn-shadow_green">
      </Form>
    </div>
  </section>
</template>

<script>
  import PaymentFormSchema from '@/validation/paymentFormSchema';
  import { Field, Form } from 'vee-validate';

  export default {
    
    components: { Form, Field },
  
    data() {
      return {
        schema: PaymentFormSchema,
      }
    },
  
    methods: {
      proceedToCheckout() {
        this.$router.push({name: 'order-delivery'});
      }
    }
    
  };
</script>

<style scoped src='./order-delivery.vue'/>
