CREATE TABLE [_users] (
    [Id] uniqueidentifier NOT NULL,
    [City] nvarchar(max) NULL,
    [Country] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [Login] nvarchar(max) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [PasswordHash] varbinary(max) NOT NULL,
    [PasswordSalt] varbinary(max) NOT NULL,
    [Phone] nvarchar(max) NULL,
    [Street] nvarchar(max) NULL,
    [Zip] nvarchar(max) NULL,
    CONSTRAINT [PK__users] PRIMARY KEY ([Id])
);