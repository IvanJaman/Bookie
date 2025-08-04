import axios from 'axios';

const api = axios.create({
  baseURL: 'https://localhost:7114/api', 
});

export default api;
