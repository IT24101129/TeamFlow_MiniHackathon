import React from 'react';
import { Link, useLocation } from 'react-router-dom';
import { CheckSquare, PlusCircle, ListTodo, Layers } from 'lucide-react';

export default function Navbar() {
  const location = useLocation();

  return (
    <header className="navbar-header">
      <div className="navbar-container">
        <Link to="/tasks" className="brand-logo">
          <div className="logo-icon-wrapper">
            <Layers className="logo-icon" size={24} />
          </div>
          <div className="brand-text">
            <span className="brand-title">Team<span className="gradient-text">Flow</span></span>
            <span className="brand-badge">SE3090 MVP</span>
          </div>
        </Link>

        <nav className="nav-links">
          <Link
            to="/tasks"
            className={`nav-btn ${location.pathname === '/tasks' || location.pathname === '/' ? 'active' : ''}`}
          >
            <ListTodo size={18} />
            <span>All Tasks</span>
          </Link>
          <Link
            to="/add-task"
            className={`nav-btn primary ${location.pathname === '/add-task' ? 'active' : ''}`}
          >
            <PlusCircle size={18} />
            <span>Add Task</span>
          </Link>
        </nav>
      </div>
    </header>
  );
}
