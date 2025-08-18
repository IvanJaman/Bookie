import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import api from '../../api/bookieApi'; 
import { getUserRole } from '../../utils/auth';

export default function UserProfile() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [user, setUser] = useState(null);
  const [loading, setLoading] = useState(true);

  const userRole = getUserRole();
  const isAdmin = userRole === "Admin";

  useEffect(() => {
    const fetchUser = async () => {
      try {
        const response = await api.get(`/users/${id}`);
        setUser(response.data);
      } catch (error) {
        console.error("Error fetching user:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchUser();
  }, [id]);

  const handleDeleteUser = async () => {
    if (!window.confirm("Are you sure you want to delete this user?")) return;

    try {
      await api.delete(`/users/${id}`);
      alert("User deleted successfully!");
      navigate("/home"); 
    } catch (err) {
      console.error(err);
      alert("Failed to delete user");
    }
  };

  if (loading) return <p>Loading...</p>;
  if (!user) return <p>User not found</p>;

  return (
    <div className="container mt-3 position-relative">
        {isAdmin && (
            <button
            className="btn btn-danger position-absolute top-0 end-0 mt-3"
            onClick={handleDeleteUser}
            >
            Delete User
            </button>
        )}

      <h2>{user.username}</h2>

      {isAdmin && user.roleName && (
        <p>
            <strong>Role:</strong> {user.roleName}
        </p>
        )}

        {isAdmin && (
        <div className="mt-2">
            <label htmlFor="roleSelect"><strong>Change this user's role:</strong></label>
            <select
            id="roleSelect"
            className="form-select w-auto d-inline-block ms-2"
            value={user.roleName}
            onChange={async (e) => {
                const newRole = e.target.value;
                try {
                await api.put(`/users/${id}/role`, { roleName: newRole });
                setUser({ ...user, roleName: newRole });
                alert("Role updated successfully!");
                } catch (err) {
                console.error(err);
                alert("Failed to update role");
                }
            }}
            >
            <option value="User">User</option>
            <option value="Publisher">Publisher</option>
            <option value="Admin">Admin</option>
            </select>
        </div>
        )}

      {user.bio && <p>{user.bio}</p>}

      {user.websiteUrl && (
        <p>
          Website:{" "}
          <a href={user.websiteUrl} target="_blank" rel="noopener noreferrer">
            {user.websiteUrl}
          </a>
        </p>
      )}

      <h4>Shelves</h4>
      <div className="d-flex flex-wrap gap-3 mt-3">
        {user.shelves.length === 0 ? (
          <p>No shelves available.</p>
        ) : (
          user.shelves.map((shelf) => {
            const sortedBooks = [...shelf.books].sort(
              (a, b) => new Date(b.addedAt) - new Date(a.addedAt)
            );
            const lastFourBooks = sortedBooks.slice(0, 4);

            const handleShelfClick = () => {
              navigate(`/Shelves/${shelf.id}`);
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
                        key={book.id}
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
