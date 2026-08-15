import axios from 'axios';

const API_BASE_URL = 'http://localhost:5000/api/tasks';

const taskApi = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

export const taskService = {
  // Fetch tasks with optional filters: status, assignee, search, sort
  async getTasks(params = {}) {
    const queryParams = new URLSearchParams();
    if (params.status && params.status !== 'All') queryParams.append('status', params.status);
    if (params.assignee && params.assignee !== 'All') queryParams.append('assignee', params.assignee);
    if (params.search) queryParams.append('search', params.search);
    if (params.sort) queryParams.append('sort', params.sort);

    const queryString = queryParams.toString();
    const url = queryString ? `?${queryString}` : '';
    
    const response = await taskApi.get(url);
    return response.data;
  },

  // Get a single task by ID
  async getTaskById(id) {
    const response = await taskApi.get(`/${id}`);
    return response.data;
  },

  // Create a new task
  async createTask(taskData) {
    const response = await taskApi.post('', taskData);
    return response.data;
  },

  // Update a full task
  async updateTask(id, taskData) {
    const response = await taskApi.put(`/${id}`, taskData);
    return response.data;
  },

  // Update only task status
  async updateTaskStatus(id, status) {
    const response = await taskApi.patch(`/${id}/status`, { status });
    return response.data;
  },

  // Delete a task
  async deleteTask(id) {
    const response = await taskApi.delete(`/${id}`);
    return response.data;
  }
};
