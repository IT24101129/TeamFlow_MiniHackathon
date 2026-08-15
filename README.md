# TeamFlow — SE3090 Full-Stack Task Management System

![TeamFlow Banner](https://img.shields.io/badge/SE3090-MVP%20Full%20Stack-6366f1)
![Backend](https://img.shields.io/badge/C%23-ASP.NET%20Core%20Web%20API-512bd4)
![Database](https://img.shields.io/badge/PostgreSQL-EF%20Core-336791)
![Frontend](https://img.shields.io/badge/React-Vite%20%2B%20Router-61dafb)

## 📌 Project Overview
**TeamFlow** is a lightweight, responsive task management application designed for small startup teams transitioning away from unmanageable spreadsheets. Built for the **SE3090 Software Engineering Frameworks** project, it features full RESTful CRUD operations, PostgreSQL database persistence via Entity Framework Core, instant filtering/sorting/search capabilities, and a metrics summary dashboard.

---

## ✨ Features

- **Task Management**: Create, view, update status (To Do, In Progress, Done), and delete tasks.
- **Dynamic Filtering & Search**: Filter tasks by status and assignee name, and search by title simultaneously.
- **Sorting**: Sort tasks by due date (earliest or latest).
- **Dashboard Metrics**: Live count summary of Total, To Do, In Progress, and Completed tasks.
- **Validation**: Server-side and client-side validation enforcing required titles, valid priorities, allowed statuses, and prohibiting past due dates.
- **Auto-Seeded Database**: Pre-seeded with 5 realistic tasks upon first database creation.
- **Responsive UI**: Glassmorphic modern dark-mode design supporting desktop, tablet, and mobile browsers.
- **Clean Architecture Extension Point**: Clean interface boundary prepared for future **Agentic AI** & **Flutter** mobile app integration.

---

## 🛠️ Technology Stack

| Layer | Technology |
| :--- | :--- |
| **Backend** | ASP.NET Core Web API (.NET 10), C# |
| **Database** | PostgreSQL + Entity Framework Core 10 |
| **Frontend** | React 19, Vite, React Router v7, Lucide Icons, Axios |
| **API Docs** | Swagger / OpenAPI |
| **Version Control** | Git & GitHub |

---

## 📁 Project Structure

```
TeamFlow/
├── backend/
│   └── TeamFlow.API/
│       ├── Controllers/
│       │   └── TasksController.cs          # REST API endpoints
│       ├── Data/
│       │   └── ApplicationDbContext.cs     # EF Core DbContext & Seed Data
│       ├── DTOs/
│       │   └── TaskDtos.cs                 # Request/Response DTO models
│       ├── Models/
│       │   └── TaskItem.cs                 # Tasks PostgreSQL Entity
│       ├── Services/
│       │   ├── ITaskService.cs             # Task service interface
│       │   ├── TaskService.cs              # Task business logic & validation
│       │   └── IAgentWorkflowService.cs    # Future Agentic AI extension point
│       ├── Migrations/                     # EF Core migration files
│       ├── Program.cs                      # Dependency Injection & Pipeline
│       └── appsettings.json                # PostgreSQL Connection Strings
│
├── frontend/
│   └── teamflow-client/
│       ├── src/
│       │   ├── components/
│       │   │   ├── Navbar.jsx              # Header navigation
│       │   │   ├── TaskSummary.jsx         # Metric summary cards
│       │   │   ├── TaskFilters.jsx         # Search & filter bar
│       │   │   └── TaskCard.jsx            # Individual task card item
│       │   ├── pages/
│       │   │   ├── TasksPage.jsx           # Main board page
│       │   │   └── AddTaskPage.jsx         # Task creation form page
│       │   ├── services/
│       │   │   └── taskService.js          # Axios API communication
│       │   ├── App.jsx                     # Router config
│       │   ├── index.css                   # Glassmorphic CSS design system
│       │   └── main.jsx
│       ├── package.json
│       └── vite.config.js
│
├── .gitignore
└── README.md
```

---

## 🚀 Quick Start Guide

### 1. Prerequisites
- **.NET SDK 10.0+** installed
- **Node.js 20+** installed
- **PostgreSQL** server running locally (Port 5432) or pgAdmin

---

### 2. Database Setup (PostgreSQL)

1. Open pgAdmin or psql shell.
2. Create the database named `TeamFlowDb`:
   ```sql
   CREATE DATABASE "TeamFlowDb";
   ```
3. Update connection string in `backend/TeamFlow.API/appsettings.json` if your local PostgreSQL password differs:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Host=localhost;Port=5432;Database=TeamFlowDb;Username=postgres;Password=YOUR_PASSWORD"
   }
   ```

---

### 3. Run Backend API

Navigate to the API folder and apply database migrations:

```bash
cd backend/TeamFlow.API

# Install EF Core tool if not already installed globally
dotnet tool install --global dotnet-ef

# Apply migrations to create PostgreSQL tables and seed data
dotnet ef database update

# Start backend server
dotnet run
```

- **API Base URL**: `http://localhost:5000` (or configured port)
- **Swagger Documentation UI**: `http://localhost:5000/swagger`

---

### 4. Run Frontend Client

Open a new terminal window:

```bash
cd frontend/teamflow-client

# Install dependencies
npm install

# Start Vite React development server
npm run dev
```

- **Frontend App URL**: `http://localhost:5173`

---

## 📡 REST API Endpoints

| Method | Endpoint | Description |
| :--- | :--- | :--- |
| `GET` | `/api/tasks` | Fetch tasks (Supports `?status=`, `?assignee=`, `?search=`, `?sort=dueDate`) |
| `GET` | `/api/tasks/{id}` | Get single task by ID |
| `POST` | `/api/tasks` | Create a new task (Validates title & due date) |
| `PUT` | `/api/tasks/{id}` | Update full task details |
| `PATCH` | `/api/tasks/{id}/status` | Update task status (`To Do`, `In Progress`, `Done`) |
| `DELETE` | `/api/tasks/{id}` | Delete task by ID |

---

## 🧠 Future Extensions

### 1. Future Agentic AI Subsystem (Component 4)
The current project defines `IAgentWorkflowService` in `Services/IAgentWorkflowService.cs` as a clean dependency injection boundary. When Agentic AI capabilities (e.g. automated task prioritization, AI approval workflows) are added in future assignment milestones, they will plug into this service layer without needing changes to PostgreSQL schemas or React frontends.

### 2. Future Flutter Mobile App Extension
The REST API returns standard JSON payloads adhering to OpenAPI specifications, allowing the future Flutter cross-platform mobile app to consume the exact same endpoints seamlessly.

---

## 👥 Git Branching Model for 4 Team Members

To ensure clean group contributions for the full SE3090 project:

- `main`: Production-ready code
- `feature/member1-task-management` *(Member 1 - Current MVP)*
- `feature/member2-team-management` *(Member 2 - Future)*
- `feature/member3-project-reporting` *(Member 3 - Future)*
- `feature/member4-ai-workflow` *(Member 4 - Future)*

### Initial Git Repository Commands:
```bash
git init
git add .
git commit -m "Initial TeamFlow full-stack MVP implementation"
git branch -M main
git remote add origin https://github.com/YOUR_USERNAME/TeamFlow.git
git push -u origin main
```
