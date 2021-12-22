<template>
   <div class="container">
      <div class="row">
         <div class="col col-md-4">
            <div class="cart-item-details">
               <p><strong>Name:           </strong>{{ cartItem.name     }}</p>
               <p><strong>Avalible:       </strong>{{ cartItem.avalible }}</p>
               <p><strong>Filling:        </strong>{{ cartItem.filling  }}</p>
               <p><strong>Weight:         </strong>{{ cartItem.weight   }}</p>
               <p><strong>Tier:           </strong>{{ cartItem.tier     }}</p>
               <p><strong>Standart Price: </strong>{{ formattedPrice    }}</p>
               <br/>
               <span class="price alert-success">
                  <strong>Custom Price: </strong>
                  {{ price !== cartItem.price ? formattedCustomPrice : formattedPrice }}</span>
            </div>
         </div>
     
         <div class="col col-md-8">
            <img class="cart-item-image img-fluid" 
                 alt="img"
                 v-if="cartItem && cartItem.image"
                 :src="require('@/assets/images/' + cartItem.image)">

            <img class="cart-item-image img-fluid"
                 alt="img"
                 v-if="cartItem && cartItem.image_slice"
                 :src="require('@/assets/images/' + cartItem.image_slice)">
         </div>
      </div>
   </div>

   <div class="container">
      <div>
         <h1 style="font-size:30px">Cake custom builder</h1>
         <hr>
         <button class="btn btn-primary"
                 type="button"
                 @click="addLayer">
            Add layer
         </button>
         <hr>
         
         <div class="row">
            <div class="col col-sm-6">
               <div class="cake">
                  <div class="layer"
                       v-for="(layer, i) in layers"
                       :key="layer.id"
                       :class="'layer-' + layer.type"
                       :style="{ height: layer.height * 10 + 'px' }"
                       @click="changeHeight(i, 1)"
                       @contextmenu.prevent="changeHeight(i, -1)">
                  </div>
               </div>
            </div>

            <div class="col col-sm-6">
               <table class="table table-bordered" v-show="hasLayers">
                  <thead>
                  <tr>
                     <th>Type</th>
                     <th>Height</th>
                     <th>Actions</th>
                  </tr>
                  </thead>
                  
                  <tbody>
                  <tr v-for="(layer, i) in layers" :key="layer.id">
                     <td style="width:300px">
                        <select class="form-control"
                                v-model=" layers[i].type">
                           
                           <option v-for="(lt, key) in layersTypes"
                                   :value="key"
                                   :key="lt.id">
                              {{ lt.label }}
                           </option>
                           
                        </select>
                     </td>
                     <td>
                        <input class="form-control"
                               type="text"
                               v-model.number="layers[i].height"
                        >
                     </td>
                     <td>
                        <button class="btn btn-danger"
                                type="button"
                                @click="deleteLayer(i)">
                           Delete
                        </button>
                     </td>
                  </tr>
                  </tbody>
               </table>
            </div>
         </div>
         <hr>
         
         <div class="alert alert-success price" v-show="hasLayers">
            <span class="price">{{ formattedCustomPrice }}</span>
         </div>
         
         <button type="button"
                 class="btn btn-warning"
                 data-v-show="hasLayers"
                 @click="addToOrders()">
            Order now
         </button>
         <hr>
      </div>
   </div>
</template>

<script>
  import { mapActions, mapGetters } from 'vuex';

  export default {

     name: "cart-item-details",
     
     props: {
        id: String
     }, 
     
     data() {
        return {
           itemDetails: null,
           layers: [], 
           
           layersTypes: {
              biscuit: {
                 price1sm: 50, 
                 label:    'Biscuit'
              }, 
              beze: {
                 price1sm: 50,
                 label: '  Beze'
              }, 
              curd: {
                 price1sm: 50, 
                 label:    'Curd'
              }
           }, 
           defaultLayerType: 'biscuit', 
           defaultHeight:    3
        }
     },
     
     computed: {
        ...mapGetters(['CART_ITEM_ID']),

        cartItem() {
           return this.CART_ITEM_ID(this.id);
        }, 
        
        price() {
           let sum = 0;
           this.layers.forEach((layer) => {
              sum += layer.height * this.layersTypes[layer.type].price1sm;
           });
           return sum;
        }, 
        
        hasLayers() {
           return this.layers.length > 0;
        },

        formattedPrice() {
           return this.$formatPrice(this.cartItem.price);
        },

        formattedCustomPrice() {
           return this.$formatPrice(this.price);
        }
     }, 
     
     methods: {
        ...mapActions(['ADD_TO_ORDERS']),

        addToOrders() {
           this.ADD_TO_ORDERS({
              id:       this.cartItem.id,
              article:  this.cartItem.article,
              image:    this.cartItem.image,
              image_slice: this.cartItem.image_slice,
              name:     this.cartItem.name,
              avalible: this.cartItem.avalible,
              category: this.cartItem.category,
              filling:  this.cartItem.filling,
              weight:   this.cartItem.weight,
              tier:     this.cartItem.tier,
              layers:   this.layers,
              price:    this.price,
           });
        },
        
        addLayer() {
           this.layers.push({
              type:   this.defaultLayerType, 
              height: this.defaultHeight
           });
        }, 
        
        changeHeight(i, dh) {
           this.layers[i].height += dh;
        }, 
        
        deleteLayer(i) {
           this.layers.splice(i, 1);
        },

        fetchItemDetails() {
           if (this.cartItem && this.cartItem.id) {
              console.log("Fetching item details for cart id:", this.cartItem.id);
           } else {
              console.error("Cart data is missing or incomplete");
           }
        },
     },

     mounted() {
        this.fetchItemDetails();
     },
  }
</script>

<style lang="scss" scoped>

  p {
     font-size: large;
  }

  .cart-item {
     display: flex;
     flex-wrap: nowrap;
     justify-content: space-evenly;
     align-items: center;
     box-shadow: 0 0 8px 0 #e0e0e0;
     padding: $padding*2;
     margin-bottom: $margin*2;
  }  
  
  .cart-item-image {
     max-width: 350px;
  }  
  
  .cart-item-details {
     text-align: left;
  }
  
  //.wrapper {
  //   padding: 15px;
  //   max-width: 500px;
  //   margin: 0 auto;
  //}

  .layer {
     transition: height 0.5s;
  }

  .layer-biscuit {
     background: url(@/assets/images/buscuit.jpg);
  }

  .layer-beze{
     background: url(@/assets/images/beze.jpg);
  }

  .layer-curd {
     background: url(@/assets/images/curd.jpg);
  }
  
  .price {
     font-size: 26px;
     margin-right: 40px;
  }

  .layer-enter-active {
     animation: layerIn 0.5s;
  }
</style>