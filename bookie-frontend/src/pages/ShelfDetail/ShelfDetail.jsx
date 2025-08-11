import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import api from '../../api/bookieApi';

export default function ShelfDetail() {
  const { shelfId } = useParams();
  const navigate = useNavigate();

  const [shelf, setShelf] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    const fetchShelf = async () => {
      try {
        const res = await api.get(`/shelves/${shelfId}`);

        const sortedBooks = res.data.books?.sort((a, b) => 
          new Date(b.addedAt) - new Date(a.addedAt)
        );

        setShelf({
        ...res.data,
        books: sortedBooks
      });
      } catch (err) {
        console.error(err);
        setError('Failed to load shelf.');
      } finally {
        setLoading(false);
      }
    };

    fetchShelf();
  }, [shelfId]);

  const handleDeleteShelf = async () => {
    if (!window.confirm('Are you sure you want to delete this shelf?')) return;

    try {
      await api.delete(`/shelves/${shelfId}`);
      navigate('/myprofile');
    } catch (err) {
      console.error(err);
      alert('Failed to delete shelf.');
    }
  };

  const handleRemoveBook = async (bookId) => {
    if (!window.confirm('Remove this book from the shelf?')) return;

    try {
      await api.delete(`/shelves/${shelfId}/books/${bookId}`);
      setShelf(prev => ({
        ...prev,
        books: prev.books.filter(b => b.bookId !== bookId)
      }));
    } catch (err) {
      console.error(err);
      alert('Failed to remove book from shelf.');
    }
  };

  const books = shelf?.books || [];

  if (loading) return <p>Loading shelf...</p>;
  if (error) return <p className="text-danger">{error}</p>;
  if (!shelf) return <p>Shelf not found.</p>;

  return (
    <div className="container mt-4">
      <div className="d-flex justify-content-between align-items-center mb-4">
        <h1>{shelf.name}</h1>
        <button className="btn btn-danger" onClick={handleDeleteShelf}>
          Delete Shelf
        </button>
      </div>

      {books.length === 0 ? (
        <p>No books in this shelf yet.</p>
      ) : (
        <div className="d-flex flex-column gap-4">
          {books.map((book) => (
            <div
              key={book.bookId}
              className="p-3 rounded shadow-sm d-flex position-relative"
              style={{ minHeight: '250px' }}
            >
              <img
                src={book.coverPhotoUrl}
                alt={book.title}
                className="img-fluid rounded"
                style={{
                  width: '160px',
                  minHeight: '250px',
                  objectFit: 'cover',
                  cursor: 'pointer',
                }}
                onClick={() => navigate(`/book/${book.bookId}`)}
              />

              <div className="ms-3 d-flex flex-column justify-content-between">
                <div>
                  <h5 className="fw-bold mb-1">{book.title}</h5>
                  <p className="mb-1"><strong>by {book.author}</strong></p>
                  <p className="mb-1">
                    <strong>Rating: {book.averageRating?.toFixed(2) ?? 'N/A'}</strong>
                  </p>
                  <p className="mb-0 text-muted">
                    {typeof book.blurb === 'string' && book.blurb.length > 500
                        ? `${book.blurb.slice(0, 500)}...`
                        : book.blurb || ''}
                    </p>
                </div>
                <button
                  className="btn btn-outline-danger btn-sm position-absolute"
                  style={{ bottom: '10px', right: '10px' }}
                  onClick={() => handleRemoveBook(book.bookId)}
                >
                  Remove from shelf
                </button>
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
}
