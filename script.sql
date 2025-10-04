CREATE DATABASE CRUD_API_REST
USE CRUD_API_REST

CREATE TABLE Users
(
    id INT PRIMARY KEY IDENTITY(1,1),
    name VARCHAR(100) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    created_at DATETIME DEFAULT GETDATE()
);

CREATE TABLE Posts
(
    id INT PRIMARY KEY IDENTITY(1,1),
    title VARCHAR(100) NOT NULL,
    content VARCHAR(1000) NOT NULL,
    created_at DATETIME DEFAULT GETDATE()
);

CREATE TABLE Comments
(
    id INT PRIMARY KEY IDENTITY(1,1),
    content VARCHAR(1000) NOT NULL,
    created_at DATETIME DEFAULT GETDATE(),
    post_id INT NOT NULL,
    FOREIGN KEY (post_id) REFERENCES Posts(id)
);

CREATE PROCEDURE CreateUser
    @name VARCHAR(100),
    @email VARCHAR(100)
AS
BEGIN
    INSERT INTO Users
        (name, email)
    VALUES
        (@name, @email);
END

CREATE PROCEDURE DeleteUser
    @id INT
AS
BEGIN
    DELETE FROM Users WHERE id = @id;
END

CREATE PROCEDURE GETUsers
AS
BEGIN
    SELECT *
    FROM Users;
END

CREATE PROCEDURE UPDATEUser
    @id INT,
    @name VARCHAR(100),
    @email VARCHAR(100)
AS
BEGIN
    UPDATE Users
    SET name = @name, email = @email
    WHERE id = @id;
END




CREATE PROCEDURE CreatePost
    @title VARCHAR(100),
    @content VARCHAR(1000)
AS
BEGIN
    INSERT INTO Posts
        (title, content)
    VALUES
        (@title, @content);
END

CREATE PROCEDURE DeletePost
    @id INT
AS
BEGIN
    DELETE FROM Posts WHERE id = @id;
END

CREATE PROCEDURE GETPosts
AS
BEGIN
    SELECT *
    FROM Posts;
END

CREATE PROCEDURE UPDATEPost
    @id INT,
    @title VARCHAR(100),
    @content VARCHAR(1000)
AS
BEGIN
    UPDATE Posts
    SET title = @title, content = @content
    WHERE id = @id;
END


CREATE PROCEDURE CreateComment
    @content VARCHAR(1000),
    @post_id INT
AS
BEGIN
    INSERT INTO Comments
        (content, post_id)
    VALUES
        (@content, @post_id);
END

CREATE PROCEDURE DeleteComment
    @id INT
AS
BEGIN
    DELETE FROM Comments WHERE id = @id;
END

CREATE PROCEDURE GETComments
AS
BEGIN
    SELECT *
    FROM Comments;
END

CREATE PROCEDURE UPDATEComment
    @id INT,
    @content VARCHAR(1000),
    @post_id INT
AS
BEGIN
    UPDATE Comments
    SET content = @content, post_id = @post_id
    WHERE id = @id;
END