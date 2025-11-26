# Inventory Management API

A simplified Inventory Management system built with **.NET 8**, **Clean Architecture**, **Domain‑Driven Design (DDD)** principles, integration mocks, and Docker‑based development environment.

This project is a technical assessment that demonstrates clean code, modular architecture, testable business logic, and external integration boundaries.

## Features

### **Categories**

* Create categories (with optional parent category)
* List categories
* Delete categories

### **Suppliers**

* Create suppliers
* List suppliers

### **Products**

* Create products with:

  * Supplier
  * Category
  * Description
  * Currency & acquisition cost
  * Acquisition cost converted to USD
  * Dates (acquired, sold, cancelled, returned)
  * Status (Created, Sold, Cancelled, Returned)

* Status change rules:

  * **Cancelled/Returned → cannot be sold**
  * **Sold → can be cancelled or returned**

### **External Integrations (Mocked)**

* Warehouse Management System (WMS)

  * Create product
  * Dispatch product
* Audit Log Service

  * Logs product creation and status changes
* Email Sender

  * Sends email to supplier when product is sold

### **Other Requirements**

* Products cannot be deleted
* Test coverage for core domain logic
* Docker development environment

## Running with Docker

### **1. Build**

```bash
docker-compose build
```

### **2. Run**

```bash
docker-compose up
```

API will be available at:

```
http://localhost:5000
```

Swagger UI:

```
http://localhost:5000/swagger
```

---

## Running Tests

```bash
dotnet test
```

Tests cover:

* Category rules
* Supplier creation
* Product domain rules (status transitions)
* Value objects (Money, Email)

## API Endpoints Summary

### **Categories**

| Method | Endpoint             | Description     |
| POST   | /api/categories      | Create category |
| GET    | /api/categories      | List categories |
| DELETE | /api/categories/{id} | Delete category |

### **Suppliers**

| Method | Endpoint       | Description     |
| POST   | /api/suppliers | Create supplier |
| GET    | /api/suppliers | List suppliers  |

### **Products**

| Method | Endpoint                  | Description           |
| POST   | /api/products             | Create product        |
| GET    | /api/products             | List all products     |
| PATCH  | /api/products/{id}/status | Change product status |


## Integration Mocks

### **WMS **

* `POST /products`
* `POST /products/{productId}/dispatch`

### **Audit Log System**

* `POST /logs`

### **Email Sender**

* Outputs email message to console


## Tech Stack

* **.NET 8 / C#**
* **EF Core**
* **Swagger**
* **Docker & Docker Compose**
* **XUnit**
  
## Author

Paulo Okino

* A full English version of all comments in code
* A walkthrough video script
