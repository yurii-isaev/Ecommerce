CREATE TABLE Products
(
    Id         UNIQUEIDENTIFIER PRIMARY KEY,
    Name       NVARCHAR(100)  NOT NULL,
    Image      VARCHAR(100)   NOT NULL,
    ImageSlice VARCHAR(100)   NOT NULL,
    Article    VARCHAR(50)    NOT NULL,
    Avalible   BIT            NOT NULL,
    Category   NVARCHAR(50)   NOT NULL,
    Filling    NVARCHAR(50)   NOT NULL,
    Weight     INT            NOT NULL,
    Tier       INT            NOT NULL,
    Price      DECIMAL(18, 9) NOT NULL
);

-- Content Initial
INSERT INTO Products (Id, Name, Image, ImageSlice, Article, Avalible,
                      Category, Filling, Weight, Tier, Price)
VALUES ('61f0c404-5cb3-11e7-907b-a6006ad3dba0',
        'Three Chocolate Cake',
        'three_chocolate_cake.jpg',
        'three_chocolate_cake_slice.jpg',
        '000523',
        1, 'cake', 'chocolate', 1400, 1, 100.234234234);