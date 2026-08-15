import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import Navbar from './components/Navbar';
import TasksPage from './pages/TasksPage';
import AddTaskPage from './pages/AddTaskPage';

export default function App() {
  return (
    <Router>
      <div className="app-layout">
        <Navbar />
        <main className="main-content">
          <Routes>
            <Route path="/" element={<Navigate to="/tasks" replace />} />
            <Route path="/tasks" element={<TasksPage />} />
            <Route path="/add-task" element={<AddTaskPage />} />
            <Route path="*" element={<Navigate to="/tasks" replace />} />
          </Routes>
        </main>
        <footer className="app-footer">
          <p>TeamFlow &copy; 2026 — SE3090 Software Engineering Frameworks MVP</p>
        </footer>
      </div>
    </Router>
  );
}
