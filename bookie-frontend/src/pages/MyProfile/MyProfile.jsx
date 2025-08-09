import React, { useEffect, useState } from 'react';
import api from '../../api/bookieApi';
import { useNavigate } from 'react-router-dom';

export default function MyProfile() {
  const [user, setUser] = useState(null);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchData = async () => {
      try {
        const userRes = await api.get('/users/me');
        const shelvesRes = await api.get(`/shelves/user/${userRes.data.id}`);

        setUser({ ...userRes.data, shelves: shelvesRes.data });
      } catch (err) {
        console.error('Failed to fetch profile or shelves:', err);
      } finally {
        setLoading(false);
      }
    };
    fetchData();
  }, []);

  if (loading) return <p>Loading profile...</p>;
  if (!user) return <p>Profile not found.</p>;

  const shelves = Array.isArray(user.shelves) ? user.shelves : [];

  return (
    <div className="position-relative p-4 border rounded bg-white shadow-sm">
      <div className="position-absolute top-0 end-0 m-3 d-flex gap-2">
        <button
          className="btn btn-primary btn-sm"
          onClick={() => navigate('/update-profile')}
        >
          Update Profile
        </button>
        <button
          className="btn btn-secondary btn-sm"
          onClick={() => navigate('/change-password')}
        >
          Change Password
        </button>
      </div>

      <h1 className="mb-3">{user.username}</h1>
      <p><strong>Email:</strong> {user.email}</p>
      <p><strong>Bio:</strong> {user.bio || 'No bio yet.'}</p>
      <p><strong>Role:</strong> {user.roleName}</p>

      <h2 className="mt-4">My Shelves</h2>  
      <div className="d-flex flex-wrap gap-3 mt-3">
        {shelves.length === 0 ? (
            <p>No shelves found.</p>
        ) : (
            shelves.map((shelf) => {
            const sortedBooks = [...shelf.books].sort(
                (a, b) => new Date(b.addedAt) - new Date(a.addedAt)
            );
            const lastFourBooks = sortedBooks.slice(0, 4);

            const handleShelfClick = () => {                
                const encodedShelfName = encodeURIComponent(shelf.name);
                navigate(`/myshelves/${encodedShelfName}`);
                
            };

            return (
                <div
                key={shelf.id}
                className="card"
                style={{ width: '180px', cursor: 'pointer' }}
                onClick={handleShelfClick}
                >
                {lastFourBooks.length > 0 ? (
                    <div
                    className="d-grid"
                    style={{
                        gridTemplateColumns: 'repeat(2, 1fr)',
                        gridTemplateRows: 'repeat(2, 125px)',
                        height: '250px',
                        overflow: 'hidden',
                    }}
                    >
                    {lastFourBooks.map((book) => (
                        <img
                        key={book.bookId}
                        src={book.coverPhotoUrl}
                        alt={`Cover of ${book.title}`}
                        style={{ width: '100%', height: '100%', objectFit: 'cover' }}
                        />
                    ))}
                    </div>
                ) : (
                    <div
                    className="d-flex align-items-center justify-content-center bg-secondary text-white"
                    style={{ height: '250px' }}
                    >
                    No books yet
                    </div>
                )}
                <div className="card-body p-2">
                    <h5 className="card-title mb-1">{shelf.name}</h5>
                    <p className="card-text mb-0">
                    {shelf.books.length} book{shelf.books.length !== 1 ? 's' : ''}
                    </p>
                </div>
                </div>
            );
            })
        )}
        </div>
    </div>
  );
}
