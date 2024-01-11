USE [master];
GO

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'Squadmakers')
BEGIN
    /****** Object:  Database [Squadmakers]    Script Date: 11/01/2024 19:51:24 ******/
    CREATE DATABASE [Squadmakers] CONTAINMENT = NONE
    ON PRIMARY
           (
               NAME = N'Squadmakers',
               FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Squadmakers.mdf',
               SIZE = 8192KB,
               MAXSIZE = UNLIMITED,
               FILEGROWTH = 65536KB
           )
    LOG ON
        (
            NAME = N'Squadmakers_log',
            FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Squadmakers_log.ldf',
            SIZE = 8192KB,
            MAXSIZE = 2048GB,
            FILEGROWTH = 65536KB
        )
    WITH CATALOG_COLLATION=DATABASE_DEFAULT, LEDGER=OFF;

    IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
    BEGIN
        EXEC [Squadmakers].[dbo].[sp_fulltext_database] @action = 'enable';
    END;
END;