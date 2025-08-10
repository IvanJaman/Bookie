import React, { useState } from 'react';
import api from '../../api/bookieApi';
import { useNavigate } from 'react-router-dom';

export default function ChangePassword() {
  const [form, setForm] = useState({
    currentPassword: '',
    newPassword: '',
    confirmPassword: ''
  });
  const [saving, setSaving] = useState(false);
  const [error, setError] = useState('');
  const [success, setSuccess] = useState('');
  const navigate = useNavigate();

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
    setSuccess('');

    if (form.newPassword !== form.confirmPassword) {
      setError('Passwords do not match.');
      setSaving(false);
      return;
    }

    try {
      await api.put('/users/change-password', form);
      setSuccess('Password changed successfully.');
      setTimeout(() => navigate('/myprofile'), 1500);
    } catch (err) {
      console.error(err);
      setError(err.response?.data || 'Password change failed.');
    } finally {
      setSaving(false);
    }
  };

  return (
    <div className="container mt-4">
      <h2>Change Password</h2>

      {error && <div className="alert alert-danger">{error}</div>}
      {success && <div className="alert alert-success">{success}</div>}

      <form onSubmit={handleSubmit} className="mt-3">
        <div className="mb-3">
          <label className="form-label">Current Password</label>
          <input
            type="password"
            name="currentPassword"
            value={form.currentPassword}
            onChange={handleChange}
            className="form-control"
            required
          />
        </div>

        <div className="mb-3">
          <label className="form-label">New Password</label>
          <input
            type="password"
            name="newPassword"
            value={form.newPassword}
            onChange={handleChange}
            className="form-control"
            required
          />
        </div>

        <div className="mb-3">
          <label className="form-label">Confirm New Password</label>
          <input
            type="password"
            name="confirmPassword"
            value={form.confirmPassword}
            onChange={handleChange}
            className="form-control"
            required
          />
        </div>

        <button
          type="submit"
          className="btn btn-primary"
          disabled={saving}
        >
          {saving ? 'Changing...' : 'Change Password'}
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
