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
          <div className="d-flex flex-wrap gap-3">
            {searchResults.map(book => (
              <div key={book.id} className="text-center" style={{ width: '150px' }}>
                <img
                  src={book.coverPhotoUrl}
                  alt={book.title}
                  className="img-fluid rounded"
                  style={{ height: '200px', objectFit: 'cover' }}
                />
                <p className="mt-2 fw-semibold">{book.title}</p>
              </div>
            ))}
          </div>
        </>
      ) : (
        Object.entries(booksByGenre).map(([genre, books]) => (
          <div
            key={genre}
            className="p-4 mb-4 rounded shadow-sm"
            style={{ backgroundColor: 'rgba(40, 167, 69, 0.1)' }}
          >
            <div className="d-flex justify-content-between align-items-center mb-3">
              <h4 className="mb-0">Newest from {genre}</h4>
              {/* Add See More button later here */}
            </div>
            <div className="d-flex overflow-auto gap-3">
              {books.map(book => (
                <div key={book.id} style={{ minWidth: '120px' }}>
                  <img
                    src={book.coverPhotoUrl}
                    alt={book.title}
                    className="img-fluid rounded"
                    style={{ height: '180px', objectFit: 'cover', width: '100%' }}
                  />
                  <p className="mt-2 text-center">{book.title}</p>
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