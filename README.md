Overview

The Online Banking System is a simple web-based banking application built using ASP.NET MVC and Entity Framework with a SQLite database

It allows users to register, log in, manage their accounts, deposit or withdraw funds, and view their transaction history — all through a clean and user-friendly dashboard.

Key Features

- User Registration & Profile: Allow new users to create an account with name, email and password; store minimal profile details and initial balance so they can log in and use the app

- Account Dashboard: A single-page overview that shows the user’s current balance, quick action buttons (Deposit / Withdraw / Transfer), and a summary of recent activity for fast decision-making

- Deposit Functionality: Users can add funds to their account through a simple form; every deposit updates the balance and creates a transaction record with timestamp and resulting balance

- Withdraw Functionality: Users can withdraw funds (with validation to prevent overdraft); withdrawals update the balance and generate a corresponding transaction entry for an audit trail

- Transaction History: A chronological table of all transactions (date, type, amount, balance after) so users can review past activity and verify their balance changes

- Persistent Storage with EF Core (SQLite): All data is stored via Entity Framework Core; SQLite for lightweight local demos 

- Input Validation & Error Handling: Forms validate required fields and numeric inputs (e.g., non-negative amounts), with friendly error messages and server-side checks to prevent invalid operations

- Fully Functional Front-End using Razor Views: The entire user interface is built using ASP.NET MVC Razor Views, providing dynamic HTML pages that display real-time data (like balance, transactions, and user info). Razor syntax allows seamless integration of C# logic within HTML, enabling responsive and data-driven pages without relying heavily on JavaScript

Tech Stack 

| Layer                | Technology                          |
| -------------------- | ----------------------------------- |
| Frontend             | ASP.NET MVC (Razor Views)           |
| Backend              | ASP.NET Core (C#)                   |
| ORM                  | Entity Framework Core               |
| Database (local dev) | SQLite (`banking.db`)               |                     |
| Tools                | Visual Studio / VS Code, dotnet CLI |

 Installation Steps

Since this is a .NET MVC project, ensure the following tools are installed on your system before running the app.

 Prerequisites

1. [](https://learn.microsoft.com/en-us/dotnet/core/sdk#how-to-install-the-net-sdk).NET SDK: Install the latest stable version (8.0+ recommended)

2. Code Editor: IDE / Code Editor → Visual Studio (recommended) or Visual Studio Code

3. SQLite Studio: For viewing and managing the OnlineBankingApp.db database file

Project Setup

1.Clone the Repository

 ```
git clone https://github.com/manavnaik5/OnlineBankingApp.git
 cd OnlineBankingApp
```

2.Restore Dependencies: In the terminal, run:
 ```
 dotnet restore
 ```

3.Database Setup (SQLite): The project uses Entity Framework Core with SQLite.
    Run the following command to create or update your local database:
 ```
 dotnet ef database update
 ```
 Note: This will automatically generate the (e.g., OnlineBankingApp.db) file inside your project’s Data folder

How to Run the Project

You can run the project using either your IDE or the command line.

1. Using Visual Studio (Recommended)

   1.Open the .sln (Solution) file in Visual Studio.\
   2.Set the OnlineBankingApp project as the startup project.\
   3.Press F5 or the "Start Debugging" button (▶) to build and run the application. The application will launch in your default web browser.\

2. Using Command Line
```
dotnet run  
```
The terminal will display the local URL (http://localhost:5252)
The application will open in your default browser

Initial Access Credentials

| Role  | Email                                               | Initial Password  |
| ----- | --------------------------------------------------- | ----------------- |
| Admin | [admin1@gmail.com]                                  | Admin@1           |
| User  | Register via “Sign Up” page                         | (Create your own) |
