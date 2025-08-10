import React, { useEffect, useState } from 'react';
import api from '../../api/bookieApi';
import { useNavigate } from 'react-router-dom';

export default function UpdateProfile() {
  const [form, setForm] = useState({
    username: '',
    bio: ''
  });
  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);
  const [error, setError] = useState('');
  const navigate = useNavigate();

  useEffect(() => {
    const fetchProfile = async () => {
      try {
        const res = await api.get('/users/me');
        setForm({
          username: res.data.username || '',
          bio: res.data.bio || ''
        });
      } catch (err) {
        console.error(err);
        setError('Failed to load profile.');
      } finally {
        setLoading(false);
      }
    };
    fetchProfile();
  }, []);

  const handleChange = (e) => {
    setForm((prev) => ({
      ...prev,
      [e.target.name]: e.target.value
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setSaving(true);
    setError('');

    try {
      await api.put('/users/profile', form);
      navigate('/myprofile');
    } catch (err) {
      console.error(err);
      setError(err.response?.data || 'Profile update failed.');
    } finally {
      setSaving(false);
    }
  };

  if (loading) return <p>Loading...</p>;

  return (
    <div className="container mt-4">
      <h2>Update Profile</h2>

      {error && <div className="alert alert-danger">{error}</div>}

      <form onSubmit={handleSubmit} className="mt-3">
        <div className="mb-3">
          <label className="form-label">Username</label>
          <input
            type="text"
            name="username"
            value={form.username}
            onChange={handleChange}
            className="form-control"
            minLength={3}
            maxLength={50}
            required
          />
        </div>

        <div className="mb-3">
          <label className="form-label">Bio</label>
          <textarea
            name="bio"
            value={form.bio}
            onChange={handleChange}
            className="form-control"
            maxLength={500}
            rows={4}
          />
        </div>

        <button
          type="submit"
          className="btn btn-primary"
          disabled={saving}
        >
          {saving ? 'Saving...' : 'Save Changes'}
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
  );
}
