<template>
  <div class="modal fade" id="deleteModal" tabindex="-1" aria-labelledby="deleteModalLabel"
       aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="deleteModalLabel">Confirm Delete</h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">Are you sure you want to delete order?</div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
          <button type="button" class="btn btn-primary" data-bs-dismiss="modal" @click="handleDelete">Delete</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  export default {
    props: {
      modalId: {
        type: String, 
        required: true
      }, 
      
      deleteOrder: {
        type: Function, 
        required: true
      },
  
      orderId: {
        type: [String, Number], // The order ID can be a string or a number
        required: true
      }
    },
  
    methods: {
      handleDelete() {
        this.deleteOrder(this.orderId);
      }
    }
  }
</script>