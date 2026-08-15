import React from 'react';
import { Search, Filter, User, RotateCcw, ArrowUpDown } from 'lucide-react';

export default function TaskFilters({
  searchTerm,
  setSearchTerm,
  statusFilter,
  setStatusFilter,
  assigneeFilter,
  setAssigneeFilter,
  sortOption,
  setSortOption,
  assigneesList = [],
  onClearFilters
}) {
  const hasActiveFilters = searchTerm !== '' || statusFilter !== 'All' || assigneeFilter !== 'All' || sortOption !== 'dueDate';

  return (
    <div className="filters-container">
      <div className="filter-group search-group">
        <Search className="filter-icon" size={18} />
        <input
          type="text"
          className="filter-input"
          placeholder="Search tasks by title..."
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
        />
      </div>

      <div className="filter-group select-group">
        <Filter className="filter-icon" size={16} />
        <select
          className="filter-select"
          value={statusFilter}
          onChange={(e) => setStatusFilter(e.target.value)}
        >
          <option value="All">All Statuses</option>
          <option value="To Do">To Do</option>
          <option value="In Progress">In Progress</option>
          <option value="Done">Done</option>
        </select>
      </div>

      <div className="filter-group select-group">
        <User className="filter-icon" size={16} />
        <select
          className="filter-select"
          value={assigneeFilter}
          onChange={(e) => setAssigneeFilter(e.target.value)}
        >
          <option value="All">All Assignees</option>
          {assigneesList.map((name) => (
            <option key={name} value={name}>
              {name}
            </option>
          ))}
        </select>
      </div>

      <div className="filter-group select-group">
        <ArrowUpDown className="filter-icon" size={16} />
        <select
          className="filter-select"
          value={sortOption}
          onChange={(e) => setSortOption(e.target.value)}
        >
          <option value="dueDate">Due Date (Earliest)</option>
          <option value="dueDateDesc">Due Date (Latest)</option>
        </select>
      </div>

      {hasActiveFilters && (
        <button className="clear-filters-btn" onClick={onClearFilters}>
          <RotateCcw size={15} />
          <span>Clear Filters</span>
        </button>
      )}
    </div>
  );
}
