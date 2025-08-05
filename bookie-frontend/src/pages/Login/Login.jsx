import React, { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import api from '../../api/bookieApi';

function Login() {
  const navigate = useNavigate();

  const [formData, setFormData] = useState({
    email: '',
    password: ''
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
      const response = await api.post('/auth/login', formData);

      if (response.data.token) {
        localStorage.setItem('token', response.data.token);
      }

      setMessage('Login successful!');
      console.log('Login response:', response.data);

      navigate('/'); // TO DO: Later change to dashboard/home
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
      <h2 className="mb-3">Login</h2>

      {message && (
        <div className="alert alert-info w-100 text-center" style={{ maxWidth: '400px' }}>
          {message}
        </div>
      )}

      <form
        onSubmit={handleSubmit}
        className="p-4 border rounded bg-light shadow-sm w-100"
        style={{ maxWidth: '400px' }}
      >
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

        <div className="mb-4">
          <input
            type="password"
            className="form-control"
            name="password"
            placeholder="Password"
            value={formData.password}
            onChange={handleChange}
            required
          />
        </div>

        <button type="submit" className="btn btn-primary w-100">
          Login
        </button>
      </form>

      <p className="mt-3">
        Don’t have an account? Register as{' '}
        <Link to="/register-user">User</Link> or{' '}
        <Link to="/register-publisher">Publisher</Link>
      </p>

      <button className="btn btn-link mt-2" onClick={() => navigate('/')}>
        Go Back
      </button>
    </div>
  );
}

export default Login;
