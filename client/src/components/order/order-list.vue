<template>
  <section class="section">
    <div v-if="orderHistorytIsEmpty">
      <p class="empty-list-message">Your order history is empty.</p>
    </div>
    <div v-else>
      <Spinner :isLoading="isLoading" />
      <div v-if="!isLoading">
        <h3>Order History</h3>
      <br>
      <table>
        <thead>
        <tr>
          <th :class="{ 'selected-filter': filters.createdAt !== '' }">
            <select v-model="filters.createdAt" class="filter-select">
              <option value="">All</option>
              <option v-for="date in uniqueDates" :key="date" :value="date">{{ date }}</option>
            </select>
            <img v-if="filters.createdAt !== ''" class="filter-icon" src="@/assets/bitmap/filter.png" alt="Filter Icon">
            <br> <br>
            <div @click="sortBy('createdAt')" class="sortable-header"> Date Created
              <span class="triangle" :class="{ 'triangle-active': sortKey === 'createdAt', 'triangle-inactive': sortKey !== 'createdAt' }">
                  {{ sortKey === 'createdAt' && sortOrder === -1 ? '▼' : '▲' }}
                </span>
            </div>
          </th>
          <!--  //  -->
          <th :class="{ 'selected-filter': filters.orderAddress !== '' }">
            <select v-model="filters.orderAddress" class="filter-select" @click.stop>
              <option value="">All</option>
              <option value="No delivery">No delivery</option>
              <option v-for="address in uniqueAddress" :key="address" :value="address">{{ address }}</option>
            </select>
            <img v-if="filters.orderAddress !== ''" class="filter-icon" src="@/assets/bitmap/filter.png" alt="Filter Icon">
            <br> <br>
            <div @click="sortBy('orderAddress')" class="sortable-header"> Order Address
              <span class="triangle" :class="{ 'triangle-active': sortKey === 'orderAddress', 'triangle-inactive': sortKey !== 'orderAddress' }">
      {{ sortKey === 'orderAddress' && sortOrder === -1 ? '▼' : '▲' }}
    </span>
            </div>
          </th>
          <!--  //  -->
          <th :class="{ 'selected-filter': filters.total !== '' }">
            <select v-model="filters.total" class="filter-select" @click.stop>
              <option value="">All</option>
              <option v-for="total in uniqueTotals" :key="total" :value="total">{{ total }}</option>
            </select>
            <img v-if="filters.total !== ''" class="filter-icon" src="@/assets/bitmap/filter.png" alt="Filter Icon">
            <br> <br>
            <div @click="sortBy('total')" class="sortable-header"> Total
              <span class="triangle" :class="{ 'triangle-active': sortKey === 'total', 'triangle-inactive': sortKey !== 'total' }">
                {{ sortKey === 'total' && sortOrder === -1 ? '▼' : '▲' }}
              </span>
            </div>
          </th>
          <!--  //  -->
          <th :class="{ 'selected-filter': filters.discount !== '' }">
            <select v-model="filters.discount" class="filter-select" @click.stop>
              <option value="">All</option>
              <option v-for="discount in uniqueDiscounts" :key="discount" :value="discount">{{ discount }}</option>
            </select>
            <img v-if="filters.discount !== ''" class="filter-icon" src="@/assets/bitmap/filter.png" alt="Filter Icon">
            <br> <br>
            <div @click="sortBy('discount')" class="sortable-header"> Discount
              <span class="triangle" :class="{ 'triangle-active': sortKey === 'discount', 'triangle-inactive': sortKey !== 'discount' }">
                {{ sortKey === 'discount' && sortOrder === -1 ? '▼' : '▲' }}
              </span>
            </div>
          </th>
          <!--  //  -->
          <th :class="{ 'selected-filter': filters.quantity !== '' }">
            <select v-model="filters.quantity" class="filter-select" @click.stop>
              <option value="">All</option>
              <option v-for="quantity in uniqueQuantities" :key="quantity" :value="quantity">{{ quantity }}</option>
            </select>
            <img v-if="filters.quantity !== ''" class="filter-icon" src="@/assets/bitmap/filter.png" alt="Filter Icon">
            <br> <br>
            <div @click="sortBy('quantity')" class="sortable-header"> Quantity
              <span class="triangle" :class="{ 'triangle-active': sortKey === 'quantity', 'triangle-inactive': sortKey !== 'quantity' }">
                {{ sortKey === 'quantity' && sortOrder === -1 ? '▼' : '▲' }}
              </span>
            </div>
          </th>
          <!--  //  -->
          <th :class="{ 'selected-filter': filters.isPaid !== '' }">
            <select v-model="filters.isPaid" class="filter-select" @click.stop>
              <option value="">All</option>
              <option value="true">Yes</option>
              <option value="false">No</option>
            </select>
            <img v-if="filters.isPaid !== ''" class="filter-icon" src="@/assets/bitmap/filter.png" alt="Filter Icon">
            <br> <br>
            <div class="sortable-header"> Is Paid? </div>
          </th>
          <!--  //  -->
          <th>Actions</th>
        </tr>
        </thead>
        <tbody>
        <tr v-for="order in filterOrders" :key="order.id">
          <td>{{ formattedDate(order.createdAt) }}</td>
          <td>{{ order.orderAddress ? order.orderAddress.address : 'No delivery' }}</td>
          <td>{{ order.total }}</td>
          <td>{{ order.discount }}</td>
          <td>{{ order.quantity }}</td>
          <td>{{ order.isPaid ? 'Yes' : 'No' }}</td>
          <td>
            <button class="btn btn-yellow indent" @click="transition(order.id)">Details</button>
            <button class="btn btn-red" data-bs-toggle="modal" data-bs-target="#deleteModal">Delete</button>
          </td>
          <DeleteModal :modal-id="deleteModalId" :delete-order="deleteOrder" :order-id="order.id"/>
        </tr>
        </tbody>
      </table>
    </div>
    </div>
  </section>
