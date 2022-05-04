# Vue cakes
Интернет магазин по заказу и конфигурации тортов.

<div align="center">
<img src="https://github.com/yurii-isaev/Ecommerce/assets/39811288/36f0db2b-6c1a-4ee2-a390-b08e0688d791" alt="logo">
<h3 align="center">Prototype</h3>
  <p align="center">
    This is a prototype of a possible ecommerce web application project. <br />
    This is not a library. Now it's just a sample project. <br />
<p align="center">
<img src="https://img.shields.io/badge/Made%20with-love-green.svg" alt="Made with love">
<a href='https://v3.ru.vuejs.org/ru/api/options-api.html'>
<img src='http://img.shields.io/badge/Client-Vue.js-a.svg'/>
</a>
<a href='https://www.microsoft.com/ru-ru/sql-server/sql-server-downloads'>
<img src='http://img.shields.io/badge/Database-MSSQL-yellow.svg'/>
</a>
<a href="https://docs.nunit.org/">
  <img src="https://img.shields.io/badge/Server Coverage-44%25-red.svg" alt="Coverage Badge"/>
</a>
<a href="https://opensource.org/licenses/">
<img src="https://img.shields.io/badge/License-none-red.svg"/>
</a>
<img src='http://img.shields.io/badge/Status-support-blue.svg'/>
</p>
<br/>
</div>

# About:
Данный проект представляет собой реактивное веб-приложение с разделенной бизнес-логикой клиент-сервер, ориентированное на клиентский сайт-рендеринг. Основной функционал приложения - выбор, конфигурация и заказ товара. Приложение состоит из набора функций, которые легко дополняются и расширяются.

Так что, Vue.js 3 - всё ещё торт !!

В описании Readme не представлены все подфункции основной функциональности, а описаны лишь основные службы управления приложением.

# Stack:
`Authentication`: jwt by cookie  
`Client`: Vue.js 3 option api   
`Database`: MS SQL server  
`Server`: Asp.net core 6 web api  
`Persistence`: Entity-framework / Dapper-dotnet  
`Tests`: nUnit  
`UI Design`: custom  
`Validation`:  vee-validate and yup / Asp.net DataAnnotations (system lib)

# Features:

###  ★ JWT authentication by user name, email and password.

###  ⚡️ Registration / Sign up with validation form / modal.
<div style="text-align: center">
<kbd style="display: inline-block; width: 45%; height: auto;">
<img src="https://github.com/yurii-isaev/Ecommerce/assets/39811288/7ef5e8a5-d446-4a79-b844-ef2bf8ca17d1" alt="Signup" style="width: 300px; height: auto;">
</kbd>
&nbsp; &nbsp;
<kbd style="display: inline-block; width: 45%; height: auto;">
 <img src="https://github.com/yurii-isaev/Ecommerce/assets/39811288/4a49d584-4137-480e-bc6d-5321ae8639e2" alt="Signup" style="width: 300px; height: auto;">
</kbd>
</div>

###  ⚡️ Authentication / Sign in with validation form / modal.
<div style="text-align: center">
<kbd style="display: inline-block; width: 45%; height: auto;">
 <img src="https://github.com/yurii-isaev/Ecommerce/assets/39811288/4a2eb4bb-befb-4348-ace9-82c72158c584" alt="SignIn" style="width: 300px; height: auto;">
</kbd>
&nbsp; &nbsp;
<kbd style="display: inline-block; width: 45%; height: auto;">
  <img src="https://github.com/yurii-isaev/Ecommerce/assets/39811288/15779956-ef5a-4147-9134-7624362b88a0" alt="SignIn" style="width: 300px; height: auto;">
</kbd>
</div>

### ⚡️ Authentication / Forgot Password with validation form / modal.
<div style="text-align: center">
<kbd style="display: inline-block; width: 45%; height: auto;">
 <img src="https://github.com/yurii-isaev/Ecommerce/assets/39811288/acb74a2d-439c-4f0a-8663-1bd6a0e107a9" alt="Forgot Password" style="width: 300px; height: auto;">
</kbd>
&nbsp; &nbsp;
<kbd style="display: inline-block; width: 45%; height: auto;">
 <img src="https://github.com/yurii-isaev/Ecommerce/assets/39811288/01f8b6c9-8913-47a2-a9b6-fa28a99904f5" alt="Forgot Password" style="width: 300px; height: auto;">
</kbd>
</div>

### ⚡️ Authentication / Change password with validation form.
<div style="text-align: center">
<kbd style="display: inline-block; width: 45%; height: auto;">
 <img src="https://github.com/yurii-isaev/Ecommerce/assets/39811288/af557261-b320-43fb-bc26-ca8e026ce669" alt="Change Password" style="width: 300px; height: auto;">
</kbd>
&nbsp; &nbsp;
<kbd style="display: inline-block; width: 45%; height: auto;">
 <img src="https://github.com/yurii-isaev/Ecommerce/assets/39811288/0e34e315-8ac2-4bc6-a685-922b7755ac82" alt="Change Password" style="width: 300px; height: auto;">
