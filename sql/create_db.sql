-- SQL script to create Users and Products tables (MS SQL)
CREATE DATABASE ECommerceDb;
GO
USE ECommerceDb;
GO
CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(200) NOT NULL,
    Email NVARCHAR(200) NOT NULL UNIQUE,
    LastLogin DATETIME2 NULL,
    RefreshToken NVARCHAR(500) NULL,
    RefreshTokenExpiry DATETIME2 NULL
);
CREATE TABLE Products (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Category NVARCHAR(100) NOT NULL,
    ProductCode NVARCHAR(50) NOT NULL UNIQUE,
    Name NVARCHAR(200) NOT NULL,
    ImagePath NVARCHAR(500) NULL,
    Price DECIMAL(18,2) NOT NULL,
    MinimumQuantity INT NOT NULL,
    DiscountRate FLOAT NOT NULL
);
