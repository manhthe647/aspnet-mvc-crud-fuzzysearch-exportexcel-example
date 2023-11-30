CREATE DATABASE IF NOT EXISTS QuanLySinhVien_6868;
USE QuanLySinhVien_6868;

CREATE TABLE Professors (
    ProfessorId INT AUTO_INCREMENT PRIMARY KEY,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    Department VARCHAR(50) NOT NULL
);

CREATE TABLE Students (
    StudentId INT AUTO_INCREMENT PRIMARY KEY,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    Dob DATE,
    GPA DECIMAL(3,2),
    AdvisorId INT,
    
);

CREATE TABLE Courses (
    CourseId INT AUTO_INCREMENT PRIMARY KEY,
    CourseName VARCHAR(100) NOT NULL,
    Credits INT NOT NULL,
    ProfessorId INT,
    
);

CREATE TABLE Users (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Username VARCHAR(100) NOT NULL,
    PasswordHash VARCHAR(100) NOT NULL
);

INSERT INTO Users (Username, PasswordHash) VALUES ('admin123', 'Admin@123');

-- Thêm dữ liệu vào bảng Professors
INSERT INTO Professors (FirstName, LastName, Department)
VALUES
('Nguyễn', 'Văn A', 'Khoa Kỹ thuật'),
('Trần', 'Thị B', 'Khoa Kinh tế'),
('Lê', 'Hoàng C', 'Khoa Sư phạm'),
('Phạm', 'Văn D', 'Khoa Y tế'),
('Hoàng', 'Thị E', 'Khoa Ngoại ngữ');

-- Thêm dữ liệu vào bảng Students
INSERT INTO Students (FirstName, LastName, Dob, GPA, AdvisorId)
VALUES
('Nguyễn', 'Văn X', '2000-01-01', 3.2, 1),
('Trần', 'Thị Y', '2001-02-03', 3.5, 2),
('Lê', 'Hoàng Z', '2002-05-15', 3.8, 1),
('Phạm', 'Văn K', '2003-08-20', 3.2, 3),
('Hoàng', 'Thị M', '2004-11-30', 3.9, 2);

-- Thêm dữ liệu vào bảng Courses
INSERT INTO Courses (CourseName, Credits, ProfessorId)
VALUES
('Công nghệ phần mềm', 3, 1),
('Kinh tế học', 4, 2),
('Toán cao cấp', 3, 3),
('Y học cơ sở', 4, 4),
('Tiếng Anh giao tiếp', 2, 5);