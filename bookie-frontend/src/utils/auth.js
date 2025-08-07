import { jwtDecode } from 'jwt-decode';

export function getUserRole() {
  const token = localStorage.getItem('token');
  if (!token) return null;

  try {
    const decoded = jwtDecode(token);
    return decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] || null;
  } catch (error) {
    console.error("Error decoding token:", error);
    return null;
  }
}

export function getUsernameFromToken() {
  const token = localStorage.getItem('token');
  if (!token) return null;

  try {
    const decoded = jwtDecode(token);
    return decoded?.['unique_name'] || null;
  } catch {
    return null;
  }
}
