import React, { useEffect, useState } from 'react';
import { useParams, useNavigate, useLocation } from 'react-router-dom';
import api from '../../api/bookieApi';

export default function MyShelf() {
  const { shelfId } = useParams();
  const navigate = useNavigate();
  const location = useLocation();

  const [shelf, setShelf] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  const [renaming, setRenaming] = useState(false);
  const [newShelfName, setNewShelfName] = useState('');
  const [renamingLoading, setRenamingLoading] = useState(false);


  const sortBooks = (books) =>
    (books ?? []).slice().sort(
      (a, b) => new Date(b.addedAt) - new Date(a.addedAt) 
    );

  const fetchShelf = async () => {
    try {
      const res = await api.get(`/shelves/${shelfId}`);
      setShelf({
        ...res.data,
        books: sortBooks(res.data.books)
      });
    } catch (err) {
      console.error(err);
      setError('Failed to load shelf.');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    const loadData = async () => {
      setLoading(true);

      try {
        const res = await api.get(`/shelves/${shelfId}`);

        if (location.state?.searchResults) {
          setShelf({
            ...res.data,
            books: sortBooks(location.state.searchResults)
          });
        } else {
          setShelf({
            ...res.data,
            books: sortBooks(res.data.books)
          });
        }
      } catch (err) {
        console.error(err);
        setError('Failed to load shelf.');
      } finally {
        setLoading(false);
      }
    };

    loadData();
  }, [shelfId, location.state]);

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
        {renaming ? (
          <div className="d-flex gap-2">
            <input
              type="text"
              className="form-control"
              value={newShelfName}
              onChange={(e) => setNewShelfName(e.target.value)}
              placeholder="New shelf name"
            />
            <button
              className="btn btn-success"
              disabled={renamingLoading}
              onClick={async () => {
                if (!newShelfName.trim()) {
                  alert("Shelf name cannot be empty");
                  return;
                }
                setRenamingLoading(true);
                try {
                  await api.put(`/shelves/rename/${shelfId}`, { newName: newShelfName.trim() });
                  setShelf(prev => ({ ...prev, name: newShelfName.trim() }));
                  setRenaming(false);
                } catch (err) {
                  console.error(err);
                  alert("Failed to rename shelf");
                } finally {
                  setRenamingLoading(false);
                }
              }}
            >
              Save
            </button>
            <button
              className="btn btn-secondary"
              onClick={() => setRenaming(false)}
            >
              Cancel
            </button>
          </div>
        ) : (
          <>
            <h1>{shelf.name}</h1>
            <div className="d-flex gap-2">
              <button
                className="btn btn-success"
                onClick={() => {
                  setRenaming(true);
                  setNewShelfName(shelf.name);
                }}
              >
                Rename Shelf
              </button>
              <button className="btn btn-danger" onClick={handleDeleteShelf}>
                Delete Shelf
              </button>
            </div>
          </>
        )}
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
