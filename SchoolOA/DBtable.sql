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

CREATE TABLE [Courses] (
    [Id] int NOT NULL IDENTITY,
    [CourseName] nvarchar(100) NOT NULL,
    [Textbook] nvarchar(100) NULL,
    CONSTRAINT [PK_Courses] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Majors] (
    [Id] int NOT NULL IDENTITY,
    [Department] nvarchar(100) NOT NULL,
    [Grade] nvarchar(100) NOT NULL,
    [MajorName] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_Majors] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Teachers] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [TeacherNumber] nvarchar(20) NOT NULL,
    [TeacherStatus] nvarchar(20) NULL,
    [TeacherComment] nvarchar(1000) NULL,
    [PhoneNumber] nvarchar(20) NULL,
    [IsMentor] bit NOT NULL,
    CONSTRAINT [PK_Teachers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Enroll] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [Sex] nvarchar(10) NOT NULL,
    [IdentityCardNumber] nvarchar(18) NOT NULL,
    [HomeAddress] nvarchar(500) NOT NULL,
    [PhoneNumber] nvarchar(20) NOT NULL,
    [Portrait] nvarchar(3000) NULL,
    [MajorId] int NOT NULL,
    [RequestResult] nvarchar(20) NULL,
    CONSTRAINT [PK_Enroll] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Enroll_Majors_MajorId] FOREIGN KEY ([MajorId]) REFERENCES [Majors] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Classes] (
    [Id] int NOT NULL IDENTITY,
    [ClassNumber] nvarchar(20) NOT NULL,
    [MajorId] int NOT NULL,
    [MentorId] int NOT NULL,
    CONSTRAINT [PK_Classes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Classes_Majors_MajorId] FOREIGN KEY ([MajorId]) REFERENCES [Majors] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Classes_Teachers_MentorId] FOREIGN KEY ([MentorId]) REFERENCES [Teachers] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [TeacherAccounts] (
    [Id] int NOT NULL IDENTITY,
    [TeacherId] int NOT NULL,
    [AccountName] nvarchar(100) NOT NULL,
    [Password] nvarchar(20) NOT NULL,
    [AccountStatus] nvarchar(20) NULL,
    [IsAdminAccount] bit NOT NULL,
    CONSTRAINT [PK_TeacherAccounts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TeacherAccounts_Teachers_TeacherId] FOREIGN KEY ([TeacherId]) REFERENCES [Teachers] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [TeacherReceivedAward] (
    [Id] int NOT NULL IDENTITY,
    [AwardName] nvarchar(100) NOT NULL,
    [AwardDate] datetime NOT NULL,
    [Detail] nvarchar(1000) NULL,
    [TeacherId] int NOT NULL,
    CONSTRAINT [PK_TeacherReceivedAward] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TeacherReceivedAward_Teachers_TeacherId] FOREIGN KEY ([TeacherId]) REFERENCES [Teachers] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [CourseResponsibleByTeacher] (
    [Id] int NOT NULL IDENTITY,
    [TeacherId] int NOT NULL,
    [CourseId] int NOT NULL,
    [ClassId] int NULL,
    [Semester] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_CourseResponsibleByTeacher] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CourseResponsibleByTeacher_Classes_ClassId] FOREIGN KEY ([ClassId]) REFERENCES [Classes] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_CourseResponsibleByTeacher_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_CourseResponsibleByTeacher_Teachers_TeacherId] FOREIGN KEY ([TeacherId]) REFERENCES [Teachers] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Students] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [Sex] nvarchar(10) NOT NULL,
    [IdentityCardNumber] nvarchar(18) NOT NULL,
    [StudentNumber] nvarchar(20) NOT NULL,
    [StudentStatus] nvarchar(20) NULL,
    [HomeAddress] nvarchar(500) NOT NULL,
    [PhoneNumber] nvarchar(20) NOT NULL,
    [Portrait] nvarchar(3000) NULL,
    [MajorId] int NOT NULL,
    [Apartment] nvarchar(20) NULL,
    [Chamber] nvarchar(20) NULL,
    [Bed] nvarchar(20) NULL,
    [ClassId] int NOT NULL,
    CONSTRAINT [PK_Students] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Students_Classes_ClassId] FOREIGN KEY ([ClassId]) REFERENCES [Classes] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Students_Majors_MajorId] FOREIGN KEY ([MajorId]) REFERENCES [Majors] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [CourseSchedule] (
    [Id] int NOT NULL IDENTITY,
    [CourseResponsibleByTeacherId] int NOT NULL,
    [ScheduledWeekday] nvarchar(20) NOT NULL,
    [ScheduledTime] nvarchar(20) NOT NULL,
    CONSTRAINT [PK_CourseSchedule] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CourseSchedule_CourseResponsibleByTeacher_CourseResponsibleByTeacherId] FOREIGN KEY ([CourseResponsibleByTeacherId]) REFERENCES [CourseResponsibleByTeacher] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [CourseSelection] (
    [Id] int NOT NULL IDENTITY,
    [StudentId] int NOT NULL,
    [CourseResponsibleByTeacherId] int NOT NULL,
    CONSTRAINT [PK_CourseSelection] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CourseSelection_CourseResponsibleByTeacher_CourseResponsibleByTeacherId] FOREIGN KEY ([CourseResponsibleByTeacherId]) REFERENCES [CourseResponsibleByTeacher] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_CourseSelection_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Students] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Examinations] (
    [Id] int NOT NULL IDENTITY,
    [Semester] nvarchar(100) NOT NULL,
    [MajorId] int NOT NULL,
    [CourseId] int NOT NULL,
    [StudentId] int NOT NULL,
    [Score] decimal(5,2) NOT NULL,
    CONSTRAINT [PK_Examinations] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Examinations_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Examinations_Majors_MajorId] FOREIGN KEY ([MajorId]) REFERENCES [Majors] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Examinations_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Students] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_Classes_MajorId] ON [Classes] ([MajorId]);