</kbd>
</div>

### ★ Filter by category with the selected `all` option.
<div style="text-align: center">
<kbd>
  <img src="https://github.com/yurii-isaev/Ecommerce/assets/39811288/beca75ea-0957-4f2b-b4fa-37674925b592" alt="Store" style="width: 100%; height: auto;">
</kbd>
</div>

### ★ Filter by category with the selected `cakes` option.
<div style="text-align: center">
<kbd style="display: inline-block; width: 80%; height: auto;">
 <img src="https://github.com/yurii-isaev/Ecommerce/assets/39811288//9f55e97a-e198-43be-99cb-2dff701e4c53" alt="Store" style="width:  500px; height: auto;">
</kbd>
</div>

### ★ Filter by category with the selected `pies` option.
<div style="text-align: center">
<kbd style="display: inline-block; width: 80%; height: auto;">
 <img src="https://github.com/yurii-isaev/Ecommerce/assets//39811288/c3100ab4-56a4-4bce-9f04-bd3b80a1add5" alt="Store" style="width: 500px; height: auto;">
</kbd>
</div>

### ★ Filter by category with the selected `rolls` option.
<div style="text-align: center">
<kbd style="display: inline-block; width: 80%; height: auto;">
 <img src="https://github.com/yurii-isaev/Ecommerce/assets/39811288/53d92092-db2e-425f-8417-08eba6a4f7a8" alt="Store" style="width: 500px; height: auto;">
</kbd>
</div>

### ★ Store filter price product.
<div style="text-align: center">
<kbd style="display: inline-block; width: 80%; height: auto;">
 <img src="https://github.com/yurii-isaev/Ecommerce/assets/39811288/3ccbe61a-8e0e-4108-b8e6-d26399335f14" alt="Store" style="width: 500px; height: auto;">
 </kbd>
</div>

### ★ Reactive store product search by name.
<div style="text-align: center">
<kbd style="display: inline-block; width: 80%; height: auto;">
 <img src="https://github.com/yurii-isaev/Ecommerce/assets/39811288/1049b4e3-cb90-42df-909d-44995b14c6a9" alt="Store" style="width: 500px; height: auto;">
 </kbd>
</div>

### ★ Add favorite when you are on the catalog page.
<div style="text-align: center">
<kbd style="display: inline-block; width: 80%; height: auto;">
 <img src="https://github.com/yurii-isaev/Ecommerce/assets/39811288/7a8c4f35-fe34-4f7c-b18d-840866930cbb" alt="Catalog" style="width: 500px; height: auto;">
 </kbd>
</div>

### ★ Favorits page.
<div style="text-align: center">
<kbd style="display: inline-block; width: 80%; height: auto;">
 <img src="https://github.com/yurii-isaev/Ecommerce/assets/39811288/78a01206-2064-4511-9f01-a1b7cefb031e" alt="Favorites" style="width: 500px; height: auto;">
 </kbd>
</div>

### ★ Add an item when you are on the catalog page.
<div style="text-align: center">
<kbd style="display: inline-block; width: 80%; height: auto;">
 <img src="https://github.com/yurii-isaev/Ecommerce/assets/39811288/b6ee7143-c4b9-4fe4-8572-70b1d5b889da" alt="Catalog" style="width: 500px; height: auto;">
 </kbd>
</div>

### ★ Cart page.
<div style="text-align: center">
<kbd style="display: inline-block; width: 80%; height: auto;">
 <img src="https://github.com/yurii-isaev/Ecommerce/assets/39811288/e4aba44c-afd6-4331-8185-66108a0c3dd2" alt="Cart" style="width: 500px; height: auto;">
</kbd>
</div>

### ★ The ability to increase the number of products.
<div style="text-align: center">
<kbd style="display: inline-block; width: 80%; height: auto;">
 <img src="https://github.com/yurii-isaev/Ecommerce/assets/39811288/5952e9dd-1c59-45e2-915f-e1c70edba59b" alt="Cart" style="width: 500px; height: auto;">
</kbd>
</div>

### ★ Cart details and cake custom builder.
<div style="text-align: center">
<kbd style="display: inline-block; width: 80%; height: auto;">
 <img src="https://github.com/yurii-isaev/Ecommerce/assets/39811288/b6fca41a-6c92-49b2-9669-04c7fac8e04d" alt="Cart" style="width: 500px; height: auto;">
</kbd>
</div>

### ★ Cake custom builder.
<div style="text-align: center">
<kbd style="display: inline-block; width: 80%; height: auto;">
 <img src="https://github.com/yurii-isaev/Ecommerce/assets/39811288/bd308b28-0157-42ce-96a2-117e494f2359" alt="Cart" style="width: 500px; height: auto;">
</kbd>
</div>

### ★ Custom price configuration.
<div style="text-align: center">
<kbd style="display: inline-block; width: 80%; height: auto;">
 <img src="https://github.com/yurii-isaev/Ecommerce/assets/39811288/255edc5c-262e-46cb-8b10-4b97be4673cb" alt="Cart" style="width: 500px; height: auto;">
