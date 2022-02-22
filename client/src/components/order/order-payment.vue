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
                       :class="{'is-invalid': errors.expMonth}"/>
                <div class="invalid-feedback">{{ errors.expMonth }}</div>
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
  import { Field, Form } from 'vee-validate';
  import { mapActions, mapGetters } from 'vuex';
  import PaymentFormSchema from '@/validation/paymentFormSchema';

  export default {
  
  components: { Form, Field },
  
  data() {
    return {
      schema: PaymentFormSchema,
      cartList: [],
      totalObj: 0,
      paid: false,
    }
  },
  
  mounted() {
    this.loadCartListFromRoute();
  },
    
  computed: {
    ...mapGetters(['USER_STATE', 'IS_ORDER_PAID', 'IS_ORDER_MESSAGE']),
    
    userID() {
      return this.USER_STATE ? this.USER_STATE.id : null;
    },
  },
  
  methods: {
    ...mapActions(['POST_USER_ORDER_TO_API', 'CLEAR_CART']),
    
    async proceedToCheckout(formValues) {
  
      const payment = {
        cardHolder: formValues.nameOnCard,
        cardNumber: formValues.numberOnCard,
        expMonth: formValues.expMonth,
        expYear: formValues.expYear,
        cvv: formValues.cvv,
        userId: this.userID
      };
      
      const order = {
        cardPayment: payment,
        orderItems: this.cartList,
        discount: this.totalObj.discount,
        quantity: this.totalObj.quantity,
        subtotal: this.totalObj.subtotal,
        tax: this.totalObj.tax,
        total: this.totalObj.total,
        isPaid: true,
      };
      
      console.warn("[ORDER-PAYMENT] order: ", order);
      
      try {
        await this.POST_USER_ORDER_TO_API(order);
        
        this.paid = this.IS_ORDER_PAID;
        
        if (this.paid) {
          await this.CLEAR_CART();
          this.$router.push({name: 'order-payment-response', query: { paymentSuccess: this.paid }});
        }
        else {
          this.$router.push({name: 'order-payment-response', query: { paymentSuccess: this.paid }});
          console.warn('Payment failed:', this.paid);
        }
      } catch (error) {
        this.$router.push({name: 'order-payment-response', query: { paymentSuccess: this.paid }});
        console.error('An error occurred:', error);
      }
    },
  
    loadCartListFromRoute() {
      const { cart, total } = this.$route.query;
      if (cart) {
        try {
          this.cartList = JSON.parse(cart);
          console.log("[ORDER-PAYMENT]::cartList:", this.cartList);
        } catch (error) {
          console.error("Error parsing cart data:", error);
        }
      }
      if (total) {
        try {
          this.totalObj = JSON.parse(total);
          // this.cartTotal = totalObj.subtotal; // или totalObj.total для итоговой суммы
          console.log("[ORDER-PAYMENT]::totalObj:", this.totalObj);
        } catch (error) {
          console.error("Error parsing total data:", error);
        }
      }
    }
  }
}
</script>

<style scoped src='./order-delivery.vue'/>
