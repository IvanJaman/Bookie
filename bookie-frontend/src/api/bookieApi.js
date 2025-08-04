import axios from 'axios';

const api = axios.create({
  baseURL: 'https://localhost:5001/api', 
  headers: { 'Content-Type': 'application/json' },
});

export const fetchBooks = () => api.get('/books');
export const fetchBookById = (id) => api.get(`/books/${id}`);
