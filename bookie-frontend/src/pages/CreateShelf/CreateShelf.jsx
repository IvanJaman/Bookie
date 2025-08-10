import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../../api/bookieApi';

export default function CreateShelf() {
  const [name, setName] = useState('');
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');

    if (!name.trim()) {
      setError('Shelf name is required.');
      return;
    }

    setLoading(true);
    try {
      await api.post('/shelves', { name });
      navigate('/myprofile'); 
    } catch (err) {
      console.error(err);
      if (err.response?.data?.errors) {
        const modelErrors = Object.values(err.response.data.errors).flat();
        setError(modelErrors.join(' '));
      } else {
        setError('Failed to create shelf. Please try again.');
      }
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="container mt-4">
      <div className="row justify-content-center">
        <div className="col-md-6">
          <div className="card shadow-sm">
            <div className="card-body">
              <h2 className="card-title mb-4">Create New Shelf</h2>
              {error && <div className="alert alert-danger">{error}</div>}

              <form onSubmit={handleSubmit}>
                <div className="mb-3">
                  <label htmlFor="shelfName" className="form-label">
                    Shelf Name
                  </label>
                  <input
                    type="text"
                    id="shelfName"
                    className="form-control"
                    value={name}
                    onChange={(e) => setName(e.target.value)}
                    maxLength="100"
                    placeholder="Enter the name of your new shelf"
                  />
                  <div className="form-text">
                    Maximum 100 characters.
                  </div>
                </div>

                <button
                  type="submit"
                  className="btn btn-success"
                  disabled={loading}
                >
                  {loading ? 'Creating...' : 'Create Shelf'}
                </button>
                <button
                  type="button"
                  className="btn btn-secondary ms-2"
                  onClick={() => navigate('/myprofile')}
                >
                  Cancel
                </button>
              </form>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
