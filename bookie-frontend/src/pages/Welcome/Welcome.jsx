import React, { useEffect } from 'react';
import styles from './Welcome.module.css';
import { Tooltip } from 'bootstrap'; 
import { useNavigate } from 'react-router-dom';

function Welcome() {
  const navigate = useNavigate();

  useEffect(() => {
    const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]');
    tooltipTriggerList.forEach((tooltipTriggerEl) => {
      new Tooltip(tooltipTriggerEl);
    });
  }, []);

  return (
    <div className={styles.container}>
      <h1>Welcome to Bookie</h1>
      <p>Please select your account type to get started:</p>

      <div className={styles.buttons}>
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
            title="User can browse, review, and rate books."
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
            title="Publisher can add new books (intended for publishing houses and indie authors)."
            style={{ fontSize: '1.2rem', cursor: 'pointer' }}
          ></i>
        </div>
      </div>
    </div>
  );
}

export default Welcome;
