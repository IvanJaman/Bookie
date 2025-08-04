import React, { useState } from 'react';
import styles from './RegisterPublisher.module.css';
import api from '../../api/bookieApi';
import { useNavigate } from 'react-router-dom';

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

  const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const response = await api.post('/auth/register-publisher', formData);
      setMessage('Publisher registered successfully!');
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
    <div className={styles.container}>
      <h2>Register as Publisher</h2>
      {message && <p>{message}</p>}
      <form onSubmit={handleSubmit} className={styles.form}>
        <input
          type="text"
          name="username"
          placeholder="Username"
          value={formData.username}
          onChange={handleChange}
          required
          minLength={3}
          maxLength={50}
        />
        <input
          type="email"
          name="email"
          placeholder="Email"
          value={formData.email}
          onChange={handleChange}
          required
        />
        <input
          type="url"
          name="websiteUrl"
          placeholder="Website URL (optional)"
          value={formData.websiteUrl}
          onChange={handleChange}
        />
        <input
          type="password"
          name="password"
          placeholder="Password"
          value={formData.password}
          onChange={handleChange}
          required
          minLength={8}
        />
        <input
          type="password"
          name="confirmPassword"
          placeholder="Confirm Password"
          value={formData.confirmPassword}
          onChange={handleChange}
          required
        />
        <button type="submit">Continue...</button>
      </form>

      <button
        className={styles.backButton}
        onClick={() => navigate('/')}
      >
        Go Back
      </button>
    </div>
  );
}

export default RegisterPublisher;
