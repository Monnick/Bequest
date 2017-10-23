IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [_pictures] (
    [Id] uniqueidentifier NOT NULL,
    [Data] varbinary(max) NULL,
    [FileName] nvarchar(max) NULL,
    [ProjectId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK__pictures] PRIMARY KEY ([Id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20171023180443_InitialMigration', N'2.0.0-rtm-26452');

GO

