CREATE TABLE [dbo].[Employees] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [FIO]           NVARCHAR (MAX) NULL,
    [id_department] INT            NOT NULL,
    [Salary]        DECIMAL          DEFAULT ((0)) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Employees_ToDepartmenr] FOREIGN KEY ([id_department]) REFERENCES [dbo].[Department] ([Id]) ON DELETE CASCADE
);

