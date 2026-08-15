import React, { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { taskService } from '../services/taskService';
import { ArrowLeft, Save, AlertCircle, CheckCircle2 } from 'lucide-react';

export default function AddTaskPage() {
  const navigate = useNavigate();

  const getTodayISO = () => new Date().toISOString().split('T')[0];

  const [formData, setFormData] = useState({
    title: '',
    assigneeName: '',
    priority: 'Medium',
    dueDate: getTodayISO(),
    status: 'To Do',
  });

  const [formErrors, setFormErrors] = useState({});
  const [submitting, setSubmitting] = useState(false);
  const [apiError, setApiError] = useState(null);
  const [successMessage, setSuccessMessage] = useState(null);

  const validate = () => {
    const errors = {};
    if (!formData.title.trim()) {
      errors.title = 'Task title is required.';
    }

    if (!formData.assigneeName.trim()) {
      errors.assigneeName = 'Assignee name is required.';
    }

    if (!formData.dueDate) {
      errors.dueDate = 'Due date is required.';
    } else {
      const selectedDate = new Date(formData.dueDate);
      const today = new Date();
      today.setHours(0, 0, 0, 0);

      if (selectedDate < today) {
        errors.dueDate = 'Due date cannot be in the past.';
      }
    }

    setFormErrors(errors);
    return Object.keys(errors).length === 0;
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
    if (formErrors[name]) {
      setFormErrors((prev) => ({ ...prev, [name]: null }));
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setApiError(null);
    setSuccessMessage(null);

    if (!validate()) return;

    setSubmitting(true);
    try {
      await taskService.createTask(formData);
      setSuccessMessage('Task created successfully! Redirecting...');
      setTimeout(() => {
        navigate('/tasks');
      }, 1000);
    } catch (err) {
      console.error('Error creating task:', err);
      setApiError(
        err.response?.data?.message ||
        'Failed to create task. Please check server validation rules.'
      );
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <div className="page-container form-page">
      <div className="back-nav">
        <Link to="/tasks" className="back-link">
          <ArrowLeft size={16} />
          <span>Back to All Tasks</span>
        </Link>
      </div>

      <div className="form-card">
        <div className="form-header">
          <h2>Create New Task</h2>
          <p>Assign tasks, set priorities, and keep your team aligned.</p>
        </div>

        {apiError && (
          <div className="form-alert error">
            <AlertCircle size={18} />
            <span>{apiError}</span>
          </div>
        )}

        {successMessage && (
          <div className="form-alert success">
            <CheckCircle2 size={18} />
            <span>{successMessage}</span>
          </div>
        )}

        <form onSubmit={handleSubmit} noValidate>
          <div className="form-group">
            <label htmlFor="title">
              Task Title <span className="required">*</span>
            </label>
            <input
              type="text"
              id="title"
              name="title"
              className={`form-control ${formErrors.title ? 'is-invalid' : ''}`}
              placeholder="e.g. Implement PostgreSQL Database Migrations"
              value={formData.title}
              onChange={handleChange}
            />
            {formErrors.title && <span className="error-text">{formErrors.title}</span>}
          </div>

          <div className="form-row">
            <div className="form-group col">
              <label htmlFor="assigneeName">
                Assignee Name <span className="required">*</span>
              </label>
              <input
                type="text"
                id="assigneeName"
                name="assigneeName"
                className={`form-control ${formErrors.assigneeName ? 'is-invalid' : ''}`}
                placeholder="e.g. Sarah Connor"
                value={formData.assigneeName}
                onChange={handleChange}
              />
              {formErrors.assigneeName && (
                <span className="error-text">{formErrors.assigneeName}</span>
              )}
            </div>

            <div className="form-group col">
              <label htmlFor="priority">Priority</label>
              <select
                id="priority"
                name="priority"
                className="form-control"
                value={formData.priority}
                onChange={handleChange}
              >
                <option value="Low">Low</option>
                <option value="Medium">Medium</option>
                <option value="High">High</option>
              </select>
            </div>
          </div>

          <div className="form-row">
            <div className="form-group col">
              <label htmlFor="dueDate">
                Due Date <span className="required">*</span>
              </label>
              <input
                type="date"
                id="dueDate"
                name="dueDate"
                className={`form-control ${formErrors.dueDate ? 'is-invalid' : ''}`}
                value={formData.dueDate}
                onChange={handleChange}
              />
              {formErrors.dueDate && <span className="error-text">{formErrors.dueDate}</span>}
            </div>

            <div className="form-group col">
              <label htmlFor="status">Initial Status</label>
              <select
                id="status"
                name="status"
                className="form-control"
                value={formData.status}
                onChange={handleChange}
              >
                <option value="To Do">To Do</option>
                <option value="In Progress">In Progress</option>
                <option value="Done">Done</option>
              </select>
            </div>
          </div>

          <div className="form-actions">
            <Link to="/tasks" className="btn-cancel">
              Cancel
            </Link>
            <button type="submit" className="btn-submit" disabled={submitting}>
              <Save size={18} />
              <span>{submitting ? 'Creating...' : 'Save Task'}</span>
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}
