CREATE TABLE ToDoItem (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(255) NOT NULL,
    IsComplete BOOLEAN NOT NULL DEFAULT 0,
    Time INT DEFAULT 1 CHECK (Time BETWEEN 1 AND 8),
    Priority INT DEFAULT 5 CHECK (Priority BETWEEN 1 AND 5)
);
