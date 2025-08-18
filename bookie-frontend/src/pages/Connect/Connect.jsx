import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../../api/bookieApi';

export default function Connect() {
  const [users, setUsers] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const navigate = useNavigate();

  useEffect(() => {
    const fetchUsers = async () => {
      setLoading(true);
      try {
        const res = await api.get('/users'); 
        setUsers(res.data);
      } catch (err) {
        console.error(err);
        setError('Failed to load users.');
      } finally {
        setLoading(false);
      }
    };

    fetchUsers();
  }, []);

  if (loading) return <p>Loading users...</p>;
  if (error) return <p className="text-danger">{error}</p>;
  if (users.length === 0) return <p>No users found.</p>;

  return (
    <div className="container mt-4">
      <h1 className="mb-4">Connect with Users</h1>
      <div className="list-group">
        {users.map((user) => (
          <div
            key={user.id}
            className="list-group-item d-flex justify-content-between align-items-center shadow-sm rounded mb-2 p-3"
          >
            <div>
              <h5 className="mb-1">{user.username}</h5>
              {user.bio && <p className="mb-0 text-muted">{user.bio}</p>}
            </div>
            <button
              className="btn btn-success"
              onClick={() => navigate(`/users/${user.id}`)}
            >
              See Profile
            </button>
          </div>
        ))}
      </div>
    </div>
  );
}
