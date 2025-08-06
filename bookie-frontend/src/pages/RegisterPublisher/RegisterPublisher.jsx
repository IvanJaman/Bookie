import React, { useState } from 'react';
import api from '../../api/bookieApi';
import { useNavigate, Link } from 'react-router-dom';

function RegisterPublisher() {
  const navigate = useNavigate();

  const [formData, setFormData] = useState({
    username: '',
    email: '',
    websiteUrl: '',
    password: '',
    confirmPassword: ''
  });

  const [message, setMessage] = useState('');

  const [showPassword, setShowPassword] = useState(false);

  const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      await api.post('/auth/register-publisher', formData);
      setMessage('Publisher registered successfully! Logging in...');

      const loginResponse = await api.post('/auth/login', {
        email: formData.email,
        password: formData.password
      });

      if (loginResponse.data.token) {
        localStorage.setItem('token', loginResponse.data.token);
      }

      navigate('/home');
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
      <h2 className="mb-3">Register as Publisher</h2>
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
            type="websiteUrl"
            className="form-control"
            name="websiteUrl"
            placeholder="website URL"
            value={formData.websiteUrl}
            onChange={handleChange}
            required
          />
        </div>
        <div className="mb-4 position-relative">
          <input
            type={showPassword ? 'text' : 'password'} 
            className="form-control"
            name="password"
            placeholder="Password"
            value={formData.password}
            onChange={handleChange}
            required
          />
          <i
            className={`bi ${showPassword ? 'bi-eye-slash' : 'bi-eye'}`} 
            style={{
              position: 'absolute',
              top: '50%',
              right: '10px',
              transform: 'translateY(-50%)',
              cursor: 'pointer',
              fontSize: '1.2rem'
            }}
            onClick={() => setShowPassword(!showPassword)} 
          ></i>
        </div>
        <div className="mb-4 position-relative">
          <input
            type={showPassword ? 'text' : 'password'} 
            className="form-control"
            name="confirmPassword"
            placeholder="Confirm Password"
            value={formData.confirmPassword}
            onChange={handleChange}
            required
          />
          <i
            className={`bi ${showPassword ? 'bi-eye-slash' : 'bi-eye'}`} 
            style={{
              position: 'absolute',
              top: '50%',
              right: '10px',
              transform: 'translateY(-50%)',
              cursor: 'pointer',
              fontSize: '1.2rem'
            }}
            onClick={() => setShowPassword(!showPassword)} 
          ></i>
        </div>
        <button type="submit" className="btn btn-primary w-100">Continue...</button>
      </form>

      <button className="btn btn-link mt-3" onClick={() => navigate('/')}>
        Go Back
      </button>
    </div>
  );
}

export default RegisterPublisher;
