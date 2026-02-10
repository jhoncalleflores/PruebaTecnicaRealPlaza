import axios from 'axios';

const API = axios.create({
  baseURL: 'http://localhost:5177/api',
});

API.interceptors.request.use((config) => {
  const user = localStorage.getItem('user');
  if (user) {
    config.headers['X-User-Auth'] = 'true';
  }
  return config;
});

export default API;
