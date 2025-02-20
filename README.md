# Flight Reservation System API Documentation

Welcome to the **Flight Reservation System API**, a simple yet powerful system to manage flights, reservations, and users. This document explains how the services work and how to set up the system, including database migrations.

---

## 📌 **Project Overview**
This API provides three main functionalities:

1. **Flight Management** - Search and retrieve flights.
2. **Reservation Management** - Book, retrieve, and cancel reservations.
3. **User Management** - Register and authenticate users.

---

## 🚀 **Endpoints and Services**

### 🔹 **Flight Service**
The `FlightController` handles flight-related operations, including searching and retrieving available flights.

#### **Endpoints**
| Method | Route            | Description |
|--------|-----------------|-------------|
| `GET`  | `/api/flight/getAll`  | Get all available flights. |
| `GET`  | `/api/flight/search?origin={origin}&destination={destination}&departureDate={date}` | Search flights based on origin, destination, and optional departure date. |

#### **Example Requests**
```http
GET /api/flight/getAll
```

```http
GET /api/flight/search?origin=NYC&destination=LAX&departureDate=2025-03-15
```

---

### 🔹 **Reservation Service**
The `ReservationController` manages flight reservations for users.

#### **Endpoints**
| Method | Route                | Description |
|--------|----------------------|-------------|
| `POST` | `/api/reservation`  | Create a new reservation for a flight. |
| `GET`  | `/api/reservation/myReservation` | Retrieve reservations for the logged-in user. |
| `DELETE` | `/api/reservation/{reservationId}` | Cancel a reservation. |

#### **Example Requests**
##### ✅ **Create a Reservation**
```http
POST /api/reservation
Content-Type: application/json

{
    "flightId": "550e8400-e29b-41d4-a716-446655440000"
}
```

##### 📌 **Get My Reservations**
```http
GET /api/reservation/myReservation
Authorization: Bearer {your_jwt_token}
```

##### ❌ **Cancel a Reservation**
```http
DELETE /api/reservation/550e8400-e29b-41d4-a716-446655440000
Authorization: Bearer {your_jwt_token}
```

---

### 🔹 **User Service**
The `UserController` is responsible for user authentication and management.

#### **Endpoints**
| Method | Route            | Description |
|--------|-----------------|-------------|
| `POST` | `/api/user/register` | Register a new user. |
| `POST` | `/api/user/login` | Log in a user and get a JWT token. |

#### **Example Requests**
##### 🆕 **User Registration**
```http
POST /api/user/register
Content-Type: application/json

{
    "fullName": "John Doe",
    "email": "johndoe@example.com",
    "password": "SecurePassword123!"
}
```

##### 🔑 **User Login**
```http
POST /api/user/login
Content-Type: application/json

{
    "email": "johndoe@example.com",
    "password": "SecurePassword123!"
}
```

---

## 🛠 **Setup and Running the Project**

### 🔹 **Aspire 9 & Docker Integration**
This project uses **Aspire 9** to provide built-in monitoring, distributed tracing, and automatic **Docker-based SQL Server setup**.

- **Real-time monitoring** of application performance.
- **Database containerization** with automatic volume persistence.
- **Improved debugging** with tracing support.

### 🔹 **Email Notifications**
After a successful flight reservation, an **email notification** is sent using SMTP settings. Ensure the following configuration in `appsettings.json`:

```json
"EmailSettings": {
    "SmtpServer": "smtp.example.com",
    "SmtpPort": "587",
    "SmtpUser": "username",
    "SmtpPass": "password",
    "FromEmail": "amirhosseinmirmohammad@gmail.com"
}
```

### 🔹 **Testing Reservation System**
This project includes **unit tests** to ensure reservation functionality works correctly.

Run the tests using:
```bash
dotnet test
```

Tests cover:
- Creating a reservation.
- Fetching user reservations.
- Canceling a reservation.

---

## 🛡 **Testing the API**
Once the project is running, you can test it using:
- **Postman**: Send requests manually.
- **Swagger UI**: Visit `https://localhost:{port}/swagger` to explore the API.

---

## 🎯 **Conclusion**
This project allows users to:
- Search for flights ✈️
- Reserve flights 🏟
- Manage reservations 📝
- Authenticate users 🔐

---

End! 🚀
