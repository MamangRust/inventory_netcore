import axios from 'axios';
import { defineStore } from 'pinia';

import { fetchWrapper, router } from '@/helpers';

const baseUrl = `${import.meta.env.VITE_API_URL}/auth`;
export const useAuthStore = defineStore({
  id: 'auth',
  state: () => ({
    user: null,
    refreshTokenTimeout: null,
  }),
  actions: {
    async login(email, password) {
      try {
        const response = await axios.post(`${baseUrl}/login`, {
          Email: email,
          Password: password,
        });

        this.setUser(response.data);
        this.startRefreshTokenTimer();
      } catch (error) {
        console.error('Login error:', error);
      }
    },
    async logout() {
      try {
        await axios.post(
          `${baseUrl}/revoke-token`,
          { Token: this.user.RefreshToken },
          { withCredentials: true }
        );
        this.stopRefreshTokenTimer();
        this.clearUser();
        router.push('/login');
      } catch (error) {
        console.error('Logout error:', error);
      }
    },
    async refreshToken() {
      try {
        if (!this.user || !this.user.RefreshToken) {
          this.clearUser();
          return;
        }

        const response = await axios.post(
          `${baseUrl}/refresh-token`,
          { RefreshToken: this.user.RefreshToken },
          { withCredentials: true }
        );

        this.setUser(response.data);
        this.startRefreshTokenTimer();
      } catch (error) {
        // Handle errors
        console.error('Refresh token error:', error);
      }
    },
    startRefreshTokenTimer() {
      if (!this.user || !this.user.jwtToken) {
        this.clearUser();
        return;
      }

      const jwtBase64 = this.user.jwtToken.split('.')[1];
      const jwtToken = JSON.parse(atob(jwtBase64));

      const expires = new Date(jwtToken.exp * 1000);
      const timeout = expires.getTime() - Date.now() - 60 * 1000;
      this.refreshTokenTimeout = setTimeout(this.refreshToken, timeout);
    },
    stopRefreshTokenTimer() {
      clearTimeout(this.refreshTokenTimeout);
    },
    setUser(user) {
      this.user = user;
    },
    clearUser() {
      this.user = null;
    },
  },
});
