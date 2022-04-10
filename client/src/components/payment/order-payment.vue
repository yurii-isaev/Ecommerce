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
                       :class="{'is-invalid': errors.nameOnCard}"
                       v-model="formData.nameOnCard"/>
                <div class="invalid-feedback">{{ errors.nameOnCard }}</div>
              </div>
            </div>
            
            <div class="form-row">
              <div class="form-group col">
                <label>Card Number:</label>
                <Field class="form-control" name="numberOnCard" type="text" placeholder="1111-2222-3333-4444"
                       :class="{'is-invalid': errors.numberOnCard}"
                       v-model="formData.numberOnCard"/>
                <div class="invalid-feedback">{{ errors.numberOnCard }}</div>
              </div>
            </div>
            
            <div class="form-row">
              <div class="form-group col">
                <label>Exp month:</label>
                <Field class="form-control" name="expMonth" type="text" placeholder="month"
                       :class="{'is-invalid': errors.expMonth}"
                       v-model="formData.expMonth"/>
                <div class="invalid-feedback">{{ errors.expMonth }}</div>
              </div>
            </div>
            
            <div class="flex">
              <div class="form-row">
                <div class="form-group col">
                  <label>Exp year:</label>
                  <Field class="form-control" name="expYear" type="number" placeholder="year"
                         :class="{'is-invalid': errors.expYear}"
                         v-model="formData.expYear"/>
                  <div class="invalid-feedback">{{ errors.expYear }}</div>
                </div>
              </div>
              
              <div class="form-row">
                <div class="form-group col">
                  <label>CVV:</label>
                  <Field class="form-control" name="cvv" type="text" placeholder="cvv"
                         :class="{'is-invalid': errors.cvv}"
                         v-model="formData.cvv"/>
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
  import { mapGetters } from 'vuex';
  import PaymentFormSchema from '@/validation/paymentFormSchema';

  export default {
    components: { Form, Field },
  
    data() {
      return {
        schema: PaymentFormSchema,
        formData: {
          nameOnCard: '',
          numberOnCard: '',
          expMonth: '',
          expYear: '',
          zipCode: '',
          cvv: ''
        },
        paid: false,
     }
   },
  
    computed: {
      ...mapGetters(['USER_STATE', 'CART_TOTAL', 'CART_STATE', 'ORDER_DETAILS', 'IS_ORDER_PAID', 'ORDER_ADDRESS']),
    
      userID() {
        return this.USER_STATE?.id || null;
      },
    
      cartList() {
        return this.CART_STATE;
      },
    
      cartTotal() {
        return this.CART_TOTAL;
      },
  
      orderAddress() {
        if (this.ORDER_ADDRESS && typeof this.ORDER_ADDRESS === 'object') {
          console.log("ORDER_ADDRESS", this.ORDER_ADDRESS)
          return this.ORDER_ADDRESS;
        } else {
          return null;
        }
      }
    },
  
    methods: {
      async proceedToCheckout() {
        const payment = {
          cardHolder: this.formData.nameOnCard,
          cardNumber: this.formData.numberOnCard,
          expMonth: this.formData.expMonth,
          expYear: this.formData.expYear,
          cvv: this.formData.cvv,
          userId: this.userID
        };
      
        const order = {
          orderCardPayment: payment,
          orderDetails: this.cartList,
          userId: this.userID,
          discount: this.cartTotal.discount,
          quantity: this.cartTotal.quantity,
          subtotal: this.cartTotal.subtotal,
          tax: this.cartTotal.tax,
          total: this.cartTotal.total,
          isPaid: true,
        };
      
        // Add orderAddress to the order only if orderAddress is not empty object
        if (Object.keys(this.orderAddress).length === 0) {
          order.orderAddress = this.orderAddress;
        }
  
        console.warn("ORDER-PAYMENT_OBJECT: ", JSON.stringify(order, null, 2));
      
        try {
          await this.$store.dispatch('POST_USER_ORDER_TO_API', order);
          this.paid = this.IS_ORDER_PAID;
        
          if (this.paid) {
            await this.$store.dispatch('CLEAR_CART');
            this.$router.push({name: 'order-payment-response', query: {paymentSuccess: this.paid}});
          } else {
            await console.warn('PAYMENT_FAILED:', this.paid);
            this.$router.push({name: 'order-payment-response', query: {paymentSuccess: this.paid}});
          }
        } catch (error) {
          this.$router.push({name: 'order-payment-response', query: {paymentSuccess: this.paid}});
          console.error('ORDER-PAYMENT_ERROR:', error);
        }
      }
    
    // mounted() {
    //   this.loadCartListFromRoute();
    // },
    // loadCartListFromRoute() {
    //   const { cart, total } = this.$route.query;
    //   if (cart) {
    //     try {
    //       this.cartList = JSON.parse(cart);
    //       console.log("[ORDER-PAYMENT]::cartList:", this.cartList);
    //     } catch (error) {
    //       console.error("Error parsing cart data:", error);
    //     }
    //   }
    //   if (total) {
    //     try {
    //       this.totalObj = JSON.parse(total);
    //       this.cartTotal = totalObj.subtotal; // или totalObj.total для итоговой суммы
    //       console.log("[ORDER-PAYMENT]::totalObj:", this.totalObj);
    //     } catch (error) {
    //       console.error("Error parsing total data:", error);
    //     }
    //   }
    // }
  }
}
</script>

<style scoped src='./order-delivery.vue'/>
