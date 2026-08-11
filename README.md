# Easy Ticket - Event Management & Ticket Booking Console Application

Console application built with **C#** and **.NET 9** connected to a **MySQL** database.
It allows users to browse events, purchase tickets, upgrade to Premium membership, and create or manage their own events.

---

## Features & Roles

- **Guest User**:
  - Can browse events and check details without logging in.
- **Standard User**:
  - Register and Login authentication with strict input validation.
  - View purchased ticket history.
  - Upgrade membership to **Premium**.
- **Premium User**:
  - All Standard User features.
  - Create new events and list them in the public catalog.
  - Manage and delete their created events.

---

## Project Structure

```text
EasyTicket
│
├── MainClass.cs             - Entry point containing the main interactive CLI menu loop
│
├── Authentication & Users
│   ├── Register.cs          - Handles registration input fields
│   ├── Login.cs             - Handles login credentials input
│   ├── AuthenticationCheck.cs - Validates user input (email format, password, requirements)
│   ├── ExistsUser.cs        - Validates credentials against the MySQL database
│   ├── CreateUser.cs        - Inserts newly registered users into the database
│   ├── UserData.cs          - User model and DB helper methods (fetching user details)
│   └── UpdateSub.cs         - Handles subscription upgrades (Standard -> Premium)
│
├── Events & Tickets
│   ├── EventCatalog.cs      - Displays available events and handles ticket selection/booking
│   ├── TicketHistory.cs     - Fetches and displays tickets purchased by the user
│   ├── CreateEvent.cs       - Handles creation of new events (Premium users only)
│   └── ManageEvents.cs      - Allows Premium users to view and delete their created events
│
└── Models_DB / Config
    └── [DB Models & Database Connection Helpers]
````
_______________________________________________________________________________________________
Application Flow & Navigation
1. Authentication Menu
Press 1 (Register): Validates registration data, creates user record, and logs in automatically.

Press 2 (Login): Authenticates user credentials against the MySQL database.

Press 3 (Guest Mode): Proceed without account credentials.

Press 4 (Exit): Closes the application.

_______________________________________________________________________________________________
2. Main Menu
1. User Management:

View My Purchased Tickets (All logged-in users)

Upgrade to Premium (Standard users)

Create a New Event (Premium users)

Manage / Delete My Created Events (Premium users)

2. Check Events:

Browse public event catalog and book tickets.

3. Log Out / Login:

Switch session or log in from Guest mode.

4. Exit Application:

Terminates execution.
_______________________________________________________________________________________________

Tech Stack & Dependencies
Language & Runtime: C# (.NET 9.0)

Database: MySQL Server

NuGet Packages:

MySqlConnector (v2.6.1) — High-performance ADO.NET data provider

MySql.Data (v26.7.0) — MySQL Data client library

_______________________________________________________________________________________________

Database Configuration
Ensure you have a local or remote MySQL instance running.

Example database table requirements:

users — id, first_name, last_name, email, password, subscription (0 = Standard, 1 = Premium)

events — id, creator_id, title, description, date, ticket_price

tickets — id, user_id, event_id, purchase_date

_______________________________________________________________________________________________


    
