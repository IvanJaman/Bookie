import React, { useEffect } from 'react';
import { Tooltip } from 'bootstrap';
import { useNavigate, Link } from 'react-router-dom';

function Welcome() {
  const navigate = useNavigate();

  useEffect(() => {
    const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]');
    tooltipTriggerList.forEach((tooltipTriggerEl) => {
      new Tooltip(tooltipTriggerEl);
    });
  }, []);

  return (
    <div className="container d-flex flex-column justify-content-center align-items-center min-vh-100 text-center">
      <h1 className="mb-3">Welcome to Bookie</h1>
      <h2 className="lead">Your favourite place to browse, rate and review  books.</h2>
      <p className="mb-4">Please select your account type to get started:</p>

      <div className="d-flex flex-column flex-md-row gap-4 mb-4">
        <div className="d-flex align-items-center gap-2">
          <button
            className="btn btn-primary btn-lg"
            onClick={() => navigate('/register-user')}
          >
            Register as User
          </button>
          <i
            className="bi bi-info-circle"
            data-bs-toggle="tooltip"
            data-bs-placement="bottom"
            title="Users can browse, rate and review books."
            style={{ fontSize: '1.2rem', cursor: 'pointer' }}
          ></i>
        </div>

        <div className="d-flex align-items-center gap-2">
          <button
            className="btn btn-primary btn-lg"
            onClick={() => navigate('/register-publisher')}
          >
            Register as Publisher
          </button>
          <i
            className="bi bi-info-circle"
            data-bs-toggle="tooltip"
            data-bs-placement="bottom"
            title="Publishers can add new books (intended for publishing houses and indie authors)."
            style={{ fontSize: '1.2rem', cursor: 'pointer' }}
          ></i>
        </div>
      </div>

      <p>
        Already have an account? <Link to="/login">Login</Link>
      </p>
    </div>
  );
}

export default Welcome;