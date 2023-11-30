CREATE DATABASE QuanLySinhVien_6868;
GO

USE QuanLySinhVien_6868;
GO

CREATE TABLE Professors (
    ProfessorId INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Department NVARCHAR(50) NOT NULL
);

CREATE TABLE Students (
    StudentId INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Dob DATE,
    GPA DECIMAL(3,2),
    AdvisorId INT,
);

CREATE TABLE Courses (
    CourseId INT IDENTITY(1,1) PRIMARY KEY,
    CourseName NVARCHAR(100) NOT NULL,
    Credits INT NOT NULL,
    ProfessorId INT,
);


CREATE TABLE Users (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Username NVARCHAR(100) NOT NULL,
        PasswordHash NVARCHAR(100) NOT NULL
);

INSERT INTO Users (Username, PasswordHash) VALUES ('admin123','Admin@123');


-- Thêm dữ liệu vào bảng Professors
INSERT INTO Professors (FirstName, LastName, Department)
VALUES
(N'Nguyễn', N'Văn A', N'Khoa Kỹ thuật'),
(N'Trần', N'Thị B', N'Khoa Kinh tế'),
(N'Lê', N'Hoàng C', N'Khoa Sư phạm'),
(N'Phạm', N'Văn D', N'Khoa Y tế'),
(N'Hoàng', N'Thị E', N'Khoa Ngoại ngữ');

-- Thêm dữ liệu vào bảng Students
INSERT INTO Students (FirstName, LastName, Dob, GPA, AdvisorId)
VALUES
(N'Nguyễn', N'Văn X', '2000-01-01', 3.2, 1),
(N'Trần', N'Thị Y', '2001-02-03', 3.5, 2),
(N'Lê', N'Hoàng Z', '2002-05-15', 3.8, 1),
(N'Phạm', N'Văn K', '2003-08-20', 3.2, 3),
(N'Hoàng', N'Thị M', '2004-11-30', 3.9, 2);

-- Thêm dữ liệu vào bảng Courses
INSERT INTO Courses (CourseName, Credits, ProfessorId)
VALUES
(N'Công nghệ phần mềm', 3, 1),
(N'Kinh tế học', 4, 2),
(N'Toán cao cấp', 3, 3),
(N'Y học cơ sở', 4, 4),
(N'Tiếng Anh giao tiếp', 2, 5);
