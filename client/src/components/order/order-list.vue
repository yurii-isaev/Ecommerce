<template>
  <section class="section">
    <table>
      <thead>
      <tr>
        <th :class="{ 'selected-filter': filters.createdAt !== '' }">
          <select v-model="filters.createdAt" class="filter-select">
            <option value="">All</option>
            <option v-for="date in uniqueDates" :key="date" :value="date">{{ date }}</option>
          </select>
          <img v-if="filters.createdAt !== ''" class="filter-icon" src="@/assets/bitmap/filter.png" alt="Filter Icon">
          <br>
          Date Created
        </th>
  
        <th :class="{ 'selected-filter': filters.orderAddress !== '' }">
          <select v-model="filters.orderAddress" class="filter-select">
            <option value="">All</option>
            <option v-for="address in uniqueAddress" :key="address" :value="address">{{ address }}</option>
          </select>
          <img v-if="filters.orderAddress !== ''" class="filter-icon" src="@/assets/bitmap/filter.png" alt="Filter Icon">
          <br>
          Date Created
        </th>
  
        <th :class="{ 'selected-filter': filters.total !== '' }">
          <select v-model="filters.total" class="filter-select">
            <option value="">All</option>
            <option v-for="total in uniqueTotals" :key="total" :value="total">{{ total }}</option>
          </select>
          <img v-if="filters.total !== ''" class="filter-icon" src="@/assets/bitmap/filter.png" alt="Filter Icon">
          <br>
          Total
        </th>
        
        <th :class="{ 'selected-filter': filters.discount !== '' }">
          <select v-model="filters.discount" class="filter-select">
            <option value="">All</option>
            <option v-for="discount in uniqueDiscounts" :key="discount" :value="discount">{{ discount }}</option>
          </select>
          <img v-if="filters.discount !== ''" class="filter-icon" src="@/assets/bitmap/filter.png" alt="Filter Icon">
          <br>
          Discount
        </th>
  
        <th :class="{ 'selected-filter': filters.quantity !== '' }">
          <select v-model="filters.quantity" class="filter-select">
            <option value="">All</option>
            <option v-for="quantity in uniqueQuantities" :key="quantity" :value="quantity">{{ quantity }}</option>
          </select>
          <img v-if="filters.quantity !== ''" class="filter-icon" src="@/assets/bitmap/filter.png" alt="Filter Icon">
          <br>
          Quantity
        </th>
  
        <th :class="{ 'selected-filter': filters.isPaid !== '' }">
          <select v-model="filters.isPaid" class="filter-select">
            <option value="">All</option>
            <option value="true">Yes</option>
            <option value="false">No</option>
          </select>
          <img v-if="filters.isPaid !== ''" class="filter-icon" src="@/assets/bitmap/filter.png" alt="Filter Icon">
          <br>
          Is Paid?
        </th>
        
        <th>Actions</th>
      </tr>
      </thead>
      <tbody>
      <tr v-for="order in filterOrders" :key="order.id">
        <td>{{ formattedDate(order.createdAt) }}</td>
        <td>{{ order.orderAddress ? order.orderAddress.address : 'No order' }}</td>
        <td>{{ order.total }}</td>
        <td>{{ order.discount }}</td>
        <td>{{ order.quantity }}</td>
        <td>{{ order.isPaid ? 'Yes' : 'No' }}</td>
        <td>
          <button class="btn btn-yellow indent" @click="routingComponent(order.id)">Details</button>
          <button class="btn btn-red">Delete</button>
        </td>
      </tr>
      </tbody>
    </table>
  </section>
</template>

<script>
  import { mapGetters } from 'vuex';

  export default {
    data() {
      return {
        filters: {
          createdAt: '',
          total: '',
          discount: '',
          quantity: '',
          isPaid: '',
          orderAddress: ''
        },
      }
    },
  
    async created() {
      try {
        await this.$store.dispatch('GET_ORDERS_FROM_API', this.userID);
      } catch (error) {
        console.error('An error occurred while retrieving data from the API:', error);
      }
    }, 
    
    computed: {
      ...mapGetters(['ORDER_LIST', 'ORDER_ADDRESS', 'USER_STATE']), 
      
      orderList() {
        return Array.isArray(this.ORDER_LIST) ? this.ORDER_LIST : [];
      },
  
      orderAddress() {
        return this.ORDER_ADDRESS;
      },
      
      userID() {
        return this.USER_STATE?.id || null;
      },
  
      filterOrders() {
        return this.orderList.filter(order => {
          return (!this.filters.createdAt || this.formattedDate(order.createdAt) === this.filters.createdAt) &&
              (this.filters.orderAddress.length === 0 || this.filters.orderAddress.includes(order.orderAddress ? order.orderAddress.address : null)) &&
              (!this.filters.total || order.total === this.filters.total) &&
              (!this.filters.discount || order.discount === this.filters.discount) &&
              (!this.filters.quantity || order.quantity === this.filters.quantity) &&
              (!this.filters.isPaid || order.isPaid.toString() === this.filters.isPaid);
        });
      },
      
      uniqueDates() {
        return [...new Set(this.orderList.map(order => this.formattedDate(order.createdAt)))];
      }, 
      
      uniqueTotals() {
        return [...new Set(this.orderList.map(order => order.total))];
      }, 
      
      uniqueDiscounts() {
        return [...new Set(this.orderList.map(order => order.discount))];
      }, 
      
      uniqueQuantities() {
        return [...new Set(this.orderList.map(order => order.quantity))];
      },

      uniqueAddress() {
        if (Array.isArray(this.orderAddress) && this.orderAddress.length > 0) {
          const addresses = this.orderAddress
              .filter(order => order != null && order.address != null)
              .map(order => order.address);
          return [...new Set(addresses)];
        } else {
          return [];
        }
      }
    }, 
    
    methods: {
      routingComponent(id) {
        this.$router.push({name: 'order-list-item-details', params: {id: id}});
      }, 
      
      formattedDate(date) {
        return this.$formatDate(date);
      }
    }
  }
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
  
  .section {
    margin: 20px;
  }

  .filter-select {
    //width: 150px; /* Adjust width as needed */
    padding: 3px;
    border-radius: 5px;
    margin-bottom: 5px;
    border: 1px solid #ccc;
  }

  /* Styles for selected filter with icon */
  .selected-filter {
    position: relative;
  }

  .selected-filter .filter-icon {
    top: 50%;
    right: 10px;
    width: 20px;
    height: 20px;
    margin-left: 5px;
  }

  @media screen and (max-width: 768px) {
    th, td {
      padding: 5px; /* Уменьшение отступов */
      font-size: 12px; /* Уменьшение размера шрифта */
    }
  
    .filter-select {
      width: 100%; /* Полное заполнение ширины родительского контейнера */
    }
  
    .selected-filter .filter-icon {
      display: none; /* Скрытие иконки фильтра */
    }
  }

  /* Дополнительные стили для горизонтальной прокрутки */
  .table-container {
    overflow-x: auto;
  }
</style>