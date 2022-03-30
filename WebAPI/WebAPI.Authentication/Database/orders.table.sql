CREATE TABLE Orders
(
    Id        UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),   -- Auto UID
    UserId    NVARCHAR(450),
    Subtotal  DECIMAL(18, 2),
    Tax       DECIMAL(18, 2),
    Total     DECIMAL(18, 2),
    Discount  DECIMAL(18, 2),
    Quantity  INT,
    IsPaid    BIT,
    CreatedAt DATETIME                     DEFAULT GETDATE(), -- Auto date
    FOREIGN KEY (UserId) REFERENCES AspNetUsers (Id)
);

CREATE TABLE OrderAddress
(
    Id                 UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    OrderId            UNIQUEIDENTIFIER,
    FullName           NVARCHAR(100) NOT NULL,
    Address            NVARCHAR(255) NOT NULL,
    City               NVARCHAR(100) NOT NULL,
    State              NVARCHAR(100) NOT NULL,
    ZipCode            NVARCHAR(20)  NOT NULL,
    ConsentPrivateData BIT           NOT NULL,
    FOREIGN KEY (OrderId) REFERENCES Orders (Id)
);

CREATE TABLE OrderDetails
(
    Id         UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    OrderId    UNIQUEIDENTIFIER,
    Article    VARCHAR(MAX),
    Avalible   BIT,
    Category   VARCHAR(MAX),
    Filling    VARCHAR(MAX),
    Image      VARCHAR(MAX),
    ImageSlice VARCHAR(MAX),
    Name       VARCHAR(MAX),
    Price      DECIMAL(18, 2),
    Quantity   INT,
    Tier       INT,
    Weight     INT,
    FOREIGN KEY (OrderId) REFERENCES Orders (Id)
);

CREATE TABLE OrderCardPayments
(
    Id         UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    OrderId    UNIQUEIDENTIFIER,
    UserId     NVARCHAR(450),
    CardHolder VARCHAR(255),
    CardNumber VARCHAR(50),
    ExpMonth   VARCHAR(2),
    ExpYear    INT,
    Cvv        VARCHAR(4),
    FOREIGN KEY (OrderId) REFERENCES Orders (Id),
    FOREIGN KEY (UserId) REFERENCES AspNetUsers (Id)
);

-- Чтобы добавить поле CreatedAt к уже существующей таблице Orders,
-- использовать оператор ALTER TABLE в SQL:
ALTER TABLE Orders ADD CreatedAt DATETIME DEFAULT GETDATE();

-- Работа с таблицами и зависимостями:
-- проверка ключей на которые ссылается таблица 'Orders'
SELECT name
FROM sys.foreign_keys
WHERE parent_object_id = OBJECT_ID('Orders'); 
-- Удаление ключа зависимости
ALTER TABLE Orders
DROP CONSTRAINT FK__Orders__UserId__147C05D0; 

-- проверка ключей которые ссылаются на таблицу 'Orders'
SELECT name
FROM sys.foreign_keys
WHERE referenced_object_id = OBJECT_ID('Orders');

-- Далее для каждого найденного имени внешнего ключа выполняем отключение
ALTER TABLE OrderAddress      DROP CONSTRAINT FK__OrderAddr__Order__184C96B4;
ALTER TABLE OrderDetails      DROP CONSTRAINT FK__OrderDeta__Order__1C1D2798;
ALTER TABLE OrderCardPayments DROP CONSTRAINT FK__OrderCard__Order__1FEDB87C;
-- Успешно очищаем таблицу
TRUNCATE TABLE Orders;

-- Восстанавливаем зависисмость ключей
ALTER TABLE OrderAddress
ADD CONSTRAINT FK__OrderAddr__Order__184C96B4
FOREIGN KEY (OrderId) REFERENCES Orders(Id);

ALTER TABLE OrderDetails
ADD CONSTRAINT FK__OrderDeta__Order__1C1D2798
FOREIGN KEY (OrderId) REFERENCES Orders(Id);

ALTER TABLE OrderCardPayments
ADD CONSTRAINT FK__OrderCard__Order__1FEDB87C
FOREIGN KEY (OrderId) REFERENCES Orders(Id);

-- Удаляет все строки из таблицы, но сохраняет структуру таблицы
TRUNCATE TABLE OrderDetails;
TRUNCATE TABLE OrderCardPayments;
TRUNCATE TABLE OrderAddress;
