import { publishAuthEvent } from "@/features/auth/authEvents";
import { getAccessToken, removeAccessToken } from "@/features/auth/authStorage";
import axios from "axios";

export const apiClient = axios.create({
  baseURL: import.meta.env.VITE_OPERIX_API_URL,
  headers: {
    "Content-Type": "application/json",
  },
});

apiClient.interceptors.request.use((config) => {
  const accessToken = getAccessToken();
  if (accessToken) {
    config.headers.Authorization = `Bearer ${accessToken}`;
  }

  return config;
});

apiClient.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response?.status === 401) {
      removeAccessToken();
      publishAuthEvent("unauthorized");
    }

    return Promise.reject(error);
  },
);
