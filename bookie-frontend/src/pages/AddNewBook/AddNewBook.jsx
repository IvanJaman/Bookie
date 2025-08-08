import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../../api/bookieApi';  

function AddBook() {
  const navigate = useNavigate();
  const [formData, setFormData] = useState({
    author: '',
    title: '',
    isbn: '',
    blurb: '',
    publicationYear: '',
    pageCount: '',
    language: '',
    coverPhotoUrl: '',
    getBookUrl: '',
    genreName: ''
  });

  const [errors, setErrors] = useState({});
  const [submitError, setSubmitError] = useState('');
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [genres, setGenres] = useState([]);

  const validate = () => {
    const newErrors = {};
    if (!formData.author.trim()) newErrors.author = 'Author is required';
    if (!formData.title.trim()) newErrors.title = 'Title is required';
    if (!formData.isbn.trim()) newErrors.isbn = 'ISBN is required';
    if (!formData.publicationYear || isNaN(formData.publicationYear)) newErrors.publicationYear = 'Valid publication year is required';
    if (!formData.pageCount || isNaN(formData.pageCount)) newErrors.pageCount = 'Valid page count is required';
    if (!formData.language.trim()) newErrors.language = 'Language is required';
    if (!formData.genreName.trim()) newErrors.genreName = 'Genre is required';
    return newErrors;
  };

  const handleChange = (e) => {
    setFormData(prev => ({
      ...prev,
      [e.target.name]: e.target.value
    }));
    setErrors(prev => ({ ...prev, [e.target.name]: '' }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setSubmitError('');
    const validationErrors = validate();
    if (Object.keys(validationErrors).length > 0) {
      setErrors(validationErrors);
      return;
    }

    setIsSubmitting(true);

    try {
      const payload = {
        ...formData,
        publicationYear: Number(formData.publicationYear),
        pageCount: Number(formData.pageCount),
      };

      await api.post('/books', payload);
      navigate('/home');
    } catch (err) {
      console.error(err);
      setSubmitError('Failed to create book. Please check your data or try again.');
    } finally {
      setIsSubmitting(false);
    }
  };

  useEffect(() => {
    api.get('/genres')
        .then(res => setGenres(res.data))
        .catch(err => console.error('Failed to fetch genres:', err));
    }, []);

  return (
    <>
      <div className="container my-5">
        <h2>Add a new book</h2>

        {submitError && <div className="alert alert-danger">{submitError}</div>}

        <form onSubmit={handleSubmit} noValidate>
          <div className="mb-3">
            <label htmlFor="author" className="form-label">Author *</label>
            <input
              type="text"
              className={`form-control border border-success shadow-sm ${errors.author ? 'is-invalid' : ''}`}
              id="author"
              name="author"
              value={formData.author}
              onChange={handleChange}
            />
            <div className="invalid-feedback">{errors.author}</div>
          </div>

          <div className="mb-3">
            <label htmlFor="title" className="form-label">Title *</label>
            <input
              type="text"
              className={`form-control border border-success shadow-sm ${errors.title ? 'is-invalid' : ''}`}
              id="title"
              name="title"
              value={formData.title}
              onChange={handleChange}
            />
            <div className="invalid-feedback">{errors.title}</div>
          </div>

          <div className="mb-3">
            <label htmlFor="isbn" className="form-label">ISBN *</label>
            <input
              type="text"
              className={`form-control border border-success shadow-sm ${errors.isbn ? 'is-invalid' : ''}`}
              id="isbn"
              name="isbn"
              value={formData.isbn}
              onChange={handleChange}
            />
            <div className="invalid-feedback">{errors.isbn}</div>
          </div>

          <div className="mb-3">
            <label htmlFor="blurb" className="form-label">Blurb</label>
            <textarea
              className="form-control border border-success shadow-sm"
              id="blurb"
              name="blurb"
              rows="3"
              value={formData.blurb}
              onChange={handleChange}
            />
          </div>

          <div className="mb-3">
            <label htmlFor="publicationYear" className="form-label">Year of Publication *</label>
            <input
              type="number"
              className={`form-control border border-success shadow-sm ${errors.publicationYear ? 'is-invalid' : ''}`}
              id="publicationYear"
              name="publicationYear"
              value={formData.publicationYear}
              onChange={handleChange}
            />
            <div className="invalid-feedback">{errors.publicationYear}</div>
          </div>

          <div className="mb-3">
            <label htmlFor="pageCount" className="form-label">Page Count *</label>
            <input
              type="number"
              className={`form-control border border-success shadow-sm ${errors.pageCount ? 'is-invalid' : ''}`}
              id="pageCount"
              name="pageCount"
              value={formData.pageCount}
              onChange={handleChange}
            />
            <div className="invalid-feedback">{errors.pageCount}</div>
          </div>

          <div className="mb-3">
            <label htmlFor="language" className="form-label">Language *</label>
            <input
              type="text"
              className={`form-control border border-success shadow-sm ${errors.language ? 'is-invalid' : ''}`}
              id="language"
              name="language"
              value={formData.language}
              onChange={handleChange}
            />
            <div className="invalid-feedback">{errors.language}</div>
          </div>

          <div className="mb-3">
            <label htmlFor="coverPhotoUrl" className="form-label">Cover Photo URL</label>
            <input
              type="text"
              className="form-control border border-success shadow-sm"
              id="coverPhotoUrl"
              name="coverPhotoUrl"
              value={formData.coverPhotoUrl}
              onChange={handleChange}
            />
          </div>

          <div className="mb-3">
            <label htmlFor="getBookUrl" className="form-label">Get Book URL</label>
            <input
              type="text"
              className="form-control border border-success shadow-sm"
              id="getBookUrl"
              name="getBookUrl"
              value={formData.getBookUrl}
              onChange={handleChange}
            />
          </div>

          <div className="mb-3">
            <label htmlFor="genreName" className="form-label">Genre *</label>
            <select
                className={`form-select border border-success shadow-sm ${errors.genreName ? 'is-invalid' : ''}`}
                id="genreName"
                name="genreName"
                value={formData.genreName}
                onChange={handleChange}
            >
                <option value="">-- Select a genre --</option>
                {genres.map((genre) => (
                <option key={genre.id} value={genre.name}>
                    {genre.name}
                </option>
                ))}
            </select>
            <div className="invalid-feedback">{errors.genreName}</div>
        </div>

          <div className="d-flex justify-content-end mt-4">
            <button
                type="submit"
                className="btn btn-primary"
                disabled={isSubmitting}
            >
                {isSubmitting ? 'Saving...' : 'Add book'}
            </button>
            </div>
        </form>
      </div>
    </>
  );
}

export default AddBook;
