import React, { useEffect, useState } from 'react';
import { useLocation } from 'react-router-dom';
import api from '../../api/bookieApi'; 

function Home() {
  const location = useLocation();
  const [booksByGenre, setBooksByGenre] = useState({});
  const [searchResults, setSearchResults] = useState(null);

  useEffect(() => {
    if (location.state?.searchResults) {
      setSearchResults(location.state.searchResults);
    } else {
      api.get('/books/recently-added-by-genre')
        .then(res => setBooksByGenre(res.data))
        .catch(err => console.error('Failed to fetch books by genre:', err));
    }
  }, [location.state]);

    return (
    <div className="container my-4">
      {searchResults ? (
        <>
          <div className="d-flex flex-column gap-4">
            {searchResults.map(book => (
              <div
                key={book.id}
                className="p-3 rounded shadow-sm d-flex"
                style={{ backgroundColor: 'rgba(128, 228, 149, 0.5)' }}
              >
                <img
                  src={book.coverPhotoUrl}
                  alt={book.title}
                  className="img-fluid rounded"
                  style={{
                    width: '180px',
                    minHeight: '300px',
                    objectFit: 'cover',
                    cursor: 'pointer'
                  }}
                  onClick={() => window.location.href = `/book/${book.id}`}
                />

                <div className="ms-3 d-flex flex-column justify-content-between">
                  <div>
                    <h5 className="fw-bold mb-1">{book.title}</h5>
                    <p className="mb-1"><strong>by {book.author}</strong> </p>
                    <p className="mb-1"><strong>Rating: {book.averageRating}</strong></p>
                    <p className="mb-0 text-muted">
                      {book.blurb.length > 200 ? `${book.blurb.slice(0, 200)}...` : book.blurb}
                    </p>
                  </div>
                </div>
              </div>
            ))}
          </div>
        </>
      ) : (
        Object.entries(booksByGenre).map(([genre, books]) => (
          <div
            key={genre}
            className="p-4 mb-4 rounded shadow-sm"
            style={{ backgroundColor: 'rgba(128, 228, 149, 0.3)' }}
          >
            <div className="d-flex justify-content-between align-items-center mb-3">
              <h4 className="mb-0">Newest from {genre}</h4>
            </div>
            <div className="d-flex overflow-auto gap-3">
              {books.map(book => (
                <div
                  key={book.id}
                  className="p-2 rounded bg-success-subtle shadow-sm"
                  style={{
                    width: '140px',
                    minHeight: '260px',
                    display: 'flex',
                    flexDirection: 'column',
                    alignItems: 'center',
                    flexShrink: 0, 
                    cursor: 'pointer'
                  }}
                  onClick={() => window.location.href = `/book/${book.id}`}
                >
                  <img
                    src={book.coverPhotoUrl}
                    alt={book.title}
                    className="img-fluid rounded"
                    style={{
                      height: '180px',
                      width: '100%',
                      objectFit: 'cover',
                    }}
                  />
                  <p
                    className="mt-2 text-center fw-semibold"
                    style={{
                      maxWidth: '100%',
                      wordWrap: 'break-word',
                      fontSize: '0.95rem',
                      lineHeight: '1.2',
                    }}
                  >
                    {book.title}
                  </p>
                </div>
              ))}
            </div>
          </div>
        ))
      )}
    </div>
  );
}

export default Home;