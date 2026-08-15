import React from 'react';
import { CheckCircle2, Clock, ListOrdered, CheckSquare } from 'lucide-react';

export default function TaskSummary({ tasks = [] }) {
  const total = tasks.length;
  const toDo = tasks.filter(t => t.status === 'To Do').length;
  const inProgress = tasks.filter(t => t.status === 'In Progress').length;
  const done = tasks.filter(t => t.status === 'Done').length;

  return (
    <section className="summary-section">
      <div className="summary-grid">
        <div className="summary-card total">
          <div className="summary-icon-box blue">
            <ListOrdered size={22} />
          </div>
          <div className="summary-details">
            <span className="summary-label">Total Tasks</span>
            <span className="summary-value">{total}</span>
          </div>
        </div>

        <div className="summary-card todo">
          <div className="summary-icon-box amber">
            <Clock size={22} />
          </div>
          <div className="summary-details">
            <span className="summary-label">To Do</span>
            <span className="summary-value">{toDo}</span>
          </div>
        </div>

        <div className="summary-card in-progress">
          <div className="summary-icon-box purple">
            <CheckSquare size={22} />
          </div>
          <div className="summary-details">
            <span className="summary-label">In Progress</span>
            <span className="summary-value">{inProgress}</span>
          </div>
        </div>

        <div className="summary-card done">
          <div className="summary-icon-box emerald">
            <CheckCircle2 size={22} />
          </div>
          <div className="summary-details">
            <span className="summary-label">Completed</span>
            <span className="summary-value">{done}</span>
          </div>
        </div>
      </div>
    </section>
  );
}
