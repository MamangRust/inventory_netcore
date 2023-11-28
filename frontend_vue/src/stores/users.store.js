import { defineStore } from 'pinia';
import { useAuthStore } from './auth.store';
import { fetchWrapper } from '@/helpers';
import { computed } from 'vue';

const baseUrl = `${import.meta.env.VITE_API_URL}/users`;

export const useUsersStore = defineStore({
  id: 'users',
  state: () => ({
    users: {},
  }),
  actions: {
    async getAll() {
      this.users = { loading: true };
      const authStore = useAuthStore();
      const jwtToken = computed(() => authStore.user?.jwtToken);

      if (!jwtToken.value) {
        return;
      }

      console.log(jwtToken.value);
      fetchWrapper
        .get(baseUrl, {
          headers: {
            Authorization: `Bearer ${jwtToken.value}`,
          },
        })
        .then((users) => (this.users = users))
        .catch((error) => (this.users = { error }));
    },
  },
});
