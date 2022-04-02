<template>
   <title>Ecommerce</title>
   <main-wrapper></main-wrapper>
</template>

<script>
  import MainWrapper from '@/pages/main-wrapper';

  export default {
     name: 'App',
     components: {
        MainWrapper,
     }
  }
</script>

<style>
  #app {
     font-family: Avenir, Helvetica, Arial, sans-serif;
     -webkit-font-smoothing: antialiased;
     -moz-osx-font-smoothing: grayscale;
     text-align: center;
     color: #2c3e50;
     padding: 1.5rem;
     /*margin-top: 60px;*/
  }  
  
  body {
    background-color: rgb(247 254 231)
  }

  img {
    cursor: pointer
  }

  /*reset styles*/
  * {
     border: 0;
     margin: 0;
     padding: 0;
  }  
  
  *, *::before, *::after {
     -moz-box-sizing: border-box;
     -webkit-box-sizing: border-box;
     box-sizing: border-box;
  }  
  
  :focus, :active {
     outline: none;
  }  
  
  a:focus, a:active {
     outline: none;
  }  
  
  header, nav, aside, footer {
     display: block;
  }  
  
  html, body {
     height: 100%;
     width: 100%;
     font-size: 90%;
     line-height: 1;
     -ms-text-size-adjust: 100%;
     -moz-text-size-adjust: 100%;
     -webkit-text-size-adjust: 100%;
  }  
  
  input, button, textarea {
     font-family: inherit;
  }  
  
  input::-ms-clear {
     display: none;
  }  
  
  button {
     cursor: pointer;
  }  
  
  button::-moz-focus-inner {
     padding: 0;
     margin: 0;
  }  
  
  a, a:visited {
     text-decoration: none;
  }  
  
  a:hover {
     text-decoration: none;
  }  
  
  ul, li {
     list-style: none;
  }  
  
  /*img {*/
  /*   vertical-align: top;*/
  /*}  */
  
  h1, h2, h3, h4, h5, h6 {
     /*font-size: inherit;*/
     /*font-weight: inherit;*/
  }  
  
  a {
     display: inherit;
     text-decoration: underline;
  }  
  
  .router-link {
     text-decoration: none;
     color: inherit;
  }  
  
  .material-icons {
     /*margin-left: 16px;*/
     font-size: 25px;
     color: #9B9B9B;
  }

  .container {
     display: flex;
     flex-wrap: wrap;
     justify-content: space-evenly;
     align-items: center;
     margin-top: 20px; 
     /*margin-bottom: 20px;*/
  }

  .empty-list-message {
    margin: 35px;
    font-size: 17px;
  }
</style>
