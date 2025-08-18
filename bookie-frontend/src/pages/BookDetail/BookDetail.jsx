import React, { useEffect, useState } from 'react';
import { Link, useParams, useNavigate } from 'react-router-dom';
import api from '../../api/bookieApi';
import 'bootstrap-icons/font/bootstrap-icons.css';
import { getUserIdFromToken, getUsernameFromToken, getUserRole } from '../../utils/auth';

function StarRating({ rating, maxStars = 5, size = '1.5rem' }) {
  const fullStars = Math.floor(rating);
  const halfStar = rating - fullStars >= 0.5;
  const emptyStars = maxStars - fullStars - (halfStar ? 1 : 0);

  const starStyle = { fontSize: size, marginLeft: '2px' };

  return (
    <span style={{ display: 'inline-flex', alignItems: 'center' }}>
      {[...Array(fullStars)].map((_, i) => (
        <i key={`full-${i}`} className="bi bi-star-fill text-success" style={starStyle}></i>
      ))}
      {halfStar && <i className="bi bi-star-half text-success" style={starStyle}></i>}
      {[...Array(emptyStars)].map((_, i) => (
        <i key={`empty-${i}`} className="bi bi-star text-success" style={starStyle}></i>
      ))}
    </span>
  );
}

export default function BookDetail() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [book, setBook] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  const [showModal, setShowModal] = useState(false);
  const [shelves, setShelves] = useState([]);
  const [adding, setAdding] = useState(false);

  const userRole = getUserRole();
  const userId = getUserIdFromToken();
  const isAdmin = userRole === "Admin";

  const [reviews, setReviews] = useState([]);
  const [loadingReviews, setLoadingReviews] = useState(true);
  const [errorReviews, setErrorReviews] = useState('');

  const [newRating, setNewRating] = useState(5);
  const [newText, setNewText] = useState('');
  const [submittingReview, setSubmittingReview] = useState(false);
  const [editingReviewId, setEditingReviewId] = useState(null);

  useEffect(() => {
    async function fetchBook() {
      try {
        const res = await api.get(`/books/${id}`);
        setBook(res.data);
      } catch {
        setError('Failed to load book details.');
      } finally {
        setLoading(false);
      }
    }

    async function fetchReviews() {
    try {
        const res = await api.get(`/reviews/book/${id}`);
        const sortedReviews = [...res.data].sort((a, b) => b.rating - a.rating);
        setReviews(sortedReviews);
    } catch {
        setErrorReviews('Failed to load reviews.');
    } finally {
        setLoadingReviews(false);
    }
    }

    fetchBook();
    fetchReviews();
  }, [id]);

  const openModal = async () => {
    try {
      const userId = getUserIdFromToken();
      if (!userId) {
        alert("User ID not found. Please log in again.");
        return;
      }

      const res = await api.get(`/shelves/user/${userId}`);
      setShelves(res.data);
      setShowModal(true);
    } catch (err) {
      console.error(err);
      alert("Failed to load shelves.");
    }
  };

  const addToShelf = async (shelfId) => {
    setAdding(true);
    try {
      await api.post(`/shelves/${shelfId}/books/${id}`);
      alert('Book succesfully added to shelf!');
      setShowModal(false);
    } catch (err) {
      console.error(err);
      alert('Failed to add book.');
    } finally {
      setAdding(false);
    }
  };

  const handleDeleteBook = async () => {
    if (!window.confirm("Are you sure you want to delete this book?")) return;

    try {
      await api.delete(`/books/${id}`);
      navigate('/');
    } catch (err) {
      console.error(err);
      alert("Failed to delete book.");
    }
  };

  const handleDeleteReview = async (reviewId) => {
    if (!window.confirm("Are you sure you want to delete this review?")) return;

    try {
      await api.delete(`/reviews/${reviewId}`);
      setReviews(prev => prev.filter(r => r.id !== reviewId));
      alert('Review deleted successfully!');
    } catch (err) {
      console.error(err);
      alert('Failed to delete review');
    }
  };

  const handleSubmitReview = async (e) => {
    e.preventDefault();
    if (newRating < 1 || newRating > 5) {
      alert('Rating must be between 1 and 5');
      return;
    }
    setSubmittingReview(true);

    try {
      if (editingReviewId) {
        await api.put(`/reviews/${editingReviewId}`, {
          rating: newRating,
          text: newText.trim(),
        });
        alert('Review updated!');
        setEditingReviewId(null); 
      } else {
        await api.post('/reviews', {
          bookId: id,
          rating: newRating,
          text: newText.trim(),
        });
        alert('Review submitted!');
      }

      setNewRating(5);
      setNewText('');

      const res = await api.get(`/reviews/book/${id}`);
      const sortedReviews = [...res.data].sort((a, b) => b.rating - a.rating);
      setReviews(sortedReviews);

      document.getElementById('reviewsSection')?.scrollIntoView({ behavior: 'smooth' });

    } catch (err) {
      console.error(err);
      alert('Failed to submit review');
    } finally {
      setSubmittingReview(false);
    }
  };

  if (loading) return <p>Loading book...</p>;
  if (error) return <p className="text-danger">{error}</p>;
  if (!book) return <p>Book not found.</p>;

  const canDelete =
    userRole === "Admin" ||
    (userRole === "Publisher" && userId && userId === book.createdByUserId);

  return (
    <div className="container mt-4 position-relative pb-5">
      <div className="row">
        <div className="col-md-4">
          <img
            src={book.coverPhotoUrl}
            alt={book.title}
            className="img-fluid rounded shadow-sm"
            style={{ objectFit: 'cover' }}
          />
        </div>

        <div className="col-md-8">
          <h1 className="fw-bold">{book.title}</h1>
          <h4 className="text-muted mb-3">by {book.author}</h4>
          <p><strong>Genre:</strong> {book.genreName}</p>
          <p><strong>Language:</strong> {book.languageName}</p>
          <p><strong>Year of Publication:</strong> {book.publicationYear}</p>
          <p><strong>Page Count:</strong> {book.pageCount}</p>
          <p><strong>ISBN:</strong> {book.isbn}</p>
          <p><strong>Published by:</strong> {book.createdByUsername}</p>

          <p>
            <strong style={{ display: 'inline-flex', alignItems: 'center', gap: '0.3em' }}>
              Average Rating:
              <StarRating rating={book.averageRating} size="1.5rem" />
              ({book.averageRating})
            </strong>
          </p>

          <div className="d-flex gap-2 mb-3">
            <button className="btn btn-success" onClick={openModal}>
              Add to Shelf
            </button>

            <a
              href={book.getBookUrl || "#"}
              target="_blank"
              rel="noopener noreferrer"
              className={`btn btn-success ${!book.getBookUrl ? "opacity-50" : ""}`}
              aria-disabled={!book.getBookUrl}
              title={!book.getBookUrl ? "E-book not available" : undefined}
              onClick={(e) => {
                if (!book.getBookUrl) e.preventDefault();
              }}
            >
              Get an E-book!
            </a>

            {canDelete && (
              <button className="btn btn-danger" onClick={handleDeleteBook}>
                Delete Book
              </button>
            )}
          </div>

          <div className="mt-4">
            <h5>Blurb</h5>
            <p>{book.blurb}</p>
          </div>

          {showModal && (
            <div className="modal show d-block" tabIndex="-1">
              <div className="modal-dialog">
                <div className="modal-content">
                  <div className="modal-header">
                    <h5 className="modal-title">Select Shelf</h5>
                    <button type="button" className="btn-close" onClick={() => setShowModal(false)}></button>
                  </div>
                  <div className="modal-body">
                    {shelves.length > 0 ? (
                      shelves.map(shelf => (
                        <button
                          key={shelf.id}
                          className="btn btn-success w-100 mb-2"
                          disabled={adding}
                          onClick={() => addToShelf(shelf.id)}
                        >
                          {shelf.name}
                        </button>
                      ))
                    ) : (
                      <p>No shelves found.</p>
                    )}
                  </div>
                  <div className="modal-footer">
                    <button type="button" className="btn btn-secondary" onClick={() => setShowModal(false)}>Close</button>
                  </div>
                </div>
              </div>
            </div>
          )}
        </div>
      </div>

      <div className="row mt-5 " id="reviewsSection">
        <div className="col-12">
          <h4>Reviews</h4>

        <form id="reviewForm" onSubmit={handleSubmitReview} className="mb-4 shadow-sm rounded p-3">
            <div className="mb-3">
            <label htmlFor="rating" className="form-label">Rate this book (1-5)</label>
            <select
                id="rating"
                className="form-select"
                value={newRating}
                onChange={(e) => setNewRating(parseInt(e.target.value, 10))}
                disabled={submittingReview}
                required
            >
                {[1, 2, 3, 4, 5].map(num => (
                <option key={num} value={num}>{num}</option>
                ))}
            </select>
            </div>

            <div className="mb-3">
            <label htmlFor="reviewText" className="form-label">Tell us how you feel about this book (optional)</label>
            <textarea
                id="reviewText"
                className="form-control"
                rows="3"
                value={newText}
                onChange={(e) => setNewText(e.target.value)}
                disabled={submittingReview}
                maxLength={1000}
                placeholder="Write your review here..."
            />
            </div>

            <button type="submit" className="btn btn-success" disabled={submittingReview}>
            {submittingReview ? 'Submitting...' : 'Submit Review'}
            </button>
        </form>

          {loadingReviews ? (
            <p>Loading reviews...</p>
          ) : errorReviews ? (
            <p className="text-danger">{errorReviews}</p>
          ) : reviews.length === 0 ? (
            <p>No reviews yet.</p>
          ) : (
            <div className="list-group">
              {reviews.map(review => (
                <div key={review.id} className="list-group-item">
                  <div className="d-flex justify-content-between align-items-center mb-1">
                    <strong>

                    <Link
                        to={review.userId === userId ? '/myProfile' : `/users/${review.userId}`}
                        className="text-decoration-none"
                      >
                        {review.username || "Anonymous"}
                      </Link>{" "}  

                    (
                    {new Date(review.createdAt).toLocaleDateString(undefined, {
                        year: 'numeric',
                        month: 'short',
                        day: 'numeric',
                    })}
                    )
                    </strong>
                    <StarRating rating={review.rating} size="1rem" />
                  </div>

                  {review.text && <p className="mb-0">{review.text}</p>}

                  {(review.userId === userId || isAdmin) && (
                  <div className="d-flex gap-2 mt-2">
                    {review.userId === userId && !isAdmin && (
                      <button
                        className="btn btn-sm btn-outline-success"
                        onClick={() => {
                          setEditingReviewId(review.id); 
                          setNewRating(review.rating);
                          setNewText(review.text || '');
                          document.getElementById('reviewForm')?.scrollIntoView({ behavior: 'smooth' });
                        }}
                      >
                        Update review
                      </button>
                    )}

                    <button
                      className="btn btn-sm btn-outline-danger"
                      onClick={() => handleDeleteReview(review.id)}
                    >
                      Delete review
                    </button>
                  </div>
                )}
                </div>
              ))}
            </div>
          )}
        </div>
      </div>
    </div>
  );
}
