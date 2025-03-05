# Bakery Fresh Bread

## Project Overview
Bakery Fresh Bread is a bakery management system designed to help workers manage orders for two bakery offices. The system allows workers to place, prepare, and track orders while considering the production limits of each bakery location. 
This project was built using **.NET 8** and was created for the backend subject within the Jala University bootcamp as a final project.

## Features
- **Manage Orders:** Workers can create and manage orders for each bakery office.
- **Bread Specialties:** Each bakery office has its own pastry chef specializing in certain types of bread.
- **Production Capacity:** The main office can produce up to **150** breads at a time, while the second office can handle **100**.
- **Order Processing:** Orders can be added and prepared, and once prepared, they are cleared from the system.
- **Navigation Between Offices:** Workers can switch between offices without losing order data.
- **Order Limits:** Ensures that the total bread quantity does not exceed the bakery's maximum capacity.
- **Total Earnings Summary:** Tracks the total number of orders and earnings.
- **Bread Preparation Process:** Each bread follows a defined preparation process, including mixing, resting, fermenting, and cooking.
- **Database Persistence:** Data is stored to maintain records of orders and earnings (Bonus feature).

## Requirements
- **Order Management System**: Workers can add, process, and track bakery orders.
- **Capacity Restrictions**: Ensures the bakery does not exceed its maximum production limit.
- **Confirmation Messages**: Workers must confirm orders before finalizing them.
- **Bread Specialties & Preparation**: Each type of bread follows specific steps and cooking conditions.
- **Multi-Office Support**: Orders remain separate for each bakery office.

## Technologies Used
- **.NET 8**
- **C#**
- **Entity Framework Core** (for database management)
- **Console Application UI** (or Web API, depending on the implementation)

## Getting Started
1. Clone the repository:
   ```sh
   git clone https://github.com/DavidCosta92/Practica-Final-BE-David-Costa.git
   ```
2. Navigate to the project directory:
   ```sh
   cd BakeryFreshBread
   ```
3. Install dependencies:
   ```sh
   dotnet restore
   ```
4. Run the application:
   ```sh
   dotnet run
   ```


