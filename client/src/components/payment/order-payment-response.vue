<template>
  <div class="container">
    <div class="row">
      <div class="col">
        <div v-if="isPaymentSuccess" class="message-box success">
          <i class="fa fa-check-circle" aria-hidden="true"></i>
          <h2> Your payment was successful </h2>
          <p> Thank you for your payment. We will <br>
            be in contact with more details shortly. </p>
        </div>
        <div v-else class="message-box success failed">
          <i class="fa fa-times-circle" aria-hidden="true"></i>
          <h2> Your payment failed </h2>
          <p> Try again later. </p>
        </div>
      </div>
    </div>
    <hr>
  </div>
</template>

<script>
  export default {
    computed: {
      isPaymentSuccess() {
        // Convert the query parameter to a boolean
        return this.$route.query.paymentSuccess === 'true';
      }
    }
  }
</script>

<style scoped>

  .container{
    justify-content: center;
  }
  
  .failed {
    border-bottom: solid 4px red !important;
  }  
  .failed i {
    color: red !important;
  }  
  
  .success {
    box-shadow: 0 15px 25px #00000019;
    padding: 45px;
    width: 100%;
    text-align: center;
    margin: 40px auto;
    border-bottom: solid 4px #28a745;
  }  
  
  .success i {
    font-size: 55px;
    color: #28a745;
  }  
  
  .success h2 {
    margin-bottom: 12px;
    font-size: 40px;
    font-weight: 500;
    line-height: 1.2;
    margin-top: 10px;
  }  
  
  .success p {
    margin-bottom: 0;
    font-size: 18px;
    color: #495057;
    font-weight: 500;
  }
</style>