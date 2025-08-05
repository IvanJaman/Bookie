import React, { useState } from 'react';
import api from '../../api/bookieApi';
import { useNavigate, Link } from 'react-router-dom';

function RegisterUser() {
  const navigate = useNavigate();

  const [formData, setFormData] = useState({
    username: '',
    email: '',
    password: '',
    confirmPassword: ''
  });

  const [message, setMessage] = useState('');

  const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const response = await api.post('/auth/register-user', formData);
      setMessage('User registered successfully!');
      console.log(response.data);
    } catch (error) {
      if (error.response) {
        setMessage(`Error: ${error.response.data}`);
      } else {
        setMessage('Something went wrong.');
      }
    }
  };

  return (
    <div className="container d-flex flex-column justify-content-center align-items-center min-vh-100">
      <h2 className="mb-3">Register as User</h2>
      <p className="mb-4">
        Already have an account? <Link to="/login">Login</Link>
      </p>
      {message && <div className="alert alert-info w-100 text-center">{message}</div>}
      
      <form onSubmit={handleSubmit} className="p-4 border rounded bg-light shadow-sm w-100" style={{ maxWidth: '400px' }}>
        <div className="mb-3">
          <input
            type="text"
            className="form-control"
            name="username"
            placeholder="Username"
            value={formData.username}
            onChange={handleChange}
            required
            minLength={3}
            maxLength={50}
          />
        </div>
        <div className="mb-3">
          <input
            type="email"
            className="form-control"
            name="email"
            placeholder="Email"
            value={formData.email}
            onChange={handleChange}
            required
          />
        </div>
        <div className="mb-3">
          <input
            type="password"
            className="form-control"
            name="password"
            placeholder="Password"
            value={formData.password}
            onChange={handleChange}
            required
            minLength={6}
          />
        </div>
        <div className="mb-4">
          <input
            type="password"
            className="form-control"
            name="confirmPassword"
            placeholder="Confirm Password"
            value={formData.confirmPassword}
            onChange={handleChange}
            required
          />
        </div>
        <button type="submit" className="btn btn-primary w-100">Continue...</button>
      </form>

      <button className="btn btn-link mt-3" onClick={() => navigate('/')}>
        Go Back
      </button>
    </div>
  );
}

export default RegisterUser;
