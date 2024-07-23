BankSystemBackend

Overview

BankSystemBackend is a backend service for managing bank operations such as account creation, transactions, and user management. It is built with a focus on scalability, security, and ease of integration with frontend systems.

Features

User Management: Create, update, and delete user accounts.

Account Management: Open, close, and manage bank accounts.

Transaction Management: Handle deposits, withdrawals, and transfers.

Authentication: Secure login and session management.

Technologies Used

Programming Language: C#

Framework: .NET Core


Database: SQL Server (or specify if another is used)

ORM: Entity Framework Core

Testing: xUnit

Getting Started

Prerequisites

.NET Core SDK

SQL Server


Installation

Clone the repository:

sh

Copy code

git clone https://github.com/iliakavan/BankSystemBackend.git
cd BankSystemBackend

Set up the database:

Update the connection string in appsettings.json.

Run migrations:

sh
Copy code

dotnet ef database update

Build and run the application:

sh
Copy code

dotnet build

dotnet run

Running Tests

Run the following command to execute the tests:

sh

Copy code

dotnet test

API Endpoints

User Endpoints

POST /api/users - Create a new user

GET /api/users/{id} - Get user details

PUT /api/users/{id} - Update user information

DELETE /api/users/{id} - Delete a user

Account Endpoints

POST /api/accounts - Open a new account

GET /api/accounts/{id} - Get account details

PUT /api/accounts/{id} - Update account information

DELETE /api/accounts/{id} - Close an account

Transaction Endpoints

POST /api/transactions/deposit - Deposit funds

POST /api/transactions/withdraw - Withdraw funds

POST /api/transactions/transfer - Transfer funds between accounts

