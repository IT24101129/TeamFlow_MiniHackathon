# TeamFlow

## SE3090 Software Engineering Frameworks — Mini Hackathon

TeamFlow is a full-stack task management system designed to help teams create, organize, track, filter, and manage tasks through a web-based interface.

The current MVP provides a RESTful ASP.NET Core Web API, PostgreSQL database integration, and a React frontend. The architecture also leaves room for future Agentic AI integration without implementing Agentic AI in the current version.

---

## Tech Stack

### Backend

* ASP.NET Core Web API
* C#
* Entity Framework Core
* PostgreSQL
* Npgsql
* Swagger / OpenAPI

### Frontend

* React
* Vite
* React Router
* Axios
* Lucide React
* CSS

### Database

* PostgreSQL
* Entity Framework Core migrations
* Seeded task data

---

## Project Structure

```text
TeamFlow/
│
├── backend/
│   └── TeamFlow.API/
│       ├── Controllers/
│       │   └── TasksController.cs
│       ├── Data/
│       │   └── ApplicationDbContext.cs
│       ├── DTOs/
│       │   └── TaskDtos.cs
│       ├── Models/
│       │   └── TaskItem.cs
│       ├── Services/
│       │   ├── IAgentWorkflowService.cs
│       │   ├── ITaskService.cs
│       │   └── TaskService.cs
│       ├── Migrations/
│       ├── Program.cs
│       ├── appsettings.json
│       └── TeamFlow.API.csproj
│
├── frontend/
│   └── teamflow-client/
│       ├── src/
│       │   ├── components/
│       │   ├── pages/
│       │   ├── services/
│       │   ├── App.jsx
│       │   └── main.jsx
│       ├── package.json
│       └── vite.config.js
│
├── .gitignore
└── README.md
```

---

## Current Features

### Task Management

* Create tasks
* View all tasks
* View individual tasks
* Update task information
* Update task status
* Delete tasks
* Server-side validation

### Task Dashboard

* Total task count
* To Do count
* In Progress count
* Completed count
* Task cards
* Status indicators
* Overdue indicators

### Search and Filtering

* Search tasks by title
* Filter by status
* Filter by assignee
* Sort tasks

### Frontend

* Responsive React interface
* React Router navigation
* Add Task page
* Task dashboard
* API integration using Axios

### API

RESTful endpoints are provided for task management and can be tested using Swagger/OpenAPI.

---

## Database

The application uses PostgreSQL with Entity Framework Core.

### Database Name

```text
TeamFlowDb
```

### Main Table

```text
Tasks
```

The initial database migration also includes seeded task data for demonstration and testing.

---

## Running the Backend

From the project root:

```powershell
cd backend\TeamFlow.API
dotnet run
```

The API runs locally on the configured ASP.NET Core URL.

Swagger can be accessed through:

```text
http://localhost:5000/swagger
```

---

## Running the Frontend

Open a second terminal:

```powershell
cd frontend\teamflow-client
npm install
npm run dev
```

The Vite development server will provide a local URL, normally:

```text
http://localhost:5173
```

---

## Database Migration

After PostgreSQL is installed and the `TeamFlowDb` database is available, run:

```powershell
cd backend\TeamFlow.API
dotnet ef database update
```

This applies the Entity Framework Core migrations and creates the required database tables.

---

## API Endpoints

The main task API is:

```text
/api/tasks
```

The API provides operations for:

```text
GET     /api/tasks
GET     /api/tasks/{id}
POST    /api/tasks
PUT     /api/tasks/{id}
PATCH   /api/tasks/{id}/status
DELETE  /api/tasks/{id}
```

Swagger provides an interactive interface for testing these endpoints.

---

## Future Extension: Agentic AI

Agentic AI is **not implemented in the current MVP**.

A service interface has been included to provide an architectural extension point for future development:

```text
IAgentWorkflowService
```

Future versions may integrate Agentic AI for features such as automated task prioritization, workflow assistance, or task recommendations.

No LLM, vector database, or agent framework is currently required to run the MVP.

---

## Future Development

Potential future improvements include:

* Team and user management
* Task assignment and team roles
* Authentication and authorization
* Project progress tracking
* Reporting and analytics
* Mobile application
* Agentic AI workflow automation

---

## Team

This project was developed as part of the SE3090 Software Engineering Frameworks coursework.

The current repository contains the complete full-stack MVP for the TeamFlow Mini Hackathon.