CREATE DATABASE LMS
USE LMS


CREATE TABLE Customers (
    customer_id INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(255),
    email_address VARCHAR(255),
    phone_number VARCHAR(15),
    address VARCHAR(255),
    credit_score INT
)

CREATE TABLE Loans (
    loan_id INT IDENTITY(1,1) PRIMARY KEY,
    customer_id INT,
    principal_amount DECIMAL(15, 2),
    interest_rate DECIMAL(5, 2),
    loan_term_months INT,
    loan_type VARCHAR(50) CHECK(loan_type IN ('CarLoan','HomeLoan')),
    loan_status VARCHAR(20) CHECK (loan_status IN ('Pending', 'Approved')),
    FOREIGN KEY (customer_id) REFERENCES Customers(customer_id)
)

INSERT INTO Customers (name, email_address, phone_number, address, credit_score)
VALUES
    ('John Doe', 'john.doe@email.com', '123-456-7890', '123 Main St, Cityville', 750),
    ('Jane Smith', 'jane.smith@email.com', '987-654-3210', '456 Oak St, Townsville', 800),
    ('Bob Johnson', 'bob.johnson@email.com', '555-123-4567', '789 Pine St, Villageton', 700),
    ('Alice Williams', 'alice.williams@email.com', '222-333-4444', '987 Elm St, Hamletville', 820),
    ('Charlie Brown', 'charlie.brown@email.com', '111-222-3333', '654 Birch St, Burgville', 680)

INSERT INTO Loans (customer_id, principal_amount, interest_rate, loan_term_months, loan_type, loan_status)
VALUES
    (1, 50000.00, 5.0, 36, 'CarLoan', 'Pending'),
    (2, 200000.00, 4.5, 60, 'HomeLoan', 'Approved'),
    (3, 75000.00, 6.2, 48, 'CarLoan', 'Pending'),
    (4, 300000.00, 3.8, 72, 'HomeLoan', 'Approved'),
    (5, 60000.00, 5.5, 24, 'CarLoan', 'Approved')





