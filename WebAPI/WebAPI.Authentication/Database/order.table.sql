CREATE TABLE Orders
(
    Id       UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Subtotal DECIMAL(18, 2),
    Tax      DECIMAL(18, 2),
    Total    DECIMAL(18, 2),
    Discount DECIMAL(18, 2),
    Quantity INT,
    IsPaid   BIT
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


-- Работа с таблицами и зависимостями
SELECT name
FROM sys.foreign_keys
WHERE parent_object_id = OBJECT_ID('OrderDetails'); -- извлечение ключа зависимости
ALTER TABLE OrderDetails DROP CONSTRAINT FK__OrderDetails__Order__61F08603; -- удаление ключа зависимости
DROP TABLE OrderDetails; -- удаление зависимой таблицы

DROP TABLE Orders; -- удаление основной таблицы
