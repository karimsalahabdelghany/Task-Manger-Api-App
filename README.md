# ✅ Task Manager — Full Stack App

A full-stack task management system with user authentication and daily task tracking. Built with **ASP.NET Core** backend and **React.js** frontend communicating via REST API.

---

## ✨ Features

- 🔐 **User Registration & JWT Authentication** — secure signup/login flow
- ✅ **Task CRUD** — create, read, update, and delete daily tasks
- 📅 **Daily Task Tracking** — organize and monitor tasks by date
- ⚛️ **React.js Frontend** — responsive SPA communicating with the API via Axios
- 🔒 **Protected Routes** — frontend routes guarded by auth state

---

## 🛠️ Tech Stack

### Backend
| Technology | Purpose |
|---|---|
| ASP.NET Core Web API | REST API |
| Entity Framework Core | ORM |
| SQL Server | Database |
| JWT | Authentication |
| C# | Language |

### Frontend
| Technology | Purpose |
|---|---|
| React.js | UI Framework |
| Axios | HTTP client for API calls |
| JavaScript | Language |

---

## 🏛️ Project Structure

```
├── Backend (ASP.NET Core)
│   ├── Controllers
│   │   ├── AuthController   (register, login)
│   │   └── TasksController  (CRUD)
│   ├── Models / Entities
│   ├── DTOs
│   ├── Data (EF Core DbContext)
│   └── Helpers (JWT generation)
│
└── Frontend (React.js)
    ├── src/
    │   ├── components/
    │   ├── pages/
    │   ├── services/ (Axios API calls)
    │   └── context/ (Auth state)
    └── public/
```

---

## 🚀 Getting Started

### Backend Setup

```bash
# 1. Clone backend repo
git clone https://github.com/karimsalahabdelghany/TaskManager-Backend.git
cd TaskManager-Backend

# 2. Update appsettings.json
# - SQL Server connection string
# - JWT Secret & Expiry

# 3. Apply migrations
dotnet ef database update

# 4. Run API
dotnet run
# API runs on https://localhost:5001
```

### Frontend Setup

```bash
# 1. Clone frontend repo
git clone https://github.com/karimsalahabdelghany/TaskManager-Frontend.git
cd TaskManager-Frontend

# 2. Install dependencies
npm install

# 3. Update API base URL in src/services/api.js

# 4. Run the app
npm start
# App runs on http://localhost:3000
```

---

## 📡 API Endpoints

| Method | Endpoint | Description | Auth |
|---|---|---|---|
| POST | `/api/auth/register` | Register new user | ❌ |
| POST | `/api/auth/login` | Login, returns JWT | ❌ |
| GET | `/api/tasks` | Get all user tasks | ✅ |
| POST | `/api/tasks` | Create new task | ✅ |
| PUT | `/api/tasks/{id}` | Update task | ✅ |
| DELETE | `/api/tasks/{id}` | Delete task | ✅ |

---

## 🔗 Repositories

- **Backend:** [TaskManager-Backend](https://github.com/karimsalahabdelghany/TaskManager-Backend)
- **Frontend:** [TaskManager-Frontend](https://github.com/karimsalahabdelghany/TaskManager-Frontend)

---

## 👤 Author

**Karim Salah** — Junior .NET Backend Developer
- 📧 karimabdelghany753@gmail.com
- 💼 [LinkedIn](https://linkedin.com/in/karim-salah22)
- 🐙 [GitHub](https://github.com/karimsalahabdelghany)
