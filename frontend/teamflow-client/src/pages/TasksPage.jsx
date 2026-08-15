import React, { useState, useEffect, useMemo } from 'react';
import { taskService } from '../services/taskService';
import TaskSummary from '../components/TaskSummary';
import TaskFilters from '../components/TaskFilters';
import TaskCard from '../components/TaskCard';
import { RefreshCw, AlertTriangle, Plus } from 'lucide-react';
import { Link } from 'react-router-dom';

export default function TasksPage() {
  const [tasks, setTasks] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  // Filter & Search states
  const [searchTerm, setSearchTerm] = useState('');
  const [statusFilter, setStatusFilter] = useState('All');
  const [assigneeFilter, setAssigneeFilter] = useState('All');
  const [sortOption, setSortOption] = useState('dueDate');

  // Load tasks from REST API
  const fetchTasks = async () => {
    setLoading(true);
    setError(null);
    try {
      const data = await taskService.getTasks();
      setTasks(data);
    } catch (err) {
      console.error('Error fetching tasks:', err);
      setError(err.response?.data?.message || 'Unable to connect to TeamFlow API backend. Please ensure the backend is running.');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchTasks();
  }, []);

  // Compute unique assignees dynamically
  const assigneesList = useMemo(() => {
    const names = tasks.map((t) => t.assigneeName).filter(Boolean);
    return Array.from(new Set(names)).sort();
  }, [tasks]);

  // Client-side filtering for immediate snappy responsiveness
  const filteredTasks = useMemo(() => {
    return tasks
      .filter((task) => {
        const matchesTitle = task.title.toLowerCase().includes(searchTerm.toLowerCase().trim());
        const matchesStatus = statusFilter === 'All' || task.status === statusFilter;
        const matchesAssignee = assigneeFilter === 'All' || task.assigneeName === assigneeFilter;
        return matchesTitle && matchesStatus && matchesAssignee;
      })
      .sort((a, b) => {
        const dateA = new Date(a.dueDate);
        const dateB = new Date(b.dueDate);
        return sortOption === 'dueDate' ? dateA - dateB : dateB - dateA;
      });
  }, [tasks, searchTerm, statusFilter, assigneeFilter, sortOption]);

  // Quick Status Update
  const handleStatusChange = async (taskId, newStatus) => {
    try {
      const updated = await taskService.updateTaskStatus(taskId, newStatus);
      setTasks((prev) => prev.map((t) => (t.taskId === taskId ? updated : t)));
    } catch (err) {
      alert(err.response?.data?.message || 'Failed to update task status.');
    }
  };

  // Delete Task
  const handleDeleteTask = async (taskId) => {
    if (!window.confirm('Are you sure you want to delete this task?')) return;
    try {
      await taskService.deleteTask(taskId);
      setTasks((prev) => prev.filter((t) => t.taskId !== taskId));
    } catch (err) {
      alert(err.response?.data?.message || 'Failed to delete task.');
    }
  };

  const handleClearFilters = () => {
    setSearchTerm('');
    setStatusFilter('All');
    setAssigneeFilter('All');
    setSortOption('dueDate');
  };

  return (
    <div className="page-container">
      <div className="page-header">
        <div>
          <h1 className="page-title">Team Tasks</h1>
          <p className="page-subtitle">Track, filter, and organize team task progress in real time.</p>
        </div>
        <button className="refresh-btn" onClick={fetchTasks} title="Reload Tasks">
          <RefreshCw size={16} className={loading ? 'spin' : ''} />
          <span>Refresh</span>
        </button>
      </div>

      <TaskSummary tasks={tasks} />

      <TaskFilters
        searchTerm={searchTerm}
        setSearchTerm={setSearchTerm}
        statusFilter={statusFilter}
        setStatusFilter={setStatusFilter}
        assigneeFilter={assigneeFilter}
        setAssigneeFilter={setAssigneeFilter}
        sortOption={sortOption}
        setSortOption={setSortOption}
        assigneesList={assigneesList}
        onClearFilters={handleClearFilters}
      />

      {loading ? (
        <div className="state-box loading-state">
          <div className="spinner"></div>
          <p>Loading tasks from PostgreSQL API...</p>
        </div>
      ) : error ? (
        <div className="state-box error-state">
          <AlertTriangle size={32} />
          <h3>Connection Error</h3>
          <p>{error}</p>
          <button className="retry-btn" onClick={fetchTasks}>Retry Connection</button>
        </div>
      ) : filteredTasks.length === 0 ? (
        <div className="state-box empty-state">
          <div className="empty-icon-wrapper">
            <Plus size={32} />
          </div>
          <h3>No tasks found</h3>
          <p>
            {tasks.length === 0
              ? "Your task board is currently empty. Get started by adding a task."
              : "No tasks match your active filter criteria. Try clearing search filters."}
          </p>
          {tasks.length === 0 ? (
            <Link to="/add-task" className="btn-primary">
              Create First Task
            </Link>
          ) : (
            <button className="btn-secondary" onClick={handleClearFilters}>
              Reset Filters
            </button>
          )}
        </div>
      ) : (
        <div className="tasks-grid">
          {filteredTasks.map((task) => (
            <TaskCard
              key={task.taskId}
              task={task}
              onStatusChange={handleStatusChange}
              onDeleteTask={handleDeleteTask}
            />
          ))}
        </div>
      )}
    </div>
  );
}
