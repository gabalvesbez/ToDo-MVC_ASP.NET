DROP DATABASE IF EXISTS ToDo;
CREATE DATABASE IF NOT EXISTS ToDo;
USE ToDo;

CREATE TABLE `AspNetRoles` (
    `Id` VARCHAR(450) NOT NULL,
    `ConcurrencyStamp` TEXT NULL,
    `Name` VARCHAR(256) NULL,
    `NormalizedName` VARCHAR(256) NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `AspNetRoleClaims` (
    `Id` INT NOT NULL AUTO_INCREMENT,
    `ClaimType` TEXT NULL,
    `ClaimValue` TEXT NULL,
    `RoleId` VARCHAR(450) NOT NULL,
    PRIMARY KEY (`Id`),
    FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AspNetUsers` (
    `Id` VARCHAR(450) NOT NULL,
    `AccessFailedCount` INT NOT NULL,
    `ConcurrencyStamp` TEXT NULL,
    `Email` VARCHAR(256) NULL,
    `EmailConfirmed` BOOLEAN NOT NULL,
    `LockoutEnabled` BOOLEAN NOT NULL,
    `LockoutEnd` DATETIME NULL,
    `NormalizedEmail` VARCHAR(256) NULL,
    `NormalizedUserName` VARCHAR(256) NULL,
    `PasswordHash` TEXT NULL,
    `PhoneNumber` TEXT NULL,
    `PhoneNumberConfirmed` BOOLEAN NOT NULL,
    `SecurityStamp` TEXT NULL,
    `TwoFactorEnabled` BOOLEAN NOT NULL,
    `UserName` VARCHAR(256) NULL,
    `Discriminator` VARCHAR(13) NOT NULL,
    `Apelido` TEXT NULL,
    `DataCriacao` DATETIME NULL,
    `PrimeiroNome` TEXT NULL,
    `UltimoLogin` DATETIME NULL,
    `UtilizadorAdmin` INT NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `AspNetUserClaims` (
    `Id` INT NOT NULL AUTO_INCREMENT,
    `ClaimType` TEXT NULL,
    `ClaimValue` TEXT NULL,
    `UserId` VARCHAR(450) NOT NULL,
    PRIMARY KEY (`Id`),
    FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AspNetUserLogins` (
    `LoginProvider` VARCHAR(450) NOT NULL,
    `ProviderKey` VARCHAR(450) NOT NULL,
    `ProviderDisplayName` TEXT NULL,
    `UserId` VARCHAR(450) NOT NULL,
    PRIMARY KEY (`LoginProvider`, `ProviderKey`),
    FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AspNetUserRoles` (
    `UserId` VARCHAR(450) NOT NULL,
    `RoleId` VARCHAR(450) NOT NULL,
    PRIMARY KEY (`UserId`, `RoleId`),
    FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE,
    FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AspNetUserTokens` (
    `UserId` VARCHAR(450) NOT NULL,
    `LoginProvider` VARCHAR(450) NOT NULL,
    `Name` VARCHAR(450) NOT NULL,
    `Value` TEXT NULL,
    PRIMARY KEY (`UserId`, `LoginProvider`, `Name`),
    FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `Categoria` (
    `Id` INT NOT NULL AUTO_INCREMENT,
    `Nome` TEXT NULL,
    `UtilizadorId` VARCHAR(450) NULL,
    PRIMARY KEY (`Id`),
    FOREIGN KEY (`UtilizadorId`) REFERENCES `AspNetUsers` (`Id`)
);

CREATE TABLE `Tarefa` (
    `Id` INT NOT NULL AUTO_INCREMENT,
    `CategoriaId` INT NULL,
    `DataCriacao` DATETIME NULL,
    `DataLimite` DATETIME NULL,
    `Estado` TEXT NULL,
    `Nome` TEXT NULL,
    `Prioridade` TEXT NULL,
    `UtilizadorId` TEXT NULL,
    PRIMARY KEY (`Id`),
    FOREIGN KEY (`CategoriaId`) REFERENCES `Categoria` (`Id`)
);

CREATE INDEX `RoleNameIndex` ON `AspNetRoles` (`NormalizedName`);
CREATE INDEX `EmailIndex` ON `AspNetUsers` (`NormalizedEmail`);
CREATE INDEX `UserNameIndex` ON `AspNetUsers` (`NormalizedUserName`);
CREATE INDEX `IX_AspNetRoleClaims_RoleId` ON `AspNetRoleClaims` (`RoleId`);
CREATE INDEX `IX_AspNetUserClaims_UserId` ON `AspNetUserClaims` (`UserId`);
CREATE INDEX `IX_AspNetUserLogins_UserId` ON `AspNetUserLogins` (`UserId`);
CREATE INDEX `IX_AspNetUserRoles_RoleId` ON `AspNetUserRoles` (`RoleId`);
CREATE INDEX `IX_Categoria_UtilizadorId` ON `Categoria` (`UtilizadorId`);
CREATE INDEX `IX_Tarefa_CategoriaId` ON `Tarefa` (`CategoriaId`);