</kbd>
</div>
<br>
<div style="text-align: center">
<kbd style="display: inline-block; width: 80%; height: auto;">
 <img src="https://github.com/yurii-isaev/Ecommerce/assets/39811288/2457b672-ec05-472b-9654-525a60565243" alt="Cart" style="width: 500px; height: auto;">
</kbd>
</div>

### ★ The ability to add delivery to an order or make a payment immediately.
<div style="text-align: center">
<kbd style="display: inline-block; width: 80%; height: auto;">
 <img src="https://github.com/yurii-isaev/Ecommerce/assets/39811288/9c5d487e-69d7-49be-b279-9640d09d1502" alt="Delivery" style="width: 500px; height: auto;">
</kbd>
</div>

### ★ The delivery order address with invalid form.
<div style="text-align: center">
<kbd style="display: inline-block; width: 80%; height: auto;">
 <img src="https://github.com/yurii-isaev/Ecommerce/assets/39811288/ca19dd92-071c-4eaf-8b67-82997bcfc25b" alt="Delivery" style="width: 500px; height: auto;">
</kbd>
</div>

### ★ The delivery order address with valid form.
<div style="text-align: center">
<kbd style="display: inline-block; width: 80%; height: auto;">
 <img src="https://github.com/yurii-isaev/Ecommerce/assets/39811288/28cca2d0-0f0f-46f6-86b5-fdbd3aad831e" alt="Delivery" style="width: 500px; height: auto;">
</kbd>
</div>

### ★ The payment data with invalid form.
<div style="text-align: center">
<kbd style="display: inline-block; width: 80%; height: auto;">
 <img src="https://github.com/yurii-isaev/Ecommerce/assets/39811288/c4e3dc6e-3b40-4c3a-b901-ccb5a54c3793" alt="Payment" style="width: 500px; height: auto;">
</kbd>
</div>

### ★ The payment data with valid form.
<div style="text-align: center">
<kbd style="display: inline-block; width: 80%; height: auto;">
 <img src="https://github.com/yurii-isaev/Ecommerce/assets/39811288/f42b33a0-fd2c-4fed-901f-a1687ff404f6" alt="Payment" style="width: 500px; height: auto;">
</kbd>
</div>

### ★ The payment data successful confirm.
<div style="text-align: center">
<kbd style="display: inline-block; width: 80%; height: auto;">
 <img src="https://github.com/yurii-isaev/Ecommerce/assets/39811288/00a93dab-0f1f-4ef3-b8d6-1ed07a74257a" alt="Payment" style="width: 500px; height: auto;">
</kbd>
</div>

### ★ Order history table.
<div style="text-align: center">
<kbd style="display: inline-block; width: 80%; height: auto;">
 <img src="https://github.com/yurii-isaev/Ecommerce/assets/39811288/dd005db9-f2f6-49b0-85f7-d19a38c68838" alt="Order" style="width: 500px; height: auto;">
</kbd>
</div>

### ★ Order history details.
<div style="text-align: center">
<kbd style="display: inline-block; width: 80%; height: auto;">
 <img src="https://github.com/yurii-isaev/Ecommerce/assets/39811288/a1d858d0-584b-4182-ad85-239301f4433a" alt="Order" style="width: 500px; height: auto;">
</kbd>
</div>

### ★ Order history delete modal.
<div style="text-align: center">
<kbd style="display: inline-block; width: 80%; height: auto;">
 <img src="https://github.com/yurii-isaev/Ecommerce/assets/39811288/a27cc582-9c80-4fae-96dc-01dfca8bb7c2" alt="Order" style="width: 500px; height: auto;">
</kbd>
</div>

### ★ Order history table with filters and sorts.
<div style="text-align: center">
<kbd style="display: inline-block; width: 80%; height: auto;">
 <img src="https://github.com/yurii-isaev/Ecommerce/assets/39811288/f7ed08f0-09d1-46f8-a544-e1dc91547bab" alt="Order" style="width: 500px; height: auto;">
</kbd>
</div>

###  ⚡️ Table filters.
<div style="text-align: center">
<kbd style="display: inline-block; width: 80%; height: auto;">
 <img src="https://github.com/yurii-isaev/Ecommerce/assets/39811288/f36f7e05-d04b-480a-9e50-0884acf183eb" alt="Order" style="width: 500px; height: auto;">
</kbd>
</div>

###  ⚡️ Table filters.
<div style="text-align: center">
<kbd style="display: inline-block; width: 80%; height: auto;">
 <img src="https://github.com/yurii-isaev/Ecommerce/assets/39811288/8ab6d6c6-568f-4de9-ab42-1f678c5a43be" alt="Order" style="width: 500px; height: auto;">
</kbd>
</div>

###  ⚡️Table sort.
<div style="text-align: center">
<kbd style="display: inline-block; width: 80%; height: auto;">
 <img src="https://github.com/yurii-isaev/Ecommerce/assets/39811288/1e7847a0-8a8d-447d-9a9d-b51d45f05f7c" alt="Order" style="width: 500px; height: auto;">
</kbd>
</div>

## License
This project is unlicensed.