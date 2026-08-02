# InvoicingSystem

A simple practice project for learning ASP.NET Core with Controllers, EF Core, and relational data.

## Features

- CRUD for Invoices and Payments
- One-to-many relationship between Invoice and Payment
- Automatic invoice status calculation (Unpaid / Paid / Overdue) based on payments
- Validation to prevent overpaying an invoice

## Tech Stack

- ASP.NET Core Web API (Controllers)
- Entity Framework Core (SQL Server)
- Scalar for API testing

## Project Structure

```
InvoicingSystem.Api/
  Controllers/
  Models/
  Enums/
  Data/
  Migrations/
```