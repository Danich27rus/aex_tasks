CREATE TABLE Managers (
	ID int IDENTITY (1, 1) PRIMARY KEY,
	Name varchar(50)
)

CREATE TABLE Customers (
	ID int IDENTITY (1, 1) PRIMARY KEY,
	Name varchar(50),
	ManagerID int,
	CONSTRAINT FK_CustomerManager FOREIGN KEY (ManagerID)
    REFERENCES Managers(ID)
)

CREATE TABLE Orders (
	ID int IDENTITY (1, 1) PRIMARY KEY,
	Date datetime,
	Amount int,
	CustomerID int,
	CONSTRAINT FK_OrderCustomer FOREIGN KEY (CustomerID)
    REFERENCES Customers(ID)
)

INSERT INTO Managers VALUES 
('Роман'),
('Анеглина'),
('Андрей')

INSERT INTO Customers VALUES
('Даниил', 1),
('Михаил', 2),
('Семён', 3)

INSERT INTO Orders VALUES
('01-01-2023 10:00:00',	500, 1),
('30-12-2022 10:00:00',	1000, 1),
('30-12-2022 10:00:00',	1000, 1),
('01-01-2023 10:00:00',	200, 2),
('30-12-2022 10:00:00',	1000, 1),
('01-01-2023 10:00:00',	800, 3),
('01-01-2023 10:00:00',	1000, 1),
('01-01-2023 10:00:00',	800, 3);


WITH Result AS (
    SELECT Amount, Customers.Name AS CustomersName, Managers.Name AS ManagersName 
    FROM Orders
		right join Customers ON Orders.CustomerID = Customers.ID
		right join Managers ON Customers.ManagerID = Managers.ID
    WHERE Date > '2023-01-01 00:00:00'
)
SELECT SUM(Amount) AS ResultValue, CustomersName, ManagersName
FROM Result
GROUP BY CustomersName, ManagersName
HAVING SUM(Amount) > 1000;