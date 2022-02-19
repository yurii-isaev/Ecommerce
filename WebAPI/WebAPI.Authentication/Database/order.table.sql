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

CREATE TABLE OrderItems
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

CREATE TABLE CardPayments
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
    FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id)
);