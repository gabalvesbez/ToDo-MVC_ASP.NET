-- Script gerado pelo Gerenciador de Pacotes do NuGet:

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Tarefa] (
    [Id] int NOT NULL IDENTITY,
    [UtilizadorId] nvarchar(450) NOT NULL,
    [Nome] nvarchar(max) NOT NULL,
    [Prioridade] nvarchar(max) NOT NULL,
    [Estado] nvarchar(max) NOT NULL,
    [CategoriaId] int NOT NULL,
    [DataCriacao] datetime2 NOT NULL,
    [DataLimite] datetime2 NOT NULL,
    CONSTRAINT [PK_Tarefa] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241204164153_TarefaCreate', N'8.0.11');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Comentario] (
    [Id] int NOT NULL IDENTITY,
    [TarefaId] int NOT NULL,
    [Comentarios] nvarchar(max) NOT NULL,
    [DataCriacao] datetime2 NOT NULL,
    CONSTRAINT [PK_Comentario] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241204171658_ComentarioCreate', N'8.0.11');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Categoria] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Categoria] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241204172106_CategoriaCreate', N'8.0.11');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Historia] (
    [Id] int NOT NULL IDENTITY,
    [TarefaId] int NOT NULL,
    [DataConclusao] datetime2 NOT NULL,
    CONSTRAINT [PK_Historia] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241204172244_HistoriaCreate', N'8.0.11');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Estatistica] (
    [Id] int NOT NULL IDENTITY,
    [UtilizadorId] int NOT NULL,
    [TarefaId] int NOT NULL,
    [DataAtualizacao] datetime2 NOT NULL,
    CONSTRAINT [PK_Estatistica] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241204172431_EstatisticaCreate', N'8.0.11');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tarefa]') AND [c].[name] = N'UtilizadorId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Tarefa] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Tarefa] ALTER COLUMN [UtilizadorId] nvarchar(450) NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tarefa]') AND [c].[name] = N'Prioridade');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Tarefa] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Tarefa] ALTER COLUMN [Prioridade] nvarchar(max) NULL;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tarefa]') AND [c].[name] = N'Estado');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Tarefa] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Tarefa] ALTER COLUMN [Estado] nvarchar(max) NULL;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tarefa]') AND [c].[name] = N'DataLimite');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Tarefa] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Tarefa] ALTER COLUMN [DataLimite] datetime2 NULL;
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tarefa]') AND [c].[name] = N'DataCriacao');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Tarefa] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Tarefa] ALTER COLUMN [DataCriacao] datetime2 NULL;
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tarefa]') AND [c].[name] = N'CategoriaId');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Tarefa] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Tarefa] ALTER COLUMN [CategoriaId] int NULL;
GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [Discriminator] nvarchar(13) NOT NULL,
    [PrimeiroNome] nvarchar(max) NULL,
    [Apelido] nvarchar(max) NULL,
    [UtilizadorAdmin] int NULL,
    [UltimoLogin] datetime2 NULL,
    [DataCriacao] datetime2 NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241213100257_Utilizador', N'8.0.11');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tarefa]') AND [c].[name] = N'UtilizadorId');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Tarefa] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [Tarefa] ALTER COLUMN [UtilizadorId] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250102004456_UpdateTarefaUtilizadorIdToString', N'8.0.11');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Categoria]') AND [c].[name] = N'Nome');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Categoria] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [Categoria] ALTER COLUMN [Nome] nvarchar(max) NULL;
GO

ALTER TABLE [Categoria] ADD [UtilizadorId] nvarchar(450) NULL;
GO

CREATE INDEX [IX_Categoria_UtilizadorId] ON [Categoria] ([UtilizadorId]);
GO

ALTER TABLE [Categoria] ADD CONSTRAINT [FK_Categoria_AspNetUsers_UtilizadorId] FOREIGN KEY ([UtilizadorId]) REFERENCES [AspNetUsers] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250104022602_AddUtilizadorIdToCategoria', N'8.0.11');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DROP TABLE [Comentario];
GO

DROP TABLE [Estatistica];
GO

DROP TABLE [Historia];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250107234425_RemoverComentariosEstatisticasHistorias', N'8.0.11');
GO

COMMIT;
GO

