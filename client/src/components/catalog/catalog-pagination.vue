<template>
  <div class="pagination">
    <button @click="goPrevious" :disabled="currentPage <= 1">Prev</button>
    <span v-for="page in pages"
          :key="page"
          class="page-number"
          :class="{'selected-page': page === currentPage}"
          @click="changePage(page)">
      {{ page }}
    </span>
    <button @click="goNext" :disabled="currentPage >= totalPages">Next</button>
  </div>
</template>

<script>
  export default {
    props: {
      currentPage: {
        type: Number, 
        required: true
      }, 
      totalPages: {
        type: Number, 
        required: true
      }, 
      totalVisible: {
        type: Number, 
        default: 5
      }
    }, 
    data() {
      return {
        selectedPage: this.currentPage // Initialize selectedPage with currentPage
      };
    }, 
    
    computed: {
      pages() {
        let pages = [];
        let startPage, endPage;
        if (this.totalPages <= this.totalVisible) {
          startPage = 1;
          endPage = this.totalPages;
        } else {
          let maxPagesBeforeCurrentPage = Math.floor(this.totalVisible / 2);
          let maxPagesAfterCurrentPage = Math.ceil(this.totalVisible / 2) - 1;
          if (this.currentPage <= maxPagesBeforeCurrentPage) {
            startPage = 1;
            endPage = this.totalVisible;
          } else if (this.currentPage + maxPagesAfterCurrentPage >= this.totalPages) {
            startPage = this.totalPages - this.totalVisible + 1;
            endPage = this.totalPages;
          } else {
            startPage = this.currentPage - maxPagesBeforeCurrentPage;
            endPage = this.currentPage + maxPagesAfterCurrentPage;
          }
        }
        for (let i = startPage; i <= endPage; i++) {
          pages.push(i);
        }
        return pages;
      }
    }, 
    
    methods: {
      changePage(page) {
        this.selectedPage = page; // Update selectedPage
        this.$emit('page-changed', page);
      }, 
      
      goNext() {
        if (this.currentPage < this.totalPages) {
          this.changePage(this.currentPage + 1);
        }
      }, 
      
      goPrevious() {
        if (this.currentPage > 1) {
          this.changePage(this.currentPage - 1);
        }
      }
    }
  }
</script>

<style scoped>
  .page-number {
    cursor: pointer;
  }

  .pagination {
    text-align: center;
    display: flex;
    margin: 10px;
    justify-content: center;
  }  
  
  .pagination button {
    background-color: #4caf50;
    color: white;
    border: none;
    padding: 10px 15px;
    margin: 0 5px;
    cursor: pointer;
    border-radius: 5px;
  }  
  
  .pagination button:hover {
    background-color: #45a049;
  }  
  
  .pagination button:disabled {
    background-color: #cccccc;
    cursor: not-allowed;
  }  
  
  .pagination .current-page {
    font-weight: bold;
    font-size: 18px;
    padding: 10px 15px;
  }  
  
  .pagination .page-number {
    padding: 10px 15px;
  }

  /* Стиль для выбранного номера страницы */
  .selected-page {
    background-color: #748292; /* Синий фон */
    color: white; /* Белый текст */
    border-radius: 5px; /* Скругление углов */
  }
</style>