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
GO

CREATE TABLE Products_New
(
    -- можно писать свой уникальный GIUD так и не писать ничего
    -- и тогда база данных сама по умолчанию генерировала нужный для неё GIUD
    Id         UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(), -- изменение порядка лоступа к столбцу
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
GO

INSERT INTO Products_New (Name, Image, ImageSlice, Article, Avalible, Category, Filling, Weight, Tier, Price)
SELECT Name,
       Image,
       ImageSlice,
       Article,
       Avalible,
       Category,
       Filling,
       Weight,
       Tier,
       Price
FROM Products;
GO

-- Удаляем старую таблицу данных 
DROP TABLE Products;
-- Переименовываем новую таблицу данных
EXEC sp_rename 'Products_New', 'Products';

-- Content Initial
INSERT INTO Products (Name, Image, ImageSlice, Article, Avalible, Category, Filling, Weight, Tier, Price)
VALUES ('Three Chocolate Cake',
        'three_chocolate_cake.jpg',
        'three_chocolate_cake_slice.jpg',
        '000523',
        1,
        'cake',
        'chocolate',
        1400,
        1,
        100.234234234);
GO
--
INSERT INTO Products (Name, Image, ImageSlice, Article, Avalible, Category, Filling, Weight, Tier, Price)
VALUES ('Tired Pear Cake',
        'tired_pear_cake.jpg',
        'tired_pear_cake_slice.jpg',
        '000405',
        1,
        'cake',
        'fruits',
        1400,
        1,
        200.23412341);
GO
--
INSERT INTO Products (Name, Image, ImageSlice, Article, Avalible, Category, Filling, Weight, Tier, Price)
VALUES ('Wild Berries Cake',
        'wild_berries_cake.jpg',
        'wild_berries_cake_slice.jpg',
        '001507',
        1,
        'cake',
        'fruits and berries',
        1400,
        1,
        8300.2364125);
GO
--
INSERT INTO Products (Name, Image, ImageSlice, Article, Avalible, Category, Filling, Weight, Tier, Price)
VALUES ('Raffaello Cake',
        'raffaello_cake.jpg',
        'raffaello_cake_slice.jpg',
        '000972',
        1,
        'cake',
        'coconut chips',
        1400,
        1,
        300);
GO
--
INSERT INTO Products (Name, Image, ImageSlice, Article, Avalible, Category, Filling, Weight, Tier, Price)
VALUES ('Lavender-blueberry Cake',
        'lavender-blueberry_cake.jpg',
        'lavender-blueberry_cake_slice.jpg',
        '0001475',
        1,
        'cake',
        'fruits',
        1400,
        1,
        300);
GO
--
INSERT INTO Products (Name, Image, ImageSlice, Article, Avalible, Category, Filling, Weight, Tier, Price)
VALUES ('Snickers Cake',
        'snickers_cake.jpg',
        'snickers_cake_slice.jpg',
        '0001436',
        1,
        'cake',
        'nuts and nougat',
        1400,
        1,
        300);
GO
--
INSERT INTO Products (Name, Image, ImageSlice, Article, Avalible, Category, Filling, Weight, Tier, Price)
VALUES ('Meringue Roll',
        'meringue_roll.jpg',
        'meringue_roll_slice.jpg',
        '0001437',
        1,
        'roll',
        'nuts and nougat',
        1400,
        1,
        500);
GO
--
INSERT INTO Products (Name, Image, ImageSlice, Article, Avalible, Category, Filling, Weight, Tier, Price)
VALUES ('Cherry Pie',
        'cherry_pie.jpg',
        'cherry_pie_slice.jpg',
        '0001438',
        1,
        'pie',
        'nuts and nougat',
        1400,
        1,
        3100);
GO