</template>

<script>
  import { mapGetters } from 'vuex';
  import DeleteModal from '@/components/order/delete-modal';
  import Spinner from '@/components/load-spinner';

  export default {
    components: { DeleteModal, Spinner },
    
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
        sortKey: 'createdAt',
        sortOrder: -1, // направление сортировки
        deleteModalId: 'deleteModal',
        isLoading: true // состояние загрузки
      }
    },
  
    mounted() {
      this.loadOrders();
      this.startPolling();
    },
  
    beforeUnmount() {
      this.stopPolling();
    },
    
    computed: {
      ...mapGetters(['ORDER_LIST', 'ALL_ORDER_ADDRESS', 'USER_STATE']),
  
      orderHistorytIsEmpty() {
        return this.orderList.length === 0;
      },
    
      orderList() {
        return Array.isArray(this.ORDER_LIST) ? this.ORDER_LIST : [];
      },
    
      orderAddress() {
        return this.ALL_ORDER_ADDRESS;
      },
    
      userID() {
        return this.USER_STATE?.id || null;
      },
  
      filterOrders() {
        let filteredOrders = this.orderList;
    
        // Apply each filter in turn
        if (this.filters.createdAt) {
          filteredOrders = filteredOrders.filter(order => this.formattedDate(order.createdAt) === this.filters.createdAt);
        }
        if (this.filters.orderAddress) {
          filteredOrders = filteredOrders.filter(order => {
            const address = order.orderAddress ? order.orderAddress.address : 'No delivery';
            return address === this.filters.orderAddress;
          });
        }
        if (this.filters.total) {
          filteredOrders = filteredOrders.filter(order => order.total === this.filters.total);
        }
        if (this.filters.discount) {
          filteredOrders = filteredOrders.filter(order => order.discount === this.filters.discount);
        }
        if (this.filters.quantity) {
          filteredOrders = filteredOrders.filter(order => order.quantity === this.filters.quantity);
        }
        if (this.filters.isPaid) {
          filteredOrders = filteredOrders.filter(order => order.isPaid.toString() === this.filters.isPaid);
        }
        if (this.sortKey) {
          filteredOrders.sort(this.sortFunction);
        }
    
        return filteredOrders;
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
      async loadOrders() {
        try {
          this.isLoading = true;
          await this.$store.dispatch('GET_ORDERS_FROM_API', this.userID);
          setTimeout(() => {this.isLoading = false}, 2000);
        } catch (error) {
          console.error('An error occurred while retrieving data from the API:', error);
          this.isLoading = false;
        }
      },
  
      // API path listener
      startPolling() {
        this.pollingInterval = setInterval(async () => {
          try {
            await this.$store.dispatch('GET_ORDERS_FROM_API', this.userID);
          } catch (error) {
            console.error('An error occurred while polling data from the API:', error);
          }
        }, 5000); // Poll every 15 seconds
      },
  
      stopPolling() {
        clearInterval(this.pollingInterval);
      },
      
      transition(id) {
        this.$router.push({name: 'order-list-item-details', params: {id: id}});
      },
  
      deleteOrder(index) {
        this.$store.dispatch('DELETE_ORDER', index);
      },
      
      formattedDate(date) {
        return this.$formatDate(date);
      },
  
      sortBy(key) {
        if (this.sortKey === key) {
          // Если ключ сортировки не изменился, меняем направление сортировки
          this.sortOrder = -this.sortOrder;
        } else {
          // Если ключ сортировки изменился, устанавливаем новый ключ и направление сортировки по умолчанию
          this.sortKey = key;
          this.sortOrder = -1;
        }
      },
  
      sortFunction(a, b) {
        let comparison = 0;
        if (typeof a[this.sortKey] === 'number') {
          comparison = a[this.sortKey] - b[this.sortKey];
        } else {
          let aValue = a[this.sortKey];
          let bValue = b[this.sortKey];
      
          if (this.sortKey === 'orderAddress') {
            aValue = aValue ? aValue.address : null;
            bValue = bValue ? bValue.address : null;
          }
      
          // Handle "No delivery" values
          if (aValue === null && bValue !== null) {
            comparison = -1;
          } else if (aValue !== null && bValue === null) {
            comparison = 1;
          } else if (aValue === null && bValue === null) {
            comparison = 0;
          } else {
            comparison = aValue.localeCompare(bValue);
          }
        }
        return comparison * this.sortOrder;
      }
    }
  }
</script>

<style lang="scss" scoped>
  .loading-spinner {
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100vh; /* Высота экрана */
  }

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

  .filter-icon {
    top: 50%;
    right: 10px;
    width: 18px;
    height: 18px;
    margin-left: 5px;
  }

  .selected-filter .filter-icon {
    top: 50%;
    right: 10px;
    width: 18px;
    height: 18px;
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

  .triangle {
    cursor: pointer;
  }

  .triangle-active {
    color: black;
  }

  .triangle-inactive {
    color: #9B9B9B;
  }
</style>