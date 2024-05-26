CREATE DATABASE LaFlor

CREATE TABLE [dbo].[Flowers] (
    [flower_id]    INT            IDENTITY (1, 1) NOT NULL,
    [flower_name]  NVARCHAR (100) NOT NULL,
    [flower_price] INT            NULL,
    [flower_details] NVARCHAR (100) NOT NULL,
    [flower_image] NVARCHAR (255) NOT NULL,
    PRIMARY KEY CLUSTERED ([flower_id] ASC)
);

CREATE TABLE [dbo].[Customer] (
    [customer_id]       INT            IDENTITY (1, 1) NOT NULL,
    [customer_username] NVARCHAR (100) NOT NULL,
    [customer_email]    NVARCHAR (100) NOT NULL,
    [customer_password] NVARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([customer_id] ASC)
);

CREATE TABLE [dbo].[Cart] (
[cart_id] INT IDENTITY (1, 1) NOT NULL,
[cart_qty] INT NULL,
[cart_total] INT NULL,
[cart_status] NVARCHAR (20) NOT NULL,
[flower_id] INT NULL,
[customer_id] INT NULL,
PRIMARY KEY CLUSTERED ([cart_id] ASC),
FOREIGN KEY ([flower_id]) REFERENCES [dbo].[Flowers] ([flower_id]),
FOREIGN KEY ([customer_id]) REFERENCES [dbo].[Customer] ([customer_id])
);

CREATE TABLE [dbo].[Orders] (
[order_id] INT IDENTITY (1, 1) NOT NULL,
[order_name] NVARCHAR (100) NOT NULL,
[order_number] NVARCHAR (100) NOT NULL,
[order_email] NVARCHAR (100) NOT NULL,
[order_payment_method] NVARCHAR (100) NOT NULL,
[order_address1] NVARCHAR (100) NOT NULL,
[order_address2] NVARCHAR (100) NOT NULL,
[order_barangay] NVARCHAR (100) NOT NULL,
[order_city] NVARCHAR (100) NOT NULL,
[order_province] NVARCHAR (100) NOT NULL,
[order_zip] NVARCHAR (100) NOT NULL,
[order_placed_date] NVARCHAR (50) NOT NULL,
[order_total] INT NULL,
[order_status] NVARCHAR (20) NOT NULL,
[customer_id] INT NULL,
PRIMARY KEY CLUSTERED ([order_id] ASC),
FOREIGN KEY ([customer_id]) REFERENCES [dbo].[Customer] ([customer_id])
);