GO

CREATE INDEX [IX_Classes_MentorId] ON [Classes] ([MentorId]);
GO

CREATE INDEX [IX_CourseResponsibleByTeacher_ClassId] ON [CourseResponsibleByTeacher] ([ClassId]);
GO

CREATE INDEX [IX_CourseResponsibleByTeacher_CourseId] ON [CourseResponsibleByTeacher] ([CourseId]);
GO

CREATE INDEX [IX_CourseResponsibleByTeacher_TeacherId] ON [CourseResponsibleByTeacher] ([TeacherId]);
GO

CREATE INDEX [IX_CourseSchedule_CourseResponsibleByTeacherId] ON [CourseSchedule] ([CourseResponsibleByTeacherId]);
GO

CREATE INDEX [IX_CourseSelection_CourseResponsibleByTeacherId] ON [CourseSelection] ([CourseResponsibleByTeacherId]);
GO

CREATE INDEX [IX_CourseSelection_StudentId] ON [CourseSelection] ([StudentId]);
GO

CREATE INDEX [IX_Enroll_MajorId] ON [Enroll] ([MajorId]);
GO

CREATE INDEX [IX_Examinations_CourseId] ON [Examinations] ([CourseId]);
GO

CREATE INDEX [IX_Examinations_MajorId] ON [Examinations] ([MajorId]);
GO

CREATE INDEX [IX_Examinations_StudentId] ON [Examinations] ([StudentId]);
GO

CREATE INDEX [IX_Students_ClassId] ON [Students] ([ClassId]);
GO

CREATE INDEX [IX_Students_MajorId] ON [Students] ([MajorId]);
GO

CREATE UNIQUE INDEX [IX_TeacherAccounts_TeacherId] ON [TeacherAccounts] ([TeacherId]);
GO

CREATE INDEX [IX_TeacherReceivedAward_TeacherId] ON [TeacherReceivedAward] ([TeacherId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220114121531_InitialDB', N'5.0.12');
GO

COMMIT;
GO

