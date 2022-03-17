<template>
  <table>
    <thead>
    <tr>
      <th>Date Created</th>
      <th>Total</th>
      <th>Discount</th>
      <th>Quantity</th>
      <th>Is Paid?</th>
      <th>Actions</th>
    </tr>
    </thead>
    <tbody>
    <!-- Order Overview -->
    <tr v-for="order in orderList" :key="order.id">
      <td>{{ formattedDate(order.createdAt) }}</td>
      <td>{{ order.total }}</td>
      <td>{{ order.discount }}</td>
      <td>{{ order.quantity }}</td>
      <td>{{ order.isPaid ? 'Yes' : 'No' }}</td>
      <td>
        <button class="btn btn-yellow indent" @click="pushDetailsComponent(order.id)">Details</button>
        <button class="btn btn-red"> Delete</button>
      </td>
    
    </tr>
    <!--region Details of the selected order -->
    <!-- <tr v-if="selectedOrderId">-->
    <!--   <td colspan="5">-->
    <!--     <div v-for="detail in selectedOrderDetails" :key="detail.id">-->
    <!--       {{ detail.name }} - {{ detail.quantity }}-->
    <!--     </div>-->
    <!--   </td>-->
    <!-- </tr> bn-->
    <!--endregion-->
    </tbody>
  </table>
</template>

<script>
  import { mapGetters } from 'vuex';

  export default {
    
    
    async created() {
      try {
        await this.$store.dispatch('GET_ORDERS_FROM_API', this.userID);
      } catch (error) {
        console.error('An error occurred while retrieving data from the API:', error);
      }
    }, 
    
    computed: {
      ...mapGetters(['ORDER_LIST', 'USER_STATE']),
  
      orderList() {
        return Array.isArray(this.ORDER_LIST) ? this.ORDER_LIST : [];
      },
  
      userID() {
        return this.USER_STATE?.id || null;
      },
    }, 
    methods: {
      // <!-- region Details of the selected order method -->
      // @click="selectorder(order)"
      // :class="{ 'selected': selectedOrderId === order.id }
      // selectorder(order) {
      //   if (order && order.id) {
      //     if (this.selectedorderid === order.id) {
      //       this.selectedorderid = null;
      //       this.selectedorderdetails = null;
      //     } else {
      //       this.selectedorderid = order.id;
      //       this.selectedorderdetails = this.orderdetails.filter(detail => detail.orderid === order.id);
      //     }
      //   }
      // },
      // <!-- endregion -->
  
      pushDetailsComponent(id) {
        this.$router.push({name: 'order-list-item-details', params: {id: id}});
      },
    
      formattedDate(date) {
        return this.$formatDate(date);
      },
    },
  };
</script>

<style lang="scss" scoped>
  table {
    width: 100%;
    border-collapse: collapse;
  }  
  
  th, td {
    border: 1px solid #ddd;
    padding: 8px;
    text-align: center;
  }  
  
  th {
    background-color: #f2f2f2;
  }

  .indent {
    margin: 7px;
  }
</style>