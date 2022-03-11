<template>
  <footer>
    <div class="footerBottom">
      <p>Copyright &copy; {{ currentYear }} -- Designed by
        <span class="designer">Yurii Isaev</span>
      </p>
    </div>
  </footer>
</template>

<script>
  export default {
    data() {
      return {
        currentYear: new Date().getFullYear()
      };
    }
  };
</script>

<style scoped>
  footer {
    border-radius: 25px;
    background-color: #111;
  }

  .footerBottom {
    background-color: #20272f;
    padding: 20px;
    text-align: center;
    border-bottom-left-radius: 10px;
    border-bottom-right-radius: 10px;
  }

  .footerBottom p {
    color: rgb(197 204 181);;
    margin: 0.3rem;
  }

  .designer {
    opacity: 0.7;
    text-transform: uppercase;
    letter-spacing: 1px;
    font-weight: 400;
    margin: 0 5px;
  }
</style>