<template>
  <div class="container">
    <div class="row">
      <div v-for="(item, index) in filteredOrderList" :key="index" class="row order-item">
        <div class="col col-md-5">
          <div class="cart-item-details">
            <p><strong>Name:    </strong> {{ item.name }}</p>
            <p><strong>Filling: </strong> {{ item.filling }}</p>
            <p><strong>Weight:  </strong> {{ item.weight }}</p>
            <p><strong>Tier:    </strong> {{ item.tier }}</p>
            <p><strong>Price:   </strong> {{ formattedPrice(item.price) }}</p>
          </div>
        </div>
        <div class="col col-md-7 image-container">
          <img class="item-image" v-if="isValidImage(item.imageSlice)"
               :src="require('@/assets/images/' + item.image)"
               width="200" height="200">
          <img class="item-image" v-if="isValidImage(item.imageSlice)"
               :src="require('@/assets/images/' + item.imageSlice)"
               width="200" height="200">
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  import { mapGetters } from 'vuex';

  export default {
    
    props: {id: String},
  
    computed: {
      ...mapGetters(['ORDER_DETAILS']), 
      
      orderDetails() {
        const array = this.ORDER_DETAILS;
        // Type check on object array
        if (Array.isArray(array) && array.length > 0 && typeof array[0] === 'object') {
          return array;
        } else {
          return null;
        }
      },
    
      hasLayers() {
        return this.layers.length > 0;
      },
    
      price() {
        let sum = 0;
        this.layers.forEach((layer) => {
          sum += layer.height * this.layersTypes[layer.type].price1sm;
        });
        return sum;
      },
  
      filteredOrderList() {
        if (this.orderDetails) {
          return this.orderDetails.filter(detail => detail.orderId === this.id);
        } else {
          return [];
        }
      }
    }, 
    
    methods: {
      formattedPrice(price) {
        return this.$formatPrice(price);
      },
  
      // Type check on image
      isValidImage(image) {
        return image && typeof image === 'string';
      }
    }
  }
</script>

<style lang="scss" scoped>
.container {
  display: flex;
  flex-direction: column;
  align-items: center;
}

.row {
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  width: 100%;
}

.order-item {
  display: flex;
  flex-direction: column; // Стартовая вертикальная ориентация
  align-items: center;
  padding: 10px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
  border-radius: 10px;
  margin-bottom: 20px;
  width: 100%; // Полная ширина на мобильных устройствах
}

.cart-item-details, .image-container {
  text-align: center;
}

.item-image {
  max-width: 100%;
  height: auto;
  margin: 10px;
  box-shadow: 0 4px 6px -1px rgba(0, 0, 0, 0.1), 0 2px 4px -1px rgba(0, 0, 0, 0.06);
  border-radius: 10px;
}

p {
  font-size: 1rem; // Базовый размер шрифта
}

// Медиа-запросы для адаптации на различных экранах
@media (min-width: 576px) {
  .order-item {
    flex-direction: row; // Горизонтальная ориентация на планшетах и выше
    width: 80%; // Уменьшаем ширину карточки
  }
  
  .cart-item-details, .image-container {
    flex: 1; // Равное распределение пространства
  }
  
  p {
    font-size: 1.1rem; // Немного увеличиваем шрифт
  }
}

@media (min-width: 768px) {
  .order-item {
    width: 60%; // Дальнейшее уменьшение ширины карточки на больших экранах
  }
}

@media (min-width: 992px) {
  .order-item {
    width: 50%; // Оптимальная ширина карточки на больших экранах
  }
}

@media (min-width: 1200px) {
  .order-item {
    width: 40%; // Узкие карточки на очень больших экранах
  }
}
</style>