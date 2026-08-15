import React from 'react';
import { Calendar, User, Trash2, AlertCircle, CheckCircle, Clock } from 'lucide-react';

export default function TaskCard({ task, onStatusChange, onDeleteTask }) {
  const formattedDueDate = new Date(task.dueDate).toLocaleDateString('en-US', {
    month: 'short',
    day: 'numeric',
    year: 'numeric'
  });

  const isOverdue = new Date(task.dueDate) < new Date(new Date().setHours(0,0,0,0)) && task.status !== 'Done';

  const getPriorityBadge = (priority) => {
    switch (priority) {
      case 'High':
        return <span className="priority-badge high">High</span>;
      case 'Medium':
        return <span className="priority-badge medium">Medium</span>;
      default:
        return <span className="priority-badge low">Low</span>;
    }
  };

  const getStatusIcon = (status) => {
    switch (status) {
      case 'Done':
        return <CheckCircle className="status-icon done" size={16} />;
      case 'In Progress':
        return <Clock className="status-icon in-progress" size={16} />;
      default:
        return <AlertCircle className="status-icon todo" size={16} />;
    }
  };

  return (
    <div className={`task-card ${isOverdue ? 'overdue' : ''} ${task.status === 'Done' ? 'completed' : ''}`}>
      <div className="card-header">
        <div className="card-title-group">
          {getStatusIcon(task.status)}
          <h3 className="task-title">{task.title}</h3>
        </div>
        {getPriorityBadge(task.priority)}
      </div>

      <div className="card-meta">
        <div className="meta-item">
          <User size={14} className="meta-icon" />
          <span className="assignee-name">{task.assigneeName}</span>
        </div>

        <div className={`meta-item ${isOverdue ? 'due-overdue' : ''}`}>
          <Calendar size={14} className="meta-icon" />
          <span>Due: {formattedDueDate}</span>
          {isOverdue && <span className="overdue-tag">Overdue</span>}
        </div>
      </div>

      <div className="card-actions">
        <div className="status-selector-wrapper">
          <label htmlFor={`status-select-${task.taskId}`} className="visually-hidden">Task Status</label>
          <select
            id={`status-select-${task.taskId}`}
            className={`status-select status-${task.status.toLowerCase().replace(/\s+/g, '-')}`}
            value={task.status}
            onChange={(e) => onStatusChange(task.taskId, e.target.value)}
          >
            <option value="To Do">To Do</option>
            <option value="In Progress">In Progress</option>
            <option value="Done">Done</option>
          </select>
        </div>

        <button
          className="delete-task-btn"
          title="Delete task"
          onClick={() => onDeleteTask(task.taskId)}
        >
          <Trash2 size={16} />
        </button>
      </div>
    </div>
  );
}
