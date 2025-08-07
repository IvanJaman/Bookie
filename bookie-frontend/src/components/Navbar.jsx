import React, { useState, useEffect } from 'react';
import { Link, useNavigate, useLocation } from 'react-router-dom';
import api from '../api/bookieApi';
import { Tooltip } from 'bootstrap';
import { getUsernameFromToken } from '../utils/auth';

function Navbar({ userRole }) {
  const [searchTerm, setSearchTerm] = useState('');
  const [username, setUsername] = useState('');
  const navigate = useNavigate();
  const location = useLocation();

  useEffect(() => {
    const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]');
    const tooltipList = [...tooltipTriggerList].map(
      (el) => new Tooltip(el, { trigger: 'hover' })
    );

    return () => {
      tooltipList.forEach((tooltip) => tooltip.dispose());
    };
  }, []);

  useEffect(() => {
    const name = getUsernameFromToken();
    if (name) setUsername(name);
  }, []);

  const handleSearch = async (e) => {
    e.preventDefault();

    try {
      let response;

      if (location.pathname.startsWith('/shelf/')) {
        const shelfId = location.pathname.split('/')[2];
        response = await api.get(`/books/shelves/${shelfId}/search`, {
          params: { title: searchTerm }
        });

        navigate(`/shelf/${shelfId}`, { state: { searchResults: response.data } });

      } else {
        response = await api.get('/books/search', {
          params: { title: searchTerm }
        });

        navigate('/home', { state: { searchResults: response.data } });
      }
    } catch (error) {
      console.error('Search error:', error);
    }
  };

  const handleLogout = () => {
    localStorage.removeItem('token');
    navigate('/');
  };

  return (
    <nav className="navbar navbar-expand-lg navbar-dark bg-primary px-3">
      <Link
        className="navbar-brand fw-bold me-4"
        style={{ fontSize: '28px' }}
        to="/home"
      >
        Bookie
      </Link>

      <div className="collapse navbar-collapse" id="navbarContent">
        <form
          className="d-flex mx-auto"
          style={{ maxWidth: '500px', width: '100%' }}
          onSubmit={handleSearch}
        >
          <input
            className="form-control me-2"
            type="search"
            placeholder="Search for books..."
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
            style={{ height: '32px', padding: '4px 8px', fontSize: '1rem' }}
          />
          <button
            className="btn btn-light"
            type="submit"
            style={{ height: '32px', padding: '4px 10px' }}
          >
            <i className="bi bi-search" style={{ fontSize: '1rem' }}></i>
          </button>
        </form>

        <ul className="navbar-nav d-flex flex-row align-items-center gap-2 ms-auto">
          {username && (
            <li className="nav-item text-white fw-semibold">{username}</li>
          )}

          {(userRole === 'Publisher' || userRole === 'Admin') && (
            <li className="nav-item">
              <Link
                className="nav-link"
                to="/add-book"
                data-bs-toggle="tooltip"
                data-bs-placement="bottom"
                title="Add a new book"
              >
                <i className="bi bi-plus-circle" style={{ fontSize: '2rem' }}></i>
              </Link>
            </li>
          )}

          <li className="nav-item">
            <Link
              className="nav-link"
              to="/myProfile"
              data-bs-toggle="tooltip"
              data-bs-placement="bottom"
              title="View profile"
            >
              <i className="bi bi-person-circle" style={{ fontSize: '2rem' }}></i>
            </Link>
          </li>

          <li className="nav-item">
            <button
              className="nav-link btn btn-link p-0 text-white d-flex align-items-center"
              onClick={handleLogout}
              data-bs-toggle="tooltip"
              data-bs-placement="bottom"
              title="Log out"
            >
              <i className="bi bi-box-arrow-right" style={{ fontSize: '2rem' }}></i>
            </button>
          </li>
        </ul>
      </div>
    </nav>
  );
}

export default Navbar;
