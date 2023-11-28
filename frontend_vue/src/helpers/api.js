import { useAuthStore } from '@/stores';
import axios from 'axios';

export const fetchWrapper = {
  get: (url, { credentials } = {}) =>
    axios
      .get(url, {
        headers: authHeader(url),
        withCredentials: credentials === 'include',
      })
      .then(handleResponse),
  post: (url, body, { credentials } = {}) =>
    axios
      .post(url, body, {
        headers: {
          ...authHeader(url),
          'Content-Type': 'application/json',
        },
        withCredentials: credentials === 'include',
      })
      .then(handleResponse),
  put: (url, body, { credentials } = {}) =>
    axios
      .put(url, body, {
        headers: {
          ...authHeader(url),
          'Content-Type': 'application/json',
        },
        withCredentials: credentials === 'include',
      })
      .then(handleResponse),
  delete: (url, { credentials } = {}) =>
    axios
      .delete(url, {
        headers: authHeader(url),
        withCredentials: credentials === 'include',
      })
      .then(handleResponse),
};

function authHeader(url) {
  const { user } = useAuthStore();
  const isLoggedIn = !!user?.jwtToken;
  const isApiUrl = url.startsWith(import.meta.env.VITE_API_URL);

  if (isLoggedIn && isApiUrl) {
    return { Authorization: `Bearer ${user.jwtToken}` };
  } else {
    return {};
  }
}

function handleResponse(response) {
  if (!response.data) {
    if ([401, 403].includes(response.status)) {
      const { user, logout } = useAuthStore();
      if (user) {
        logout();
      }
    }
    return Promise.reject(response.statusText || 'Server error');
  }

  return response.data;
}
